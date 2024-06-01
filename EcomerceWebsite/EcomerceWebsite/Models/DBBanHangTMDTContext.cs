using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EcomerceWebsite.Models
{
    public partial class DBBanHangTMDTContext : DbContext
    {
        public DBBanHangTMDTContext()
        {
        }

        public DBBanHangTMDTContext(DbContextOptions<DBBanHangTMDTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS01;Initial Catalog=DBBanHangTMDT;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__CD65CB85B8AF1680");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.CustomerCustomerId })
                    .HasName("PK__cart__34DD4753EFFDCF88");

                entity.ToTable("cart");

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.CustomerCustomerId).HasColumnName("customer_customer_id");

                entity.Property(e => e.ProductProductId).HasColumnName("product_product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.CustomerCustomer)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.CustomerCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cart_customer");

                entity.HasOne(d => d.ProductProduct)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.ProductProductId)
                    .HasConstraintName("cart_product");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerCustomerId).HasColumnName("Customer_customer_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentPaymentId).HasColumnName("Payment_payment_id");

                entity.Property(e => e.ShipmentShipmentId).HasColumnName("Shipment_shipment_id");

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("total_price")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.CustomerCustomer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerCustomerId)
                    .HasConstraintName("order_customer");

                entity.HasOne(d => d.PaymentPayment)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.PaymentPaymentId)
                    .HasConstraintName("order_payment");

                entity.HasOne(d => d.ShipmentShipment)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ShipmentShipmentId)
                    .HasConstraintName("order_shipment");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderItemId, e.OrderOrderId })
                    .HasName("PK__order_it__A9DF22CD114A0399");

                entity.ToTable("order_item");

                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");

                entity.Property(e => e.OrderOrderId).HasColumnName("order_order_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductProductId).HasColumnName("product_product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.OrderOrder)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_item_order");

                entity.HasOne(d => d.ProductProduct)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.ProductProductId)
                    .HasConstraintName("order_item_product");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId)
                    .HasColumnName("payment_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CustomerCustomerId).HasColumnName("customer_customer_id");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("payment_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentMethod)
                    .HasColumnName("payment_method")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CustomerCustomer)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CustomerCustomerId)
                    .HasConstraintName("payment_customer");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryCategoryId).HasColumnName("Category_category_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Sku)
                    .HasColumnName("SKU")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.CategoryCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryCategoryId)
                    .HasConstraintName("Product_Category");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("shipment");

                entity.Property(e => e.ShipmentId)
                    .HasColumnName("shipment_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerCustomerId).HasColumnName("customer_customer_id");

                entity.Property(e => e.ShipmentDate)
                    .HasColumnName("shipment_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.CustomerCustomer)
                    .WithMany(p => p.Shipment)
                    .HasForeignKey(d => d.CustomerCustomerId)
                    .HasConstraintName("shipment_customer");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.HasKey(e => new { e.WishlistId, e.CustomerCustomerId })
                    .HasName("PK__wishlist__4F5E24AAA930EDCE");

                entity.ToTable("wishlist");

                entity.Property(e => e.WishlistId).HasColumnName("wishlist_id");

                entity.Property(e => e.CustomerCustomerId).HasColumnName("Customer_customer_id");

                entity.Property(e => e.ProductProductId).HasColumnName("product_product_id");

                entity.HasOne(d => d.CustomerCustomer)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.CustomerCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wishlist_customer");

                entity.HasOne(d => d.ProductProduct)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.ProductProductId)
                    .HasConstraintName("wishlist_product");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
