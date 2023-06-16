using Microsoft.EntityFrameworkCore;
using Shopping.DatabaseMigrations;

namespace Shopping.OrderAPI.Models.Context
{
    public class MySQLContext
        : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { this.ApplyMigrations(); }

        public DbSet<OrderDetail> Details { get; set; }
        public DbSet<OrderHeader> Headers { get; set; }
    }
}
