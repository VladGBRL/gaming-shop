using Gaming_Shop.ShopManagement.Data;
using Gaming_Shop.ShopManagement.DTOs;
using Gaming_Shop.ShopManagement.Entities;
using Gaming_Shop.ShopManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.ShopManagement.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ShopManagementDbContext _context;

        public ProductServices(ShopManagementDbContext context)
        {
            _context = context;
        }
        public async Task<ProductGetDTO> AddProductAsync(ProductAddDTO productDto)
        {
            var product = new Product { Name = productDto.Name, Description = productDto.Description, Price = productDto.Price, Stock = productDto.Stock, SupplierID = productDto.SupplierID, CategoryID = productDto.CategoryID };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductGetDTO { ProductID = product.ProductID, Name = product.Name, Description = product.Description, Price = product.Price, Stock = product.Stock, SupplierID = product.SupplierID, CategoryID = product.CategoryID };
        }
        public async Task<bool> DeleteProductsAsync(int id_Product)
        {
            var product = await _context.Products.FindAsync(id_Product);
            if (product == null) { return false; }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<ProductGetDTO> UpdateProductsAsync(int id_Product, ProductAddDTO productDto)
        {
            var product = await _context.Products.FindAsync(id_Product);
            if (product == null)
            {
                return null;
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;
            product.SupplierID = productDto.SupplierID;
            product.CategoryID = productDto.CategoryID;

            await _context.SaveChangesAsync();

            return new ProductGetDTO
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                SupplierID = product.SupplierID,
                CategoryID = product.CategoryID,
             
            };
        }
        public async Task<ProductGetDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductGetDTO
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                SupplierID = product.SupplierID,
                CategoryID = product.CategoryID
            };
        }
    }
}
