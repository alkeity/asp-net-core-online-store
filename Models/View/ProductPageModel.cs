using OnlineStore.Data.Models;
using OnlineStore.Models.Containers;

namespace OnlineStore.Models.View
{
    public class ProductPageModel
    {
        public required Product Product { get; set; }
        public Review NewReview { get; set; }
        public required Page<Review> ReviewContainer { get; set; }
    }
}
