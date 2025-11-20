using Gaming_Shop.ShopManagement.DTOs;
using Gaming_Shop.ShopManagement.Interfaces;
using Gaming_Shop.ShopManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaming_Shop.ShopManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryServices.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryServices.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddDTO dto)
        {
            var category = await _categoryServices.AddCategoryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = category.CategoryID }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryAddDTO dto)
        {
            var updated = await _categoryServices.UpdateCategoryAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryServices.DeleteCategoryAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
