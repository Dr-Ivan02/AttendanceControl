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

        public Student()
        {
        }

        public Student(string code, string name, int courseId)
        {
            Code = code;
            Name = name;
            CourseId = courseId;
            IsActive = true;
        }

        public override string GetDisplayName()
        {
            return $"{Code} - {Name}";
        }
    }
}