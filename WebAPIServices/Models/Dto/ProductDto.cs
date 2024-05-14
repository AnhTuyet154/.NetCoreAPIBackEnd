using System.ComponentModel.DataAnnotations;

namespace WebAPIServices.Models.DTO
{
    public class ProductDto
    { 
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
    }
}