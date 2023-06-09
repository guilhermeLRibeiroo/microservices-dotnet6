using Shopping.MessageBus;

namespace Shopping.OrderAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void Send(BaseMessage message, string queueName);
    }
}
