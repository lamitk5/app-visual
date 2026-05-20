using System;

namespace Quan_ly_Homestay.Model
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string TypeName { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"{TypeName} - {BasePrice:N0} VNĐ";
        }
    }
}
