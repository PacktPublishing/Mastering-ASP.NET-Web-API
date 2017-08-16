using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyWallet.ModelsContexts;
//using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.Extensions.Logging;

namespace MyWallet.Controllers
{
    [Route("api/[controller]")]
    public class WalletController : Controller
    {
        private readonly ExpenseContext _context;
        //UnComment this for non Serilog Sink to Database
        private readonly ILogger<WalletController> _logger;

        //private readonly Serilog.ILogger _logger;

        //UnComment this for non Serilog Sink to Database
        public WalletController(ExpenseContext context, ILogger<WalletController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Comment when using non Serilog sink to database
        //public WalletController(ExpenseContext context, Serilog.ILogger logger)
        //{
        //    _context = context;
        //    _logger = logger;
        //}

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
                _logger.LogInformation($"Daily Expense for {id} does not exists!!");
                //UnComment this for Serilog Sink to Database
                //_logger.Information($"Daily Expense for {id} does not exists!!");
                return NotFound();                
            }
            return Ok(spentItem);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]DailyExpense value)
        {
            if (value == null)
            {
                _logger.LogError("Request Object was NULL");
                //UnComment this for non Serilog Sink to Database
                //_logger.Error("Request Object was NULL");
                return BadRequest();
            }
            CheckMovieBudget(value);

            if (!ModelState.IsValid)
            {                
                return BadRequest(ModelState);
            }
            var newSpentItem = _context.DailyExpenses.Add(value);
            Save();
            return Ok(newSpentItem.Entity.Id);
        }

        private void CheckMovieBudget(DailyExpense expense)
        {            
            if (expense.SpentOn == ExpenseType.Movies && expense.Amount > 300)
            {
                //UnComment this for non Serilog Sink to Database
                //_logger.Error("Errorneous input for Movie");
                _logger.LogError("Errorneous input for Movie");
                ModelState.AddModelError("MovieLimit", $"Max movie expense limit is 300 and your amount is {expense.Amount}");
            }            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]DailyExpense value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var spentItem = _context.DailyExpenses.Find(id);
            if (spentItem == null)
            {
                //UnComment this for non Serilog Sink to Database
                //_logger.Information($"Daily Expense for {id} does not exists!!");
                _logger.LogInformation($"Daily Expense for {id} does not exists!!");
                return NotFound();
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
                //UnComment this for non Serilog Sink to Database
                //_logger.Information($"Daily Expense for {id} does not exists!!");
                _logger.LogInformation($"Daily Expense for {id} does not exists!!");
                return NotFound();
            }
            _context.Remove(spentItem);
            Save();
            return NoContent();
        }        

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}