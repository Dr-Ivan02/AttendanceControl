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
            ValidateRequest(request);

            if (_attendanceRepository.GetByCourseAndDate(request.CourseId, request.Date) is not null)
                throw new ArgumentException("Ya existe una asistencia registrada para este curso en esa fecha.");

            var newAttendance = new Attendance(request.Date.Date, request.CourseId)
            {
                Details = request.Details
                    .Select(d => new AttendanceDetail(d.StudentId, d.IsPresent))
                    .ToList()
            };

            _attendanceRepository.Create(newAttendance);
            return _mapper.Map<AttendanceDTO>(_attendanceRepository.GetById(newAttendance.Id)!);
        }

        public bool Update(int id, CreateAttendanceDTO request)
        {
            var existingAttendance = _attendanceRepository.GetById(id);
            if (existingAttendance == null)
                return false;

            ValidateRequest(request);

            var sameDateAttendance = _attendanceRepository.GetByCourseAndDate(request.CourseId, request.Date);
            if (sameDateAttendance is not null && sameDateAttendance.Id != id)
                throw new ArgumentException("Ya existe una asistencia registrada para este curso en esa fecha.");

            existingAttendance.Date = request.Date.Date;
            existingAttendance.CourseId = request.CourseId;
            existingAttendance.Details.Clear();

            foreach (var detail in request.Details)
            {
                existingAttendance.Details.Add(new AttendanceDetail(detail.StudentId, detail.IsPresent));
            }

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

        private void ValidateRequest(CreateAttendanceDTO request)
        {
            var course = _courseRepository.GetById(request.CourseId);
            if (course == null || !course.IsActive)
                throw new ArgumentException($"No existe un Curso activo con Id = {request.CourseId}.");

            var courseStudents = _courseRepository.GetStudentsByCourse(request.CourseId).ToList();
            if (courseStudents.Count == 0)
                throw new ArgumentException("No puedes registrar asistencia de un curso sin estudiantes activos.");

            if (request.Details.Count == 0)
                throw new ArgumentException("La asistencia debe incluir a los estudiantes del curso.");

            var repeatedStudentId = request.Details
                .GroupBy(d => d.StudentId)
                .FirstOrDefault(g => g.Count() > 1);

            if (repeatedStudentId is not null)
                throw new ArgumentException($"El estudiante con Id = {repeatedStudentId.Key} está repetido en la asistencia.");

            var expectedStudentIds = courseStudents.Select(s => s.Id).OrderBy(id => id).ToList();
            var receivedStudentIds = request.Details.Select(d => d.StudentId).OrderBy(id => id).ToList();

            if (!expectedStudentIds.SequenceEqual(receivedStudentIds))
                throw new ArgumentException("La asistencia debe incluir exactamente a los estudiantes activos del curso seleccionado.");

            foreach (var detail in request.Details)
            {
                var student = _studentRepository.GetById(detail.StudentId);
                if (student == null || !student.IsActive || student.CourseId != request.CourseId)
                    throw new ArgumentException($"El estudiante con Id = {detail.StudentId} no pertenece al curso seleccionado.");
            }
        }
    }
}
