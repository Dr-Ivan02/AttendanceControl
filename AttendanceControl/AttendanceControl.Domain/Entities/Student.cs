using AttendanceControl.Domain.Core;

namespace AttendanceControl.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}