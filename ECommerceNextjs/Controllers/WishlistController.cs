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
        public readonly AuthenticationService _authService = null!;

        public WishlistController(WishlistService wishlistService, AuthenticationService authService)
        {
            _wishlistService = wishlistService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToWishlist(ProductModel product, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                await _wishlistService.addToWishlist(product);
                return CreatedAtAction("AddProductToWishlist", product);
            } else
            {
                return BadRequest("Bad Token");
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetProductByEmail([FromQuery] string email, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                var product = await _wishlistService.GetWishlistItems(email);
                if (product == null)
                {
                    return NotFound();
                }
                return product;
            } else
            {
                return BadRequest("Bad Token");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] long id, string email, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                var book = await _wishlistService.GetWishlistItems(email);
                if (book == null)
                {
                    return NotFound();
                }
                await _wishlistService.DeleteProductFromWishlist(id);
                return NoContent();
            } else
            {
                return BadRequest("Bad Token");
            }
                
        }
    }
}
