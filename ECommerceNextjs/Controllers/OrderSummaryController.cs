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

        public OrderSummaryController(OrderSummaryService orderSummaryService)
        {
            _orderSummaryService = orderSummaryService;
        }

        [HttpPost]
        public async Task<IActionResult> StoreOrderSummary(OrderSummaryModel orderSummary)
        {
            await _orderSummaryService.storeOrderSummary(orderSummary);
            return CreatedAtAction("StoreOrderSummary", orderSummary);

        }

        [HttpGet]
        public async Task<ActionResult<List<OrderSummaryModel>>> GetOrders([FromQuery] string email)
        {
            var orders = await _orderSummaryService.GetOrders(email);
            if (orders == null)
            {
                return NotFound();
            }
            return orders;
        }

    }
}
