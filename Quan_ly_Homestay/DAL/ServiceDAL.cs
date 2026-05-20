using System.Data;
using System.Data.SqlClient;

namespace Quan_ly_Homestay.DAL
{
    public class ServiceDAL
    {
        private readonly string connectionString;

        public ServiceDAL()
        {
            connectionString = DatabaseConfig.ConnectionString;
        }

        public DataTable GetAllAsDataTable()
        {
            const string sql = @"SELECT ServiceId, ServiceName, Price, Unit, IsActive
                                 FROM Services
                                 WHERE IsActive = 1
                                 ORDER BY ServiceName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
    }
}
