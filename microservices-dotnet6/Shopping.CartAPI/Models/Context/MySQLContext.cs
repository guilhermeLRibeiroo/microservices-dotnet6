using Microsoft.EntityFrameworkCore;
using Shopping.DatabaseMigrations;

namespace Shopping.CartAPI.Models.Context
{
    public class MySQLContext
        : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { this.ApplyMigrations(); }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
    }
}
