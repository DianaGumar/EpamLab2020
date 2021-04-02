using System.Web.Mvc;

namespace TicketManagement.Web
{
    public sealed class FilterConfig
    {
        private FilterConfig()
        {
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters?.Add(new HandleErrorAttribute());
        }
    }
}
