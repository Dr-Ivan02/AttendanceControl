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
            CreateMap<Course, CourseDTO>()
                .ForMember(dest => dest.InstructorName,
                    opt => opt.MapFrom(src => src.Instructor != null ? src.Instructor.Name : string.Empty));
            CreateMap<CreateCourseDTO, Course>();

            CreateMap<Student, StudentDTO>();
            CreateMap<CreateStudentDTO, Student>();

            CreateMap<Student, StudentWithCourseDTO>()
                .ForMember(dest => dest.CourseName,
                    opt => opt.MapFrom(src => src.Course != null ? src.Course.Name : "Sin Curso Asignado"));

            CreateMap<Instructor, InstructorDTO>();
            CreateMap<CreateInstructorDTO, Instructor>();

            CreateMap<Attendance, AttendanceDTO>()
                .ForMember(dest => dest.CourseName,
                    opt => opt.MapFrom(src => src.Course != null ? src.Course.Name : string.Empty))
                .ForMember(dest => dest.TotalStudents,
                    opt => opt.MapFrom(src => src.Details.Count))
                .ForMember(dest => dest.PresentCount,
                    opt => opt.MapFrom(src => src.Details.Count(d => d.IsPresent)))
                .ForMember(dest => dest.AbsentCount,
                    opt => opt.MapFrom(src => src.Details.Count(d => !d.IsPresent)));
            CreateMap<CreateAttendanceDTO, Attendance>();
            CreateMap<AttendanceDetail, AttendanceDetailDTO>()
                .ForMember(dest => dest.StudentCode,
                    opt => opt.MapFrom(src => src.Student != null ? src.Student.Code : string.Empty))
                .ForMember(dest => dest.StudentName,
                    opt => opt.MapFrom(src => src.Student != null ? src.Student.Name : string.Empty));
            CreateMap<CreateAttendanceDetailDTO, AttendanceDetail>();
        }
    }
}
