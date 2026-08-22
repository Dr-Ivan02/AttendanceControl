using AutoMapper;
using AttendanceControl.Application.Contract;
using AttendanceControl.Application.Dtos.Course;
using AttendanceControl.Application.Dtos.Student;
using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Repositories;

namespace AttendanceControl.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly CourseRepository _courseRepository;
        private readonly InstructorRepository _instructorRepository;
        private readonly IMapper _mapper;

        public CourseService(CourseRepository courseRepository, InstructorRepository instructorRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public IEnumerable<CourseDTO> GetAll()
        {
            var courses = _courseRepository.GetAll().Where(c => c.IsActive);
            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public IEnumerable<CourseDTO> GetAll(bool onlyActive)
        {
            var courses = _courseRepository.GetAll();
            if (onlyActive)
                courses = courses.Where(c => c.IsActive);

            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public CourseDTO? GetById(int id)
        {
            var course = _courseRepository.GetById(id);
            return course == null || !course.IsActive ? null : _mapper.Map<CourseDTO>(course);
        }

        public CourseDTO Create(CreateCourseDTO request)
        {
            ValidateFields(request.Code, request.Name, request.InstructorId);

            var newCourse = _mapper.Map<Course>(request);
            newCourse.IsActive = true;

            _courseRepository.Create(newCourse);
            return _mapper.Map<CourseDTO>(newCourse);
        }

        public bool Update(int id, CreateCourseDTO request)
        {
            ValidateFields(request.Code, request.Name, request.InstructorId);

            var existingCourse = _courseRepository.GetById(id);
            if (existingCourse == null || !existingCourse.IsActive)
                return false;

            existingCourse.Name = request.Name;
            existingCourse.Code = request.Code;
            existingCourse.InstructorId = request.InstructorId;

            _courseRepository.Update(existingCourse);
            return true;
        }

        public bool Delete(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null || !course.IsActive)
                return false;

            course.IsActive = false;
            _courseRepository.Update(course);
            return true;
        }

        public IEnumerable<StudentDTO>? GetStudentsByCourse(int courseId)
        {
            var course = _courseRepository.GetById(courseId);
            if (course == null || !course.IsActive)
                return null;

            var students = _courseRepository.GetStudentsByCourse(courseId);
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        private void ValidateFields(string code, string name, int instructorId)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("El código del curso es obligatorio.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del curso es obligatorio.");

            var instructor = _instructorRepository.GetById(instructorId);
            if (instructor == null || !instructor.IsActive)
                throw new ArgumentException($"No existe un Instructor activo con Id = {instructorId}.");
        }
    }
}
