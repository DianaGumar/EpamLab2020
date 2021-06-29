using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
/////using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketManagement.BusinessLogic.Standart.CustomMiddleware;

namespace TicketManagement.WebClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ////// something interesting
            ////services.AddDbContext<ApplicationDbContext>(options =>
            ////    options.UseSqlServer(Configuration.GetConnectionString("MainConnection")));
            ////////services.AddDatabaseDeveloperPageExceptionFilter();

            ////services.AddIdentity<IdentityUser, IdentityRole>(
            ////    options =>
            ////    {
            ////        options.SignIn.RequireConfirmedAccount = false;
            ////        options.SignIn.RequireConfirmedEmail = false;
            ////    })
            ////    .AddEntityFrameworkStores<ApplicationDbContext>()
            ////    .AddDefaultTokenProviders();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // app = pipeline
            app.UseRouting();

            // ничего не знает об аутентификации и базе данных пользователей,
            // но при этом должен иметь понятие о валидности пользователя (используется во view)

            ////app.UseAuthorization();
            ////app.UseMiddleware<TokenMiddleware>();
            // вызов кастомного middleware
            app.UseTokenAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    /////pattern: "{controller=Event}/{action=Index}/{id?}");
                    pattern: "{controller=Event}/{action=Index}");
        });
        }
    }
}
