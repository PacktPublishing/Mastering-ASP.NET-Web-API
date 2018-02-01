using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace filters_demo
{
    //Action Filter example
    public class CheckPubliserNameAttribute : TypeFilterAttribute
    {
        public CheckPubliserNameAttribute() : base(typeof
          (CheckPubliserName))
        {
        }
    }
    public class CheckPubliserName : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // You can work with Action Result
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var headerValue = context.HttpContext.Request.Headers["publiser-name"];            
            if (!headerValue.Equals("Packt"))
            {
                context.Result = new BadRequestObjectResult("Invalid Header");               
            }
        }
    }
}
