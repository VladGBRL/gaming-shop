using Gaming_Shop.ShopManagement.Data;
using Gaming_Shop.ShopManagement.DTOs;
using Gaming_Shop.ShopManagement.Entities;
using Gaming_Shop.ShopManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ProductServices : IProductServices
{
    private readonly ShopManagementDbContext _context;

    public ProductServices(ShopManagementDbContext context)
    {
        _context = context;
    }

    public async Task<ProductGetDTO> AddProductAsync(ProductAddDTO productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Stock = productDto.Stock,
            SupplierID = productDto.SupplierID,
            CategoryID = productDto.CategoryID
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

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

    public async Task<bool> DeleteProductsAsync(int id_Product)
    {
        var product = await _context.Products.FindAsync(id_Product);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ProductGetDTO?> UpdateProductsAsync(int id_Product, ProductAddDTO productDto)
    {
        var product = await _context.Products.FindAsync(id_Product);
        if (product == null) return null;

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
            CategoryID = product.CategoryID
        };
    }

    public async Task<ProductGetDTO?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductID == id);

        if (product == null) return null;

        return new ProductGetDTO
        {
            ProductID = product.ProductID,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            SupplierID = product.SupplierID,
            SupplierName = product.Supplier.SupplierName,
            CategoryID = product.CategoryID,
            CategoryName = product.Category.CategoryName
        };
    }

    public async Task<List<ProductGetDTO>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Category)
            .Select(p => new ProductGetDTO
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                SupplierID = p.SupplierID,
                SupplierName = p.Supplier.SupplierName,
                CategoryID = p.CategoryID,
                CategoryName = p.Category.CategoryName
            })
            .ToListAsync();
    }
    public async Task<List<ProductGetDTO>> SearchAsync(string? query)
    {
        query = query?.ToLower() ?? "";

        return await _context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Category)
            .Where(p =>
                p.Name.ToLower().Contains(query) ||
                p.Supplier.SupplierName.ToLower().Contains(query) ||
                p.Category.CategoryName.ToLower().Contains(query)
            )
            .Select(p => new ProductGetDTO
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                SupplierID = p.SupplierID,
                SupplierName = p.Supplier.SupplierName,
                CategoryID = p.CategoryID,
                CategoryName = p.Category.CategoryName
            })
            .ToListAsync();
    }

    public async Task<List<ProductGetDTO>> FilterAsync(
    decimal? minPrice,
    decimal? maxPrice,
    string? categoryName,
    string? supplierName)
    {
        var query = _context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Category)
            .AsQueryable();

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        if (!string.IsNullOrWhiteSpace(categoryName))
            query = query.Where(p => p.Category.CategoryName.ToLower()
                .Contains(categoryName.ToLower()));

        if (!string.IsNullOrWhiteSpace(supplierName))
            query = query.Where(p => p.Supplier.SupplierName.ToLower()
                .Contains(supplierName.ToLower()));

        return await query
            .Select(p => new ProductGetDTO
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                SupplierID = p.SupplierID,
                SupplierName = p.Supplier.SupplierName,
                CategoryID = p.CategoryID,
                CategoryName = p.Category.CategoryName
            })
            .ToListAsync();
    }

}
