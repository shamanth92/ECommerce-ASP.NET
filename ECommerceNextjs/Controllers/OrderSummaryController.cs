using ECommerceNextjs.Models;
using ECommerceNextjs.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNextjs.Controllers
{
    [ApiController]
    [Route("ecommerce/[controller]")]
    public class OrderSummaryController: ControllerBase
    {
        public readonly OrderSummaryService _orderSummaryService = null!;
        public readonly AuthenticationService _authService = null!;

        public OrderSummaryController(OrderSummaryService orderSummaryService, AuthenticationService authService)
        {
            _orderSummaryService = orderSummaryService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> StoreOrderSummary(OrderSummaryModel orderSummary, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                await _orderSummaryService.storeOrderSummary(orderSummary);
                return CreatedAtAction("StoreOrderSummary", orderSummary);
            } else
            {
                return BadRequest("Bad Token");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderSummaryModel>>> GetOrders([FromQuery] string email, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                var orders = await _orderSummaryService.GetOrders(email);
                if (orders == null)
                {
                    return NotFound();
                }
                return orders;
            } else
            {
                return BadRequest("Bad Token");
            }
        }

    }
}
