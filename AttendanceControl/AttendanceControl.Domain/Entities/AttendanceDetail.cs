using AttendanceControl.Domain.Core;

namespace AttendanceControl.Domain.Entities
{
    public class AttendanceDetail : BaseEntity
    {
        public bool IsPresent { get; set; }

        public int AttendanceId { get; set; }
        public Attendance? Attendance { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public AttendanceDetail()
        {
        }

        public AttendanceDetail(int studentId, bool isPresent)
        {
            StudentId = studentId;
            IsPresent = isPresent;
        }

        public override string GetDisplayName()
        {
            var status = IsPresent ? "Presente" : "Ausente";
            return $"Estudiante {StudentId} - {status}";
        }
    }
}
