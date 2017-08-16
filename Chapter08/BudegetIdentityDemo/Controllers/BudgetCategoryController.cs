using BudegetIdentityDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace BudegetIdentityDemo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class BudgetCategoryController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(GetItems());
        }
        
        private IList<BudgetCategory> GetItems()
        {
            var listitems = new List<BudgetCategory>
            {
                new BudgetCategory() { Id = 10, Name = "Food", Amount = 200 }
            };
            return listitems;
        }
    }
}
