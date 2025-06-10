using OnlineStore.Models.Entities;

namespace OnlineStore.Models.View
{
    public class ProductPageModel
    {
        public required Product Product { get; set; }
        public required List<Review> Reviews { get; set; }
        public Review NewReview { get; set; }
    }
}
