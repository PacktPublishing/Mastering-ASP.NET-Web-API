using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomRouteConstraints
{
    public class DomainConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var isMatch = false;
            if (values["domName"].ToString().Contains("@packt.com"))
            {
                isMatch = true;
            }
            return isMatch;
        }
    }

}
