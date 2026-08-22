using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Core;

namespace AttendanceControl.Infrastructure.Repositories
{
    public class InstructorRepository : BaseRepository<Instructor>
    {
        public InstructorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Instructor> GetAll()
        {
            return _context.Instructors
                .Where(i => i.IsActive)
                .ToList();
        }

        public void Update(Instructor instructor)
        {
            _context.SaveChanges();
        }
    }
}
