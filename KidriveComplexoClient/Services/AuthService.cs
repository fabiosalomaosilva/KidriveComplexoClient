using Blazored.LocalStorage;
using KidriveComplexoClient.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KidriveComplexoClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("auth", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.token);
            ((ApiAuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.email);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> GetAuthState()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<string> GetNome()
        {
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var nome = auth.User.Claims.FirstOrDefault(p => p.Type == "nomeCompleto")?.Value;
            return nome;
        }

        public async Task<string> GetAvatar()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Claims.FirstOrDefault(p => p.Type == "foto")?.Value;
        }

        public async Task<string> GetEmail()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Claims.FirstOrDefault(p => p.Type == "email")?.Value;
        }
        public async Task<string> GetSetor()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Claims.FirstOrDefault(p => p.Type == "setor")?.Value;
        }
        public async Task<string> GetCargo()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Claims.FirstOrDefault(p => p.Type == "cargo")?.Value;
        }
    }
}
