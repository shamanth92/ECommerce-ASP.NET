using ECommerceNextjs.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNextjs.Controllers
{
    [ApiController]
    [Route("ecommerce/[controller]")]
    public class FetchProductsController : ControllerBase
    {
        private readonly FetchProductsService _fetchProductsService;

        public FetchProductsController(FetchProductsService fetchProductsService)
        {
            _fetchProductsService = fetchProductsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExternalProducts([FromQuery] long? id)
        {
            string apiUrl = "https://fakestoreapi.com/products/" + id;
            if (id == null)
            {
                var products = await _fetchProductsService.GetProductDetails(apiUrl);
                return Ok(products);
            }
            else
            {
                var products = await _fetchProductsService.GetProductDetailsByID(apiUrl);
                return Ok(products);
            }
        }
    }
}
