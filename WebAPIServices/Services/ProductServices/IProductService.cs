using WebAPIServices.Dto.Product;

namespace WebAPIServices.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetSingleProductAsync(int id);
        Task<List<ProductDto>> AddProductAsync(CreateProductDto productDto);
        Task<List<ProductDto>> UpdateProductAsync(int id, UpdateProductDto productDto);
        Task<List<ProductDto>> DeleteProductAsync(int id);
    }
}
