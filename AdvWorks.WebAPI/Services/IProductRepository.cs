using AdvWorksAPI.DataAccess;
using System.Collections.Generic;

namespace AdvWorks.WebAPI.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int productId);
        bool ProductExists(int productId);
        void AddProduct(Product proddetails);
        void DeleteProduct(Product proddetails);
        bool Save();
    }
}