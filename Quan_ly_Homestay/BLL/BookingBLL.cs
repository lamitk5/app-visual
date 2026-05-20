using System;
using System.Collections.Generic;
using System.Data;
using Quan_ly_Homestay.DAL;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.BLL
{
    public class BookingBLL
    {
        private readonly BookingDAL bookingDAL;
        private readonly CustomerDAL customerDAL;

        private readonly PhongBLL phongBLL;

        public BookingBLL()
        {
            bookingDAL = new BookingDAL();
            customerDAL = new CustomerDAL();
            phongBLL = new PhongBLL();
        }

        public DataTable GetAvailableRooms(DateTime checkIn, DateTime checkOut)
        {
            if (checkOut <= checkIn)
                return new DataTable();

            // Tự động cập nhật trạng thái phòng đã quá ngày check-out
            try { phongBLL.CapNhatPhongQuaHanCheckout(); } catch { }

            return bookingDAL.GetAvailableRooms(checkIn, checkOut);
        }

        public DataTable GetAvailableRooms(int homestayId, DateTime checkIn, DateTime checkOut)
        {
            if (checkOut <= checkIn)
                return new DataTable();

            // Tự động cập nhật trạng thái phòng đã quá ngày check-out
            try { phongBLL.CapNhatPhongQuaHanCheckout(); } catch { }

            return bookingDAL.GetAvailableRoomsByHomestay(homestayId, checkIn, checkOut);
        }

        public bool ProcessBooking(string customerName, string phone, string idCard,
            int roomId, int userId, DateTime checkIn, DateTime checkOut, decimal deposit)
        {
            try
            {
                // 1. Find or create customer
                int customerId = customerDAL.FindOrCreateCustomer(customerName, phone, idCard);

                // 2. Create booking
                bookingDAL.AddBooking(customerId, roomId, userId, checkIn, checkOut, deposit);
                return true;
            }
            catch (Exception ex)
            {
                // Throw chi tiết lỗi để UI hiển thị
                throw new Exception("Lỗi khi đặt phòng: " + ex.Message, ex);
            }
        }

        public DataTable GetBookingHistory(int homestayId, string keyword, string status)
        {
            return bookingDAL.GetBookingHistory(homestayId, keyword?.Trim(), status?.Trim());
        }
    }
}
