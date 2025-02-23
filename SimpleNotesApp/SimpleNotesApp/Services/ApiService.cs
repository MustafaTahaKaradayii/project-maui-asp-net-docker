using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleNotesApp.Models; // For the Note model

namespace SimpleNotesApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        // Base URL of your API - adjust port as needed.
        // If your API uses HTTPS and a different port, update accordingly.
        private const string BaseUrl = "http://localhost:8080/api/notes";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Note>>(BaseUrl);
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Note>($"{BaseUrl}/{id}");
        }

        public async Task<Note> CreateNoteAsync(Note newNote)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, newNote);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Note>();
        }

        public async Task UpdateNoteAsync(int id, Note updatedNote)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", updatedNote);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
