using System.ComponentModel.DataAnnotations;

namespace AdvWorks.WebAPI.Models
{
    public class ProductDetailsDTO
    {   

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        public string ProductNumber { get; set; }

        [StringLength(15)]
        public string Color { get; set; }        

        public short ReorderPoint { get; set; }
        
        public decimal StandardCost { get; set; }
        
        public decimal ListPrice { get; set; }

        public decimal? Weight { get; set; }

        public int DaysToManufacture { get; set; }
    }
}
