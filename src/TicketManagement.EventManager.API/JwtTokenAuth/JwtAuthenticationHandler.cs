﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TicketManagement.API.JwtTokenAuth
{
    public class JwtAuthenticationHandler : AuthenticationHandler<JwtAuthenticationOptions>
    {
        public JwtAuthenticationHandler(
            IOptionsMonitor<JwtAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            // достаём токен из хедера
            var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);

            // создаём http клиента для связи с авторизационным сервисом для проверки валидности токена
            var httpClient = new HttpClient();
#pragma warning disable S1075 // URIs should not be hardcoded
            httpClient.BaseAddress = new Uri("http://localhost:5000");
#pragma warning restore S1075 // URIs should not be hardcoded

            // создаём get запрос
            var response = await httpClient.GetAsync(new Uri($"users/validate?token={token}"));
            httpClient.Dispose();

            // проверка ответа - валиден ли токен
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var identity = new ClaimsIdentity(jwtToken.Claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Unauthorized");
        }
    }
}