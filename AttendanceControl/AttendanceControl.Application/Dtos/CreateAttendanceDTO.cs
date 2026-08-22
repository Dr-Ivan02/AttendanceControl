using AttendanceControl.Domain.Enums;

namespace AttendanceControl.Application.Dtos.Attendance
{
    public class CreateAttendanceDTO
    {
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        public List<CreateAttendanceDetailDTO> Details { get; set; } = new();
    }

    public class CreateAttendanceDetailDTO
    {
        public int StudentId { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}