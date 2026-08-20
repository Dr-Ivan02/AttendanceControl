namespace AttendanceControl.Application.Dtos.Attendance
{
    public class AttendanceDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}