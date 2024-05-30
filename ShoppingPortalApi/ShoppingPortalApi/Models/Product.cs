using System.ComponentModel.DataAnnotations;

namespace ShoppingPortalApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? ImageUrl { get; set; }
        public string? Style { get; set;}
    }
}
