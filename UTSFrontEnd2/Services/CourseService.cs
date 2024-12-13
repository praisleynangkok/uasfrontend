using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UTSFrontEnd2.Models;
using System.Collections.Generic;

namespace UTSFrontEnd2.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _httpClient.GetFromJsonAsync<List<Course>>("https://actualbackendapp.azurewebsites.net/api/courses");
        }

        public async Task<Course> GetCourse(int id)
        {
            return await _httpClient.GetFromJsonAsync<Course>($"https://actualbackendapp.azurewebsites.net/api/courses/{id}");
        }

        public async Task CreateCourse(Course course)
        {
            await _httpClient.PostAsJsonAsync("https://actualbackendapp.azurewebsites.net/api/courses", course);
        }

        public async Task UpdateCourse(Course course)
        {
            await _httpClient.PutAsJsonAsync($"https://actualbackendapp.azurewebsites.net/api/courses/{course.Id}", course);
        }

        public async Task DeleteCourse(int id)
        {
            await _httpClient.DeleteAsync($"https://actualbackendapp.azurewebsites.net/api/courses/{id}");
        }

        public async Task<List<Course>> SearchCourses(string category, string courseName)
        {
            var url = $"https://actualbackendapp.azurewebsites.net/api/courses/search?category={category}&courseName={courseName}";
            return await _httpClient.GetFromJsonAsync<List<Course>>(url);
        }
    }
}
