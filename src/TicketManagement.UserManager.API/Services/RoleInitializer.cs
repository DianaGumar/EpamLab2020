using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TicketManagement.UserManager.API.Services
{
    public static class RoleInitializer
    {
        // метод инициализации базы identity начальными ролями
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _ = userManager; // без админа

            if (await roleManager?.FindByNameAsync("authorizeduser") == null)
            {
                await roleManager?.CreateAsync(new IdentityRole("authorizeduser"));
            }

            if (await roleManager?.FindByNameAsync("eventmanager") == null)
            {
                await roleManager?.CreateAsync(new IdentityRole("eventmanager"));
            }

            if (await roleManager?.FindByNameAsync("venuemanager") == null)
            {
                await roleManager?.CreateAsync(new IdentityRole("venuemanager"));
            }
        }
    }
}
