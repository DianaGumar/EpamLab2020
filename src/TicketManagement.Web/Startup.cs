using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketManagement.Web.Startup))]

namespace TicketManagement.Web
{
    public sealed partial class Startup
    {
        private Startup()
        {
        }

        public static void Configuration(IAppBuilder app)
        {
            ////services.AddDbContext<StipContext>(options =>
            ////options.UseSqlServer(Configuration.GetConnectionString("StipDatabase")));

            ConfigureAuth(app);
        }
    }
}
