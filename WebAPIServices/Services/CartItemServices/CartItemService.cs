using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIServices.Data;
using WebAPIServices.Dto.CartItem;
using WebAPIServices.Mappers;

namespace WebAPIServices.Services.CartItemServices
{
    public class CartItemService : ICartItemService
    {
        private readonly DataContext _context;

        public CartItemService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CartItemDto>> GetAllCartItemAsync()
        {
            var cartItems = await _context.CartItems
                .Select(c => c.ToCartItemDto())
                .ToListAsync();

            return cartItems;
        }

        public async Task<CartItemDto> GetSingleCartItemAsync(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return null;
            }

            return cartItem.ToCartItemDto();
        }

        public async Task<List<CartItemDto>> AddCartItemAsync(CreateCartItemDto createCartItemDto)
        {
            var cartItem = createCartItemDto.ToCartFromCreateDTO();

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return await GetAllCartItemAsync();
        }

        public async Task<List<CartItemDto>> UpdateCartItemAsync(int id, UpdateCarttDto updateCartItemDto)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return null;
            }

            cartItem.Quantity = updateCartItemDto.Quantity;

            await _context.SaveChangesAsync();

            return await GetAllCartItemAsync();
        }

        public async Task<List<CartItemDto>> DeleteCartItemAsync(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return await GetAllCartItemAsync();
        }
    }
}
