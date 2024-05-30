using System.ComponentModel.DataAnnotations;

namespace ShoppingPortalApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }
        public List<Product> ?Cart { get; set; }

    }
}
