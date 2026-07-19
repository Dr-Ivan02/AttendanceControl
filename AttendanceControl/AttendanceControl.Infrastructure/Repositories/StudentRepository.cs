using Microsoft.EntityFrameworkCore;
using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Core;

namespace AttendanceControl.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Update(Student student)
        {
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetStudentsWithCourse()
        {
            return _entities.Include(s => s.Course).ToList();
        }
    }
}