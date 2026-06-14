using Microsoft.AspNetCore.Mvc;
using AttendanceControl.Api.Models.Entities;

namespace AttendanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private static readonly List<Student> _students = new()
        {
        new Student { Id = 1, Code = "STU001", Name = "Ivan Hernandez", CourseId = 1 },
        new Student { Id = 2, Code = "STU002", Name = "John Doe", CourseId = 1 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAll() => Ok(_students);

        [HttpGet("{id}")]
        public ActionResult<Student> GetById(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
                return BadRequest("Name is required.");

            if (student.CourseId <= 0)
                return BadRequest("CourseId is required and must be valid.");

            student.Id = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
            _students.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Student student)
        {
            var existing = _students.FirstOrDefault(s => s.Id == id);
            if (existing == null) return NotFound();

            existing.Name = student.Name;
            existing.Code = student.Code;
            existing.IsActive = student.IsActive;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _students.FirstOrDefault(s => s.Id == id);
            if (existing == null) return NotFound();

            _students.Remove(existing);
            return NoContent();
        }
    }
}