using Microsoft.EntityFrameworkCore;
using Shopping.DatabaseMigrations;

namespace Shopping.ProductAPI.Models.Context
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class MySQLContext
        : DbContext
    {
        public MySQLContext() { }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { this.ApplyMigrations(); }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Product Name",
                Price = new decimal(100.5),
                Description = "Product Description",
                ImageURL = "imageurl",
                CategoryName = "Product Category"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Another Product Name",
                Price = new decimal(100.5),
                Description = "Product Description 3",
                ImageURL = "imageurl",
                CategoryName = "Product Category"
            });
        }
    }
}
