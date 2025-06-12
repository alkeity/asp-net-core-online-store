using Microsoft.Data.SqlClient;
using OnlineStore.Models.Containers;
using OnlineStore.Models.Entities;

namespace OnlineStore.Data.Repositories.Implementations
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly int PRODUCTS_PER_PAGE_DEFAULT;

        public ProductRepository(IConfiguration config) : base(config)
        {
            PRODUCTS_PER_PAGE_DEFAULT = config.GetValue<int>("ProductsPerPage");
        }

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

        public Page<Product> GetAll(int page = 0, int amount = -1)
        {
            if (page < 0) throw new ArgumentOutOfRangeException("Page number cannot be negative.");
            if (amount < 0) amount = PRODUCTS_PER_PAGE_DEFAULT;

            List<Product> products = new List<Product>();
            int productsTotal = 0;

            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                    (
                    "SELECT ID, ProductName, Description, Price FROM Products " +
                    "ORDER BY ID OFFSET @rowSkip ROWS FETCH NEXT @amount ROWS ONLY",
                    conn
                    );
                cmd.Parameters.Add("rowSkip", System.Data.SqlDbType.Int).Value = page * amount;
                cmd.Parameters.Add("amount", System.Data.SqlDbType.Int).Value = amount;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read()) products.Add(ReadProduct(reader));
                    }
                }

                cmd.CommandText = "SELECT COUNT(*) FROM Products";
                productsTotal = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return new Page<Product>()
            {
                CurPage = page,
                MaxPage = Convert.ToInt32(Math.Ceiling((double)(productsTotal / amount))),
                Items = products,
                ItemAmount = amount
            };
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
