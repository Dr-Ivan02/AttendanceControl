using AttendanceControl.Application.Contract;
using AttendanceControl.Application.Dtos.Course;
using AttendanceControl.Application.Dtos.Student;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDTO>> GetAll()
        {
            return Ok(_courseService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<CourseDTO> GetById(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public ActionResult<CourseDTO> Create([FromBody] CreateCourseDTO request)
        {
            try
            {
                var created = _courseService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateCourseDTO request)
        {
            try
            {
                if (!_courseService.Update(id, request)) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_courseService.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("{courseId}/students")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudentsByCourse(int courseId)
        {
            var students = _courseService.GetStudentsByCourse(courseId);
            if (students == null) return NotFound();
            return Ok(students);
        }
    }
}