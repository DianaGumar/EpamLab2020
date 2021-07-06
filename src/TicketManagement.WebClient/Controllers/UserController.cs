using System;
using System.Collections.Generic;
using System.Linq;
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

        private async Task<List<SelectListItem>> GetRoles()
        {
            // запрос к серверу
            var roles = await _httpClient.GetFromJsonAsync<List<string>>("api/User/get_all_roles");

            // преобразование в селектед лист
            var items = new List<SelectListItem>();
            roles.ForEach(r => items.Add(new SelectListItem { Text = r, Value = r }));

            return items;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View(new RegisterViewModel { ExistingRoles = await GetRoles() });
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel user)
        {
            HttpResponseMessage response = await _httpClient
                .PostAsJsonAsync<RegisterViewModel>("api/User/register", user);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                // сохранение в куки полученного токена
                HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
                {
                    HttpOnly = true, // чтобы с js никто не получил доступ
                    SameSite = SameSiteMode.Strict,
                });

                return RedirectToAction("Index", "Event");
            }

            // если что то не так снова возвращает представление с ролями
            ModelState.AddModelError("", "Sms wrong");

            if (user != null)
            {
                user.ExistingRoles = await GetRoles();
                return View(user);
            }
            else
            {
                return View(new RegisterViewModel { ExistingRoles = await GetRoles() });
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel user) // +
        {
            if (user == null || !ModelState.IsValid)
            {
                return View(user);
            }

            HttpResponseMessage response = await _httpClient
                .PostAsJsonAsync<LoginViewModel>("api/User/login", user);

            // added to cookie
            if (response.IsSuccessStatusCode && response.Headers.Contains("Authorization"))
            {
                // достаём токен из хедера запроса
                var token = response.Headers.GetValues("Authorization").ToArray()[0];

                // кладём токен в куки
                HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                });

                return RedirectToAction("Index", "Event");
            }

            // реализовать прокидывание уточняющих сообщений при неудачной попытке
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
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
