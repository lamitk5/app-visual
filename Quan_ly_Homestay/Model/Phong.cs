using System;

namespace Quan_ly_Homestay.Model
{
    /// <summary>
    /// Trang thai phong tu database: Available, Occupied, Cleaning, Maintenance
    /// </summary>
    public enum TrangThaiPhong
    {
        Trong = 0,        // Available
        DangThue = 1,     // Occupied
        DangDonDep = 2,   // Cleaning
        BaoTri = 3,       // Maintenance
        DaDat = 4         // Reserved (tu them neu can)
    }

    public class Phong
    {
        // Tu bang Rooms
        public int RoomId { get; set; }           // Ma phong (RoomId)
        public string RoomName { get; set; }      // So phong (RoomName) - hien thi tren so do
        public int RoomTypeId { get; set; }       // Loai phong ID
        public int HomestayId { get; set; }       // Homestay ID
        public string Status { get; set; }        // Trang thai string tu DB: Available, Occupied, Cleaning, Maintenance
        public bool IsActive { get; set; }        // Xoa mem

        // Tu bang RoomTypes (JOIN)
        public string TypeName { get; set; }      // Ten loai phong: Phong Don, Phong Doi
        public decimal BasePrice { get; set; }    // Gia co ban

        // Tu bang Homestays (JOIN)
        public string HomestayName { get; set; }  // Ten homestay

        // Enum property de de su dung
        public TrangThaiPhong TrangThai
        {
            get
            {
                if (string.IsNullOrEmpty(Status))
                    return TrangThaiPhong.Trong;

                string s = Status.ToLower();
                if (s == "available")
                    return TrangThaiPhong.Trong;
                if (s == "occupied")
                    return TrangThaiPhong.DangThue;
                if (s == "cleaning")
                    return TrangThaiPhong.DangDonDep;
                if (s == "maintenance")
                    return TrangThaiPhong.BaoTri;
                return TrangThaiPhong.Trong;
            }
        }

        // Alias properties de tuong thich voi code cu
        public int MaPhong => RoomId;
        public string SoPhong => RoomName;
        public decimal GiaPhong => BasePrice;

        public string TenLoaiPhong => TypeName ?? "Khong xac dinh";

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case TrangThaiPhong.Trong: return "Trong";
                    case TrangThaiPhong.DangThue: return "Dang thue";
                    case TrangThaiPhong.DangDonDep: return "Dang don";
                    case TrangThaiPhong.BaoTri: return "Bao tri";
                    case TrangThaiPhong.DaDat: return "Da dat";
                    default: return "Khong xac dinh";
                }
            }
        }

        public ConsoleColor MauTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case TrangThaiPhong.Trong: return ConsoleColor.Green;
                    case TrangThaiPhong.DangThue: return ConsoleColor.Red;
                    case TrangThaiPhong.DangDonDep: return ConsoleColor.Yellow;
                    case TrangThaiPhong.BaoTri: return ConsoleColor.Gray;
                    case TrangThaiPhong.DaDat: return ConsoleColor.Cyan;
                    default: return ConsoleColor.White;
                }
            }
        }

        public string KyHieuTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case TrangThaiPhong.Trong: return "O";
                    case TrangThaiPhong.DangThue: return "X";
                    case TrangThaiPhong.DangDonDep: return "C";
                    case TrangThaiPhong.BaoTri: return "M";
                    case TrangThaiPhong.DaDat: return "R";
                    default: return "?";
                }
            }
        }
    }
}
