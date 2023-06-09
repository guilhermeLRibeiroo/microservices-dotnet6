
using Shopping.Email.Messages;

namespace Shopping.Email.Repositories
{
    public interface IEmailRepository
    {
        Task LogEmail(UpdatePaymentResultMessage message);
    }
}
