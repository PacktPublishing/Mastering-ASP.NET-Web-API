using FluentValidation;
using Puhoi.Models.Core.Models;
using Puhoi.Models.Models;
using Puhoi.Models.Validators;

namespace Puhoi.Models.Core.Validators
{
    public class ProductModelValidator : BaseValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Size).NotEmpty();
        }
    }
}
