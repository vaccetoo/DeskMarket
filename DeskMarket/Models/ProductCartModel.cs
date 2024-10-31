namespace DeskMarket.Models
{
    public class ProductCartModel
    {
        public int Id { get; set; } 
        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
