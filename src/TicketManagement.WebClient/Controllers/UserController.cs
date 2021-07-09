﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketManagement.BusinessLogic.Entities;
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

        // user managing
        public async Task<IActionResult> GetUserInfo()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            // получиаем данные о текущем залогиненном пользователе не из кук, а по запросу к апи
            var user = await _httpClient.GetFromJsonAsync<IdentityUser>($"api/User/get_user/{HttpContext.User.Identity.Name}");

            TMUser obj = new TMUser();
            obj.UserLastName = user.UserName;
            obj.UserEmail = user.Email;

            ////// получение ролей пользователя
            ////ticketUser.Role = HttpContext.User.Claim

            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            ////// забираем токен с куки, разлогиниваем на клиенте
            ////var token = HttpContext.Request.Cookies["secret_jwt_key"];
            ////var decodedJwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            ////// забираем из токена клеймы с юзером, записываем их в контекст
            ////ClaimsIdentity claimsIdentity = new ClaimsIdentity(decodedJwt.Claims, "UserInfo",
            ////    ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            ////HttpContext.User = new GenericPrincipal(claimsIdentity, null); // вместо null должны быть стринги ролей

            ////var user = HttpContext.User.Identity.Name;
            HttpResponseMessage response = await _httpClient.GetAsync("api/User/logout");

            if (response.IsSuccessStatusCode)
            {
                await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Index", "Event");
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

            if (response.IsSuccessStatusCode &&
                response.Headers.Contains("Authorization") &&
                response.Headers.Contains("AuthorizationRoles"))
            {
                // taking token from header and writing to cookie
                var token = response.Headers.GetValues("Authorization").ToArray()[0];
                var roles = response.Headers.GetValues("AuthorizationRoles").ToArray()[0];
                AuthorizeHandle(token, roles);

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
            if (response.IsSuccessStatusCode
                && response.Headers.Contains("Authorization")
                && response.Headers.Contains("AuthorizationRoles"))
            {
                // taking token from header and writing to cookie
                var token = response.Headers.GetValues("Authorization").ToArray()[0];
                var roles = response.Headers.GetValues("AuthorizationRoles").ToArray()[0];
                AuthorizeHandle(token, roles);

                return RedirectToAction("Index", "Event");
            }

            // реализовать прокидывание уточняющих сообщений при неудачной попытке
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        private async void AuthorizeHandle(string token, string roles)
        {
            HttpContext.Response.Cookies.Append("secret_jwt_key", token,
                    new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.Strict });

            var decodedJwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var arrRoles = roles.Split(',');

            // забираем из токена клеймы с юзером, записываем их в контекст
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(decodedJwt.Claims, "UserInfo",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.User = new GenericPrincipal(claimsIdentity, arrRoles);

            // оповещаем систему о залогиненности
            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, HttpContext.User);
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
