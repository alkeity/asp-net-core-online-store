using Microsoft.Data.SqlClient;
using OnlineStore.Data;
using OnlineStore.Data.Models;
using OnlineStore.Models.Containers;
using System.Xml.Linq;

namespace OnlineStore.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        private readonly int PRODUCTS_PER_PAGE_DEFAULT;

        public ProductService(IConfiguration config, AppDbContext db)
        {
            _db = db;
            PRODUCTS_PER_PAGE_DEFAULT = config.GetValue<int>("ProductsPerPage");
        }

        public List<Product> GetProducts()
        {
            return _db.Products.ToList();
        }

        public Page<Product> GetProducts(int page, int amount = -1)
        {
            if (amount < 0) amount = PRODUCTS_PER_PAGE_DEFAULT;
            double pages = _db.Products.Count() / amount;

            return new Page<Product>()
            {
                CurPage = page,
                MaxPage = Convert.ToInt32(Math.Ceiling(pages)),
                Items = _db.Products.ToList(), // PagedList.MVC?
                ItemAmount = amount
            };
        }

        public Product? GetProductById(long id)
        {
            return _db.Products.Find(id);
        }
    }
}
