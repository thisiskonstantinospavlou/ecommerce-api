using Data.EF.Infrastructure;
using Data.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF
{
    public class EcommerceContext : DbContext, IEcommerceContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PostalAddress).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MembershipId).IsRequired();

                entity.HasOne(e => e.MembershipFkNavigation).WithMany(e => e.CustomerFkNavigation).HasForeignKey(k => k.MembershipId);
                entity.HasMany(e => e.OrderFkNavigation).WithOne(e => e.CustomerFkNavigation).HasForeignKey(k => k.CustomerId).OnDelete(DeleteBehavior.Cascade);

                entity.HasData(new Customer { CustomerId = 1, Name = "Alan", PostalAddress = "2 One Way, SE1 1AA", Email = "alan@test.com", MembershipId = 1 });
                entity.HasData(new Customer { CustomerId = 2, Name = "Maria", PostalAddress = "52 Billington Street, BN1 1AA", Email = "maria@test.com", MembershipId = 1 });
                entity.HasData(new Customer { CustomerId = 3, Name = "John", PostalAddress = "76 Margeret Street, SW2 1AA", Email = "john@test.com", MembershipId = 1 });
                entity.HasData(new Customer { CustomerId = 4, Name = "Lisa", PostalAddress = "16 Broadway Street, SB1 1AA", Email = "john@test.com", MembershipId = 1 });
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => e.MembershipId);
                entity.Property(e => e.MembershipId).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(20);

                entity.HasMany(e => e.CustomerFkNavigation).WithOne(e => e.MembershipFkNavigation).HasForeignKey(k => k.MembershipId);

                entity.HasData(new Membership { MembershipId = 1, Name = "None" });
                entity.HasData(new Membership { MembershipId = 2, Name = "Book" });
                entity.HasData(new Membership { MembershipId = 3, Name = "Video" });
                entity.HasData(new Membership { MembershipId = 4, Name = "Premium" });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.IncomingOrderId).IsRequired();
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();

                entity.HasOne(e => e.CustomerFkNavigation).WithMany(e => e.OrderFkNavigation).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.ProductFkNavigation).WithMany(e => e.OrderFkNavigation);

                entity.HasData(new Order { Id = 1, CustomerId = 1, IncomingOrderId = 1, ProductId = 5 });
                entity.HasData(new Order { Id = 2, CustomerId = 1, IncomingOrderId = 1, ProductId = 6 });
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
                entity.Property(e => e.TypeId).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IsPhysical).IsRequired();

                entity.HasOne(e => e.ProductTypeFkNavigation).WithMany(e => e.ProductFkNavigation)
                    .HasForeignKey(k => k.TypeId);


                entity.HasData(new Product { ProductId = 1, Name = "Book Membership", IsPhysical = false, TypeId = 1 });
                entity.HasData(new Product { ProductId = 2, Name = "Video Membership", IsPhysical = false, TypeId = 1 });
                entity.HasData(new Product { ProductId = 3, Name = "Premium Membership", IsPhysical = false, TypeId = 1 });
                entity.HasData(new Product { ProductId = 4, Name = "Comprehensive First Aid Training", IsPhysical = false, TypeId = 3 });
                entity.HasData(new Product { ProductId = 5, Name = "The Girl on the train", IsPhysical = true, TypeId = 3 });
                entity.HasData(new Product { ProductId = 6, Name = "Attack On Titan, Volume 1", IsPhysical = false, TypeId = 2 });
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.ProductTypeId);
                entity.Property(e => e.ProductTypeId).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                entity.HasMany(e => e.ProductFkNavigation).WithOne(e => e.ProductTypeFkNavigation)
                    .HasForeignKey(k => k.TypeId);


                entity.HasData(new ProductType { ProductTypeId = 1, Name = "Membership" });
                entity.HasData(new ProductType { ProductTypeId = 2, Name = "Video" });
                entity.HasData(new ProductType { ProductTypeId = 3, Name = "Book" });
            });
        }
    }
}