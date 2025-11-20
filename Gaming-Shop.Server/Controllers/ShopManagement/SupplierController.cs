using Gaming_Shop.ShopManagement.DTOs;
using Gaming_Shop.ShopManagement.Interfaces;
using Gaming_Shop.ShopManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaming_Shop.ShopManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierServices _supplierServices;

        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _supplierServices.GetAllAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _supplierServices.GetSupplierByIdAsync(id);
            if (supplier == null) return NotFound();
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierAddDTO dto)
        {
            var supplier = await _supplierServices.AddSupplierAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = supplier.SupplierID }, supplier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SupplierAddDTO dto)
        {
            var updated = await _supplierServices.UpdateSupplierAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _supplierServices.DeleteSupplierAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
