using System.Threading.Tasks;
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

        ////[HttpPost]
        ////public async Task<IActionResult> Logout()
        ////{
        ////    await _signInManager.SignOutAsync();
        ////    return RedirectToAction("Index", "Home");
        ////}

        [HttpPost("logout")]
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
