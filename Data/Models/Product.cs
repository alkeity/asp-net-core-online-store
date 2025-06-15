using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [StringLength(128, MinimumLength = 3)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();

    }
}
