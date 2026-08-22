using AttendanceControl.Domain.Core;

namespace AttendanceControl.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public DateTime Date { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public List<AttendanceDetail> Details { get; set; } = new();

        public Attendance()
        {
        }

        public Attendance(DateTime date, int courseId)
        {
            Date = date;
            CourseId = courseId;
        }

        public override string GetDisplayName()
        {
            return $"{Date:yyyy-MM-dd} - Curso {CourseId}";
        }
    }
}
