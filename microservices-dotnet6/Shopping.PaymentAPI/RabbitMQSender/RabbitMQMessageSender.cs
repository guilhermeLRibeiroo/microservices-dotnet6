using RabbitMQ.Client;
using Shopping.PaymentAPI.Messages;
using Shopping.MessageBus;
using System.Text;
using System.Text.Json;

namespace Shopping.PaymentAPI.RabbitMQSender
{
    public class RabbitMQMessageSender
        : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueue";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueue";

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }
        public void Send(BaseMessage message)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, durable: false);

                channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
                channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);

                channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail");
                channel.QueueBind(PaymentOrderUpdateQueueName, ExchangeName, "PaymentOrder");

                byte[] body = GetMessageAsByteArr(message);

                channel.BasicPublish(exchange: ExchangeName, "PaymentEmail", basicProperties: null, body: body);
                channel.BasicPublish(exchange: ExchangeName, "PaymentOrder", basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArr(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize((UpdatePaymentResultMessage)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;

            CreateConnection();

            return _connection != null;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }
    }
}
