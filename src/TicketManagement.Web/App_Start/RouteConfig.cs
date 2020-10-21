using System.Web.Mvc;
using System.Web.Routing;

namespace TicketManagement.Web
{
    public sealed class RouteConfig
    {
        private RouteConfig()
        {
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // TMEventModelsController
            // ReadAll
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TMEventModels", action = "ReadAll", id = UrlParameter.Optional });
        }
    }
}
