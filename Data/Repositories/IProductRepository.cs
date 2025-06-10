using OnlineStore.Models.Entities;

namespace OnlineStore.Data.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        Product? GetById(long id);
    }
}
