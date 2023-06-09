using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shopping.OrderAPI.Messages;
using Shopping.OrderAPI.Models;
using Shopping.OrderAPI.RabbitMQSender;
using Shopping.OrderAPI.Repositories;
using System.Text;
using System.Text.Json;

namespace Shopping.OrderAPI.MessageConsumer
{
    public class RabbitMQCheckoutConsumer
        : BackgroundService
    {
        private readonly OrderRepository _orderRepository;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(OrderRepository orderRepository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _orderRepository = orderRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evnt) =>
            {
                var content = Encoding.UTF8.GetString(evnt.Body.ToArray());
                CheckoutHeaderVO headerVO = JsonSerializer.Deserialize<CheckoutHeaderVO>(content);
                ProcessOrder(headerVO).GetAwaiter().GetResult();
                _channel.BasicAck(evnt.DeliveryTag, false);
            };

            _channel.BasicConsume("checkoutqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderVO headerVO)
        {
            var orderHeader = new OrderHeader
            {
                UserId = headerVO.UserId,
                FirstName = headerVO.FirstName,
                LastName = headerVO.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = headerVO.CardNumber,
                CouponCode = headerVO.CouponCode,
                CVV = headerVO.CVV,
                DiscountAmount = headerVO.DiscountAmount,
                Email = headerVO.Email,
                ExpirationDate = headerVO.ExpirationDate,
                OrderTime = DateTime.Now,
                PurchaseAmount = headerVO.PurchaseAmount,
                PaymentStatus = false,
                PhoneNumber = headerVO.PhoneNumber,
                DateTime = headerVO.DateTime
            };

            foreach(var detail in headerVO.CartDetails)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = detail.ProductId,
                    ProductName = detail.Product.Name,
                    Price = detail.Product.Price,
                    Count = detail.Count
                };
                orderHeader.CartTotalItems += orderDetail.Count;
                orderHeader.OrderDetails.Add(orderDetail);
            }

            await _orderRepository.AddOrder(orderHeader);

            var paymentVO = new PaymentVO
            {
                OrderId = orderHeader.Id,
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                Email = orderHeader.Email,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpirationDate = orderHeader.ExpirationDate,
                PurchaseAmount = orderHeader.PurchaseAmount,
                Creation = DateTime.Now,
            };

            try
            {
                _rabbitMQMessageSender.Send(paymentVO, "orderpaymentprocessqueue");
            }
            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }
    }
}
