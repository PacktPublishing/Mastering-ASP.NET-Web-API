using Microsoft.EntityFrameworkCore;

namespace PersonalBudget.Models
{
    public class PersonalBudgetContext : DbContext
    {
        public PersonalBudgetContext(DbContextOptions<PersonalBudgetContext> options)
            :base(options)
        {

        }
        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
