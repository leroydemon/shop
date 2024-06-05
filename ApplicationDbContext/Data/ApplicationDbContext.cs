using DbLevel.Models;
using Microsoft.EntityFrameworkCore;

namespace DbLevel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductStorage> ProductStorages { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(n => n.Name).IsRequired();
            });
            modelBuilder.Entity<Product>().Property(n => n.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Cart>().Property(n => n.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Cart>().Property(n => n.TotalPrice).HasPrecision(18, 2);

        }
    }
}
