using AttendanceControl.Application.Contract;
using AttendanceControl.Application.Dtos.Attendance;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/attendances")]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AttendanceDTO>> GetAll()
        {
            return Ok(_attendanceService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<AttendanceDTO> GetById(int id)
        {
            var attendance = _attendanceService.GetById(id);
            if (attendance == null) return NotFound();
            return Ok(attendance);
        }

        [HttpPost]
        public ActionResult<AttendanceDTO> Create([FromBody] CreateAttendanceDTO request)
        {
            try
            {
                var created = _attendanceService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateAttendanceDTO request)
        {
            try
            {
                if (!_attendanceService.Update(id, request)) return NotFound();
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
            if (!_attendanceService.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}