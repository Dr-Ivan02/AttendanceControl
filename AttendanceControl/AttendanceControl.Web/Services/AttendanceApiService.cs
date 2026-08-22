using System.Net.Http.Json;
using AttendanceControl.Web.Models;

namespace AttendanceControl.Web.Services
{
    public class AttendanceApiService
    {
        private readonly HttpClient _http;

        public AttendanceApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AttendanceDTO>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<AttendanceDTO>>("attendances") ?? new();
        }

        public async Task<AttendanceDTO?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"attendances/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<AttendanceDTO>();
        }

        public async Task<bool> CreateAsync(CreateAttendanceDTO dto)
        {
            var response = await _http.PostAsJsonAsync("attendances", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, CreateAttendanceDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"attendances/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"attendances/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}