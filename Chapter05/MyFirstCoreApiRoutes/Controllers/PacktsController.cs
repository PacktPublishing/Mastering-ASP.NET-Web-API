using Microsoft.AspNetCore.Mvc;

namespace MyFirstCoreApiRoutes.Controllers
{
    // Example of Multiple Routes
    [Route("Stocks")]
    [Route("[controller]")]
    public class PacktsController : Controller
    {
        [HttpGet("Check")]
        [HttpGet("Available")]
        public string GetStock()
        {
            return "Its multiple route";
        }
    }

}
