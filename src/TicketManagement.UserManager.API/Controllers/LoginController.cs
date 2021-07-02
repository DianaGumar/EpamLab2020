using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.UserManager.API.Models;
using UserApi.Services;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TicketManagement.UserManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
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
        public async Task<IActionResult> Login([FromBody] LoginViewModel model) // +
        {
            var user = await _userManager.FindByEmailAsync(model?.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user?.UserName, model?.Password,
                (bool)model?.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    Request.Headers["Authorization"] = _jwtTokenService.GetToken(user);
                    return Ok(_jwtTokenService.GetToken(user)); // забрасывает в тело вместо хедера
                }
            }

            return Forbid();
        }

        [HttpGet("validate")]
        public IActionResult Validate(string token)
        {
            return _jwtTokenService.ValidateToken(token) ? Ok() : (IActionResult)Forbid();
        }
    }
}
