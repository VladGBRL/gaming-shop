using Gaming_Shop.CartManagement.DTOs;
using Gaming_Shop.CartManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gaming_Shop.Server.Controllers.CartManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToCart(
            int userId,
            [FromBody] AddToCartDto dto)
        {
            await _cartService.AddToCartAsync(
                userId,
                dto.ProductId,
                dto.Quantity
            );

            return Ok();
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(
            int userId,
            int productId)
        {
            await _cartService.RemoveFromCartAsync(userId, productId);
            return NoContent();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }
    }
}
