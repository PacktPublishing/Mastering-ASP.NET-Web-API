using System.Collections.Generic;
using System.Linq;
using AdvWorksAPI.DataAccess;

namespace AdvWorks.WebAPI.Services
{
    public class ProductRepository : IProductRepository
    {
        private AdvWorksContext _context;
        public ProductRepository(AdvWorksContext context)
        {
            _context = context;
        }

        public void AddProduct(Product proddetails)
        {
            _context.Products.Add(proddetails);
        }

        public void DeleteProduct(Product proddetails)
        {
            _context.Products.Remove(proddetails);
        }

        public Product GetProduct(int productId)
        {
            return _context.Products.Where(c => c.ProductID == productId).FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.Take(10).ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(c => c.ProductID == productId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}