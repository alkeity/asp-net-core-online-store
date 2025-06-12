using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.Containers;
using OnlineStore.Models.Entities;
using OnlineStore.Models.View;
using OnlineStore.Services;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IProductService productService, int page = 0)
        {
            // TODO user choice of products per page
            page = Math.Clamp(page, 0, int.MaxValue);
            Page<Product> result = productService.GetProducts(page);

            return View(result);
        }
    }
}
