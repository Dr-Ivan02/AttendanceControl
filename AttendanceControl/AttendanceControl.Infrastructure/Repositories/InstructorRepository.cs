using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Core;

namespace AttendanceControl.Infrastructure.Repositories
{
    public class InstructorRepository : BaseRepository<Instructor>
    {
        private const string CodePrefix = "PROF";

        public InstructorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Instructor> GetAll()
        {
            return _context.Instructors
                .Where(i => i.IsActive)
                .ToList();
        }

        public void Update(Instructor instructor)
        {
            _context.SaveChanges();
        }

        public string GenerateNextCode()
        {
            var maxNumber = _context.Instructors
                .AsEnumerable()
                .Select(i => ExtractSequence(i.Code))
                .DefaultIfEmpty(0)
                .Max();

            return $"{CodePrefix}{(maxNumber + 1):D3}";
        }

        private static int ExtractSequence(string code)
        {
            if (!string.IsNullOrEmpty(code) && code.StartsWith(CodePrefix) &&
                int.TryParse(code.AsSpan(CodePrefix.Length), out var number))
            {
                return number;
            }
            return 0;
        }
    }
}