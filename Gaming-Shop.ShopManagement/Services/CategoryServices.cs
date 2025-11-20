using Gaming_Shop.ShopManagement.Data;
using Gaming_Shop.ShopManagement.DTOs;
using Gaming_Shop.ShopManagement.Entities;
using Gaming_Shop.ShopManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gaming_Shop.ShopManagement.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ShopManagementDbContext _context;

        public CategoryServices(ShopManagementDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryGetDTO> AddCategoryAsync(CategoryAddDTO dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryGetDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CategoryGetDTO?> UpdateCategoryAsync(int id, CategoryAddDTO dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            category.CategoryName = dto.CategoryName;
            await _context.SaveChangesAsync();

            return new CategoryGetDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };
        }

        public async Task<CategoryGetDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryGetDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };
        }
    }
}
