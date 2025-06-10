using Microsoft.Data.SqlClient;
using OnlineStore.Data.Repositories;
using OnlineStore.Models.Entities;
using System.Xml.Linq;

namespace OnlineStore.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> GetProducts()
        {
            return _repository.GetAll();
        }

        public Product? GetProductById(long id)
        {
            return _repository.GetById(id);
        }
    }
}
