using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIServices.Data;
using WebAPIServices.Dto.Product;
using WebAPIServices.Helper;
using WebAPIServices.Mappers;

namespace WebAPIServices.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync(QueryObject query)
        {
            var productList = _context.Products
                .Include(p => p.Category)
                .Include(p => p.CartItems)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.ProductName))
            {
                productList = productList.Where(p => p.Name == query.ProductName);
            }

            if (!string.IsNullOrWhiteSpace(query.CategoryName))
            {
                productList = productList.Where(p => p.Category.Name.Contains(query.CategoryName));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase)){
                    productList = query.isDecsending ? productList.OrderByDescending(s=>s.Price) : productList.OrderBy(s=>s.Price);
                }
            }

            var products = await productList.ToListAsync();
            return products.Select(p => p.ToProductDto()).ToList();
        }

        public async Task<ProductDto> GetSingleProductAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.CartItems)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            return product?.ToProductDto();
        }

        public async Task<List<ProductDto>> DeleteProductAsync(int id, QueryObject query)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return await GetAllProductsAsync(query);
        }


        public async Task<ProductDto> AddProductAsync(CreateProductDto productDto)
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
            return newProduct.ToProductDto();
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            if (productDto.CategoryId == null)
            {
                return null;
            }

            var category = await _context.Categories.FindAsync(productDto.CategoryId.Value);
            if (category == null)
            {
                return null;
            }

            product = productDto.ToProductFromUpdate(product, productDto.CategoryId.Value);

            await _context.SaveChangesAsync();
            return product.ToProductDto();
        }
    }
}
