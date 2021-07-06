using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TicketManagement.UserManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost("logout")]
        public async void Logout()
        {
            //// JwtBearerDefaults.AuthenticationScheme
            await _signInManager.SignOutAsync();
        }
    }
}
