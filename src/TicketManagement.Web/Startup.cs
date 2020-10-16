using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketManagement.Web.Startup))]
namespace TicketManagement.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
