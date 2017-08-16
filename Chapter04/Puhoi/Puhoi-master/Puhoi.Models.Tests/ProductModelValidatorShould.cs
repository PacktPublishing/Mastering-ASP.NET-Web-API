using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Puhoi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Puhoi.Models.Tests
{
    [TestClass]
    public class ProductModelValidatorShould
    {
        [TestMethod]
        public void ReturnNoValidationErrorsForAGreenModel()
        {
            // arrange 
            ProductModel model = GreenProductModel.Model();

            // act 
            IEnumerable<ValidationResult> validationResult =
                model.Validate(new ValidationContext(this));

            // assert 
            validationResult.Should().HaveCount(0);
        }

        [TestMethod]
        public void ReturnInValidWhenNameIsEmpty()
        {
            // arrange 
            ProductModel model = GreenProductModel.Model();
            model.Name = string.Empty;

            //act 
            IEnumerable<ValidationResult> validationResult =
               model.Validate(new ValidationContext(this));

            //assert 
            validationResult.Should().HaveCount(1);
        }

        [TestMethod]
        public void ReturnInValidWhenDescriptionIsEmpty()
        {
            // arrange 
            ProductModel model = GreenProductModel.Model();
            model.Description = string.Empty;

            //act 
            IEnumerable<ValidationResult> validationResult =
               model.Validate(new ValidationContext(this));

            //assert 
            validationResult.Should().HaveCount(1);
        }

        [TestMethod]
        public void ReturnInValidWhenSizeIsEmpty()
        {
            // arrange 
            ProductModel model = GreenProductModel.Model();
            model.Size = string.Empty;

            //act 
            IEnumerable<ValidationResult> validationResult =
               model.Validate(new ValidationContext(this));

            //assert 
            validationResult.Should().HaveCount(1);
        }
    }
}
