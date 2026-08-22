namespace AttendanceControl.Application.Dtos.Course
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? InstructorId { get; set; }
        public string InstructorName { get; set; } = string.Empty;
    }
}
