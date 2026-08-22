using AttendanceControl.Domain.Core;
using AttendanceControl.Domain.Enums;

namespace AttendanceControl.Domain.Entities
{
    public class AttendanceDetail : BaseEntity
    {
        public AttendanceStatus Status { get; set; }

        public int AttendanceId { get; set; }
        public Attendance? Attendance { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public AttendanceDetail() { }

        public AttendanceDetail(int studentId, AttendanceStatus status)
        {
            StudentId = studentId;
            Status = status;
        }

        public override string GetDisplayName() => $"Estudiante {StudentId} - {Status}";
    }
}