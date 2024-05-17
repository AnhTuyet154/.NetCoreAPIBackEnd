using System.ComponentModel.DataAnnotations;

namespace WebAPIServices.Dto.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
    }
}
