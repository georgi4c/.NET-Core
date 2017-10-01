using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;

namespace StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<License> Licenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TestDb;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentCourse>()
                .HasKey(sc => new {sc.CourseId, sc.StudentId});

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(sc => sc.StudentId);

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(s => s.Students)
                .HasForeignKey(sc => sc.CourseId);

            builder.Entity<Course>()
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);

            builder.Entity<Course>()
                .HasMany(c => c.Homeworks)
                .WithOne(h => h.Course)
                .HasForeignKey(h => h.CourseId);

            builder.Entity<Student>()
                .HasMany(s => s.Homeworks)
                .WithOne(h => h.Student)
                .HasForeignKey(h => h.StudentId);

            builder.Entity<Resource>()
                .HasMany(c => c.Licenses)
                .WithOne(l => l.Resource)
                .HasForeignKey(c => c.ResourceId);
        }
    }
}