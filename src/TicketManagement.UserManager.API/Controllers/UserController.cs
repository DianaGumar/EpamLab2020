using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TicketManagement.UserManager.API.Controllers
{
    // crud user operations
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // собственная модель пользователя будет с ролью и кошельком
        [HttpGet("get_user")]
        public async Task<IdentityUser> Get(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return user;
        }

        [HttpDelete("{id}")]
        public async Task<IdentityResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);

            return result;
        }
    }
}
