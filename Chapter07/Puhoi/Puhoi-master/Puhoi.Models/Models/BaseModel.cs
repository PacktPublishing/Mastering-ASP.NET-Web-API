using FluentValidation;
using Puhoi.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Puhoi.Models.Models
{
    public  class BaseModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        //{
        //    return GetValidator().Validate(this).ToValidationResult();
        //}

        //protected abstract IValidator GetValidator();
    }
}
