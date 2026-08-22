namespace AttendanceControl.Domain.Core
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public abstract string GetDisplayName();
    }
}