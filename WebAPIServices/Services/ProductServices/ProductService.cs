using Newtonsoft.Json;
using WebAPIServices.Data;
using WebAPIServices.Dto.Product;
using WebAPIServices.Mapers;

namespace WebAPIServices.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var productList = await _context.Products
                .Include(p => p.Category)
                .Select(p => p.ToProductDto())
                .ToListAsync();

            return productList;
        }

        public async Task<ProductDto> GetSingleProductAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .Select(p => p.ToProductDto())
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<List<ProductDto>> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
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
            return await GetAllProductsAsync();
        }

        public async Task<List<ProductDto>> UpdateProductAsync(int id, UpdateProductDto request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            if (request.CategoryId == null)
            {
                return null;
            }

            var category = await _context.Categories.FindAsync(request.CategoryId.Value);
            if (category == null)
            {
                return null;
            }

            // Use mapper to update product
            product = request.ToProductFromUpdate(product, request.CategoryId.Value);

            await _context.SaveChangesAsync();
            return await GetAllProductsAsync();
        }
    }
}
