using Gaming_Shop.ShopManagement.DTOs;
using Gaming_Shop.ShopManagement.Interfaces;
using Gaming_Shop.ShopManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaming_Shop.ShopManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productServices.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productServices.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductAddDTO dto)
        {
            var product = await _productServices.AddProductAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.ProductID }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductAddDTO dto)
        {
            var updated = await _productServices.UpdateProductsAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productServices.DeleteProductsAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            var results = await _productServices.SearchAsync(q);
            return Ok(results);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> Filter(
    [FromQuery] decimal? minPrice,
    [FromQuery] decimal? maxPrice,
    [FromQuery] string? categoryName,
    [FromQuery] string? supplierName)
        {
            var results = await _productServices.FilterAsync(minPrice, maxPrice, categoryName, supplierName);
            return Ok(results);
        }


    }
}
