using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PersonalBudget.Models;

namespace PersonalBudget
{
    public class IdProtectorConverter : ITypeConverter<BudgetCategory, BudgetCategoryDTO>
    {
        private readonly IDataProtector protector;       
        
        public IdProtectorConverter(IDataProtectionProvider protectionprovider, StringConstants strconsts)
        {
            this.protector = protectionprovider.CreateProtector(strconsts.IdQryStr);
        }
        public BudgetCategoryDTO Convert(BudgetCategory source, BudgetCategoryDTO destination, ResolutionContext context)
        {
            return new BudgetCategoryDTO
            {
                Name = source.Name,
                Amount = source.Amount,
                EncryptId = this.protector.Protect(source.Id.ToString())
            };
        }
    }
}