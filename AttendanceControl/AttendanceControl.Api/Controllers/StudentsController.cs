using AttendanceControl.Application.Contract;
using AttendanceControl.Application.Dtos.Student;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> GetAll()
        {
            return Ok(_studentService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDTO> GetById(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<StudentDTO> Create([FromBody] CreateStudentDTO request)
        {
            try
            {
                var created = _studentService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateStudentDTO request)
        {
            try
            {
                if (!_studentService.Update(id, request)) return NotFound();
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
            if (!_studentService.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("with-course")]
        public ActionResult<IEnumerable<StudentWithCourseDTO>> GetStudentsWithCourse()
        {
            return Ok(_studentService.GetStudentsWithCourse());
        }
    }
}