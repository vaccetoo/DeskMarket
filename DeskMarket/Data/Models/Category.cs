using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.Constants.ValidationConstants;

namespace DeskMarket.Data.Models
{
    [Comment("Category of the product")]
    public class Category
    {
        [Key]
        [Comment("Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Name of the category")]
        public string Name { get; set; } = null!;

        public IEnumerable<Product> Products { get; set; } 
            = new List<Product>();
    }
}