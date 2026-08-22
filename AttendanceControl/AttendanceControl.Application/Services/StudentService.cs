using AutoMapper;
using AttendanceControl.Application.Contract;
using AttendanceControl.Application.Dtos.Student;
using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Repositories;

namespace AttendanceControl.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public StudentService(StudentRepository studentRepository, CourseRepository courseRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public IEnumerable<StudentDTO> GetAll()
        {
            var students = _studentRepository.GetAll().Where(s => s.IsActive);
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public StudentDTO? GetById(int id)
        {
            var student = _studentRepository.GetById(id);
            return student == null || !student.IsActive ? null : _mapper.Map<StudentDTO>(student);
        }

        public StudentDTO Create(CreateStudentDTO request)
        {
            ValidateFields(request.Code, request.Name, request.CourseId);

            var newStudent = _mapper.Map<Student>(request);
            newStudent.IsActive = true;

            _studentRepository.Create(newStudent);
            return _mapper.Map<StudentDTO>(newStudent);
        }

        public bool Update(int id, CreateStudentDTO request)
        {
            var existingStudent = _studentRepository.GetById(id);
            if (existingStudent == null)
                return false;

            ValidateFields(request.Code, request.Name, request.CourseId);

            existingStudent.Name = request.Name;
            existingStudent.Code = request.Code;
            existingStudent.CourseId = request.CourseId;

            _studentRepository.Update(existingStudent);
            return true;
        }

        public bool Delete(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null || !student.IsActive)
                return false;

            student.IsActive = false;
            _studentRepository.Update(student);
            return true;
        }

        public IEnumerable<StudentWithCourseDTO> GetStudentsWithCourse()
        {
            var students = _studentRepository.GetStudentsWithCourse();
            return _mapper.Map<IEnumerable<StudentWithCourseDTO>>(students);
        }

        private void ValidateFields(string code, string name, int courseId)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("El código del estudiante es obligatorio.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del estudiante es obligatorio.");

            var course = _courseRepository.GetById(courseId);
            if (course == null || !course.IsActive)
                throw new ArgumentException($"No existe un Curso activo con Id = {courseId}.");
        }
    }
}
