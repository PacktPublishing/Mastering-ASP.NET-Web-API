using Microsoft.EntityFrameworkCore;

namespace AsyncWalletDemo.ModelsContext
{
    public class WalletContext : DbContext
    {
        public WalletContext(DbContextOptions<WalletContext> options)
            : base(options)
        {

        }
        public DbSet<DailyExpense> Wallet { get; set; }
    }
}
