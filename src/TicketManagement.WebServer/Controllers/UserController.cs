using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
/////using Microsoft.AspNetCore.Http;
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
            // запрос на получение лолей  GetAllRoles
            var roles = await _httpClient.GetFromJsonAsync<List<string>>("api/Register/get_all_roles");
            return roles;
        }

        [HttpPost("register")] // +
        public async Task<IActionResult> Register([FromBody] RegisterViewModel user) // клиент отправляет в метод данные юзера
        {
            HttpResponseMessage response = await _httpClient
                .PostAsJsonAsync<RegisterViewModel>("api/Register/register", user);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return Ok(token);
            }

            return Forbid();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel user) // +
        {
            HttpResponseMessage response = await _httpClient
                .PostAsJsonAsync<LoginViewModel>("api/Login/login", user);
            if (response.IsSuccessStatusCode)
            {
                ////var token = Request.Headers["Authorization"];
                var token = await response.Content.ReadAsStringAsync(); // -
                return Ok(token);
            }

            return Forbid();
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
