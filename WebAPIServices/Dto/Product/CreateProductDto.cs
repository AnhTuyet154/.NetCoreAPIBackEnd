using System.ComponentModel.DataAnnotations;

namespace WebAPIServices.Dto.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        [MaxLength(30, ErrorMessage = "Color cannot be longer than 30 characters.")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image is required.")]
        [Url(ErrorMessage = "Invalid image URL.")]
        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters long.")]
        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
