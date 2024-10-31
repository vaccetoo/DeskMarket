using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskMarket.Data.Models
{
    [PrimaryKey(nameof(ProductId), nameof(ClientId))]
    [Comment("Relationship between product and client")]
    public class ProductClient
    {
        [Required]
        [Comment("Product identifier")]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey(nameof(ProductId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Product Product { get; set; } = null!;

        [Required]
        [Comment("Client (IdentityUser) identifier")]
        public string ClientId { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(ClientId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public IdentityUser Client { get; set; } = null!;
    }
}