using Microsoft.AspNetCore.Mvc;

namespace CustomRouteConstraints.Controllers
{
    //Custom route constraint in Attribute routes
    public class EmployeeController : Controller
    {                
        [HttpGet("api/employee/{domName:domain}")]
        public string Get(string domName)
        {
            return ($"Domain Name Constraint Passed - {domName}");
        }
    }
}
