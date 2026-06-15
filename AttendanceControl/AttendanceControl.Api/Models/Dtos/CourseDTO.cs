namespace AttendanceControl.Api.Models.Dtos
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}