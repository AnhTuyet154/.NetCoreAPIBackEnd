using Microsoft.AspNetCore.Mvc;
using WebAPIServices.Dto.CartItem;
using System.Threading.Tasks;
using WebAPIServices.Services.CartItemServices;
using WebAPIServices.Dto.Product;

namespace WebAPIServices.Controllers
{
    [Route("api/CartItem")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CartItemDto>>> GetAllCartIteM()
        {
            var products = await _cartItemService.GetAllCartItemAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetSingleProduct(int id)
        {
            var result = await _cartItemService.GetSingleCartItemAsync(id);
            if (result == null)
            {
                return NotFound("Cart not found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddCartItem(CreateCartItemDto createCartItemDto)
        {
            var result = await _cartItemService.AddCartItemAsync(createCartItemDto);
            if (result == null)
            {
                return BadRequest("Invalid cart item data.");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CartItemDto>> UpdateCartItem(int id, UpdateCarttDto updateCartItemDto)
        {
            var result = await _cartItemService.UpdateCartItemAsync(id, updateCartItemDto);
            if (result == null)
            {
                return NotFound("Cart item not found");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCartItem(int id)
        {
            var result = await _cartItemService.DeleteCartItemAsync(id);
            if (result==null)
            {
                return NotFound("Cart item not found");
            }
            return Ok(result);
        }
    }
}
