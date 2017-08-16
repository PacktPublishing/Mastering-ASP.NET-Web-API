using FluentValidation;
using Puhoi.Models.Models;
using Puhoi.Models.Validators;

namespace Puhoi.Models.Core.Validators
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
