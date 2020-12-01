﻿using System.Data.Entity;
using TicketManagement.Web.Models;

namespace TicketManagement.Web
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            ////var userStore = new UserStore<ApplicationUser>(context);
            ////var roleStore = new RoleStore<IdentityRole>(context);

            ////var userManager = new ApplicationUserManager(userStore);
            ////var roleManager = new RoleManager<IdentityRole>(roleStore);

            ////// create roles
            ////roleManager.Create(new IdentityRole { Name = "eventmanager" });
            ////roleManager.Create(new IdentityRole { Name = "authorizeduser" });
            ////roleManager.Create(new IdentityRole { Name = "venuemanager" });

            ////userManager.Dispose();
            ////roleManager.Dispose();
            ////userStore.Dispose();
            ////roleStore.Dispose();

            base.Seed(context);
        }
    }
}