using System.ComponentModel.DataAnnotations;
using WebAPIServices.Dto.Product;

namespace WebAPIServices.Dto.Category
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
    }
}
