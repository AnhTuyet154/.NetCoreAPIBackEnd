using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIServices.Dto.Order;
using WebAPIServices.Services.OrderServices;

namespace WebAPIServices.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetSingleOrder(int id)
        {
            var order = await _orderService.GetSingleOrderAsync(id);
            if (order == null)
            {
                return NotFound("Order not found");
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<List<OrderDto>>> AddOrder(CreateOrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.AddOrderAsync(orderDto);
            if (result == null)
            {
                return BadRequest("Invalid order data.");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<OrderDto>>> UpdateOrder(int id, UpdateOrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.UpdateOrderAsync(id, orderDto);
            if (result == null)
            {
                return NotFound("Order not found");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<OrderDto>>> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (result == null)
            {
                return NotFound("Order not found");
            }
            return Ok(result);
        }
    }
}
