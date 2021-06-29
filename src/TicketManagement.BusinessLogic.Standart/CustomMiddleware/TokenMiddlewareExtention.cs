using Microsoft.AspNetCore.Builder;

namespace TicketManagement.BusinessLogic.Standart.CustomMiddleware
{
    public static class TokenMiddlewareExtention
    {
        // метод расширения для пользовательского middleware
        public static IApplicationBuilder UseTokenAuth(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenMiddleware>();
        }
    }
}
