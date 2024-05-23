namespace WebAPIServices.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Contact { get; set; }= string.Empty;
        public double TotalPrice { get; set; }
        public string? UserId { get; set; }
        public Account? Accounts { get; set; }
    }
}
