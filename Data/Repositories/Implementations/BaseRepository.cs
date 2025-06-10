using Microsoft.Data.SqlClient;

namespace OnlineStore.Data.Repositories.Implementations
{
    public class BaseRepository
    {
        private static readonly string CONN_STR_DEFAULT = "Default";
        private readonly string _connStr;

        public BaseRepository(IConfiguration config)
        {
            string tmpStr = config.GetConnectionString(CONN_STR_DEFAULT);

            if (tmpStr == null) throw new MissingFieldException("Failed to get default connection string.");
            _connStr = tmpStr;
        }

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connStr);
        }
    }
}
