using Microsoft.AspNetCore.Hosting;
////using Microsoft.AspNetCore.Identity;
////using Microsoft.AspNetCore.Identity.UI;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.Extensions.Configuration;
////using Microsoft.Extensions.DependencyInjection;
////using TicketManagement.AccountManager.API.Data;

[assembly: HostingStartup(typeof(TicketManagement.AccountManager.API.Areas.Identity.IdentityHostingStartup))]

namespace TicketManagement.AccountManager.API.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder?.ConfigureServices((context, services) =>
            {
            });
        }
    }
}