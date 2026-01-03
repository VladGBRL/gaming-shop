using Gaming_Shop.WishlistManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gaming_Shop.Server.Controllers.WishlistManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost("{userId}/{productId}")]
        public async Task<IActionResult> AddToWishlist(int userId, int productId)
        {
            await _wishlistService.AddToWishlistAsync(userId, productId);
            return Ok();
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromWishlist(int userId, int productId)
        {
            await _wishlistService.RemoveFromWishlistAsync(userId, productId);
            return NoContent();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWishlist(int userId)
        {
            var wishlist = await _wishlistService.GetWishlistAsync(userId);
            return Ok(wishlist);
        }
    }

}
