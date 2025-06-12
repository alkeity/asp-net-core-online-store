using OnlineStore.Models.Containers;
using OnlineStore.Models.Entities;

namespace OnlineStore.Models.View
{
    public class ProductPageModel
    {
        public required Product Product { get; set; }
        public Review NewReview { get; set; }
        public required Page<Review> ReviewContainer { get; set; }
    }
}
