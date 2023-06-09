using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shopping.OrderAPI.Messages;
using Shopping.OrderAPI.Repositories;
using System.Text;
using System.Text.Json;

namespace Shopping.OrderAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer
        : BackgroundService
    {
        private readonly OrderRepository _orderRepository;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueue";

        public RabbitMQPaymentConsumer(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, durable: false);
            _channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);
            _channel.QueueBind(PaymentOrderUpdateQueueName, ExchangeName, "PaymentOrder");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evnt) =>
            {
                var content = Encoding.UTF8.GetString(evnt.Body.ToArray());
                var vo = JsonSerializer.Deserialize<UpdatePaymentResultVO>(content);
                UpdatePaymentStatus(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evnt.DeliveryTag, false);
            };

            _channel.BasicConsume(PaymentOrderUpdateQueueName, false, consumer);
            return Task.CompletedTask;
        }

        private async Task UpdatePaymentStatus(UpdatePaymentResultVO updatePaymentResultVO)
        {
            try
            {
                await _orderRepository.UpdateOrderPaymentStatus(updatePaymentResultVO.OrderId, updatePaymentResultVO.Status);
            }
            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }
    }
}
