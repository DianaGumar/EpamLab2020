using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketManagement.UserManager.API.Dto;

namespace UserApi.DataAccess
{
    public class UserApiDbContext : IdentityDbContext<User>
    {
        public UserApiDbContext(DbContextOptions<UserApiDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
