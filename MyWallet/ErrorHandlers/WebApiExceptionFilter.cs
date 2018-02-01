using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace MyWallet.ErrorHandlers
{
    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger<WebApiExceptionFilter> _Logger;

        public WebApiExceptionFilter(ILogger<WebApiExceptionFilter> logger)
        {
            _Logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            WebAPIError apiError = null;
            if (context.Exception is WebApiException)
            {
                // Here we handle known MyWallet API errors
                var ex = context.Exception as WebApiException;
                context.Exception = null;
                apiError = new WebAPIError(ex.Message);                

                context.HttpContext.Response.StatusCode = (int)ex.StatusCode;
                _Logger.LogWarning($"MyWallet API thrown error: {ex.Message}", ex);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new WebAPIError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;                
            }
            else
            {
                // Unhandled errors
#if !DEBUG
                        var msg = "An unhandled error occurred.";
                        string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new WebAPIError(msg);
                apiError.detail = stack;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // handle logging here
                _Logger.LogError(new EventId(0), context.Exception, msg);
            }

            // always return a JSON result
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }        
    }
}