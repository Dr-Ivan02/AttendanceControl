using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceControl.Api.Data;
using AttendanceControl.Api.Models.Entities;
using AttendanceControl.Api.Models.Dtos;

namespace AttendanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> GetAll()
        {
            var students = _context.Students.ToList();
            var studentDTOs = students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                CourseId = s.CourseId
            }).ToList();

            return Ok(studentDTOs);
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDTO> GetById(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Code = student.Code,
                Name = student.Name,
                CourseId = student.CourseId
            };

            return Ok(studentDTO);
        }

        [HttpPost]
        public ActionResult<StudentDTO> Create([FromBody] CreateStudentDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("El nombre del estudiante es obligatorio.");
            }

            // Validar la integridad referencial 
            var courseExists = _context.Courses.Any(c => c.Id == request.CourseId);
            if (!courseExists)
            {
                return BadRequest($"No existe un Curso con Id = {request.CourseId}.");
            }

            var newStudent = new Student
            {
                Code = request.Code,
                Name = request.Name,
                CourseId = request.CourseId,
                IsActive = true
            };

            _context.Students.Add(newStudent);
            _context.SaveChanges();

            var responseDTO = new StudentDTO
            {
                Id = newStudent.Id,
                Code = newStudent.Code,
                Name = newStudent.Name,
                CourseId = newStudent.CourseId
            };

            return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, responseDTO);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateStudentDTO request)
        {
            var existingStudent = _context.Students.Find(id);
            if (existingStudent == null) return NotFound();

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("El nombre del estudiante no puede estar vacío.");
            }

            var courseExists = _context.Courses.Any(c => c.Id == request.CourseId);
            if (!courseExists)
            {
                return BadRequest($"No existe un Curso con Id = {request.CourseId}.");
            }

            existingStudent.Name = request.Name;
            existingStudent.Code = request.Code;
            existingStudent.CourseId = request.CourseId;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("with-course")]
        public ActionResult<IEnumerable<StudentWithCourseDTO>> GetStudentsWithCourse()
        {
            var students = _context.Students
                .Include(s => s.Course)
                .ToList();

            var resultList = students.Select(s => new StudentWithCourseDTO
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                CourseId = s.CourseId,
                CourseName = s.Course != null ? s.Course.Name : "Sin Curso Asignado"
            }).ToList();

            return Ok(resultList);
        }
    }
}