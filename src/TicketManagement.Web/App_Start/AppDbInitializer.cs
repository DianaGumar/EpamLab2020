using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TicketManagement.Web.Models;

namespace TicketManagement.Web
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var roleStore = new RoleStore<IdentityRole>(context);

            var userManager = new ApplicationUserManager(userStore);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // create roles
            var venueManager = new IdentityRole { Name = "venue_manager" };
            var eventManager = new IdentityRole { Name = "event_manager" };
            var authorizedUser = new IdentityRole { Name = "authorized_user" };

            // add roles to db
            roleManager.Create(venueManager);
            roleManager.Create(eventManager);
            roleManager.Create(authorizedUser);

            userManager.Dispose();
            roleManager.Dispose();
            userStore.Dispose();
            roleStore.Dispose();

            base.Seed(context);
        }
    }
}