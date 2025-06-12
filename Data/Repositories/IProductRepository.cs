using OnlineStore.Models.Containers;
using OnlineStore.Models.Entities;

namespace OnlineStore.Data.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Page<Product> GetAll(int page = 0, int amount = -1);
        Product? GetById(long id);
    }
}
