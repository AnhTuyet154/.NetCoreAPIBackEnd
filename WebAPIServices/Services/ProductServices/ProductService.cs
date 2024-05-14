using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using WebAPIServices.Models.DTO;

namespace WebAPIServices.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private IMemoryCache? _cache;

        public void SetCache(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        private static List<ProductDto> products = new List<ProductDto>
        {
            new ProductDto{
                Id=1,
                Name = "Laptop HP Omen 16",
                Price=37290,
                Category = "Laptop",
                Color = "black",
                Description="HP Omen 16 laptop n0087AX R7 ",
                Image= "https://www.omen.com/content/dam/sites/omen/worldwide/laptops/hero-banner-desktop-i7-3-new-image.png"
            },
            new ProductDto{
                Id=2,
                Name = "Laptop Apple MacBook Air",
                Price=37290,
                Category = "Laptop",
                Color = "yellow",
                Description="13 inch M2 16GB/512GB/10GPU (Z15Z0003L) ",
                Image= "https://tse4.mm.bing.net/th?id=OIP.Gugj6FOZ22QOi6iH2rTQSgHaE8&pid=Api&P=0&h=180g"
            }
        };


        public async Task<List<ProductDto>> AddProductAsync(ProductDto productDto)
        {
            if (productDto != null)
            {
                products.Add(productDto);
            }
            return await Task.FromResult(products);
        }


        public async Task<List<ProductDto>> DeleteProductAsync(int id)
        {
            var product = products.Find(x => x.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
            return await Task.FromResult(products);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var cachedData = _cache?.Get<string>("all_products");
            if (cachedData != null)
            {
                return DeserializeProductList(cachedData);
            }

            var productList = await Task.FromResult(products);
            _cache?.Set("all_products", SerializeProductList(productList), TimeSpan.FromMinutes(10));
            return productList;
            //return await Task.FromResult(products);
        }

        public async Task<ProductDto> GetSingleProductAsync(int id)
        {
            var cachedData = _cache?.Get<string>($"product_{id}");
            if (cachedData != null)
            {
                return DeserializeProductList(cachedData).FirstOrDefault(x => x.Id == id);
            }

            var product = await Task.FromResult(products.FirstOrDefault(x => x.Id == id)); // Đổi tên biến cục bộ thành product
            _cache?.Set($"product_{id}", SerializeProductList(new List<ProductDto> { product }), TimeSpan.FromMinutes(10));
            return product;
            //return await Task.FromResult(products.FirstOrDefault(x => x.Id == id));
        }

        public async Task<List<ProductDto>> UpdateProductAsync(int id, ProductDto request)
        {
            var product = products.Find(x => x.Id == id);
            if (product == null)
            {
                return null;
            }
            product.Name = request.Name;
            product.Price = request.Price;
            product.Category = request.Category;
            product.Color = request.Color;
            product.Image = request.Image;
            product.Description = request.Description;
            return await Task.FromResult(products);
        }
        private string SerializeProductList(List<ProductDto> products)
        {
            return JsonConvert.SerializeObject(products);
        }

        private List<ProductDto> DeserializeProductList(string serializedData)
        {
            return JsonConvert.DeserializeObject<List<ProductDto>>(serializedData);
        }

    }
}
