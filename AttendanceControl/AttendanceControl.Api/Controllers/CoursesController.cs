using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Repositories;
using AttendanceControl.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseRepository _courseRepository;

        public CoursesController(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDTO>> GetAll()
        {
            var courses = _courseRepository.GetAll();
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
            var course = _courseRepository.GetById(id);
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

            _courseRepository.Create(newCourse);

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

            var existingCourse = _courseRepository.GetById(id);
            if (existingCourse == null) return NotFound();

            existingCourse.Name = request.Name;
            existingCourse.Code = request.Code;

            _courseRepository.Update(existingCourse);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null) return NotFound();

            _courseRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("{courseId}/students")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudentsByCourse(int courseId)
        {
            if (!_courseRepository.Exists(courseId)) return NotFound();

            var students = _courseRepository.GetStudentsByCourse(courseId);

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