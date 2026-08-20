using AutoMapper;
using AttendanceControl.Application.Contract;
using AttendanceControl.Application.Dtos.Attendance;
using AttendanceControl.Domain.Entities;
using AttendanceControl.Infrastructure.Repositories;

namespace AttendanceControl.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AttendanceRepository _attendanceRepository;
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public AttendanceService(AttendanceRepository attendanceRepository, StudentRepository studentRepository, CourseRepository courseRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public IEnumerable<AttendanceDTO> GetAll()
        {
            var attendances = _attendanceRepository.GetAll();
            return _mapper.Map<IEnumerable<AttendanceDTO>>(attendances);
        }

        public AttendanceDTO? GetById(int id)
        {
            var attendance = _attendanceRepository.GetById(id);
            return attendance == null ? null : _mapper.Map<AttendanceDTO>(attendance);
        }

        public AttendanceDTO Create(CreateAttendanceDTO request)
        {
            ValidateReferences(request.StudentId, request.CourseId);

            var newAttendance = _mapper.Map<Attendance>(request);
            _attendanceRepository.Create(newAttendance);
            return _mapper.Map<AttendanceDTO>(newAttendance);
        }

        public bool Update(int id, CreateAttendanceDTO request)
        {
            ValidateReferences(request.StudentId, request.CourseId);

            var existingAttendance = _attendanceRepository.GetById(id);
            if (existingAttendance == null)
                return false;

            existingAttendance.Date = request.Date;
            existingAttendance.IsPresent = request.IsPresent;
            existingAttendance.StudentId = request.StudentId;
            existingAttendance.CourseId = request.CourseId;

            _attendanceRepository.Update(existingAttendance);
            return true;
        }

        public bool Delete(int id)
        {
            var attendance = _attendanceRepository.GetById(id);
            if (attendance == null)
                return false;

            _attendanceRepository.Delete(id);
            return true;
        }

        private void ValidateReferences(int studentId, int courseId)
        {
            if (_studentRepository.GetById(studentId) == null)
                throw new ArgumentException($"No existe un Estudiante con Id = {studentId}.");

            if (!_courseRepository.Exists(courseId))
                throw new ArgumentException($"No existe un Curso con Id = {courseId}.");
        }
    }
}