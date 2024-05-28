using System.Threading.Tasks;
using WebAPIServices.Dto.Product;
using WebAPIServices.Helper;

namespace WebAPIServices.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync(QueryObject query);
        Task<ProductDto> GetSingleProductAsync(int id);
        Task<ProductDto> AddProductAsync(CreateProductDto productDto);
        Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto productDto);
        Task<List<ProductDto>> DeleteProductAsync(int id, QueryObject query);
    }
}
