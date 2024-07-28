using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace sarisari
{
    public class DataDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Drawer> Drawers { get; set; }
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Size>().HasKey(x => x.Id);
            modelBuilder.Entity<Stock>().HasKey(x => x.Id);
            modelBuilder.Entity<Supplier>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x=>x.Id);
            modelBuilder.Entity<Drawer>().HasKey(x => x.Id);
        }
    }
}
