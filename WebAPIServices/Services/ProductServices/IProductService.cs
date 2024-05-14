using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIServices.Models.DTO;

namespace WebAPIServices.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetSingleProductAsync(int id);
        Task<List<ProductDto>> AddProductAsync(ProductDto productDto);
        Task<List<ProductDto>> UpdateProductAsync(int id, ProductDto productDto);
        Task<List<ProductDto>> DeleteProductAsync(int id);
        void SetCache(IMemoryCache memoryCache);
    }
}
