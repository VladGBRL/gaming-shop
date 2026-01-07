using Gaming_Shop.AccountManagement.Data;
using Gaming_Shop.CartManagement.Data;
using Gaming_Shop.CartManagement.DTOs;
using Gaming_Shop.CartManagement.Entities;
using Gaming_Shop.CartManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gaming_Shop.CartManagement.Services;

public class CartService : ICartService
{
    private readonly CartDbContext _context;
    private readonly AccountManagementDbContext _accountDbContext;

    public CartService(CartDbContext context, AccountManagementDbContext accountDbContext)
    {
        _context = context;
        _accountDbContext = accountDbContext;
    }

    public async Task<CartDto> GetCartAsync(int userId)
    {
        var user = await _accountDbContext.Users.FindAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            return new CartDto
            {
                Items = Enumerable.Empty<CartItemDto>(),
                TotalPrice = 0
            };
        }

        var productIds = cart.Items.Select(i => i.ProductId).ToList();

        var products = await _context.Products
            .Where(p => productIds.Contains(p.ProductID))
            .ToDictionaryAsync(
                p => p.ProductID,
                p => new { p.Name, p.Price }
            );

        var items = cart.Items.Select(i =>
        {
            var product = products.GetValueOrDefault(i.ProductId);

            var price = product?.Price ?? i.Price; 

            return new CartItemDto
            {
                ProductId = i.ProductId,
                ProductName = product?.Name,
                Price = price,
                Quantity = i.Quantity
            };
        }).ToList();

        return new CartDto
        {
            CartId = cart.Id,
            Items = items,
            TotalPrice = items.Sum(i => i.Price * i.Quantity)
        };
    }


    public async Task AddToCartAsync(int userId, int productId, int quantity)
    {
        var user = await _accountDbContext.Users.FindAsync(userId);
        if (user == null)
            throw new Exception("User not found");
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductID == productId);

        if (product == null)
            throw new Exception("Product not found");


        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem>()
            };

            _context.Carts.Add(cart);
        }

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item == null)
        {
            cart.Items.Add(new CartItem
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = quantity
            });
        }
        else
        {
            item.Quantity += quantity;
        }

        await _context.SaveChangesAsync();
    }


    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        var user = await _accountDbContext.Users.FindAsync(userId);
        if (user == null)
            throw new Exception("User not found");
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null) return;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            cart.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ClearCartAsync(int userId)
    {
        var user = await _accountDbContext.Users.FindAsync(userId);
        if (user == null)
            throw new Exception("User not found");
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null) return;

        cart.Items.Clear();
        await _context.SaveChangesAsync();
    }
}
