using OnlineStore.Models.Containers;
using OnlineStore.Models.Entities;

namespace OnlineStore.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Page<Product> GetProducts(int page);
        Product? GetProductById(long id);
    }
}
