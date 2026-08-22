using System.Net.Http.Json;
using AttendanceControl.Web.Models;

namespace AttendanceControl.Web.Services
{
    public class InstructorApiService
    {
        private readonly HttpClient _http;

        public InstructorApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<InstructorDTO>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<InstructorDTO>>("instructors") ?? new();
        }

        public async Task<InstructorDTO?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"instructors/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<InstructorDTO>();
        }

        public async Task<bool> CreateAsync(CreateInstructorDTO dto)
        {
            var response = await _http.PostAsJsonAsync("instructors", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, CreateInstructorDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"instructors/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"instructors/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}