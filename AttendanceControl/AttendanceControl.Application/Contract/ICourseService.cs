using AttendanceControl.Application.Dtos.Course;
using AttendanceControl.Application.Dtos.Student;

namespace AttendanceControl.Application.Contract
{
    public interface ICourseService
    {
        IEnumerable<CourseDTO> GetAll();
        CourseDTO? GetById(int id);
        CourseDTO Create(CreateCourseDTO request);
        bool Update(int id, CreateCourseDTO request);
        bool Delete(int id);
        IEnumerable<StudentDTO>? GetStudentsByCourse(int courseId);
    }
}