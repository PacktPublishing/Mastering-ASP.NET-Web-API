using AsyncWalletDemo.ModelsContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MyWalletAsyncDemo.Controllers
{
    [Route("api/[controller]")]
    public class WalletController : Controller
    {
        private readonly WalletContext _context;       
        private readonly ILogger<WalletController> _logger;        

        
        public WalletController(WalletContext context, ILogger<WalletController> logger)
        {
            _context = context;
            _logger = logger;
        }        

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var spentList = await _context.Wallet.ToListAsync();            
            return Ok(spentList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {            
            var spentItem = await _context.Wallet.FindAsync(id);
            if (spentItem == null)
            {
                _logger.LogInformation($"Daily Expense for {id} does not exists!!");                
                return NotFound();
            }
            return Ok(spentItem);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DailyExpense value)
        {
            if (value == null)
            {
                _logger.LogError("Request Object was NULL");                
                return BadRequest();
            }
            CheckMovieBudget(value);

            if (!ModelState.IsValid)
            {                
                return BadRequest(ModelState);
            }
            var newSpentItem = _context.Wallet.AddAsync(value);
            await SaveAsync();
            return Ok(newSpentItem.Result.Entity.Id);
        }

        private void CheckMovieBudget(DailyExpense expense)
        {            
            if (expense.SpentOn == ExpenseType.Movies && expense.Amount > 300)
            {                
                _logger.LogError("Errorneous input for Movie");
                ModelState.AddModelError("MovieLimit", $"Max movie expense limit is 300 and your amount is {expense.Amount}");
            }            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DailyExpense value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var spentItem = await _context.Wallet.FindAsync(id);
            if (spentItem == null)
            {                
                _logger.LogInformation($"Daily Expense for {id} does not exists!!");
                return NotFound();
            }
            //Assign Values 
            // Use AutoMapper if more properties
            spentItem.Amount = value.Amount;
            spentItem.Description = value.Description;
            spentItem.SpentOn = value.SpentOn;

            _context.Update(spentItem);
            await SaveAsync();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var spentItem = _context.Wallet.FindAsync(id);
            if (spentItem == null)
            {                
                _logger.LogInformation($"Daily Expense for {id} does not exists!!");
                return NotFound();
            }
            _context.Remove(spentItem);
            await SaveAsync();
            return NoContent();
        }        

        public async Task<bool> SaveAsync()
        {
            var isSaveSuccess = true;
            int retrnRows = await _context.SaveChangesAsync();

            if (retrnRows == 0)
            {
                isSaveSuccess = false;
            }

            return isSaveSuccess;
        }
    }
}