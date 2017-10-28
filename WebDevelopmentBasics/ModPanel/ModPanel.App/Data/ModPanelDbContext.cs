namespace ModPanel.App.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ModPanelDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ModPanelExamDb;Integrated Security=True;");
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            
        }
    }
}
