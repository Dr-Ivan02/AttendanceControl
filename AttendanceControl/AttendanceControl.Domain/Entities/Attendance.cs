using AttendanceControl.Domain.Core;

namespace AttendanceControl.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}