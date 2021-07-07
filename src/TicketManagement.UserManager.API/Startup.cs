using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TicketManagement.UserManager.API.Data;
using UserApi.Services;
using UserApi.Settings;

namespace TicketManagement.UserManager.API
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
            // HttpContextAccessor создаётся единожды при первом обращении используется для доступа к контексту
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    Configuration.GetConnectionString("MainConnection")));
            ////services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // достаёт из файла конфигурации
            var tokenSettings = Configuration.GetSection(nameof(JwtTokenSettings));
            var validIssuer2 = tokenSettings[nameof(JwtTokenSettings.JwtIssuer)];
            _ = validIssuer2;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => // добавление обработчика проверки подлинности jwt
                {
                    options.RequireHttpsMetadata = false; // для использования передачи по протоколу https
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = tokenSettings[nameof(JwtTokenSettings.JwtIssuer)],
                        ValidateAudience = true,
                        ValidAudience = tokenSettings[nameof(JwtTokenSettings.JwtAudience)],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(tokenSettings[nameof(JwtTokenSettings.JwtSecretKey)])),
                        ValidateLifetime = false,
                    };
                });

            services.Configure<JwtTokenSettings>(tokenSettings);
            services.AddScoped<JwtTokenService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketManagement.UserManager.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketManagement.UserManager.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
