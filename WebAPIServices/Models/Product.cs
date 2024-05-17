namespace WebAPIServices.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
