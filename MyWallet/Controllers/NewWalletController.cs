using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyWallet.ModelsContexts;
using Microsoft.Extensions.Logging;
using MyWallet.ErrorHandlers;
using System;
using System.Net;

namespace MyWallet.Controllers
{
    [ServiceFilter(typeof(WebApiExceptionFilter))]
    [Route("api/[controller]")]
    public class NewWalletController : Controller
    {
        private readonly ExpenseContext _context;
        private readonly ILogger<WalletController> _logger;

        public NewWalletController(ExpenseContext context, ILogger<WalletController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var spentList = _context.DailyExpenses.ToList();
            return Ok(spentList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var spentItem = _context.DailyExpenses.Find(id);
            if (spentItem == null)
            {                
                throw new WebApiException($"Daily Expense for {id} does not exists!!", HttpStatusCode.NotFound);
            }
            return Ok(spentItem);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]DailyExpense value)
        {
            if (value == null)
            {   
                throw new WebApiException("Request Object was NULL", HttpStatusCode.BadRequest);                
            }
            CheckMovieBudget(value);

            if (!ModelState.IsValid)
            {
                throw new WebApiException($"Max movie expense limit is 300 and your amount is {value.Amount}", HttpStatusCode.BadRequest);                
            }
            var newSpentItem = _context.DailyExpenses.Add(value);
            Save();
            return Ok(newSpentItem.Entity.Id);
        }

        private void CheckMovieBudget(DailyExpense expense)
        {
            if (expense.SpentOn == ExpenseType.Movies && expense.Amount > 300)
            {                
                ModelState.AddModelError("MovieLimit", $"Max movie expense limit is 300 and your amount is {expense.Amount}");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]DailyExpense value)
        {
            if (value == null)
            {
                throw new WebApiException("Request Object was NULL", HttpStatusCode.BadRequest);
            }

            var spentItem = _context.DailyExpenses.Find(id);
            if (spentItem == null)
            {
                throw new WebApiException($"Daily Expense for {id} does not exists!!", HttpStatusCode.NotFound);
            }
            //Assign Values 
            // Use AutoMapper if more properties
            spentItem.Amount = value.Amount;
            spentItem.Description = value.Description;
            spentItem.SpentOn = value.SpentOn;

            _context.Update(spentItem);
            Save();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var spentItem = _context.DailyExpenses.Find(id);
            if (spentItem == null)
            {
                return NotFound();
            }
            _context.Remove(spentItem);
            Save();
            return NoContent();
        }

        
        [Route("ThrowExceptionMethod")]
        public IActionResult ThrowExceptionMethod()
        {
            try
            {
                string emailId = null;
                if (emailId.Length > 0)
                {
                    return Ok();
                }
                throw new WebApiException("Email is empty", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}