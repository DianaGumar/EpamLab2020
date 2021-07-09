using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.WebServer.Models;

namespace TicketManagement.WebServer.Controllers
{
    // один контроллер для всего userAPI
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IDisposable
    {
        private bool _disposed;
        private HttpClient _httpClient;

        public UserController()
        {
            _disposed = false;
            _httpClient = new HttpClient();

            // подключается к user api
#pragma warning disable S1075 // URIs should not be hardcoded
            _httpClient.BaseAddress = new Uri("https://localhost:5021/"); // адрес апи к которому будешь обращаться
#pragma warning restore S1075 // URIs should not be hardcoded
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet("get_all_roles")]
        public async Task<IList<string>> GetAllRoles() // +
        {
            // запрос на получение ролей  GetAllRoles
            var roles = await _httpClient.GetFromJsonAsync<List<string>>("api/Register/get_all_roles");
            return roles;
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Logout/logout");

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return Forbid();
        }

        [HttpPost("register")] // +
        public async Task<IActionResult> Register([FromBody] RegisterViewModel user)
        {
            HttpResponseMessage response = await _httpClient
                .PostAsJsonAsync<RegisterViewModel>("api/Register/register", user);

            if (response.IsSuccessStatusCode &&
                response.Headers.Contains("Authorization") &&
                response.Headers.Contains("AuthorizationRoles"))
            {
                var token = response.Headers.GetValues("Authorization").ToArray()[0];
                var roles = response.Headers.GetValues("AuthorizationRoles").ToArray()[0];
                Response.Headers.Add("Authorization", token);
                Response.Headers.Add("AuthorizationRoles", roles);
                return Ok();
            }

            return Forbid();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel user) // +
        {
            HttpResponseMessage response = await _httpClient
                .PostAsJsonAsync<LoginViewModel>("api/Login/login", user);
            if (response.IsSuccessStatusCode &&
                response.Headers.Contains("Authorization") &&
                response.Headers.Contains("AuthorizationRoles"))
            {
                var token = response.Headers.GetValues("Authorization").ToArray()[0];
                var roles = response.Headers.GetValues("AuthorizationRoles").ToArray()[0];
                Response.Headers.Add("Authorization", token);
                Response.Headers.Add("AuthorizationRoles", roles);
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid username or password." });
        }

        [HttpGet("get_user")]
        public async Task<IdentityUser> Get(string name)
        {
            var user = await _httpClient
                .GetFromJsonAsync<IdentityUser>($"api/User/get_user/{name}");
            return user;
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(string id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/User/{id}");
            var contents = await response.Content.ReadAsStringAsync();
            return contents;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing && _httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
