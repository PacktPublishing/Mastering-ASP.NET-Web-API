using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BudegetIdentityDemo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IdentityDbSeeder>();

            services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BudgetConnStr"),
                sqlopt => sqlopt.MigrationsAssembly("BudegetIdentityDemo")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>();

            // Avoids redirecting to Account/Login, only for API routes            

            services.AddCookieAuthentication(config =>
            {
                config.Events =
                  new CookieAuthenticationEvents()
                  {
                      OnRedirectToLogin = (ctx) =>
                      {
                          if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                          {
                              ctx.Response.StatusCode = 401;
                          }

                          return Task.CompletedTask;
                      },
                      OnRedirectToAccessDenied = (ctx) =>
                      {
                          if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                          {
                              ctx.Response.StatusCode = 403;
                          }

                          return Task.CompletedTask;
                      }
                  };
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IdentityDbSeeder identitySeeder)
        {
            app.UseAuthentication();
            app.UseIdentity();
            app.UseMvc();
        }
    }
}
