using WebAPIServices.Dto.Order;
using WebAPIServices.Dto.Product;

namespace WebAPIServices.Services.OrderServices
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetSingleOrderAsync(int id);
        Task<List<OrderDto>> AddOrderAsync(CreateOrderDto productDto);
        Task<List<OrderDto>> UpdateOrderAsync(int id, UpdateOrderDto productDto);
        Task<List<OrderDto>> DeleteOrderAsync(int id);
    }
}
