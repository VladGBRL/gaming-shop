using Gaming_Shop.ShopManagement.Data;
using Gaming_Shop.ShopManagement.DTOs;
using Gaming_Shop.ShopManagement.Entities;
using Gaming_Shop.ShopManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gaming_Shop.ShopManagement.Services
{
    public class SupplierServices : ISupplierServices
    {
        private readonly ShopManagementDbContext _context;

        public SupplierServices(ShopManagementDbContext context)
        {
            _context = context;
        }

        public async Task<SupplierGetDTO> AddSupplierAsync(SupplierAddDTO dto)
        {
            var supplier = new Supplier
            {
                SupplierName = dto.SupplierName,
                Contact = dto.Contact,
                Address = dto.Address
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return new SupplierGetDTO
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.SupplierName,
                Contact = supplier.Contact,
                Address = supplier.Address
            };
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SupplierGetDTO?> UpdateSupplierAsync(int id, SupplierAddDTO dto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return null;

            supplier.SupplierName = dto.SupplierName;
            supplier.Contact = dto.Contact;
            supplier.Address = dto.Address;

            await _context.SaveChangesAsync();

            return new SupplierGetDTO
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.SupplierName,
                Contact = supplier.Contact,
                Address = supplier.Address
            };
        }

        public async Task<SupplierGetDTO?> GetSupplierByIdAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return null;

            return new SupplierGetDTO
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.SupplierName,
                Contact = supplier.Contact,
                Address = supplier.Address
            };
        }
    }
}
