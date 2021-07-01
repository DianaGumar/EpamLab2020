using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TicketManagement.BusinessLogic.Standart.CustomMiddleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // чёто происходит с токеном
            // вынести классы проверки токена на валидность в бл
            // заюзать их здесь

            // создать собственный middleware который будет проверять токен на валидность
            // при каждом запросе (не лазя в бд)
            // разместить мидлваре в бл
            // перенести метод проверки токена в бл, перебрасывать в него параметры из сеттингов
            // и использовать в каждом апи чтоб не дублировать код запросов
            // проверять токен на валидность
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
