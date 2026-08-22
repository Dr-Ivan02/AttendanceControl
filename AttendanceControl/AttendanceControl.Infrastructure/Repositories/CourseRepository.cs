using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace AttendanceControl.Infrastructure.Repositories
{
    public class CourseRepository : BaseRepository<Course>
    {
        public ApplicationDbContext? Context { get; set; }

        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Course> GetAll()
        {
            return _context.Courses
                .Include(c => c.Instructor)
                .ToList();
        }

        public override Course? GetById(int id)
        {
            return _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Update(Course course)
        {
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetStudentsByCourse(int courseId)
        {
            return _context.Students.Where(s => s.CourseId == courseId && s.IsActive).ToList();
        }

        public bool Exists(int courseId)
        {
            return _entities.Any(c => c.Id == courseId);
        }
    }
}
