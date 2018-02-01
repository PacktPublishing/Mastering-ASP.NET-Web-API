using Microsoft.EntityFrameworkCore;

namespace MyWallet.ModelsContexts
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options)
            :base(options)
        {

        }
        public DbSet<DailyExpense> DailyExpenses { get; set; }
    }
}
