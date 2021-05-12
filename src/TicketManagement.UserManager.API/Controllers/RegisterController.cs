using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.UserManager.API.Models;
using UserApi.Services;

namespace TicketManagement.UserManager.API.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtTokenService _jwtTokenService;

        public RegisterController(RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            JwtTokenService jwtTokenService)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        ////[HttpGet]
        ////public IActionResult Register()
        ////{
        ////    var roles = _roleManager.Roles.ToList();
        ////    var items = new List<SelectListItem>();
        ////    roles.ForEach(r => items.Add(new SelectListItem { Text = r.Name, Value = r.Name }));

        ////    return View(new RegisterViewModel { ExistingRoles = items });
        ////}

        // возвращает токен
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var user = new IdentityUser { UserName = model?.Email, Email = model?.Email };
            var result = await _userManager.CreateAsync(user, model?.Password);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model?.Role))
                {
                    await _userManager.AddToRoleAsync(user, model?.Role);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(_jwtTokenService.GetToken(user));
                /////return RedirectToAction("Index", "Home");
            }

            return BadRequest(result.Errors);

            ////foreach (var error in result.Errors)
            ////{
            ////    ModelState.AddModelError(string.Empty, error.Description);
            ////}

            // If we got this far, something failed, redisplay form
            ////if (model != null)
            ////{
            ////    var roles = _roleManager.Roles.ToList();
            ////    var items = new List<SelectListItem>();
            ////    roles.ForEach(r => items.Add(new SelectListItem { Text = r.Name, Value = r.Name }));
            ////    model.ExistingRoles = items;
            ////}

            ////return View(model);
        }
    }
}
