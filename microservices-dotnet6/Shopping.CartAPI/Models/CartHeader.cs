﻿using Shopping.CartAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.CartAPI.Models
{
    [Table("cart_header")]
    public class CartHeader
        : BaseEntity
    {
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("coupon_code")]
        public string? CouponCode { get; set; }
    }
}
