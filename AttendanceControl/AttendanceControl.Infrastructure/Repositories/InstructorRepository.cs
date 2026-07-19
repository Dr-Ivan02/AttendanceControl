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
    }
}