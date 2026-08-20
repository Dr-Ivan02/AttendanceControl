using AttendanceControl.Application.Dtos.Instructor;

namespace AttendanceControl.Application.Contract
{
    public interface IInstructorService
    {
        IEnumerable<InstructorDTO> GetAll();
        InstructorDTO? GetById(int id);
        InstructorDTO Create(CreateInstructorDTO request);
        bool Update(int id, CreateInstructorDTO request);
        bool Delete(int id);
    }
}