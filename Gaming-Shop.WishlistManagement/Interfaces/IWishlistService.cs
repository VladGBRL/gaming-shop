using Gaming_Shop.WishlistManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.WishlistManagement.Interfaces
{
    public interface IWishlistService
    {
        Task AddToWishlistAsync(int userId, int productId);
        Task RemoveFromWishlistAsync(int userId, int productId);
        Task<WishlistDto> GetWishlistAsync(int userId);
    }
}
