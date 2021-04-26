using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApi.Entities.Models
{
    public partial class DB_CloudKitchenContext : DbContext
    {
        public DB_CloudKitchenContext()
        {
        }

        public DB_CloudKitchenContext(DbContextOptions<DB_CloudKitchenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Billing> Billings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<ItemImageCarousel> ItemImageCarousels { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<StatusBilling> StatusBillings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DUNGPT;Database=DB_CloudKitchen;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Billing>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Billing__C3905BCFD9CA70CD");

                entity.ToTable("Billing");

                entity.Property(e => e.BillAmout).HasColumnType("money");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Billings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Billing__Custome__534D60F1");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Billings)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__Billing__Restaur__5441852A");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Billings)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Billing__StatusI__5535A963");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CustomerAddress).HasMaxLength(500);

                entity.Property(e => e.CustomerContactPhone).HasMaxLength(100);

                entity.Property(e => e.CustomerEmailId)
                    .HasMaxLength(200)
                    .HasColumnName("CustomerEmailID");

                entity.Property(e => e.CustomerName).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.Username).HasMaxLength(100);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ImageType).HasMaxLength(255);

                entity.Property(e => e.ItemCategoryId).HasColumnName("ItemCategoryID");

                entity.Property(e => e.ItemDescription).HasMaxLength(1000);

                entity.Property(e => e.ItemName).HasMaxLength(500);

                entity.Property(e => e.ItemPrice).HasColumnType("money");

                entity.Property(e => e.MainImagePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.HasOne(d => d.ItemCategory)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemCategoryId)
                    .HasConstraintName("FK_Item_ItemCategory");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_Item_Restaurants");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("ItemCategory");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoryName).HasMaxLength(500);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<ItemImageCarousel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ItemImageCarousel");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Title).HasMaxLength(1000);

                entity.HasOne(d => d.Item)
                    .WithMany()
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ItemImageCarousel_Item");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__OrderDeta__ItemI__59063A47");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__5812160E");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ImagePath).HasMaxLength(255);

                entity.Property(e => e.ImageType).HasMaxLength(255);

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 12)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 12)");

                entity.Property(e => e.RestaurantAddress).HasMaxLength(500);

                entity.Property(e => e.RestaurantEmail).HasMaxLength(500);

                entity.Property(e => e.RestaurantName).HasMaxLength(500);

                entity.Property(e => e.RestaurantPhone).HasMaxLength(500);
            });

            modelBuilder.Entity<StatusBilling>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__StatusBi__C8EE2063364C8F20");

                entity.ToTable("StatusBilling");

                entity.Property(e => e.StatusName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
