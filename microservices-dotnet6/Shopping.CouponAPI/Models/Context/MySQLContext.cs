using Microsoft.EntityFrameworkCore;
using Shopping.DatabaseMigrations;

namespace Shopping.CouponAPI.Models.Context
{
    public class MySQLContext
        : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { this.ApplyMigrations(); }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                Code = "model_creation_coupon_1",
                DiscountAmount = 10
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                Code = "model_creation_coupon_2",
                DiscountAmount = 15
            });
        }
    }
}
