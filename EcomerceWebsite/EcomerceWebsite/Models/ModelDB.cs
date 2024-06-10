using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EcomerceWebsite.Models
{
    public partial class ModelDB : DbContext
    {
        public ModelDB()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<order_item> order_item { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<shipment> shipments { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<wishlist> wishlists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.phone_number)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .HasMany(e => e.carts)
                .WithRequired(e => e.account)
                .HasForeignKey(e => e.account_account_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<account>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.account)
                .HasForeignKey(e => e.account_account_id);

            modelBuilder.Entity<account>()
                .HasMany(e => e.shipments)
                .WithOptional(e => e.account)
                .HasForeignKey(e => e.account_account_id);

            modelBuilder.Entity<account>()
                .HasMany(e => e.wishlists)
                .WithRequired(e => e.account)
                .HasForeignKey(e => e.account_account_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.Category_category_id);

            modelBuilder.Entity<Order>()
                .Property(e => e.total_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.order_item)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.order_order_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<order_item>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Payment)
                .HasForeignKey(e => e.Payment_payment_id);

            modelBuilder.Entity<Product>()
                .Property(e => e.productCode)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.SKU)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.size)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.carts)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.product_product_id);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.order_item)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.product_product_id);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.wishlists)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.product_product_id);

            modelBuilder.Entity<shipment>()
                .Property(e => e.zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<shipment>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.shipment)
                .HasForeignKey(e => e.Shipment_shipment_id);
        }
    }
}
