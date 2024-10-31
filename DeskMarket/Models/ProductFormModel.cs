using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.Constants.ValidationConstants;
using static DeskMarket.Common.Messages.ErrorMessages;

namespace DeskMarket.Models
{
    public class ProductFormModel
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(ProductNameMaxLength, 
            MinimumLength = ProductNameMinLength, 
            ErrorMessage = StringLengthErrorMessage)]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(ProductDescriptionMaxLength, 
            MinimumLength = ProductDescriptionMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range((double)ProductMinPrice, (double)ProductMaxPrice,
            ErrorMessage = RangeErrorMessage)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string AddedOn { get; set; } = null!;

        public string SellerId { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; } 

        public IEnumerable<CategoryModel> Categories { get; set; } 
            = new List<CategoryModel>();
    }
}
