using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace middleware_basics
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region UseMethod
            //Uncomment this to learn Use Method

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware 1<br/>");
            //    // Calls next middleware in pipeline
            //    await next.Invoke();
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware 2<br/>");
            //    // Calls next middleware in pipeline
            //    await next.Invoke();
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware 3<br/>");
            //    // No invoke method to pass next middleware                
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware 4<br/>");
            //    // Calls next middleware in pipeline
            //    await next.Invoke();
            //});

            #endregion

            #region MapMethod
            //Uncomment when learning Map method

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware 1 without Map <br/>");
            //    await next();
            //});

            //app.Map(new PathString("/packtchap2"), branch =>
            //{
            //    branch.Run(async context => { await context.Response.WriteAsync("packtchap2 - Middleware 1<br/>"); });
            //});

            //app.Map(new PathString("/packtchap5"), branch =>
            //{
            //    branch.Run(async context => { await context.Response.WriteAsync("packtchap5 - Middleware 2<br/>"); });
            //});

            //app.Run(async context => { await context.Response.WriteAsync("Middleware 2 without Map<br/>"); });
            #endregion

            #region MapWhenMethod
            //Uncomment code to learn MapWhen

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware 1 - Map When <br/>");
            //    await next();
            //});

            //app.MapWhen(context => context.Request.Query.ContainsKey("packtquery"), (appbuilder) =>
            //{
            //    appbuilder.Run(async (context) =>
            //    {
            //        await context.Response.WriteAsync("In side Map When <br/>");
            //    });
            //});
            #endregion

            #region MiddlewareOrder
            //Comment this region when working with other concepts

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 1<br/>");
                // Calls next middleware in pipeline
                await next.Invoke();
                await context.Response.WriteAsync("Middleware 1 while return<br/>");
            });

            app.Map(new PathString("/packtchap2"), branch =>
            {
                branch.Run(async context => { await context.Response.WriteAsync("packtchap2 - Middleware 1<br/>"); });
            });

            app.Run(async context => {
                await context.Response.WriteAsync("Run() - a Terminal Middleware <br/>");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 2<br/>");
                // Calls next middleware in pipeline
                await next.Invoke();

            });
            #endregion
        }
    }
}
