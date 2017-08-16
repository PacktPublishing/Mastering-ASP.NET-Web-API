using System.Collections.Generic;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Puhoi.Models.Core.Models;

namespace Puhoi.Models.xUnit.Tests
{
        public class ProductModelValidatorShould
        {
            [Fact]
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

            [Fact]
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

            [Fact]
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

            [Fact]
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

