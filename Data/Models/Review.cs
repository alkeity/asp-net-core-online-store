using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OnlineStore.Data.Models
{
    public class Review
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Product")]
        public required long ProductID { get; set; }
        public required Product Product { get; set; }
        [ForeignKey("User")]
        public required long UserID { get; set; }
        public required User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [StringLength(128, MinimumLength = 3)]
        public required string Username { get; set; }
        public required string Text { get; set; }
        [Range(1,5)]
        public required byte Rating { get; set; }
    }
}
