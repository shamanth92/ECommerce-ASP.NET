using ECommerceNextjs.Models;
using ECommerceNextjs.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNextjs.Controllers
{
    [ApiController]
    [Route("ecommerce/[controller]")]
    public class WishlistController: ControllerBase
    {
        public readonly WishlistService _wishlistService = null!;

        public WishlistController(WishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToWishlist(ProductModel product)
        {
            await _wishlistService.addToWishlist(product);
            return CreatedAtAction("AddProductToWishlist", product);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetProductByEmail([FromQuery] string email)
        {
            var product = await _wishlistService.GetWishlistItems(email);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] long id, string email)
        {
            var book = await _wishlistService.GetWishlistItems(email);
            if (book == null)
            {
                return NotFound();
            }
            await _wishlistService.DeleteProductFromWishlist(id);
            return NoContent();
        }
    }
}
