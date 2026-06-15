using Microsoft.AspNetCore.Mvc;
using AttendanceControl.Api.Data;
using AttendanceControl.Api.Models.Entities;
using AttendanceControl.Api.Models.Dtos;

namespace AttendanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDTO>> GetAll()
        {
            var courses = _context.Courses.ToList();
            var courseDTOs = courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                Code = c.Code,
                Name = c.Name
            }).ToList();

            return Ok(courseDTOs);
        }

        [HttpGet("{id}")]
        public ActionResult<CourseDTO> GetById(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            var courseDTO = new CourseDTO
            {
                Id = course.Id,
                Code = course.Code,
                Name = course.Name
            };

            return Ok(courseDTO);
        }

        [HttpPost]
        public ActionResult<CourseDTO> Create([FromBody] CreateCourseDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("El nombre del curso es obligatorio.");
            }

            var newCourse = new Course
            {
                Code = request.Code,
                Name = request.Name,
                IsActive = true
            };

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            var responseDTO = new CourseDTO
            {
                Id = newCourse.Id,
                Code = newCourse.Code,
                Name = newCourse.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = newCourse.Id }, responseDTO);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateCourseDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("El nombre del curso no puede estar vacío.");
            }

            var existingCourse = _context.Courses.Find(id);
            if (existingCourse == null) return NotFound();

            existingCourse.Name = request.Name;
            existingCourse.Code = request.Code;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{courseId}/students")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudentsByCourse(int courseId)
        {
            var exists = _context.Courses.Any(c => c.Id == courseId);
            if (!exists) return NotFound();

            var students = _context.Students
                .Where(s => s.CourseId == courseId)
                .ToList();

            var studentDTOs = students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                CourseId = s.CourseId
            }).ToList();

            return Ok(studentDTOs);
        }
    }
}