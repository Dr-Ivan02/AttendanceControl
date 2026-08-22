namespace AttendanceControl.Application.Dtos.Attendance
{
    public class AttendanceDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int TotalStudents { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public List<AttendanceDetailDTO> Details { get; set; } = new();
    }

    public class AttendanceDetailDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public bool IsPresent { get; set; }
    }
}
