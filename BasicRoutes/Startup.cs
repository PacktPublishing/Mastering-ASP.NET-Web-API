using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BasicRoutes
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            //Uncomment when learning custom route constraints

            //services.Configure<RouteOptions>(options =>
            //options.ConstraintMap.Add("domain", typeof(DomainConstraint)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region BasicRoute
            //Uncomment this to learn basic routing

            //HTTP pipeline now handles routing
            app.UseRouter(new RouteHandler(
                context => context.Response.WriteAsync("Mastering Web API!!")
                ));
            #endregion

            #region RouteBuilder
            // Uncomment to learn route builder, comment BasicRoute

            //var routes = new RouteBuilder(app)
            //    .MapGet("greeting", context => context.Response.WriteAsync("Good morning!! Packt readers."))
            //    .MapGet("review/{msg}", context => context.Response.WriteAsync($"This book is , {context.GetRouteValue("msg")}"))
            //    .MapPost("packtpost", context => context.Response.WriteAsync("Glad you did Post !"))
            //    .Build();

            //app.UseRouter(routes);
            #endregion

            #region RouteConstraintsNotApplied
            //Uncomment to learn when route constraints not applied, comment other regions

            //var routeBuilder = new RouteBuilder(app);
            //routeBuilder.MapGet("employee/{id}", context =>
            //{
            //    var idValue = context.GetRouteValue("id");
            //    return context.Response.WriteAsync($"The number is - {idValue}");
            //});

            //var routes = routeBuilder.Build();
            //app.UseRouter(routes);

            #endregion

            #region RouteConstraintApplied
            // Uncommment this to learn Route constraints when applied. Comment other regions

            //var routeBuilder = new RouteBuilder(app);

            //// Id parameter should be Integer now
            //// Route constraints fails, response returned is 404
            //routeBuilder.MapGet("employee/{id:int}", context =>
            //{
            //    var idValue = context.GetRouteValue("id");
            //    return context.Response.WriteAsync($"The number is - {idValue}");
            //});

            //// Name parameter should be length 8
            //// Route constraints fails, response returned is 404
            //routeBuilder.MapGet("product/{name:length(8)}", context =>
            //{
            //    var nameValue = context.GetRouteValue("id");
            //    return context.Response.WriteAsync($"The Name is - {nameValue}");
            //});

            //var routes = routeBuilder.Build();
            //app.UseRouter(routes);

            #endregion

            #region CustomRouteConstraints
            // Uncomment when learning custom route constraints

            //var routeBuilder = new RouteBuilder(app);

            //// domName parameter should have @packt.com
            //routeBuilder.MapGet("api/employee/{domName:domain}", context =>
            //{
            //    var domName = context.GetRouteValue("domName");
            //    return context.Response.WriteAsync($"Domain Name Constraint Passed - {domName}");
            //});

            //var routes = routeBuilder.Build();
            //app.UseRouter(routes);


            #endregion
        }
    }
}
