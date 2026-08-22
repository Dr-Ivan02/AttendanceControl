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

        public Attendance()
        {
        }

        public Attendance(DateTime date, bool isPresent, int studentId, int courseId)
        {
            Date = date;
            IsPresent = isPresent;
            StudentId = studentId;
            CourseId = courseId;
        }

        public override string GetDisplayName()
        {
            var estado = IsPresent ? "Presente" : "Ausente";
            return $"{Date:yyyy-MM-dd} - {estado}";
        }
    }
}