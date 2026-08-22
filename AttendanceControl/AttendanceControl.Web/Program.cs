using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AttendanceControl.Web;
using AttendanceControl.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7031/api/")
});

builder.Services.AddScoped<CourseApiService>();
builder.Services.AddScoped<StudentApiService>();
builder.Services.AddScoped<InstructorApiService>();
builder.Services.AddScoped<AttendanceApiService>();

await builder.Build().RunAsync();