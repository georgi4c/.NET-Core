namespace SocialNetwork.Data
{
    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Data.Configurations;
    using SocialNetwork.Models;

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=SocialNetworkDb; Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserFriendConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
