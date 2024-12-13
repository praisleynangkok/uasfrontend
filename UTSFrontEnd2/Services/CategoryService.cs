using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UTSFrontEnd2.Models;
using System.Collections.Generic;

namespace UTSFrontEnd2.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>("https://actualbackendapp.azurewebsites.net/api/categories");
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _httpClient.GetFromJsonAsync<Category>($"https://actualbackendapp.azurewebsites.net/api/categories/{id}");
        }

        public async Task CreateCategory(Category category)
        {
            await _httpClient.PostAsJsonAsync("https://actualbackendapp.azurewebsites.net/api/categories", category);
        }

        public async Task UpdateCategory(Category category)
        {
            await _httpClient.PutAsJsonAsync($"https://actualbackendapp.azurewebsites.net/api/categories/{category.Id}", category);
        }

        public async Task DeleteCategory(int id)
        {
            await _httpClient.DeleteAsync($"https://actualbackendapp.azurewebsites.net/api/categories/{id}");
        }
    }
}
