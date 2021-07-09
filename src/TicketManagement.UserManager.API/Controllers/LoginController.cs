using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.UserManager.API.Models;
using UserApi.Services;

namespace TicketManagement.UserManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;

        public LoginController(RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            JwtTokenService jwtTokenService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

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
                    var roles = await _userManager.GetRolesAsync(user);

                    var info = await _userManager.GetClaimsAsync(user);
                    _ = info;

                    // закидываем токен в хедер ответа
                    Response.Headers.Add("Authorization", _jwtTokenService.GetToken(user));
                    Response.Headers.Add("AuthorizationRoles", roles.ToArray());
                    return Ok();
                }
            }

            return BadRequest(new { errorText = "Invalid username or password." });
        }

        [HttpGet("validate")]
        public IActionResult Validate(string token)
        {
            return _jwtTokenService.ValidateToken(token) ? Ok() : (IActionResult)Forbid();
        }
    }
}
