using Microsoft.AspNetCore.Identity;

namespace TicketManagement.UserManager.API.Dto
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
