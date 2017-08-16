using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PersonalBudget.Models;
using System.Text;

namespace PersonalBudget
{
    public class Startup
    {
        public IConfiguration Configuration;
        public IHostingEnvironment Environment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                //Remove the localhost to provide access for all clients
                options.AddPolicy("DemoCorsPolicy",
                    c => c.WithOrigins("http://localhost:3000/"));
            });
            services.AddDataProtection();
            services.AddSingleton<StringConstants>();
            //Claim Authorization Policy
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("SuperUsers", p => p.RequireClaim("SuperUser", "True"));
            });

            //JWT 
            services.AddJwtBearerAuthentication("PacktAuthentication", options =>
            {
                options.ClaimsIssuer = Configuration["Tokens:Issuer"];
                options.Audience = Configuration["Tokens:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateLifetime = true
                };
            });

            services.AddMvc();
            //Using EF Core InMemory Database instead of real database
            services.AddDbContext<PersonalBudgetContext>(opt => opt.UseInMemoryDatabase("PersonalBudget"));            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDataProtectionProvider dataprovider, StringConstants strconsts)
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.ConstructServicesUsing(type => new IdProtectorConverter(dataprovider, strconsts));
                cfg.CreateMap<BudgetCategoryDTO, BudgetCategory>();
                cfg.CreateMap<BudgetCategory, BudgetCategoryDTO>().ConvertUsing<IdProtectorConverter>();

            });
            app.UseCors("DemoCorsPolicy");
            app.UseAuthentication();

            var context = app.ApplicationServices.GetService<PersonalBudgetContext>();
            TestData.AddTestData(context);

            app.UseMvc();
        }
    }
}
