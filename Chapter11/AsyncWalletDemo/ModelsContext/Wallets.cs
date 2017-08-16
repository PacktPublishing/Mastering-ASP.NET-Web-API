using System;

namespace AsyncWalletDemo.ModelsContext
{
    //public class Wallets
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class DailyExpense
    {        
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
