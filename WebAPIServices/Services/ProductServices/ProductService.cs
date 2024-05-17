using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using WebAPIServices.Data;
using WebAPIServices.Dto.Product;
using WebAPIServices.Mapers;

namespace WebAPIServices.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private IMemoryCache _cache;

        private string SerializeProductList(List<ProductDto> products)
        {
            return JsonConvert.SerializeObject(products);
        }

        private List<ProductDto> DeserializeProductList(string serializedData)
        {
            return JsonConvert.DeserializeObject<List<ProductDto>>(serializedData);
        }


        private void InvalidateCache(string key)
        {
            _cache.Remove(key);
        }

        public void SetCache(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public ProductService(DataContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var cachedData = _cache.Get<string>("all_products");
            if (cachedData != null)
            {
                return DeserializeProductList(cachedData);
            }

            var productList = await _context.Products
                .Include(p => p.Category)
                .Select(p => p.ToProductDto())
                .ToListAsync();

            _cache.Set("all_products", SerializeProductList(productList), TimeSpan.FromMinutes(10));
            return productList;
        }

        public async Task<ProductDto> GetSingleProductAsync(int id)
        {
            var cachedData = _cache.Get<string>($"product_{id}");
            if (cachedData != null)
            {
                return DeserializeProductList(cachedData).FirstOrDefault(x => x.Id == id);
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .Select(p => p.ToProductDto())
                .FirstOrDefaultAsync();

            if (product != null)
            {
                _cache.Set($"product_{id}", SerializeProductList(new List<ProductDto> { product }), TimeSpan.FromMinutes(10));
            }
            return product;
        }

        public async Task<List<ProductDto>> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                InvalidateCache("all_products");
            }
            return await GetAllProductsAsync();
        }

        public async Task<List<ProductDto>> AddProductAsync(CreateProductDto productDto)
        {
            if (productDto.CategoryId == null)
            {
                return null;
            }

            var category = await _context.Categories.FindAsync(productDto.CategoryId);
            if (category == null)
            {
                return null;
            }

            var newProduct = productDto.ToProductFromCreate(productDto.CategoryId.Value);

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            InvalidateCache("all_products");
            return await GetAllProductsAsync();
        }
        public async Task<List<ProductDto>> UpdateProductAsync(int id, UpdateProductDto request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null; // Product not found
            }

            if (request.CategoryId == null)
            {
                return null; // CategoryId is required
            }

            var category = await _context.Categories.FindAsync(request.CategoryId.Value);
            if (category == null)
            {
                return null; // Category not found
            }

            // Use mapper to update product
            product = request.ToProductFromUpdate(product, request.CategoryId.Value);

            await _context.SaveChangesAsync();
            InvalidateCache("all_products");

            return await GetAllProductsAsync();
        }


        

            
        

    }
}
