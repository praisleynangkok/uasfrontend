using System.Net.Http.Json;

public interface IAuthService
{
    Task<bool> Login(string username, string password);
    Task Logout();
    Task<string?> GetToken();
}

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly TokenAuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, TokenAuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> Login(string username, string password)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", new { username, password });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (!string.IsNullOrEmpty(result?.Token))
                {
                    await _authStateProvider.SetToken(result.Token);
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during login: {ex.Message}");
        }

        return false;
    }

    public async Task Logout()
    {
        await _authStateProvider.RemoveToken();
    }

    public async Task<string?> GetToken()
    {
        return await _authStateProvider.GetTokenAsync();
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
