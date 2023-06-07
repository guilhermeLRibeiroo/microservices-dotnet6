using Microsoft.EntityFrameworkCore;
using Shopping.Email.Messages;
using Shopping.Email.Models;
using Shopping.Email.Models.Context;

namespace Shopping.Email.Repositories
{
    public class EmailRepository
        : IEmailRepository
    {
        private readonly DbContextOptions<MySQLContext> _contextOptions;

        public EmailRepository(DbContextOptions<MySQLContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public async Task LogEmail(UpdatePaymentResultMessage message)
        {
            var email = new EmailLog
            {
                Email = message.Email,
                SendingDate = DateTime.Now,
                Log = $"Order - {message.OrderId} has been created sucessfully!"
            };

            await using var _db = new MySQLContext(_contextOptions);
            _db.EmailLogs.Add(email);
            await _db.SaveChangesAsync();
        }
    }
}
