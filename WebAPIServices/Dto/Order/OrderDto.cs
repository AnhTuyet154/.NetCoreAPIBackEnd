namespace WebAPIServices.Dto.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public double TotalPrice { get; set; }
        public string? UserId { get; set; }
    }
}
