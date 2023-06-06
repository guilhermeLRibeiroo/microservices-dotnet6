using Shopping.MessageBus;

namespace Shopping.PaymentAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void Send(BaseMessage message, string queueName);
    }
}
