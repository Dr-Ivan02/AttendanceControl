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
        private readonly IMapper _mapper;

        public InstructorService(InstructorRepository instructorRepository, IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public IEnumerable<InstructorDTO> GetAll()
        {
            var instructors = _instructorRepository.GetAll();
            return _mapper.Map<IEnumerable<InstructorDTO>>(instructors);
        }

        public InstructorDTO? GetById(int id)
        {
            var instructor = _instructorRepository.GetById(id);
            return instructor == null ? null : _mapper.Map<InstructorDTO>(instructor);
        }

        public InstructorDTO Create(CreateInstructorDTO request)
        {
            ValidateFields(request.Name);

            var newInstructor = _mapper.Map<Instructor>(request);
            newInstructor.IsActive = true;

            _instructorRepository.Create(newInstructor);
            return _mapper.Map<InstructorDTO>(newInstructor);
        }

        public bool Update(int id, CreateInstructorDTO request)
        {
            ValidateFields(request.Name);

            var existingInstructor = _instructorRepository.GetById(id);
            if (existingInstructor == null)
                return false;

            existingInstructor.Name = request.Name;
            existingInstructor.Code = request.Code;

            _instructorRepository.Update(existingInstructor);  
            return true;
        }

        public bool Delete(int id)
        {
            var instructor = _instructorRepository.GetById(id);
            if (instructor == null)
                return false;

            _instructorRepository.Delete(id);
            return true;
        }

        private static void ValidateFields(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del instructor es obligatorio.");
        }
    }
}