using AutoMapper;
using AttendanceControl.Application.Contract;
using AttendanceControl.Application.Dtos.Instructor;
using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Repositories;

namespace AttendanceControl.Application.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly InstructorRepository _instructorRepository;
        private readonly CourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public InstructorService(InstructorRepository instructorRepository, CourseRepository courseRepository, IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public IEnumerable<InstructorDTO> GetAll()
        {
            var instructors = _instructorRepository.GetAll().Where(i => i.IsActive);
            return _mapper.Map<IEnumerable<InstructorDTO>>(instructors);
        }

        public InstructorDTO? GetById(int id)
        {
            var instructor = _instructorRepository.GetById(id);
            return instructor == null || !instructor.IsActive ? null : _mapper.Map<InstructorDTO>(instructor);
        }

        public InstructorDTO Create(CreateInstructorDTO request)
        {
            ValidateFields(request.Name);

            var newInstructor = _mapper.Map<Instructor>(request);
            newInstructor.Code = _instructorRepository.GenerateNextCode();
            newInstructor.IsActive = true;

            _instructorRepository.Create(newInstructor);
            return _mapper.Map<InstructorDTO>(newInstructor);
        }

        public bool Update(int id, CreateInstructorDTO request)
        {
            ValidateFields(request.Name);

            var existingInstructor = _instructorRepository.GetById(id);
            if (existingInstructor == null || !existingInstructor.IsActive)
                return false;

            existingInstructor.Name = request.Name;

            _instructorRepository.Update(existingInstructor);
            return true;
        }

        public bool Delete(int id)
        {
            var instructor = _instructorRepository.GetById(id);
            if (instructor == null || !instructor.IsActive)
                return false;

            var hasActiveCourses = _courseRepository.GetAll()
                .Any(c => c.IsActive && c.InstructorId == id);

            if (hasActiveCourses)
                throw new ArgumentException("No puedes desactivar un instructor con cursos activos asignados.");

            instructor.IsActive = false;
            _instructorRepository.Update(instructor);
            return true;
        }

        private static void ValidateFields(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del instructor es obligatorio.");
        }
    }
}