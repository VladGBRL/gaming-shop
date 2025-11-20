using Gaming_Shop.ShopManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.ShopManagement.Interfaces
{
    public interface ISupplierServices
    {
        Task<SupplierGetDTO> AddSupplierAsync(SupplierAddDTO dto);
        Task<bool> DeleteSupplierAsync(int id);
        Task<SupplierGetDTO?> UpdateSupplierAsync(int id, SupplierAddDTO dto);
        Task<SupplierGetDTO?> GetSupplierByIdAsync(int id);
    }
}
