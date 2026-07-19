using AttendanceControl.Application.Dtos.Student;

namespace AttendanceControl.Application.Contract
{
    public interface IStudentService
    {
        IEnumerable<StudentDTO> GetAll();
        StudentDTO? GetById(int id);
        StudentDTO Create(CreateStudentDTO request);
        bool Update(int id, CreateStudentDTO request);
        bool Delete(int id);
        IEnumerable<StudentWithCourseDTO> GetStudentsWithCourse();
    }
}