using System.ComponentModel.DataAnnotations;

namespace WebAPIServices.Dto.CartItem
{
    public class UpdateCarttDto
    {
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Product is required.")]
        public int? ProductId { get; set; }
        [Required(ErrorMessage = "UserId is required.")]
        public string? UserId { get; set; }
    }
}