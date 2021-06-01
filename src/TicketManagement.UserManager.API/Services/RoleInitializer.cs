﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TicketManagement.UserManager.API.Services
{
    public static class RoleInitializer
    {
        // метод инициализации базы identity начальными ролями и админом
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _ = userManager;

            // added roles authorizeduser eventmanager venuemanager
            // увы ef не позволяет асинки в отношении одного контекста бд
            ////var roles = new List<string> { "authorizeduser", "eventmanager", "venuemanager" };
            ////roles.ForEach(async r =>
            ////{
            ////    if (await roleManager?.FindByNameAsync(r) == null)
            ////    {
            ////        await roleManager?.CreateAsync(new IdentityRole(r));
            ////    }
            ////});

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

            // added admin-user
            ////string adminEmail = "admin@gmail.com";
            ////string password = "_Aa123456";
            ////if (await userManager?.FindByNameAsync(adminEmail) == null)
            ////{
            ////    var admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
            ////    IdentityResult result = await userManager?.CreateAsync(admin, password);
            ////    if (result.Succeeded)
            ////    {
            ////        await userManager.AddToRoleAsync(admin, "admin");
            ////    }
            ////}
        }
    }
}
