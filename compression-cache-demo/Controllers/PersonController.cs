using GenFu;
using Microsoft.AspNetCore.Mvc;
using System;

namespace compression_cache_demo.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        // GET: api/values
        [HttpGet]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public IActionResult Get()
        {       
            //Generate demo list using GenFu package
            //Returns 200 counts of Person object
            var personlist = A.ListOf<Person>(200);
            return Ok(personlist);
        }        
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public int Age { get; set; }
        public DateTime DoB { get; set; }
    }
}
