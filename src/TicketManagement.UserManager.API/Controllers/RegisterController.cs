using System.Collections.Generic;
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
    public class RegisterController : ControllerBase
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

        [HttpGet("get_all_roles")]
        public ActionResult<IList<string>> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var items = new List<string>();
            roles.ForEach(r => items.Add(r.Name));

            return items;
        }

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

                // immediately signIns after registration
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(_jwtTokenService.GetToken(user));
            }

            // если не sucsess то нужно дропать юзера
            return BadRequest(result.Errors);
        }
    }
}
