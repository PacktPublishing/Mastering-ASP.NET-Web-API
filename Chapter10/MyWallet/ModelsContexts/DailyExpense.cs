using System;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.ModelsContexts
{

    public class DailyExpense
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public DateTime SpentDate { get; set; }        
        public ExpenseType SpentOn { get; set; }
    }

    public enum ExpenseType
    {
        Food,
        Movies,
        Petrol,
        Clothing,
        Others
    }
}