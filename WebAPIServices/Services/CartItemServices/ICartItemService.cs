using WebAPIServices.Dto.CartItem;
using WebAPIServices.Dto.Product;

namespace WebAPIServices.Services.CartItemServices
{
    public interface ICartItemService
    {
        Task<List<CartItemDto>> GetAllCartItemAsync();
        Task<CartItemDto> GetSingleCartItemAsync(int id);
        Task<List<CartItemDto>> AddCartItemAsync(CreateCartItemDto productDto);
        Task<List<CartItemDto>> UpdateCartItemAsync(int id, UpdateCarttDto productDto);
        Task<List<CartItemDto>> DeleteCartItemAsync(int id);
    }
}
