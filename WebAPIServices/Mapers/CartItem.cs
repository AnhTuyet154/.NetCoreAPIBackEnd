using WebAPIServices.Models;
using WebAPIServices.Dto.CartItem;

namespace WebAPIServices.Mappers
{
    public static class CartItemMapper
    {
        public static CartItemDto ToCartItemDto(this CartItem cartItem)
        {
            return new CartItemDto
            {
                Id = cartItem.Id,
                Quantity = cartItem.Quantity,
                ProductId = cartItem.ProductId,
                UserId = cartItem.UserId
            };
        }

        public static CartItem ToCartFromCreateDTO(this CreateCartItemDto createCartItemDto)
        {
            return new CartItem
            {
                Quantity = createCartItemDto.Quantity,
                ProductId = createCartItemDto.ProductId,
                UserId = createCartItemDto.UserId
            };
        }

        public static CartItem ToCartFromUpdateDTO(this UpdateCarttDto updateCartItemDto)
        {
            return new CartItem
            {
                Quantity = updateCartItemDto.Quantity,
                ProductId = updateCartItemDto.ProductId,
                UserId = updateCartItemDto.UserId
            };
        }
    }
}
