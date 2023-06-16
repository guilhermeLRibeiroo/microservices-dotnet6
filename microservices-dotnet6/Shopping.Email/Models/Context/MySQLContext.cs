using Microsoft.EntityFrameworkCore;
using Shopping.DatabaseMigrations;

namespace Shopping.Email.Models.Context
{
    public class MySQLContext
        : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { this.ApplyMigrations(); }

        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
