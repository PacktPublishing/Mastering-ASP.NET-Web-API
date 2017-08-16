using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Puhoi.Models.Core.Validators;

namespace Puhoi.Models.Core.Models
{
    public class ProductModel : BaseModel, IValidatableObject
    {
        public string Description { get; set;}
        public string Name { get; set; }
        public int Count { get; set; }
        public string Size { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ProductModelValidator validator = new ProductModelValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(this);
            return result.Errors.Select(item =>
            new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
