﻿// <auto-generated />
using System;
using EcommerceApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcommerceApi.Migrations
{
    [DbContext(typeof(EcommerceDbContext))]
    [Migration("20240520032344_PaymentAndFeature")]
    partial class PaymentAndFeature
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EcommerceApi.Models.Database.Address", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("customerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("zipCode")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("customerId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Category", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("parentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("parentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Customer", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("identityNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("identityType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("userId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.FavoriteProduct", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("customerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("principalProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("customerId");

                    b.HasIndex("principalProductId");

                    b.ToTable("FavoriteProducts");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Feature", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("featureCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("label")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("featureCategoryId");

                    b.ToTable("Feature");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.FeatureCategory", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("FeatureCategory");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.FeatureProduct", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("featureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("productId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("featureId");

                    b.HasIndex("productId");

                    b.ToTable("FeatureProduct");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Order", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("addressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("creationDates")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("customerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("dateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("isPaid")
                        .HasColumnType("bit");

                    b.Property<double>("subtotal")
                        .HasColumnType("float");

                    b.Property<double>("total")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("addressId");

                    b.HasIndex("customerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.OrderProduct", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("orderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("productId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("orderId");

                    b.HasIndex("productId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.OrderRecord", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("orderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("orderState")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("orderId");

                    b.ToTable("OrderRecords");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Payment", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("ammount")
                        .HasColumnType("float");

                    b.Property<Guid>("customerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("method")
                        .HasColumnType("int");

                    b.Property<Guid>("orderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("customerId");

                    b.HasIndex("orderId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.PaymentCard", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("company")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<Guid?>("customerid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("cvv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("endWith")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("expirationDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ownerName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("id");

                    b.HasIndex("customerid");

                    b.ToTable("PaymentCard");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.PrincipalProduct", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("categoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.HasIndex("categoryId");

                    b.ToTable("PrincipalProducts");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Product", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("imageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<Guid>("principalProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("principalProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Role", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("roleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("roleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Address", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Customer", "customer")
                        .WithMany("addresses")
                        .HasForeignKey("customerId")
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Category", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Category", "parent")
                        .WithMany("subcategories")
                        .HasForeignKey("parentId");

                    b.Navigation("parent");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Customer", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.User", "user")
                        .WithMany("customers")
                        .HasForeignKey("userId")
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.FavoriteProduct", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Customer", "customer")
                        .WithMany("favoriteProducts")
                        .HasForeignKey("customerId")
                        .IsRequired();

                    b.HasOne("EcommerceApi.Models.Database.PrincipalProduct", "principalProduct")
                        .WithMany()
                        .HasForeignKey("principalProductId")
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("principalProduct");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Feature", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.FeatureCategory", "featureCategory")
                        .WithMany("features")
                        .HasForeignKey("featureCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("featureCategory");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.FeatureProduct", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Feature", "feature")
                        .WithMany("featureProducts")
                        .HasForeignKey("featureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceApi.Models.Database.Product", "product")
                        .WithMany("featureProducts")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("feature");

                    b.Navigation("product");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Order", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Address", "address")
                        .WithMany()
                        .HasForeignKey("addressId")
                        .IsRequired();

                    b.HasOne("EcommerceApi.Models.Database.Customer", "customer")
                        .WithMany("orders")
                        .HasForeignKey("customerId")
                        .IsRequired();

                    b.Navigation("address");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.OrderProduct", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Order", "order")
                        .WithMany("orderProducts")
                        .HasForeignKey("orderId")
                        .IsRequired();

                    b.HasOne("EcommerceApi.Models.Database.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .IsRequired();

                    b.Navigation("order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.OrderRecord", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Order", "order")
                        .WithMany("orderRecords")
                        .HasForeignKey("orderId")
                        .IsRequired();

                    b.Navigation("order");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Payment", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceApi.Models.Database.Order", "order")
                        .WithMany("payments")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("order");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.PaymentCard", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Customer", "customer")
                        .WithMany("paymentCards")
                        .HasForeignKey("customerid");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.PrincipalProduct", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Category", "category")
                        .WithMany()
                        .HasForeignKey("categoryId")
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Product", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.PrincipalProduct", "principalProduct")
                        .WithMany("products")
                        .HasForeignKey("principalProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("principalProduct");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.User", b =>
                {
                    b.HasOne("EcommerceApi.Models.Database.Role", "role")
                        .WithMany()
                        .HasForeignKey("roleId")
                        .IsRequired();

                    b.Navigation("role");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Category", b =>
                {
                    b.Navigation("subcategories");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Customer", b =>
                {
                    b.Navigation("addresses");

                    b.Navigation("favoriteProducts");

                    b.Navigation("orders");

                    b.Navigation("paymentCards");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Feature", b =>
                {
                    b.Navigation("featureProducts");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.FeatureCategory", b =>
                {
                    b.Navigation("features");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Order", b =>
                {
                    b.Navigation("orderProducts");

                    b.Navigation("orderRecords");

                    b.Navigation("payments");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.PrincipalProduct", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.Product", b =>
                {
                    b.Navigation("featureProducts");
                });

            modelBuilder.Entity("EcommerceApi.Models.Database.User", b =>
                {
                    b.Navigation("customers");
                });
#pragma warning restore 612, 618
        }
    }
}
