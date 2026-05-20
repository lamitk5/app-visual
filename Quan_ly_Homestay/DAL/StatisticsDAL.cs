using System;
using System.Data;
using System.Data.SqlClient;

namespace Quan_ly_Homestay.DAL
{
    public class StatisticsDAL
    {
        private readonly string connectionString;

        public StatisticsDAL()
        {
            connectionString = DatabaseConfig.ConnectionString;
        }

        public DataTable GetDailyRevenue(DateTime fromDate, DateTime toDate)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(
                @"SELECT
                    CAST(ISNULL(b.PaymentDate, b.CheckInDate) AS DATE) AS Ngay,
                    ISNULL(SUM(b.TotalAmount + ISNULL(b.Deposit, 0)), 0) AS Revenue,
                    COUNT(b.BookingId) AS BookingCount,
                    CASE WHEN COUNT(b.BookingId) > 0
                         THEN ISNULL(SUM(b.TotalAmount + ISNULL(b.Deposit, 0)), 0) / COUNT(b.BookingId)
                         ELSE 0 END AS AvgPerBooking
                  FROM Bookings b
                  WHERE b.IsDeleted = 0
                    AND b.Status IN ('CheckedIn', 'CheckedOut')
                    AND CAST(ISNULL(b.PaymentDate, b.CheckInDate) AS DATE) >= CAST(@FromDate AS DATE)
                    AND CAST(ISNULL(b.PaymentDate, b.CheckInDate) AS DATE) <= CAST(@ToDate AS DATE)
                  GROUP BY CAST(ISNULL(b.PaymentDate, b.CheckInDate) AS DATE)
                  ORDER BY Ngay", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                adapter.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                adapter.Fill(table);
            }
            return table;
        }

        public DataTable GetRevenueByHomestay(DateTime fromDate, DateTime toDate)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(
                @"SELECT
                    h.HomestayName,
                    ISNULL(SUM(b.TotalAmount + ISNULL(b.Deposit, 0)), 0) AS Revenue,
                    COUNT(b.BookingId) AS BookingCount
                  FROM Homestays h
                  LEFT JOIN Rooms r ON h.HomestayId = r.HomestayId
                  LEFT JOIN Bookings b ON r.RoomId = b.RoomId
                    AND b.IsDeleted = 0
                    AND b.Status IN ('CheckedIn', 'CheckedOut')
                    AND CAST(ISNULL(b.PaymentDate, b.CheckInDate) AS DATE) >= CAST(@FromDate AS DATE)
                    AND CAST(ISNULL(b.PaymentDate, b.CheckInDate) AS DATE) <= CAST(@ToDate AS DATE)
                  WHERE h.IsActive = 1
                  GROUP BY h.HomestayId, h.HomestayName
                  HAVING ISNULL(SUM(b.TotalAmount + ISNULL(b.Deposit, 0)), 0) > 0
                  ORDER BY Revenue DESC", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                adapter.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                adapter.Fill(table);
            }
            return table;
        }
    }
}
