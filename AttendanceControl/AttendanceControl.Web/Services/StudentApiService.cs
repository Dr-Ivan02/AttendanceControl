using System.Net.Http.Json;
using AttendanceControl.Web.Models;

namespace AttendanceControl.Web.Services
{
    public class StudentApiService
    {
        private readonly HttpClient _http;

        public StudentApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<StudentDTO>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<StudentDTO>>("students") ?? new();
        }

        public async Task<StudentDTO?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"students/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<StudentDTO>();
        }

        public async Task<bool> CreateAsync(CreateStudentDTO dto)
        {
            var response = await _http.PostAsJsonAsync("students", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, CreateStudentDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"students/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"students/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}