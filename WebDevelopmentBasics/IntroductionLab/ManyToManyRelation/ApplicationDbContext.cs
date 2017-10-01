using Microsoft.EntityFrameworkCore;

namespace ManyToManyRelation
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TestDb;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentsCourses>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});

            builder.Entity<StudentsCourses>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(s => s.StudentId);

            builder.Entity<StudentsCourses>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}