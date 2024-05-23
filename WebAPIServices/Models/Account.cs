using Microsoft.AspNetCore.Identity;

namespace WebAPIServices.Models
{
    public class Account : IdentityUser
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
