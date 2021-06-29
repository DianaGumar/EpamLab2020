using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.UserManager.API.Models;
using UserApi.Services;

namespace TicketManagement.UserManager.API.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;

        public LoginController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        ////[HttpGet]
        ////public async Task<IActionResult> Login()
        ////{
        ////    // Clear the existing external cookie to ensure a clean login process
        ////    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ////    return View(new LoginViewModel());
        ////}

        [HttpPost("login")]
        ////[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model?.Email);
            if (user == null)
            {
                ////ModelState.AddModelError(string.Empty, "User dont exist.");
                ////return View(model);
                return Forbid();
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(user?.UserName, model?.Password,
                (bool) model?.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(_jwtTokenService.GetToken(user));
                ////return RedirectToAction("Index", "Home");
            }

            //////ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            // If we got this far, something failed, redisplay form
            //////return View(model);

            return Forbid();
        }

        [HttpGet("validate")]
        public IActionResult Validate(string token)
        {


            return _jwtTokenService.ValidateToken(token) ? Ok() : (IActionResult)Forbid();
        }
    }
}
