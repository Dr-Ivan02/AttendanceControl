using System.Net.Http.Json;
using AttendanceControl.Web.Models;

namespace AttendanceControl.Web.Services
{
    public class CourseApiService
    {
        private readonly HttpClient _http;

        public CourseApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CourseDTO>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<CourseDTO>>("courses") ?? new();
        }

        public async Task<CourseDTO?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"courses/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CourseDTO>();
        }

        public async Task<bool> CreateAsync(CreateCourseDTO dto)
        {
            var response = await _http.PostAsJsonAsync("courses", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, CreateCourseDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"courses/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"courses/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}