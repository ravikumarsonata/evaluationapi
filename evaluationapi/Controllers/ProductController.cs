using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eValuate.WebApi.Interfaces;
using eValuate.WebApi.Models;

namespace eValuate.WebApi.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductRepositoryAsync _productRepository;

        public ProductController(IProductRepositoryAsync productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> GellAll()
        {
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            //return Ok(product);
            return new JsonResult(product);
        }

        [HttpPost("addproduct")]
        [Consumes("application/json")]
        public async Task<ActionResult> AddProduct(Product entity)
        {
            await _productRepository.AddProduct(entity);
            return Ok(entity);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(Product entity, int id)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _productRepository.UpdateProduct(entity, id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                throw;
            }

            
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            

            try
            {
                var product = _productRepository.GetById(id);
                if (product == null)
                {
                    //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _productRepository.RemoveProduct(id);
                //return NoContent();
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
