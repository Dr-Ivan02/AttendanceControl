using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace AttendanceControl.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>
    {
        private const string CodePrefix = "EST";

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Student> GetAll()
        {
            return _context.Students
                .Include(s => s.Course)
                .Where(s => s.IsActive)
                .ToList();
        }

        public override Student? GetById(int id)
        {
            return _context.Students
                .Include(s => s.Course)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Update(Student student)
        {
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetStudentsWithCourse()
        {
            return _context.Students
                .Include(s => s.Course)
                .Where(s => s.IsActive)
                .ToList();
        }

        public string GenerateNextCode()
        {
            var maxNumber = _context.Students
                .AsEnumerable()
                .Select(s => ExtractSequence(s.Code))
                .DefaultIfEmpty(0)
                .Max();

            return $"{CodePrefix}{(maxNumber + 1):D4}";
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