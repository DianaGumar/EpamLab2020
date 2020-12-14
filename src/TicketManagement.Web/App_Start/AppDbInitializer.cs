////using Microsoft.AspNet.Identity;
////using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TicketManagement.Web.Models;

namespace TicketManagement.Web
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //////emexample @gmail.com
            //////    x6@9hkrmWZNjmzY34

            ////var userStore = new UserStore<ApplicationUser>(context);
            ////var roleStore = new RoleStore<IdentityRole>(context);

            ////var userManager = new ApplicationUserManager(userStore);
            ////var roleManager = new RoleManager<IdentityRole>(roleStore);

            ////// create roles
            ////var user = new ApplicationUser
            ////{
            ////    UserName = "emexample @gmail.com",
            ////    Email = "emexample @gmail.com",
            ////};
            ////var result = UserManager.CreateAsync(user, "x6@9hkrmWZNjmzY34");
            ////if (result.Succeeded)
            ////{
            ////    _userService.CreateTMUser(
            ////        new TMUser { UserId = user.Id, Balance = 0, UserLastName = "" });
            ////    UserManager.AddToRoleAsync(user.Id, model.UserRole);
            ////}

            ////roleManager.Create(new IdentityRole { Name = "eventmanager" });
            ////roleManager.Create(new IdentityRole { Name = "authorizeduser" });
            ////roleManager.Create(new IdentityRole { Name = "venuemanager" });

            ////userManager.Dispose();
            ////roleManager.Dispose();
            ////userStore.Dispose();
            ////roleStore.Dispose();

            ////base.Seed(context);
        }
    }
}