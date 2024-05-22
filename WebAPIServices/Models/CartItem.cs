namespace WebAPIServices.Models
{
    public class CartItem
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
