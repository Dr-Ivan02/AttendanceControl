using AttendanceControl.Domain.Core;

namespace AttendanceControl.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }

        public List<Student> Students { get; set; } = new();
        public List<Attendance> Attendances { get; set; } = new();

        public Course()
        {
        }

        public Course(string code, string name, int instructorId)
        {
            Code = code;
            Name = name;
            InstructorId = instructorId;
            IsActive = true;
        }

        public override string GetDisplayName()
        {
            return $"{Code} - {Name}";
        }
    }
}
