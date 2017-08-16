using Microsoft.VisualStudio.TestTools.UnitTesting;
using Puhoi.Models.Models;
using FluentAssertions;
using System;

namespace Puhoi.Models.Tests
{
    [TestClass]
    public class ProductModelShould
    {
        [TestMethod]
        public void BeABaseModel()
        {
            // arrange 
            ProductModel model = new ProductModel();

            // act 
            BaseModel baseModel = (BaseModel) model;

            // assert
            baseModel.Should().NotBeNull();
        }

        [TestMethod]
        public void HaveADescriptionProperty()
        {
            //arrange 
            const string testDescription = "Test Description";

            // act
            ProductModel model = new ProductModel { Description = testDescription };

            // assert 
            model.Description.Should().Be(testDescription);
        }

        [TestMethod]
        public void HaveANameProperty()
        {
            //arrange 
            const string testName = "Test Name";

            // act
            ProductModel model = new ProductModel { Name = testName};

            // assert 
            model.Name.Should().Be(testName);
        }

        [TestMethod]
        public void HaveASizeProperty()
        {
            //arrange 
            const string testSize = "Large";

            // act
            ProductModel model = new ProductModel { Size = testSize };

            // assert 
            model.Size.Should().Be(testSize);
        }

        [TestMethod]
        public void HaveACountProperty()
        {
            //arrange 
            const int testCount = 100;

            // act
            ProductModel model = new ProductModel { Count = testCount };

            // assert 
            model.Count.Should().Be(testCount);
        }

    }
}

