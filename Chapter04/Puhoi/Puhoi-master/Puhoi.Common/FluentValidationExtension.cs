using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Puhoi.Common
{
    public static class FluentValidationExtension
    {
        public static IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> ToValidationResult(
           this FluentValidation.Results.ValidationResult validationResult)
        {
            IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> results =
                validationResult.Errors.Select(
                    item => new ValidationResult(item.ErrorMessage, new List<String> { item.PropertyName }));
            return results;
        }
    }
}
