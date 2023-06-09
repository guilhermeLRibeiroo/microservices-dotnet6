using Shopping.MessageBus;

namespace Shopping.OrderAPI.Messages
{
    public class UpdatePaymentResultVO
        : BaseMessage
    {
        public long OrderId { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
