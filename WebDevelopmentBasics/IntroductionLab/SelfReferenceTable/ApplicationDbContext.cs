using Microsoft.EntityFrameworkCore;
using OneToManyRelationship;

namespace SelfReferenceTable
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TestDb;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees);

            builder
                .Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(m => m.Subordinates)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}