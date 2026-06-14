using Microsoft.AspNetCore.Mvc;
using AttendanceControl.Api.Models.Entities;

namespace AttendanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private static readonly List<Course> _courses = new()
        {
            new Course { Id = 1, Code = "CS101", Name = "Software Programming I" },
            new Course { Id = 2, Code = "CS102", Name = "Database Design" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetAll() => Ok(_courses);

        [HttpGet("{id}")]
        public ActionResult<Course> GetById(int id)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            return course == null ? NotFound() : Ok(course);
        }

        [HttpPost]
        public ActionResult<Course> Create(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.Name))
                return BadRequest("Name is required.");

            course.Id = _courses.Any() ? _courses.Max(c => c.Id) + 1 : 1;
            _courses.Add(course);
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Course course)
        {
            var existing = _courses.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            existing.Name = course.Name;
            existing.Code = course.Code;
            existing.IsActive = course.IsActive;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _courses.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            _courses.Remove(existing);
            return NoContent();
        }
    }
}