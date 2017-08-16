using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CustomRouteConstraints
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adding Router Middleware
            services.AddRouting();
            services.Configure<RouteOptions>(options =>
            options.ConstraintMap.Add("domain",
            typeof(DomainConstraint)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var routeBuilder = new RouteBuilder(app);
            // domName parameter should have @packt.com
            routeBuilder.MapGet("api/employee/{domName:domain}", context
            =>
            {
                var domName = context.GetRouteValue("domName");
                return context.Response.WriteAsync(
                $"Domain Name Constraint Passed - {domName}");
            });
            var routes = routeBuilder.Build();
            app.UseRouter(routes);
        }
    }
}
