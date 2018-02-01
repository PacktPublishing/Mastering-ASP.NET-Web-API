using System;
using System.Net;

namespace MyWallet.ErrorHandlers
{
    public class WebApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }        

        public WebApiException(string message,
                            HttpStatusCode statusCode = HttpStatusCode.InternalServerError) :
        base(message)
    {
            StatusCode = statusCode;
        }
        public WebApiException(Exception ex, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(ex.Message)
    {
            StatusCode = statusCode;
        }
    }
}
