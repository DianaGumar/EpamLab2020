using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TicketManagement.Web.Models;

namespace TicketManagement.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
#pragma warning disable CA1822 // Mark members as static
        protected void Application_Start()
#pragma warning restore CA1822 // Mark members as static
        {
            Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer());

            ResolverConfig.RegisterResolver();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
