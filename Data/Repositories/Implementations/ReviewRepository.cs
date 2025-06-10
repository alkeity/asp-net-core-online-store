using Microsoft.Data.SqlClient;
using OnlineStore.Models.Entities;

namespace OnlineStore.Data.Repositories.Implementations
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(IConfiguration config) : base(config) { }

        public void Create(Review review)
        {
            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                    (
                    "INSERT INTO Reviews (ProductID, PostDate, Username, Rating, ReviewText)" +
                    "VALUES (@productID, @date, @username, @rating, @text)",
                    conn
                    );
                cmd.Parameters.Add("@productID", System.Data.SqlDbType.BigInt).Value = review.ProductID;
                cmd.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = review.Date;
                cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 150).Value = review.Username;
                cmd.Parameters.Add("@rating", System.Data.SqlDbType.TinyInt).Value = review.Rating;
                cmd.Parameters.Add("@text", System.Data.SqlDbType.NText).Value = review.Text;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong while adding new review to db: {ex.Message}");
                }
            }
        }

        public List<Review> GetAllForProduct(long productId)
        {
            List<Review> reviews = new List<Review>();

            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                    (
                    "SELECT ID, PostDate, Username, Rating, ReviewText FROM Reviews WHERE ProductID = @productID",
                    conn
                    );
                cmd.Parameters.Add("@productID", System.Data.SqlDbType.BigInt).Value = productId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read()) reviews.Add(ReadReview(reader, productId));
                    }
                }
            }

            return reviews;
        }

        private Review ReadReview(SqlDataReader reader, long productID)
        {
            return new Review()
            {
                Id = reader.GetInt64(0),
                ProductID = productID,
                Date = reader.GetDateTime(1),
                Username = reader.GetString(2),
                Rating = reader.GetByte(3),
                Text = !reader.IsDBNull(4) ? reader.GetString(4) : ""
            }
            ;
        }
    }
}
