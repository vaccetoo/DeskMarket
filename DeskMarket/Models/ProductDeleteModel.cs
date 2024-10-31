namespace DeskMarket.Models
{
    public class ProductDeleteModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        public string Seller { get; set; } = null!;

        public string SellerId { get; set; } = null!;
    }
}
