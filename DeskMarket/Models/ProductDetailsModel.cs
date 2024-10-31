namespace DeskMarket.Models
{
    public class ProductDetailsModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; } = null!;

        public string AddedOn { get; set; } = null!;

        public string Seller { get; set; } = null!;

        public bool HasBought { get; set; }
    }
}
