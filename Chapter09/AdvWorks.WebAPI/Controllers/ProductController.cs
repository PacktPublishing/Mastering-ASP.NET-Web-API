using AdvWorks.WebAPI.Models;
using AdvWorks.WebAPI.Services;
using AdvWorksAPI.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace AdvWorks.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
         
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var prodlist = _productRepository.GetProducts();
            var results = Mapper.Map<IEnumerable<ProductDTO>>(prodlist);
            return Ok(results);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!_productRepository.ProductExists(id))
            {
                return NotFound();
            }
            var prod = _productRepository.GetProduct(id);
            var results = Mapper.Map<ProductDTO>(prod);
            return Ok(results);
        }
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ProductDetailsDTO prod)
        {
            if (prod == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mappedProduct = Mapper.Map<Product>(prod);
            //This is make DTO simple for this example
            //Adding values for properties not present in ProductDetailsDTO
            //It errors out if NOT NUll values are not supplied
            mappedProduct.MakeFlag = true;
            mappedProduct.FinishedGoodsFlag = true;
            mappedProduct.SafetyStockLevel = 200;
            mappedProduct.SellStartDate = DateTime.Now;
            mappedProduct.rowguid = Guid.NewGuid();
            _productRepository.AddProduct(mappedProduct);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            else
            {
                return StatusCode(201, "Created Successfully");
            }
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProductDetailsDTO prod)
        {
            if (prod == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_productRepository.ProductExists(id))
            {
                return NotFound();
            }
            var saveprodrecord = _productRepository.GetProduct(id);
            Mapper.Map(prod, saveprodrecord);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            else
            {
                return StatusCode(202, "Updated Successfully");
            }
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_productRepository.ProductExists(id))
            {
                return NotFound();
            }
            var prodToDelete = _productRepository.GetProduct(id);
            _productRepository.DeleteProduct(prodToDelete);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
