using Gaming_Shop.ShopManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.ShopManagement.Interfaces
{
    public interface IProductServices
    {
        Task<ProductGetDTO> AddProductAsync(ProductAddDTO productDto);
        Task<bool> DeleteProductsAsync(int id_Product);
        Task<ProductGetDTO> UpdateProductsAsync(int id_Product, ProductAddDTO productDto);
        Task<ProductGetDTO> GetProductByIdAsync(int id);
    }
}
