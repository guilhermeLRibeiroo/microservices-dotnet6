﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shopping.CouponAPI.Models.Context;

#nullable disable

namespace Shopping.CouponAPI.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20230520233408_SeedCouponDatabase")]
    partial class SeedCouponDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Shopping.CouponAPI.Models.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("code");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("discount_amount");

                    b.HasKey("Id");

                    b.ToTable("coupon");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Code = "model_creation_coupon_1",
                            DiscountAmount = 10m
                        },
                        new
                        {
                            Id = 2L,
                            Code = "model_creation_coupon_2",
                            DiscountAmount = 15m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}