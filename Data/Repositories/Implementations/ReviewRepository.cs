using Microsoft.Data.SqlClient;
using OnlineStore.Models.Containers;
using OnlineStore.Models.Entities;

namespace OnlineStore.Data.Repositories.Implementations
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        private readonly int REVIEWS_PER_PAGE_DEFAULT;

        public ReviewRepository(IConfiguration config) : base(config)
        {
            REVIEWS_PER_PAGE_DEFAULT = config.GetValue<int>("ReviewsPerPage");
        }

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

                cmd.ExecuteNonQuery();
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

        public Page<Review> GetAllForProduct(long productId, int page, int amount)
        {
            if (page < 0) throw new ArgumentOutOfRangeException("Page number cannot be negative.");
            if (amount < 0) amount = REVIEWS_PER_PAGE_DEFAULT;

            List<Review> reviews = new List<Review>();
            int reviewsTotal = 0;

            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                    (
                    "SELECT ID, PostDate, Username, Rating, ReviewText FROM Reviews WHERE ProductID = @productID " +
                    "ORDER BY ID OFFSET @rowSkip ROWS FETCH NEXT @amount ROWS ONLY",
                    conn
                    );
                cmd.Parameters.Add("@productID", System.Data.SqlDbType.BigInt).Value = productId;
                cmd.Parameters.Add("rowSkip", System.Data.SqlDbType.Int).Value = page * amount;
                cmd.Parameters.Add("amount", System.Data.SqlDbType.Int).Value = amount;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read()) reviews.Add(ReadReview(reader, productId));
                    }
                }

                cmd.CommandText = "SELECT COUNT(*) FROM Reviews WHERE ProductID = @productID";
                reviewsTotal = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return new Page<Review>()
            {
                CurPage = page,
                MaxPage = Convert.ToInt32(Math.Ceiling((double)(reviewsTotal / amount))),
                Items = reviews,
                ItemAmount = amount
            };
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
