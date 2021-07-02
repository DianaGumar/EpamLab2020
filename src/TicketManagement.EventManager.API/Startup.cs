using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TicketManagement.BusinessLogic;
using TicketManagement.Web;

namespace TicketManagement.EventManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketManagement.EventManager.API", Version = "v1" });

                ////c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                ////{
                ////    Description = "Jwt Token is required to access the endpoints",
                ////    In = ParameterLocation.Header,
                ////    Name = "Authorization",
                ////    Type = SecuritySchemeType.ApiKey,
                ////});

                ////c.AddSecurityRequirement(new OpenApiSecurityRequirement
                ////{
                ////    {
                ////        new OpenApiSecurityScheme
                ////        {
                ////            Reference = new OpenApiReference
                ////            {
                ////                Id = "Bearer",
                ////                Type = ReferenceType.SecurityScheme,
                ////            },
                ////        },
                ////        System.Array.Empty<string>()
                ////    },
                ////});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketManagement.EventManager.API v1"));
            }

            ////app.UseHttpsRedirection();
            app.UseRouting();
            ////app.useauthentication();
            ////app.useauthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // регестрирую собстенные модули
            builder.RegisterModule(new DbRepositoryModule());
            builder.RegisterModule(new DbServiceModule());
        }
    }
}
