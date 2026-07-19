using Microsoft.EntityFrameworkCore;
using AttendanceControl.Infrastructure.Context;
using AttendanceControl.Infrastructure.Repositories;
using AttendanceControl.Application.Contract;
using AttendanceControl.Application.Services;
using AttendanceControl.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<InstructorRepository>();
builder.Services.AddScoped<AttendanceRepository>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();