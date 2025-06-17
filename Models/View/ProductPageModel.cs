using OnlineStore.Data.Models;
using OnlineStore.Models.Containers;
using OnlineStore.Models.DTO;

namespace OnlineStore.Models.View
{
    public class ProductPageModel
    {
        public required Product Product { get; set; }
        public ReviewDTO NewReview { get; set; }
        public required Page<ReviewDTO> ReviewContainer { get; set; }
    }
}
