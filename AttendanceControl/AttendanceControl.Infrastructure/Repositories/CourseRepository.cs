using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Core;

namespace AttendanceControl.Infrastructure.Repositories
{
    public class CourseRepository : BaseRepository<Course>
    {
        public ApplicationDbContext? Context { get; set; }

        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Update(Course course)
        {
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetStudentsByCourse(int courseId)
        {
            return _context.Students.Where(s => s.CourseId == courseId).ToList();
        }

        public bool Exists(int courseId)
        {
            return _entities.Any(c => c.Id == courseId);
        }
    }
}