////using System.Text;
////using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
////using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
////using Microsoft.AspNetCore.WebUtilities;
using TicketManagement.AccountManager.API.Models;

namespace TicketManagement.AccountManager.API.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterController(RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = _roleManager.Roles.ToList();
            var items = new List<SelectListItem>();
            roles.ForEach(r => items.Add(new SelectListItem { Text = r.Name, Value = r.Name }));

            return View(new RegisterViewModel { ExistingRoles = items });
        }

        [HttpPost]
#pragma warning disable S1541 // Methods and properties should not be too complex
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
#pragma warning restore S1541 // Methods and properties should not be too complex
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model?.Email, Email = model?.Email };
                var result = await _userManager.CreateAsync(user, model?.Password);

                if (result.Succeeded)
                {
                    if (model?.Role != null)
                    {
                        await _userManager.AddToRoleAsync(user, model?.Role);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            if (model != null)
            {
                var roles = _roleManager.Roles.ToList();
                var items = new List<SelectListItem>();
                roles.ForEach(r => items.Add(new SelectListItem { Text = r.Name, Value = r.Name }));
                model.ExistingRoles = items;
            }

            return View(model);
        }
    }
}
