using Microsoft.EntityFrameworkCore;

namespace Shopping.Email.Models.Context
{
    public class MySQLContext
        : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
