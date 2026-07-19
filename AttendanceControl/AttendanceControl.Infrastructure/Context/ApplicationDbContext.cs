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
    }
}