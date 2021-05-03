using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserApi.Dto;
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtTokenService _jwtTokenService;

        public UsersController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            JwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model) /////[FromForm]
        {
            var user = new IdentityUser
            {
                UserName = model?.Login,
            };

            var result = await _userManager.CreateAsync(user, model?.Password);
            if (result.Succeeded)
            {
                return Ok(_jwtTokenService.GetToken(user));
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Login into the new API.
        /// </summary>
        /// <remarks>
        /// Here is the code sample of usage.
        /// </remarks>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model?.Login, model?.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model?.Login);
                return Ok(_jwtTokenService.GetToken(user));
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
