using AutoMapper;
using AttendanceControl.Domain.Entities;
using AttendanceControl.Application.Dtos.Course;
using AttendanceControl.Application.Dtos.Student;
using AttendanceControl.Application.Dtos.Instructor;
using AttendanceControl.Application.Dtos.Attendance;

namespace AttendanceControl.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CreateCourseDTO, Course>();

            CreateMap<Student, StudentDTO>();
            CreateMap<CreateStudentDTO, Student>();

            CreateMap<Student, StudentWithCourseDTO>()
                .ForMember(dest => dest.CourseName,
                    opt => opt.MapFrom(src => src.Course != null ? src.Course.Name : "Sin Curso Asignado"));

            CreateMap<Instructor, InstructorDTO>();
            CreateMap<CreateInstructorDTO, Instructor>();

            CreateMap<Attendance, AttendanceDTO>();
            CreateMap<CreateAttendanceDTO, Attendance>();
        }
    }
}