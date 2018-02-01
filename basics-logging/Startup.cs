using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace basics_logging
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var strtpLogger = loggerFactory.CreateLogger<Startup>();
            strtpLogger.LogTrace("Looking at Trace level ");
            strtpLogger.LogDebug("This is Debug level");
            strtpLogger.LogInformation("You are Startup class - FY Information");
            strtpLogger.LogWarning("Warning - Entered Startup so soon");
            strtpLogger.LogError("This result in Null reference exception");
            strtpLogger.LogCritical("Critical - No Disk space");


            app.UseMvc();
        }
    }
}
