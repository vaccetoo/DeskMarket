using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DeskMarket.Common.Constants.ValidationConstants;

namespace DeskMarket.Data.Models
{
    [Comment("Product in the Market")]
    public class Product
    {
        [Key]
        [Comment("Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        [Comment("Name of the product")]
        public string ProductName { get; set; } = null!;

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        [Comment("Description of the product")]
        public string Description { get; set; } = null!;

        [Required]
        [Precision(PricePrecision, PriceScale)]
        [Comment("Price of the product")]
        public decimal Price { get; set; }

        [Comment("Url to the product Image")]
        public string? ImageUrl { get; set; }

        [Required]
        [Comment("IdentityUser identifier")]
        public string SellerId { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(SellerId))]
        public IdentityUser Seller { get; set; } = null!;

        [Required]
        [Comment("Date and time the product has been added on")]
        public DateTime AddedOn { get; set; }

        [Required]
        [Comment("Category identifier")]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public IEnumerable<ProductClient> ProductClients { get; set; } 
            = new List<ProductClient>();
    }
}
