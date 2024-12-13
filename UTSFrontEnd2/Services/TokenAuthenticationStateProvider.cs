using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class TokenAuthenticationStateProvider : AuthenticationStateProvider
{
    private string? _token;

    public Task SetToken(string token)
    {
        _token = token;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        return Task.CompletedTask;
    }

    public Task RemoveToken()
    {
        _token = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        return Task.CompletedTask;
    }

    public async Task<string?> GetTokenAsync()
    {
        return _token;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (string.IsNullOrEmpty(_token))
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        }

        var claims = new[] { new Claim(ClaimTypes.Name, "User") };
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return Task.FromResult(new AuthenticationState(user));
    }
}
