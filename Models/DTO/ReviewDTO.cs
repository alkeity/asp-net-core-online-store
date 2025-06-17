using OnlineStore.Data.Models;

namespace OnlineStore.Models.DTO
{
    public class ReviewDTO
    {
        public required string Username { get; set; }
        public required string Text { get; set; }
        public required byte Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public static ReviewDTO FromEntity(Review review)
        {
            return new ReviewDTO()
            {
                Username = review.Username,
                Text = review.Text,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt,
            };
        }
    }
}
