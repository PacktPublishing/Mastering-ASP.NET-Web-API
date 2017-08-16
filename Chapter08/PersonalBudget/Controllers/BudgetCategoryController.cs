using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PersonalBudget.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace PersonalBudget.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BudgetCategoryController : Controller
    {
        private readonly PersonalBudgetContext _context;
        private readonly IDataProtector protector;

        public BudgetCategoryController(PersonalBudgetContext context,
            IDataProtectionProvider protectionprovider, StringConstants strconsts)
        {
            this.protector = protectionprovider.CreateProtector(strconsts.IdQryStr);
            _context = context;
        }


        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var categoryList = _context.BudgetCategories.ToList();
            var results = Mapper.Map<IEnumerable<BudgetCategoryDTO>>(categoryList);            
            return Ok(results);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var decryptId = int.Parse(protector.Unprotect(id));
            var item = _context.BudgetCategories.Find(decryptId);
            if (item == null)
            {                
                return NotFound();
            }
            var results = Mapper.Map<BudgetCategoryDTO>(item);
            return Ok(results);
        }

        // POST api/values
        [HttpPost]
        [Authorize(Policy = "SuperUsers")]
        public IActionResult Post([FromBody]BudgetCategoryDTO value)
        {
            if (value == null)
            {                
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mappeditem = Mapper.Map<BudgetCategory>(value);
            var newItem = _context.BudgetCategories.Add(mappeditem);
            Save();            
            var dtomapped = Mapper.Map<BudgetCategoryDTO>(mappeditem);

            return Ok(dtomapped);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]BudgetCategoryDTO value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var decryptId = int.Parse(protector.Unprotect(id));
            var item = _context.BudgetCategories.Find(decryptId);
            if (item == null)
            {                
                return NotFound();
            }
            
            Mapper.Map(value, item);

            _context.Update(item);
            Save();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var decryptId = int.Parse(protector.Unprotect(id));
            var item = _context.BudgetCategories.Find(decryptId);
            if (item == null)
            {                
                return NotFound();
            }
            _context.Remove(item);
            Save();
            return Ok();
        }

        private bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
