using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace custom_middleware
{
    // Custom ASP.NET Core Middleware
    public class PacktHeaderValidator
    {   
        private RequestDelegate _next;

        public PacktHeaderValidator(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //If matches pipeline processing continues
            if (context.Request.Headers["packt-book"].Equals("Mastering Web API"))
            {   
                await _next.Invoke(context);
            }
            else
            {
                // Pipeline processing ends
                context.Response.StatusCode = 400; //Bad request
            }            
        }
    }

    #region ExtensionMethod
    //Extension Method to give friendly name for custom middleware
    public static class PacktHeaderValidatorExtension
    {
        public static IApplicationBuilder UsePacktHeaderValidator(this IApplicationBuilder app)
        {
            app.UseMiddleware<PacktHeaderValidator>();
            return app;
        }
    }
    #endregion
}
