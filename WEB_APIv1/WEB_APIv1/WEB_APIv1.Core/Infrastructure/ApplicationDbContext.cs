// ---------------------------------------
// ---
// ---
// ---
// ---------------------------------------

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WEB_APIv1.Core.Models;
using WEB_APIv1.Core.Models.Account;
using WEB_APIv1.Core.Models.Shop;
using WEB_APIv1.Core.Services.Account;

namespace WEB_APIv1.Core.Infrastructure
{
    //public class ApplicationDbContext(DbContextOptions options, IUserIdAccessor userIdAccessor) :
    //    IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    //{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IUserIdAccessor _userIdAccessor;

        // Constructor usado por EF Core en diseño (migraciones)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Design-time constructor used by migrations
        }

        // Constructor con dependencias adicionales (para inyección en runtime)
        [ActivatorUtilitiesConstructor]
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserIdAccessor userIdAccessor)
            : base(options)
        {
            _userIdAccessor = userIdAccessor;
        }


        public DbSet<Customer> Customers { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string priceDecimalType = "decimal(18,2)";
            const string tablePrefix = "App";

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Claims)
                .WithOne()
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Roles)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationRole>()
                .HasMany(r => r.Claims)
                .WithOne()
                .HasForeignKey(c => c.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>()
                .HasMany(r => r.Users)
                .WithOne()
                .HasForeignKey(r => r.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Customer>().Property(c => c.ContactName).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.ContactName);
            builder.Entity<Customer>().Property(c => c.Address).HasMaxLength(100);
            builder.Entity<Customer>().Property(c => c.Phone).IsUnicode(false).HasMaxLength(30);
            builder.Entity<Customer>().Property(c => c.City).HasMaxLength(50);
            builder.Entity<Customer>().ToTable($"{tablePrefix}{nameof(Customers)}");

            builder.Entity<ProductCategory>().Property(p => p.CategoryName).IsRequired().HasMaxLength(100);
            builder.Entity<ProductCategory>().Property(p => p.Description).HasMaxLength(500);
            builder.Entity<ProductCategory>().ToTable($"{tablePrefix}{nameof(ProductCategories)}");

            builder.Entity<Product>().Property(p => p.ProductName).IsRequired().HasMaxLength(100);
            builder.Entity<Product>().HasIndex(p => p.ProductName);
            builder.Entity<Product>().Property(p => p.UnitPrice).HasColumnType(priceDecimalType);
            builder.Entity<Product>().ToTable($"{tablePrefix}{nameof(Products)}");

            builder.Entity<Order>().Property(o => o.ShipName).HasMaxLength(500);
            builder.Entity<Order>().ToTable($"{tablePrefix}{nameof(Orders)}");

            builder.Entity<OrderDetail>().Property(p => p.UnitPrice).HasColumnType(priceDecimalType);
            builder.Entity<OrderDetail>().Property(p => p.Discount).HasColumnType(priceDecimalType);
            builder.Entity<OrderDetail>().ToTable($"{tablePrefix}{nameof(OrderDetails)}");
        }

        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddAuditInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddAuditInfo()
        {

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity &&
                           (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
            }
        }
    }
}
