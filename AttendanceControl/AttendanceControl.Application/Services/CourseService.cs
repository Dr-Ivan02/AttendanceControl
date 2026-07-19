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
        private readonly IMapper _mapper;

        public CourseService(CourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public IEnumerable<CourseDTO> GetAll()
        {
            var courses = _courseRepository.GetAll();
            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public CourseDTO? GetById(int id)
        {
            var course = _courseRepository.GetById(id);
            return course == null ? null : _mapper.Map<CourseDTO>(course);
        }

        public CourseDTO Create(CreateCourseDTO request)
        {
            ValidateFields(request.Name);

            var newCourse = _mapper.Map<Course>(request);
            newCourse.IsActive = true;

            _courseRepository.Create(newCourse);
            return _mapper.Map<CourseDTO>(newCourse);
        }

        public bool Update(int id, CreateCourseDTO request)
        {
            ValidateFields(request.Name);

            var existingCourse = _courseRepository.GetById(id);
            if (existingCourse == null)
                return false;

            existingCourse.Name = request.Name;
            existingCourse.Code = request.Code;

            _courseRepository.Update(existingCourse);
            return true;
        }

        public bool Delete(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null)
                return false;

            _courseRepository.Delete(id);
            return true;
        }

        public IEnumerable<StudentDTO>? GetStudentsByCourse(int courseId)
        {
            if (!_courseRepository.Exists(courseId))
                return null;

            var students = _courseRepository.GetStudentsByCourse(courseId);
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        private static void ValidateFields(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del curso es obligatorio.");
        }
    }
}