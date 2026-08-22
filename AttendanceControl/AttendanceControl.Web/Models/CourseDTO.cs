namespace AttendanceControl.Web.Models
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? InstructorId { get; set; }
        public string InstructorName { get; set; } = string.Empty;
    }

    public class CreateCourseDTO
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int InstructorId { get; set; }
    }
}
