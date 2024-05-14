using Microsoft.AspNetCore.Mvc;
using WebAPIServices.Models.DTO;
using WebAPIServices.Services.ProductServices;

namespace WebAPIServices.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetSingleProduct(int id)
        {
            var result = await _productService.GetSingleProductAsync(id);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductDto>>> AddProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDto.Price <= 0)
            {
                return BadRequest("Price must be a positive number.");
            }

            var result = await _productService.AddProductAsync(productDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<ProductDto>>> UpdateProduct(int id, ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDto.Price <= 0)
            {
                return BadRequest("Price must be a positive number.");
            }

            var result = await _productService.UpdateProductAsync(id, productDto);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductDto>>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            return Ok(result);
        }
    }
}