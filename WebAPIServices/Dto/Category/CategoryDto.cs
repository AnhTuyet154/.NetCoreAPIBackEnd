using System.ComponentModel.DataAnnotations;
using WebAPIServices.Dto.Product;

namespace WebAPIServices.Dto.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ProductDto>? Products { get; set; }    
    }
}
