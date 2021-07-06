using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketManagement.UserManager.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // при первом обращении создаётся бд
            Database.EnsureCreated();
        }

        ////protected override void OnModelCreating(ModelBuilder modelBuilder)
        ////{
        ////    // здесь могла бы быть начальная инициализация
        ////}
    }
}
