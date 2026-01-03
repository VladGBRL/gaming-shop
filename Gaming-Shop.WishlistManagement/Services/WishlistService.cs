using Gaming_Shop.AccountManagement.Entities;
using Gaming_Shop.AccountManagement.Data;
using Gaming_Shop.WishlistManagement.Data;
using Gaming_Shop.WishlistManagement.DTOs;
using Gaming_Shop.WishlistManagement.Entities;
using Gaming_Shop.WishlistManagement.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gaming_Shop.WishlistManagement.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly WishlistDbContext _wishlistDbContext;
        private readonly AccountManagementDbContext _accountDbContext;

        public WishlistService(
            WishlistDbContext wishlistDbContext,
            AccountManagementDbContext accountDbContext)
        {
            _wishlistDbContext = wishlistDbContext;
            _accountDbContext = accountDbContext;
        }

        // Adds a product to the user's wishlist
        public async Task AddToWishlistAsync(int userId, int productId)
        {
            // Check if user exists in AccountManagementDbContext
            var user = await _accountDbContext.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            // Get or create wishlist for this user
            var wishlist = await _wishlistDbContext.Wishlists
                .Include(w => w.Items)
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId
                };
                _wishlistDbContext.Wishlists.Add(wishlist);
            }

            // Add product if it doesn't already exist
            if (!wishlist.Items.Any(i => i.ProductId == productId))
            {
                wishlist.AddItem(productId);
            }

            await _wishlistDbContext.SaveChangesAsync();
        }

        // Removes a product from the user's wishlist
        public async Task RemoveFromWishlistAsync(int userId, int productId)
        {
            // Verify user exists
            var user = await _accountDbContext.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var wishlist = await _wishlistDbContext.Wishlists
                .Include(w => w.Items)
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wishlist == null) return;

            wishlist.RemoveItem(productId);
            await _wishlistDbContext.SaveChangesAsync();
        }

        // Retrieves the wishlist for a given user
        public async Task<WishlistDto> GetWishlistAsync(int userId)
        {
            // Verify user exists
            var user = await _accountDbContext.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            // Load wishlist with items
            var wishlist = await _wishlistDbContext.Wishlists
                .Include(w => w.Items)
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wishlist == null)
                return new WishlistDto
                {
                    UserId = userId,
                    Items = new List<WishlistItemDto>()
                };

            // Get product IDs
            var productIds = wishlist.Items.Select(i => i.ProductId).ToList();

            // Load products from the product table
            var products = await _wishlistDbContext.Products
                .Where(p => productIds.Contains(p.ProductID))
                .ToDictionaryAsync(p => p.ProductID, p => p.Name);

            // Map wishlist items with product name
            var itemsDto = wishlist.Items
                .Select(i => new WishlistItemDto
                {
                    ProductId = i.ProductId,
                    Name = products.ContainsKey(i.ProductId) ? products[i.ProductId] : null,
                    AddedAt = i.AddedAt
                })
                .ToList();

            return new WishlistDto
            {
                UserId = wishlist.UserId,
                Items = itemsDto
            };
        }

    }
}
