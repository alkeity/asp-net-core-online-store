using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Data.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
