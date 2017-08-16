using Microsoft.AspNetCore.Mvc;

namespace filters_demo.Controllers
{
    [Route("api/[controller]")]
    
    public class EmployeeController : Controller
    {
        // GET api/values/5        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            if (id == 0)
            {
                throw new ZeroValueException("Employee Id Cannot be Zero");
            }
            return "value is " + id;
        }
    }
}