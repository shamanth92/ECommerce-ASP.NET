using ECommerceNextjs.Models;
using ECommerceNextjs.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json;

namespace ECommerceNextjs.Controllers
{
    [ApiController]
    [Route("ecommerce/[controller]")]
    public class CheckoutCartController: ControllerBase
    {
        public readonly CheckoutCartService _checkoutCartService = null!;

        public CheckoutCartController(CheckoutCartService checkoutCartService)
        {
            _checkoutCartService = checkoutCartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemsToCart(CheckoutCartModel products)
        {
            var getProducts = products.products.ToList();
            foreach (var product in getProducts)
            {
                List<CheckoutProductModel> getSavedCart = await _checkoutCartService.GetCartItems(product.email);
                var isPresent = getSavedCart.FirstOrDefault(doc => doc.id == product.id);
                if (isPresent != null)
                {
                    await _checkoutCartService.UpdateCartItemsQuantity(product.email, "quantity", product.id);
                } else
                {
                    await _checkoutCartService.addItemsToCart(product);
                }
            }
            return CreatedAtAction("AddItemsToCart", products);

        }

        [HttpGet]
        public async Task<ActionResult<List<CheckoutProductModel>>> GetCartItems([FromQuery] string email)
        {
            var checkoutProducts = await _checkoutCartService.GetCartItems(email);
            if (checkoutProducts == null)
            {
                return NotFound();
            }
            return checkoutProducts;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItemFromCart([FromQuery] long id, string email)
        {
            var checkoutProducts = await _checkoutCartService.GetCartItems(email);
            if (checkoutProducts == null)
            {
                return NotFound();
            }
            await _checkoutCartService.DeleteItemFromCart(id);
            return NoContent();
        }
    }
}
