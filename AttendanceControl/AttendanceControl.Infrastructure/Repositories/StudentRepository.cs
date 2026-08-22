using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace AttendanceControl.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Student> GetAll()
        {
            return _context.Students
                .Include(s => s.Course)
                .Where(s => s.IsActive)
                .ToList();
        }

        public override Student? GetById(int id)
        {
            return _context.Students
                .Include(s => s.Course)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Update(Student student)
        {
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetStudentsWithCourse()
        {
            return _context.Students
                .Include(s => s.Course)
                .Where(s => s.IsActive)
                .ToList();
        }
    }
}
