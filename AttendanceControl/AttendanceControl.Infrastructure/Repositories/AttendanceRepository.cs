using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Core;

namespace AttendanceControl.Infrastructure.Repositories
{
    public class AttendanceRepository : BaseRepository<Attendance>
    {
        public AttendanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Update(Attendance attendance)
        {
            _context.SaveChanges();
        }
    }
}