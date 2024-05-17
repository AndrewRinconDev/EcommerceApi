using EcommerceApi.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Context
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PrincipalProduct> PrincipalProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRecord> OrderRecords { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne<Customer>("customer")
                .WithMany("addresses")
                .HasForeignKey("customerId")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();


            modelBuilder.Entity<Category>()
                .HasOne<Category>("parent")
                .WithMany("subcategories")
                .HasForeignKey("parentId")
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Customer>()
                .HasOne<User>("user")
                .WithMany("customers")
                .HasForeignKey("userId")
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FavoriteProduct>(b =>
            {
                b.HasOne<Customer>("customer")
                    .WithMany("favoriteProducts")
                    .HasForeignKey("customerId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.HasOne<Product>("product")
                    .WithMany()
                    .HasForeignKey("productId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.Navigation("customer");

                b.Navigation("product");
            });

            modelBuilder.Entity<Order>(b =>
            {
                b.HasOne<Address>("address")
                    .WithMany()
                    .HasForeignKey("addressId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.HasOne<Customer>("customer")
                    .WithMany("orders")
                    .HasForeignKey("customerId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.Navigation("address");

                b.Navigation("customer");
            });

            modelBuilder.Entity<OrderProduct>(b =>
            {
                b.HasOne<Order>("order")
                    .WithMany("orderProducts")
                    .HasForeignKey("orderId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.HasOne<Product>("product")
                    .WithMany()
                    .HasForeignKey("productId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.Navigation("order");

                b.Navigation("product");
            });

            modelBuilder.Entity<OrderRecord>(b =>
            {
                b.HasOne<Order>("order")
                    .WithMany("orderRecords")
                    .HasForeignKey("orderId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.Navigation("order");
            });

            modelBuilder.Entity<Product>()
                .HasOne<Category>("category")
                .WithMany()
                .HasForeignKey("categoryId")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasOne<Role>("role")
                .WithMany()
                .HasForeignKey("roleId")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();
        }
    }
}
