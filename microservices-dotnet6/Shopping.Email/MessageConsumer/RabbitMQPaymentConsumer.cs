using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shopping.Email.Messages;
using Shopping.Email.Repositories;
using System.Text;
using System.Text.Json;

namespace Shopping.Email.MessageConsumer
{
    public class RabbitMQPaymentConsumer
        : BackgroundService
    {
        private readonly EmailRepository _emailRepository;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueue";

        public RabbitMQPaymentConsumer(EmailRepository emailRepository)
        {
            _emailRepository = emailRepository;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, durable: false);
            _channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
            _channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evnt) =>
            {
                var content = Encoding.UTF8.GetString(evnt.Body.ToArray());
                var message = JsonSerializer.Deserialize<UpdatePaymentResultMessage>(content);
                ProcessLogs(message).GetAwaiter().GetResult();
                _channel.BasicAck(evnt.DeliveryTag, false);
            };

            _channel.BasicConsume(PaymentEmailUpdateQueueName, false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessLogs(UpdatePaymentResultMessage message)
        {
            try
            {
                await _emailRepository.LogEmail(message);
            }
            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }
    }
}
