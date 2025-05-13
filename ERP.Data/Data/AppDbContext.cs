using ERP.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERP.Data.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        
        #region DB Tables
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderDetail> CustomerOrdersDetails { get; set; }
        public DbSet<CustomerOrderTransaction> CustomerOrderTransactions { get; set; }
        public DbSet<CustomerBalance> CustomerBalances { get; set; }
        public DbSet<Supplier> Suppliers{ get; set; }
        public DbSet<SupplierOrder> SupplierOrders{ get; set; }
        public DbSet<SupplierOrderDetail> supplierOrderDetails{ get; set; }
        public DbSet<SupplierOrderTransaction> supplierOrderTransactions{ get; set; }
        public DbSet<SupplierBalance> SupplierBalances{ get; set; } 
        public DbSet<PartSupplier> PartSuppliers { get; set; }
        public DbSet<PartSupplierOrder> PartSupplierOrders { get; set; }
        public DbSet<PartSupplierOrderDetail> PartsupplierOrderDetails { get; set; }
        public DbSet<PartSupplierTransaction> PartsupplierOrderTransactions { get; set; }
        public DbSet<PartSupplierBalance> PartSupplierBalances { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Factory> Factories{ get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<InventoryTransactionDetail> InventoryTransactionDetails { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductCategory> ProductCategories{ get; set; }
        public DbSet<AssemblyPart> AssemblyParts{ get; set; }
        public DbSet<ProductAssemblyPart> ProductAssemblyParts { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            #region Identity Tables 
            modelBuilder.Entity<ApplicationUser>(e => e.ToTable("Users"));
            modelBuilder.Entity<IdentityRole>(e => e.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<string>>(e=>e.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<string>>(e=>e.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(e=>e.ToTable("RoleClaims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(e=>e.ToTable("UserLogins"));
            modelBuilder.Entity<IdentityUserToken<string>>(e=>e.ToTable("UserTokens"));
            #endregion

            #region Tables relations
            // Product - Supplier (Optional)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);

            // Product - ProductCategory
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProductCategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite unique constraint
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Name, p.ProductCategoryID })
                .IsUnique();

            // Product - Factory
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Factory)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.FactoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Many-to-Many Relationship
            modelBuilder.Entity<ProductAssemblyPart>()
                .HasKey(pa => new { pa.ProductID, pa.AssemblyPartID }); // Composite Primary Key

            modelBuilder.Entity<ProductAssemblyPart>()
                .HasOne(pa => pa.Product)
                .WithMany(p => p.ProductAssemblyParts)
                .HasForeignKey(pa => pa.ProductID)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if product is deleted

            modelBuilder.Entity<ProductAssemblyPart>()
                .HasOne(pa => pa.AssemblyPart)
                .WithMany(ap => ap.ProductAssemblyParts)
                .HasForeignKey(pa => pa.AssemblyPartID)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if part is deleted
            #endregion

            #region tables indexes
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Supplier>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique(); 
            modelBuilder.Entity<PartSupplier>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();
            #endregion
         
        }

    }
}
