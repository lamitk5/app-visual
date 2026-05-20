using System;
using System.Data;
using System.Data.SqlClient;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.DAL
{
    public class CustomerDAL
    {
        private readonly string connectionString;

        public CustomerDAL()
        {
            connectionString = DatabaseConfig.ConnectionString;
        }

        public int AddCustomer(string fullName, string phone, string idCard)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Customers (FullName, Phone, IdCard) 
                                 OUTPUT INSERTED.CustomerId 
                                 VALUES (@FullName, @Phone, @IdCard)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdCard", (object)idCard ?? DBNull.Value);

                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public int FindOrCreateCustomer(string fullName, string phone, string idCard)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Try to find existing customer by IdCard (only if provided)
                if (!string.IsNullOrWhiteSpace(idCard))
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT CustomerId FROM Customers WHERE IdCard = @IdCard", conn))
                    {
                        cmd.Parameters.AddWithValue("@IdCard", idCard);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            return (int)result;
                    }
                }

                // Try to find by FullName and Phone (if phone provided)
                if (!string.IsNullOrWhiteSpace(fullName) && !string.IsNullOrWhiteSpace(phone))
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT CustomerId FROM Customers WHERE FullName = @FullName AND Phone = @Phone", conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            return (int)result;
                    }
                }

                // Create new customer - if IdCard is empty, insert as NULL
                string insertQuery = @"INSERT INTO Customers (FullName, Phone, IdCard) 
                      OUTPUT INSERTED.CustomerId 
                      VALUES (@FullName, @Phone, @IdCard)";
                      
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(phone) ? DBNull.Value : (object)phone);
                    
                    // Nếu IdCard rỗng, insert NULL và bỏ qua ràng buộc UNIQUE
                    if (string.IsNullOrWhiteSpace(idCard))
                        cmd.Parameters.AddWithValue("@IdCard", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@IdCard", idCard);
                        
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }

    public class BookingDAL
    {
        private readonly string connectionString;

        public BookingDAL()
        {
            connectionString = DatabaseConfig.ConnectionString;
        }

        public int AddBooking(int customerId, int roomId, int userId, DateTime checkIn, DateTime checkOut, decimal deposit)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Thêm booking
                    string insertQuery = @"INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status) 
                                 OUTPUT INSERTED.BookingId
                                 VALUES (@CustomerId, @RoomId, @UserId, @CheckIn, @CheckOut, @Deposit, 'CheckedIn')";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction);
                    insertCmd.Parameters.AddWithValue("@CustomerId", customerId);
                    insertCmd.Parameters.AddWithValue("@RoomId", roomId);
                    insertCmd.Parameters.AddWithValue("@UserId", userId);
                    insertCmd.Parameters.AddWithValue("@CheckIn", checkIn);
                    insertCmd.Parameters.AddWithValue("@CheckOut", (object)checkOut == null ? DBNull.Value : (object)checkOut);
                    insertCmd.Parameters.AddWithValue("@Deposit", deposit);

                    int bookingId = (int)insertCmd.ExecuteScalar();

                    // 2. Cập nhật trạng thái phòng thành 'occupied'
                    string updateQuery = "UPDATE Rooms SET Status = 'occupied' WHERE RoomId = @RoomId";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction);
                    updateCmd.Parameters.AddWithValue("@RoomId", roomId);
                    updateCmd.ExecuteNonQuery();

                    transaction.Commit();
                    return bookingId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public DataTable GetAvailableRooms(DateTime checkIn, DateTime checkOut)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(
                @"SELECT r.RoomId, r.RoomName, rt.TypeName AS RoomType, rt.BasePrice AS Price
                  FROM Rooms r
                  INNER JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                  WHERE r.IsActive = 1
                  AND r.Status NOT IN ('Maintenance', 'Cleaning')
                  AND r.RoomId NOT IN (
                      SELECT RoomId FROM Bookings
                      WHERE Status IN ('Pending', 'CheckedIn') AND IsDeleted = 0
                      AND CheckInDate < @CheckOut AND (CheckOutDate IS NULL OR CheckOutDate > @CheckIn)
                  )
                  ORDER BY r.RoomName", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@CheckIn", checkIn);
                adapter.SelectCommand.Parameters.AddWithValue("@CheckOut", checkOut);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable GetAvailableRoomsByHomestay(int homestayId, DateTime checkIn, DateTime checkOut)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(
                @"SELECT r.RoomId, r.RoomName, rt.TypeName AS RoomType, rt.BasePrice AS Price
                  FROM Rooms r
                  INNER JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                  WHERE r.IsActive = 1
                  AND r.Status NOT IN ('Maintenance', 'Cleaning')
                  AND r.HomestayId = @HomestayId
                  AND r.RoomId NOT IN (
                      SELECT RoomId FROM Bookings
                      WHERE Status IN ('Pending', 'CheckedIn') AND IsDeleted = 0
                      AND CheckInDate < @CheckOut AND (CheckOutDate IS NULL OR CheckOutDate > @CheckIn)
                  )
                  ORDER BY r.RoomName", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@HomestayId", homestayId);
                adapter.SelectCommand.Parameters.AddWithValue("@CheckIn", checkIn);
                adapter.SelectCommand.Parameters.AddWithValue("@CheckOut", checkOut);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable GetBookingHistory(int homestayId, string keyword, string status)
        {
            string sql = @"SELECT
                    b.BookingId AS [Mã ĐP],
                    h.HomestayName AS [Homestay],
                    r.RoomName AS [Phòng],
                    c.FullName AS [Khách hàng],
                    c.Phone AS [SĐT],
                    b.CheckInDate AS [Ngày nhận],
                    b.CheckOutDate AS [Ngày trả],
                    b.PaymentDate AS [Ngày thanh toán],
                    rt.BasePrice AS [Giá/Ngày],
                    ISNULL((SELECT SUM(td.TotalPrice) FROM TempInvoiceDetails td JOIN TempInvoices ti ON td.InvoiceID = ti.InvoiceID WHERE ti.RoomID = r.RoomId AND td.IsCancelled = 0), 0) AS [Phí DV],
                    b.Deposit AS [Tiền cọc],
                    b.TotalAmount + ISNULL(b.Deposit, 0) AS [Tổng tiền],
                    b.Status AS [Trạng thái],
                    b.CheckInDate AS [Ngày đặt]
                FROM Bookings b
                JOIN Customers c ON b.CustomerId = c.CustomerId
                JOIN Rooms r ON b.RoomId = r.RoomId
                JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                JOIN Homestays h ON r.HomestayId = h.HomestayId
                WHERE b.IsDeleted = 0";

            if (homestayId > 0)
                sql += " AND r.HomestayId = @HomestayId";

            if (!string.IsNullOrWhiteSpace(keyword))
                sql += " AND (c.FullName LIKE @Keyword OR c.Phone LIKE @Keyword)";

            if (!string.IsNullOrWhiteSpace(status))
                sql += " AND b.Status = @Status";

            sql += " ORDER BY b.BookingId DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                if (homestayId > 0)
                    adapter.SelectCommand.Parameters.AddWithValue("@HomestayId", homestayId);
                if (!string.IsNullOrWhiteSpace(keyword))
                    adapter.SelectCommand.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                if (!string.IsNullOrWhiteSpace(status))
                    adapter.SelectCommand.Parameters.AddWithValue("@Status", status);

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
    }
}
