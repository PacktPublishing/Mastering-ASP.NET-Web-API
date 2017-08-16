using FluentValidation;
using Puhoi.Models.Models;

namespace Puhoi.Models.Validators
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
