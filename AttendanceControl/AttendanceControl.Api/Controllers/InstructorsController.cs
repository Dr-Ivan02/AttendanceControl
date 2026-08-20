using AttendanceControl.Application.Contract;
using AttendanceControl.Application.Dtos.Instructor;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/instructors")]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstructorDTO>> GetAll()
        {
            return Ok(_instructorService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<InstructorDTO> GetById(int id)
        {
            var instructor = _instructorService.GetById(id);
            if (instructor == null) return NotFound();
            return Ok(instructor);
        }

        [HttpPost]
        public ActionResult<InstructorDTO> Create([FromBody] CreateInstructorDTO request)
        {
            try
            {
                var created = _instructorService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateInstructorDTO request)
        {
            try
            {
                if (!_instructorService.Update(id, request)) return NotFound();
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
            if (!_instructorService.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}