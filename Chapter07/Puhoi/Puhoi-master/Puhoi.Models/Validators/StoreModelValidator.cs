using FluentValidation;
using Puhoi.Models.Models;

namespace Puhoi.Models.Validators
{
    public class StoreModelValidator : BaseValidator<StoreModel>
    {
        public StoreModelValidator()
        {
            RuleFor(s => s.Name).NotNull().Length(1,100);
            RuleFor(s => s.Description).NotNull().Length(1, 100);
            RuleFor(s => s.DisplayName).NotNull().Length(1, 100);
            RuleFor(s => s.NumberOfProducts).NotNull().InclusiveBetween(1,50);
        }
    }
}
