using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace httpmodule_to_middleware
{
    public class AspxMiddleware
    {
        private RequestDelegate _next;
        public AspxMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().EndsWith(".aspx"))
            {
                await context.Response.WriteAsync("<h2><font color=red>" +
                    "AspxMiddleware: Beginning of Request" +
                    "</font></h2><hr>");
            }

            await _next.Invoke(context);

            if (context.Request.Path.ToString().EndsWith(".aspx"))
            {
                await context.Response.WriteAsync("<hr><h2><font color=red>" +
                "AspxMiddleware: End of Request</font></h2>");
            }
            
        }
    }

    #region ExtensionMethod
    //Extension Method to give friendly name for custom middleware
    public static class AspxMiddlewareExtension
    {
        public static IApplicationBuilder UseAspxMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AspxMiddleware>();
            return app;
        }
    }
    #endregion

}
