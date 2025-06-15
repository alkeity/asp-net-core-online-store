using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Models;
using OnlineStore.Models.View;
using OnlineStore.Services;
using OnlineStore.Services.Implementations;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;

        public ProductController(IProductService productService, IReviewService reviewService)
        {
            _productService = productService;
            _reviewService = reviewService;
        }

        [HttpGet]
        [Route("{controller}/{action}/{id:int?}")]
        public IActionResult Index(int? id, int page = 0)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            Product? product = _productService.GetProductById((int) id);

            if (product == null)
                return RedirectToAction("Index", "Home");

            page = Math.Clamp(page, 0, int.MaxValue);
            ProductPageModel pageModel = new ProductPageModel()
            {
                Product = product,
                NewReview = new Review() { ProductID = product.Id, Rating = 5, Text = "", Username = "Anonymous", Product = product },
                ReviewContainer = _reviewService.GetReviews(product.Id, page)
            };

            return View(pageModel);
        }

        [HttpPost]
        [Route("{controller}/{action}")]
        public IActionResult AddReview(ProductPageModel pageModel)
        {
            if (pageModel.NewReview.ProductID == null || pageModel.NewReview.Username == null || pageModel.NewReview.Rating < 1 || pageModel.NewReview.Rating > 5 || pageModel.NewReview.Text == null)
            {
                return BadRequest();
            }
            _reviewService.AddReview(pageModel.NewReview);

            Product product = _productService.GetProductById(pageModel.NewReview.ProductID);
            pageModel.Product = product;
            pageModel.ReviewContainer = _reviewService.GetReviews(product.Id, 0);
            pageModel.NewReview.Text = "";
            pageModel.NewReview.Rating = 5;
            return View("/Views/Product/Index.cshtml", pageModel);
        }
    }
}
