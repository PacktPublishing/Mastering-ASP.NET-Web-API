


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PuhoiAPI.Attributes
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if( !context.ModelState.IsValid )
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            base.OnActionExecuting(context);
        }
    }
}
