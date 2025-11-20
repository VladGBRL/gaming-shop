using Gaming_Shop.ShopManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.ShopManagement.Interfaces
{
    public interface ICategoryServices
    {
        Task<CategoryGetDTO> AddCategoryAsync(CategoryAddDTO dto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<CategoryGetDTO?> UpdateCategoryAsync(int id, CategoryAddDTO dto);
        Task<CategoryGetDTO?> GetCategoryByIdAsync(int id);
        Task<List<CategoryGetDTO>> GetAllAsync();

        }
}
