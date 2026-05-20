using System;
using System.Data;
using System.Data.SqlClient;

namespace Quan_ly_Homestay.DAL
{
    public class InvoiceDAL
    {
        private readonly string connectionString;

        public InvoiceDAL()
        {
            connectionString = DatabaseConfig.ConnectionString;
        }

        public int GetOrCreateTempInvoice(int roomId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT InvoiceID FROM TempInvoices WHERE RoomID = @RoomID AND Status = N'Chưa thanh toán'", conn))
                {
                    cmd.Parameters.AddWithValue("@RoomID", roomId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        return Convert.ToInt32(result);
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO TempInvoices (RoomID, CreatedDate, Status, TotalAmount) VALUES (@RoomID, GETDATE(), N'Chưa thanh toán', 0); SELECT SCOPE_IDENTITY();",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@RoomID", roomId);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool AddInvoiceDetail(int invoiceId, int serviceId, int quantity, decimal unitPrice)
        {
            decimal totalPrice = quantity * unitPrice;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO TempInvoiceDetails (InvoiceID, ServiceID, Quantity, UnitPrice, TotalPrice, AddedDate, IsCancelled)
                  VALUES (@InvoiceID, @ServiceID, @Quantity, @UnitPrice, @TotalPrice, GETDATE(), 0)", conn))
            {
                cmd.Parameters.AddWithValue("@InvoiceID", invoiceId);
                cmd.Parameters.AddWithValue("@ServiceID", serviceId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteInvoiceDetail(int detailId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE TempInvoiceDetails SET IsCancelled = 1 WHERE DetailID = @DetailID", conn))
            {
                cmd.Parameters.AddWithValue("@DetailID", detailId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetActiveServicesByRoom(int roomId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(
                @"SELECT td.DetailID, s.ServiceName, td.Quantity, td.UnitPrice, td.TotalPrice
                  FROM TempInvoiceDetails td
                  JOIN TempInvoices ti ON td.InvoiceID = ti.InvoiceID
                  JOIN Services s ON td.ServiceID = s.ServiceId
                  WHERE ti.RoomID = @RoomID
                    AND td.IsCancelled = 0
                    AND ti.Status = N'Chưa thanh toán'
                  ORDER BY td.AddedDate DESC", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@RoomID", roomId);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public bool ProcessCheckout(int bookingId, decimal totalAmount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand(
                        @"UPDATE Bookings
                          SET PaymentDate = GETDATE(),
                              TotalAmount = @TotalAmount,
                              Status = 'CheckedOut'
                          WHERE BookingId = @BookingId", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BookingId", bookingId);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(
                        @"UPDATE Rooms
                          SET Status = 'Cleaning'
                          WHERE RoomId = (SELECT RoomId FROM Bookings WHERE BookingId = @BookingId)
                            AND IsActive = 1", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BookingId", bookingId);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(
                        @"UPDATE TempInvoices
                          SET Status = N'Đã thanh toán', PaidDate = GETDATE()
                          WHERE RoomID = (SELECT RoomId FROM Bookings WHERE BookingId = @BookingId)
                            AND Status = N'Chưa thanh toán'", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BookingId", bookingId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Lỗi checkout: " + ex.Message, ex);
                }
            }
        }

        public DataTable GetActiveBookingByRoomId(int roomId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(
                @"SELECT
                    b.BookingId,
                    b.RoomId,
                    c.FullName AS CustomerName,
                    c.Phone AS CustomerPhone,
                    r.RoomName,
                    rt.BasePrice AS RoomPrice,
                    b.CheckInDate,
                    b.CheckOutDate,
                    b.PaymentDate,
                    b.Deposit,
                    b.Status
                  FROM Bookings b
                  JOIN Customers c ON b.CustomerId = c.CustomerId
                  JOIN Rooms r ON b.RoomId = r.RoomId
                  JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                  WHERE b.RoomId = @RoomId
                    AND b.Status = 'CheckedIn'
                    AND b.IsDeleted = 0", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@RoomId", roomId);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
    }
}
