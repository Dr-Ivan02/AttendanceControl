using Microsoft.EntityFrameworkCore;
using AttendanceControl.Domain.Entities;

namespace AttendanceControl.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Instructor> Instructors { get; set; } = null!;
        public DbSet<Attendance> Attendances { get; set; } = null!;
        public DbSet<AttendanceDetail> AttendanceDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Attendances)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasIndex(a => new { a.CourseId, a.Date })
                .IsUnique();

            modelBuilder.Entity<AttendanceDetail>()
                .HasOne(d => d.Attendance)
                .WithMany(a => a.Details)
                .HasForeignKey(d => d.AttendanceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttendanceDetail>()
                .HasOne(d => d.Student)
                .WithMany()
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AttendanceDetail>()
                .HasIndex(d => new { d.AttendanceId, d.StudentId })
                .IsUnique();
        }
    }
}
