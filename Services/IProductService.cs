using OnlineStore.Data.Models;
using OnlineStore.Models.Containers;

namespace OnlineStore.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Page<Product> GetProducts(int page, int amount = -1);
        Product? GetProductById(long id);
    }
}
