using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace filters_demo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        // GET: api/values
        [HttpGet]
        [AllowAnonymous]
        public string Get()
        {
            return "Year is " + DateTime.Now.Year.ToString();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {   
            return "value is " + id;
        }
    }
}
