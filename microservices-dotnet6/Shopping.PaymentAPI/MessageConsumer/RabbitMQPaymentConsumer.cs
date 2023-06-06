using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shopping.PaymentAPI.Messages;
using Shopping.PaymentAPI.RabbitMQSender;
using Shopping.PaymentProcessor;
using System.Text;
using System.Text.Json;

namespace Shopping.PaymentAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer
        : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private IProcessPayment _processPayment;

        public RabbitMQPaymentConsumer(IRabbitMQMessageSender rabbitMQMessageSender, IProcessPayment processPayment)
        {
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _processPayment = processPayment;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentprocessqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evnt) =>
            {
                var content = Encoding.UTF8.GetString(evnt.Body.ToArray());
                var headerVO = JsonSerializer.Deserialize<PaymentMessage>(content);
                ProcessPayment(headerVO).GetAwaiter().GetResult();
                _channel.BasicAck(evnt.DeliveryTag, false);
            };

            _channel.BasicConsume("orderpaymentprocessqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessPayment(PaymentMessage headerVO)
        {
            var result = _processPayment.PaymentProcessor();
            var paymentResult = new UpdatePaymentResultMessage
            {
                Status = result,
                OrderId = headerVO.OrderId,
                Email = headerVO.Email,
                Creation = DateTime.Now,
            };

            try
            {
                _rabbitMQMessageSender.Send(paymentResult, "orderpaymentresultqueue");
            }
            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }
    }
}
