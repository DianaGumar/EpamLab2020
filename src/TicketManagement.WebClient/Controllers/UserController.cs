using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketManagement.WebClient.Models;

namespace TicketManagement.WebClient.Controllers
{
    public class UserController : Controller
    {
        private HttpClient _httpClient;

        public UserController()
        {
            _httpClient = new HttpClient();

            // подключается к venue api
#pragma warning disable S1075 // URIs should not be hardcoded
            _httpClient.BaseAddress = new Uri("https://localhost:5101/"); // адрес сервера, к которому обращается
#pragma warning restore S1075 // URIs should not be hardcoded
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            // инициализировать базу ролями
            // запрос к серверу
            var roles = await _httpClient.GetFromJsonAsync<List<string>>("api/User/get_all_roles");

            // преобразование в селектед лист
            var items = new List<SelectListItem>();
            roles.ForEach(r => items.Add(new SelectListItem { Text = r, Value = r }));

            return View(new RegisterViewModel { ExistingRoles = items });
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel user)
        {
            // общение посредством сетевых запросов http протокола
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("login", user?.Email),
                new KeyValuePair<string, string>("password", user?.Password),
            });
            var result = await _httpClient.PostAsync("api/User/register", formContent);
            formContent.Dispose();

            if (result.IsSuccessStatusCode)
            {
                // вернуть представление

                ////var token = await result.Content.ReadAsStringAsync();

                ////// сохранение в куки полученного токена
                ////HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
                ////{
                ////    HttpOnly = true, // чтобы с js никто не получил доступ
                ////    SameSite = SameSiteMode.Strict,
                ////});

                ////return Ok();
            }

            // если что то не так снова возвращает представление с ролями
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] UserModel user)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("login", user?.Login),
                new KeyValuePair<string, string>("password", user?.Password),
            });
            var result = await _httpClient.PostAsync("users/login", formContent);
            formContent.Dispose();

            if (result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsStringAsync();
                HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                });

                return Ok();
            }

            return Forbid();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }

            base.Dispose(disposing);
        }
    }
}
