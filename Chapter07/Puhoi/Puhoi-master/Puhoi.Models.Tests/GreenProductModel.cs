using Puhoi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puhoi.Models.Tests
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
