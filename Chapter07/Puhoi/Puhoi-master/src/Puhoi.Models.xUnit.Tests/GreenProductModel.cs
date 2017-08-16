using System;
using Puhoi.Models.Core.Models;

namespace Puhoi.Models.xUnit.Tests
{
    public static class GreenProductModel
    {
        public static ProductModel Model()
        {
            return new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = "Gold Strong Blue Cheese",
                Description = "Award winning Blue Cheese from Puhoi Valley",
                Count = 10000,
                Size = "Small"
            };
        }
    }
}
