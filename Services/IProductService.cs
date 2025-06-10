using OnlineStore.Models.Entities;

namespace OnlineStore.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product? GetProductById(long id);
    }
}
