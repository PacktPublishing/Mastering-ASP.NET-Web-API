
using Puhoi.Models.Validators;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Puhoi.Models.Models
{
    public class StoreModel : BaseModel, IValidatableObject
    {
        public int NumberOfProducts { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            StoreModelValidator validator = new StoreModelValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(this);
            return result.Errors.Select(item => 
            new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
