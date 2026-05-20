using System;

namespace Quan_ly_Homestay.Model
{
    /// <summary>
    /// DTO cho thông tin phòng (dùng cho BookingUI)
    /// </summary>
    public class RoomDTO
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1}) - {2:N0} VNĐ", RoomNumber, RoomType, Price);
        }
    }

    /// <summary>
    /// DTO cho thông tin khách hàng
    /// </summary>
    public class CustomerDTO
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdCard { get; set; }
    }

    /// <summary>
    /// DTO cho thông tin đặt phòng
    /// </summary>
    public class BookingDTO
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Deposit { get; set; }
        public decimal RoomFee { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal TotalAmount { get; set; }
        public int DaysStayed { get; set; }
    }
}
