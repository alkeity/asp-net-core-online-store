using Microsoft.Data.SqlClient;
using OnlineStore.Models.Domain;
using System.Xml.Linq;

namespace OnlineStore.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly string _connectionString;

        public ProductService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default");
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, ProductName FROM Products", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Product product = new Product()
                            {
                                Id = reader.GetInt64(0),
                                Name = reader.GetString(1),
                            };
                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        public Product? GetProductById(long id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, ProductName FROM Products WHERE ID = @id", conn);
                cmd.Parameters.Add("@id", System.Data.SqlDbType.BigInt).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return new Product() { Id = reader.GetInt64(0), Name = reader.GetString(1) };
                        }
                    }
                }
            }
            return null;
        }
    }
}
