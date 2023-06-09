using Shopping.MessageBus;

namespace Shopping.Email.Messages
{
    public class UpdatePaymentResultMessage
        : BaseMessage
    {
        public long OrderId { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
