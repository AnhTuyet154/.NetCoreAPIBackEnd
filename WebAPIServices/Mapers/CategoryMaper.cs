using System.Collections.Generic;
using System.Linq;
using WebAPIServices.Dto.Category;
using WebAPIServices.Dto.Product;
using WebAPIServices.Models;

namespace WebAPIServices.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                Description = categoryModel.Description,
                Products = categoryModel.Products != null ? categoryModel.Products.Select(c => c.ToProductDto()).ToList() : new List<ProductDto>()
            };
        }

        public static Category ToCategoryFromCreateDTO(this CreateCategoryDto categoryDto)
        {
            return new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                // Products property is not set in creation
            };
        }

        public static Category ToCategoryFromUpdateDTO(this UpdateCategoryDto categoryDto, Category category)
        {
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            // Products property should not be modified during update
            return category;
        }
    }
}
