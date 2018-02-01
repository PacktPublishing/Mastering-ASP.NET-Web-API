using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace filters_demo
{
    // Exception Filter example
    public class PacktExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;

            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ZeroValueException))
            {
                message = context.Exception.Message;
                status = HttpStatusCode.InternalServerError;
            }
            context.Result = new JsonResult(new
            {
                Code = (int)status,
                Message = message
            });

        }
    }
}