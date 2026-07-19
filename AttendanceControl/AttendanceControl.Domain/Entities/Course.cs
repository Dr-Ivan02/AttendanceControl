
using AttendanceControl.Domain.Core;

namespace AttendanceControl.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public List<Student> Students { get; set; } = new();
    }
}