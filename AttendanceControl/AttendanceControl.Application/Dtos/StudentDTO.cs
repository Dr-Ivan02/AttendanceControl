namespace AttendanceControl.Application.Dtos.Student
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int CourseId { get; set; }
    }
}