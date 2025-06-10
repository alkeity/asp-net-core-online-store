using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.Entities;
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
        public IActionResult Index(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            Product? product = _productService.GetProductById((int) id);

            if (product == null)
                return RedirectToAction("Index", "Home");

            ProductPageModel pageModel = new ProductPageModel()
            {
                Product = product,
                Reviews = _reviewService.GetReviews(product.Id, 10),
                NewReview = new Review() { ProductID = product.Id, Rating = 5, Text = "", Username = "Anonymous" }
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
            pageModel.Reviews = _reviewService.GetReviews(product.Id, 10);
            pageModel.NewReview.Text = "";
            pageModel.NewReview.Rating = 5;
            return View("/Views/Product/Index.cshtml", pageModel);
        }
    }
}
