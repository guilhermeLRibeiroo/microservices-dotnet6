using Shopping.MessageBus;

namespace Shopping.CartAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void Send(BaseMessage message, string queueName);
    }
}
