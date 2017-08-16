using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWallet.ErrorHandlers;
using MyWallet.ModelsContexts;
using NLog.Extensions.Logging;
using NLog.Web;

namespace MyWallet
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Used for Handling Web API gracefully
            services.AddScoped<WebApiExceptionFilter>();

            //Using EF Core InMemory Database instead of real database
            services.AddDbContext<ExpenseContext>(opt => opt.UseInMemoryDatabase("MyWalletDB"));

            #region NLog
            //needed for NLog.Web
            //UnComment when working with NLOG
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region SerilogDatabase         
            //services.AddSingleton<Serilog.ILogger>(x =>
            //{
            //    return new LoggerConfiguration().WriteTo.MSSqlServer(Configuration["Serilog:ConnectionString"], Configuration["Serilog:TableName"]).CreateLogger();
            //});

            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region NLog
            //UnComment when working with NLOG
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            #endregion

            app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
