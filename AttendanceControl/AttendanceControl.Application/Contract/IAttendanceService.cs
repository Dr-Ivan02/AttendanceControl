using AttendanceControl.Application.Dtos.Attendance;

namespace AttendanceControl.Application.Contract
{
    public interface IAttendanceService
    {
        IEnumerable<AttendanceDTO> GetAll();
        AttendanceDTO? GetById(int id);
        AttendanceDTO Create(CreateAttendanceDTO request);
        bool Update(int id, CreateAttendanceDTO request);
        bool Delete(int id);
    }
}