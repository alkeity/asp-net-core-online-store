using Microsoft.Data.SqlClient;
using OnlineStore.Models.Entities;

namespace OnlineStore.Data.Repositories.Implementations
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IConfiguration config) : base(config) { }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, ProductName, Description, Price FROM Products", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read()) products.Add(ReadProduct(reader));
                    }
                }
            }

            return products;
        }

        public Product? GetById(long id)
        {
            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, ProductName, Description, Price FROM Products WHERE ID = @id", conn);
                cmd.Parameters.Add("@id", System.Data.SqlDbType.BigInt).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return ReadProduct(reader);
                    }
                }
            }
            return null;
        }

        private Product ReadProduct(SqlDataReader reader)
        {
            return new Product()
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                Description = !reader.IsDBNull(2) ? reader.GetString(2) : null,
                Price = reader.GetDecimal(3)
            };
        }
    }
}
