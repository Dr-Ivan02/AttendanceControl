using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace AttendanceControl.Infrastructure.Repositories
{
    public class AttendanceRepository : BaseRepository<Attendance>
    {
        public AttendanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Attendance> GetAll()
        {
            return _context.Attendances
                .Include(a => a.Course)
                .Include(a => a.Details)
                    .ThenInclude(d => d.Student)
                .OrderByDescending(a => a.Date)
                .ThenBy(a => a.Course!.Code)
                .ToList();
        }

        public override Attendance? GetById(int id)
        {
            return _context.Attendances
                .Include(a => a.Course)
                .Include(a => a.Details)
                    .ThenInclude(d => d.Student)
                .FirstOrDefault(a => a.Id == id);
        }

        public Attendance? GetByCourseAndDate(int courseId, DateTime date)
        {
            var day = date.Date;
            return _context.Attendances
                .Include(a => a.Details)
                .FirstOrDefault(a => a.CourseId == courseId && a.Date.Date == day);
        }

        public void Update(Attendance attendance)
        {
            _context.SaveChanges();
        }
    }
}
