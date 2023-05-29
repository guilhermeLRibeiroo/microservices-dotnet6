using RabbitMQ.Client;
using Shopping.CartAPI.Messages;
using Shopping.MessageBus;
using System.Text;
using System.Text.Json;

namespace Shopping.CartAPI.RabbitMQSender
{
    public class RabbitMQMessageSender
        : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }
        public void Send(BaseMessage message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            _connection = factory.CreateConnection();
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);

            byte[] body = GetMessageAsByteArr(message);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }

        private byte[] GetMessageAsByteArr(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize((CheckoutHeaderVO)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
