﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.CartAPI.Models
{
    [Table("product")]
    public class Product
        : IEquatable<Product>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(150)]
        public string? Name { get; set; }

        [Column("price")]
        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Column("description")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Column("category_name")]
        [StringLength(50)]
        public string? CategoryName { get; set; }

        [Column("image_url")]
        [StringLength(300)]
        public string? ImageURL { get; set; }

        public bool Equals(Product? other)
        {
            if (other == null)
                return false;

            return this.Id == other.Id
                && this.Name == other.Name
                && this.Price == other.Price
                && this.Description == other.Description
                && this.CategoryName == other.CategoryName
                && this.ImageURL == other.ImageURL;
        }

        public void UpdateProperties(Product product)
        {
            this.Name = product.Name;
            this.Price = product.Price;
            this.Description = product.Description;
            this.CategoryName = product.CategoryName;
            this.ImageURL = product.ImageURL;
        }
    }
}
