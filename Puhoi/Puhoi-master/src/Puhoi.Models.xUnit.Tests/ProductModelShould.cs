using FluentAssertions;
using Puhoi.Models.Core.Models;
using Xunit;


namespace Puhoi.Models.xUnit.Tests
{
    public class ProductModelShould
    {

        [Fact]
        public void BeABaseModel()
        {
            // arrange 
            ProductModel model = new ProductModel();

            // act 
            BaseModel baseModel = (BaseModel)model;

            // assert
            baseModel.Should().NotBeNull();
        }



        [Fact]
        public void HaveADescriptionProperty()
        {
            //arrange 
            const string testDescription = "Test Description";

            // act
            ProductModel model = new ProductModel { Description = testDescription };

            // assert 
            model.Description.Should().Be(testDescription);
        }

        [Fact]
        public void HaveANameProperty()
        {
            //arrange 
            const string testName = "Test Name";

            // act
            ProductModel model = new ProductModel { Name = testName };

            // assert 
            model.Name.Should().Be(testName);
        }

        [Fact]
        public void HaveASizeProperty()
        {
            //arrange 
            const string testSize = "Large";

            // act
            ProductModel model = new ProductModel { Size = testSize };

            // assert 
            model.Size.Should().Be(testSize);
        }

        [Fact]
        public void HaveACountProperty()
        {
            //arrange 
            const int testCount = 100;

            // act
            ProductModel model = new ProductModel { Count = testCount };

            // assert 
            model.Count.Should().Be(testCount);
        }

        //[Fact]
        //public void PassingTest()
        //{
        //    Assert.Equal(4, Add(2, 2));
        //}

        //[Fact]
        //public void FailingTest()
        //{
        //    Assert.Equal(5, Add(2, 2));
        //}

        //int Add(int x, int y)
        //{
        //    return x + y;
        //}
    }
}
