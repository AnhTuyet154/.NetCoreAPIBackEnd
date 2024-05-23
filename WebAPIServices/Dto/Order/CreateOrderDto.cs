using System.ComponentModel.DataAnnotations;

namespace WebAPIServices.Dto.Order
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double TotalPrice { get; set; }


        [Required(ErrorMessage = "Contact is required.")]
        [MaxLength(30, ErrorMessage = "Contact cannot be longer than 30 characters.")]
        public string Contact { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "UserId is required.")]
        public string? UserId { get; set; }

    }
}
