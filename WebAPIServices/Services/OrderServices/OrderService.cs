using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIServices.Data;
using WebAPIServices.Dto.Order;
using WebAPIServices.Mappers;

namespace WebAPIServices.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Select(o => o.ToOrderDto())
                .ToListAsync();
            return orders;
        }

        public async Task<OrderDto> GetSingleOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }
            return order.ToOrderDto();
        }

        public async Task<List<OrderDto>> AddOrderAsync(CreateOrderDto orderDto)
        {
            var order = orderDto.ToOrderFromCreateDto();
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return await GetAllOrdersAsync();
        }

        public async Task<List<OrderDto>> UpdateOrderAsync(int id, UpdateOrderDto orderDto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }
            orderDto.ToOrderFromUpdateDto(order);
            await _context.SaveChangesAsync();
            return await GetAllOrdersAsync();
        }

        public async Task<List<OrderDto>> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return await GetAllOrdersAsync();
        }
    }
}
