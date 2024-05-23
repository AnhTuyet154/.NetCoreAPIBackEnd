using WebAPIServices.Dto.Order;
using WebAPIServices.Models;

namespace WebAPIServices.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Email = order.Email,
                Address = order.Address,
                Contact = order.Contact,
                TotalPrice = order.TotalPrice,
                UserId = order.UserId
            };
        }

        public static Order ToOrderFromCreateDto(this CreateOrderDto createOrderDto)
        {
            return new Order
            {
                Email = createOrderDto.Email,
                Address = createOrderDto.Address,
                Contact = createOrderDto.Contact,
                TotalPrice = createOrderDto.TotalPrice,
                UserId = createOrderDto.UserId
            };
        }

        public static void ToOrderFromUpdateDto(this UpdateOrderDto updateOrderDto, Order order)
        {
            order.Email = updateOrderDto.Email;
            order.Address = updateOrderDto.Address;
            order.Contact = updateOrderDto.Contact;
            order.TotalPrice = updateOrderDto.TotalPrice;
            order.UserId = updateOrderDto.UserId;
        }

    }
}
