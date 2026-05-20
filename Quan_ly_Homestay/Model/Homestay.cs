using System;

namespace Quan_ly_Homestay.Model
{
    public class Homestay
    {
        public int HomestayId { get; set; }
        public string HomestayName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }

        // Display property: Tên kèm địa chỉ
        public string DisplayName => $"{HomestayName} - {Address}";
    }
}
