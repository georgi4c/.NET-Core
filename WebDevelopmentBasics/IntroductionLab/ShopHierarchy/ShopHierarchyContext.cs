using Microsoft.EntityFrameworkCore;

namespace ShopHierarchy
{
    public class ShopHierarchyContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TestDb;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>()
                .HasOne(c => c.Salesman)
                .WithMany(s => s.Customers)
                .HasForeignKey(c => c.SalesmanId);

            builder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            builder.Entity<Customer>()
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);

            builder.Entity<OrderItem>()
                .HasKey(oi => new {oi.ItemId, oi.OrderId});

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(o => o.OrderId);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany(i => i.Orders)
                .HasForeignKey(i => i.ItemId);

            builder.Entity<Item>()
                .HasMany(i => i.Reviews)
                .WithOne(r => r.Item)
                .HasForeignKey(r => r.ItemId);

        }
    }
}