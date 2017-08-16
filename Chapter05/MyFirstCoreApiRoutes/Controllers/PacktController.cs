using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyFirstCoreApiRoutes.Controllers
{    
    // Attribute Routing Example
    [Route("api/[controller]")]
    public class PacktController : Controller
    {
        // Get: api/packt/show
        [HttpGet("Show")]
        public string Show()
        {
            return "I am Packt Show !!";
        }

        // GET api/packt
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Packt 1", "Packt 2" };
        }

        // GET: /api/packt/13
        [HttpGet("{id:int}", Name = "GetPacktById", Order = 0)]
        public string Get(int id)
        {
            return "Response from GetPacktById" + id;
        }

        // POST: /api/packt
        [HttpPost]
        public IActionResult Post()
        {
            return Content("Created Post !!");
        }

        // POST: /api/packt/packtpost
        [HttpPost("packtpost")]
        public IActionResult Post([FromBody]string chapterName)
        {
            return Content("You invoked packt post");
        }

        // PUT api/packt/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/packt/15
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
