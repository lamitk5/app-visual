using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.DAL
{
    public class PhongDAL
    {
        private readonly string connectionString;

        public PhongDAL()
        {
            connectionString = DatabaseConfig.ConnectionString;
        }

        public List<Phong> LayDanhSachPhong()
        {
            List<Phong> danhSachPhong = new List<Phong>();

            const string sql = @"SELECT r.RoomId, r.RoomName, r.RoomTypeId, r.HomestayId, 
                                         r.Status, r.IsActive,
                                         rt.TypeName, rt.BasePrice,
                                         h.HomestayName
                                  FROM Rooms r
                                  INNER JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                                  INNER JOIN Homestays h ON r.HomestayId = h.HomestayId
                                  WHERE r.IsActive = 1
                                  ORDER BY r.RoomName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachPhong.Add(MapReaderToPhong(reader));
                        }
                    }
                }
            }

            return danhSachPhong;
        }

        public List<Phong> LayDanhSachPhongTheoTrangThai(TrangThaiPhong trangThai)
        {
            List<Phong> danhSachPhong = new List<Phong>();

            string statusString;
            switch (trangThai)
            {
                case TrangThaiPhong.Trong: statusString = "Available"; break;
                case TrangThaiPhong.DangThue: statusString = "Occupied"; break;
                case TrangThaiPhong.DangDonDep: statusString = "Cleaning"; break;
                case TrangThaiPhong.BaoTri: statusString = "Maintenance"; break;
                default: statusString = "Available"; break;
            }

            const string sql = @"SELECT r.RoomId, r.RoomName, r.RoomTypeId, r.HomestayId, 
                                         r.Status, r.IsActive,
                                         rt.TypeName, rt.BasePrice,
                                         h.HomestayName
                                  FROM Rooms r
                                  INNER JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                                  INNER JOIN Homestays h ON r.HomestayId = h.HomestayId
                                  WHERE r.Status = @Status AND r.IsActive = 1
                                  ORDER BY r.RoomName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", statusString);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachPhong.Add(MapReaderToPhong(reader));
                        }
                    }
                }
            }

            return danhSachPhong;
        }

        public Phong LayPhongTheoMa(int roomId)
        {
            const string sql = @"SELECT r.RoomId, r.RoomName, r.RoomTypeId, r.HomestayId, 
                                         r.Status, r.IsActive,
                                         rt.TypeName, rt.BasePrice,
                                         h.HomestayName
                                  FROM Rooms r
                                  INNER JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                                  INNER JOIN Homestays h ON r.HomestayId = h.HomestayId
                                  WHERE r.RoomId = @RoomId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomId", roomId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToPhong(reader);
                        }
                    }
                }
            }

            return null;
        }

        public Phong LayPhongTheoTen(string roomName)
        {
            const string sql = @"SELECT r.RoomId, r.RoomName, r.RoomTypeId, r.HomestayId, 
                                         r.Status, r.IsActive,
                                         rt.TypeName, rt.BasePrice,
                                         h.HomestayName
                                  FROM Rooms r
                                  INNER JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                                  INNER JOIN Homestays h ON r.HomestayId = h.HomestayId
                                  WHERE r.RoomName = @RoomName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomName", roomName);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToPhong(reader);
                        }
                    }
                }
            }

            return null;
        }

        public int ThemPhong(Phong phong)
        {
            const string sql = @"INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status, IsActive)
                                 VALUES (@RoomName, @RoomTypeId, @HomestayId, @Status, 1);
                                 SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomName", phong.RoomName);
                    cmd.Parameters.AddWithValue("@RoomTypeId", phong.RoomTypeId);
                    cmd.Parameters.AddWithValue("@HomestayId", phong.HomestayId);
                    cmd.Parameters.AddWithValue("@Status", phong.Status ?? "Available");
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool SuaPhong(Phong phong)
        {
            const string sql = @"UPDATE Rooms SET 
                                        RoomName = @RoomName,
                                        RoomTypeId = @RoomTypeId,
                                        HomestayId = @HomestayId
                                 WHERE RoomId = @RoomId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomName", phong.RoomName);
                    cmd.Parameters.AddWithValue("@RoomTypeId", phong.RoomTypeId);
                    cmd.Parameters.AddWithValue("@HomestayId", phong.HomestayId);
                    cmd.Parameters.AddWithValue("@RoomId", phong.RoomId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool XoaPhong(int roomId)
        {
            // Xoa mem - set IsActive = 0
            const string sql = "UPDATE Rooms SET IsActive = 0 WHERE RoomId = @RoomId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomId", roomId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool CapNhatTrangThaiPhong(int roomId, TrangThaiPhong trangThaiMoi)
        {
            string statusString;
            switch (trangThaiMoi)
            {
                case TrangThaiPhong.Trong: statusString = "Available"; break;
                case TrangThaiPhong.DangThue: statusString = "Occupied"; break;
                case TrangThaiPhong.DangDonDep: statusString = "Cleaning"; break;
                case TrangThaiPhong.BaoTri: statusString = "Maintenance"; break;
                default: statusString = "Available"; break;
            }

            const string sql = @"UPDATE Rooms SET Status = @Status WHERE RoomId = @RoomId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomId", roomId);
                    cmd.Parameters.AddWithValue("@Status", statusString);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool CapNhatGiaPhong(int roomTypeId, decimal giaMoi)
        {
            // Cap nhat gia trong bang RoomTypes
            const string sql = @"UPDATE RoomTypes SET BasePrice = @BasePrice WHERE RoomTypeId = @RoomTypeId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomTypeId", roomTypeId);
                    cmd.Parameters.AddWithValue("@BasePrice", giaMoi);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Phong> TimKiemPhong(string tuKhoa)
        {
            List<Phong> danhSachPhong = new List<Phong>();

            const string sql = @"SELECT r.RoomId, r.RoomName, r.RoomTypeId, r.HomestayId, 
                                         r.Status, r.IsActive,
                                         rt.TypeName, rt.BasePrice,
                                         h.HomestayName
                                  FROM Rooms r
                                  INNER JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                                  INNER JOIN Homestays h ON r.HomestayId = h.HomestayId
                                  WHERE r.RoomName LIKE @TuKhoa OR rt.TypeName LIKE @TuKhoa
                                  ORDER BY r.RoomName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachPhong.Add(MapReaderToPhong(reader));
                        }
                    }
                }
            }

            return danhSachPhong;
        }

        /// <summary>
        /// Tự động cập nhật trạng thái phòng đã quá ngày check-out.
        /// Các booking đã quá CheckOutDate sẽ chuyển thành CheckedOut, phòng chuyển thành Available.
        /// Trả về số lượng booking được cập nhật.
        /// </summary>
        public int CapNhatPhongQuaHanCheckout()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Cập nhật các booking đã quá ngày check-out thành CheckedOut
                    string updateBookings = @"
                        UPDATE Bookings
                        SET Status = 'CheckedOut'
                        WHERE Status = 'CheckedIn'
                        AND IsDeleted = 0
                        AND CheckOutDate IS NOT NULL
                        AND CAST(CheckOutDate AS DATE) < CAST(GETDATE() AS DATE)";

                    int bookingCount;
                    using (SqlCommand cmd = new SqlCommand(updateBookings, conn, transaction))
                    {
                        bookingCount = cmd.ExecuteNonQuery();
                    }

                    // 2. Cập nhật trạng thái phòng: chỉ những phòng Occupied mà không còn booking CheckedIn nào
                    if (bookingCount > 0)
                    {
                        string updateRooms = @"
                            UPDATE Rooms
                            SET Status = 'Available'
                            WHERE Status = 'Occupied' AND IsActive = 1
                            AND RoomId NOT IN (
                                SELECT DISTINCT RoomId FROM Bookings
                                WHERE Status = 'CheckedIn' AND IsDeleted = 0
                            )";

                        using (SqlCommand cmd = new SqlCommand(updateRooms, conn, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return bookingCount;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Dictionary<TrangThaiPhong, int> ThongKePhongTheoTrangThai()
        {
            Dictionary<TrangThaiPhong, int> thongKe = new Dictionary<TrangThaiPhong, int>();

            const string sql = @"SELECT Status, COUNT(*) as SoLuong 
                                 FROM Rooms 
                                 WHERE IsActive = 1
                                 GROUP BY Status";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string status = reader.GetString(0);
                            int soLuong = reader.GetInt32(1);
                            
                            TrangThaiPhong trangThai;
                            string s = status?.ToLower() ?? "";
                            if (s == "available")
                                trangThai = TrangThaiPhong.Trong;
                            else if (s == "occupied")
                                trangThai = TrangThaiPhong.DangThue;
                            else if (s == "cleaning")
                                trangThai = TrangThaiPhong.DangDonDep;
                            else if (s == "maintenance")
                                trangThai = TrangThaiPhong.BaoTri;
                            else
                                trangThai = TrangThaiPhong.Trong;
                            
                            thongKe[trangThai] = soLuong;
                        }
                    }
                }
            }

            return thongKe;
        }

        public List<RoomType> LayDanhSachLoaiPhong()
        {
            List<RoomType> danhSach = new List<RoomType>();

            const string sql = @"SELECT RoomTypeId, TypeName, BasePrice, IsActive
                                 FROM RoomTypes
                                 WHERE IsActive = 1
                                 ORDER BY TypeName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new RoomType
                            {
                                RoomTypeId = reader.GetInt32(0),
                                TypeName = reader.GetString(1),
                                BasePrice = reader.GetDecimal(2),
                                IsActive = reader.GetBoolean(3)
                            });
                        }
                    }
                }
            }

            return danhSach;
        }

        private Phong MapReaderToPhong(SqlDataReader reader)
        {
            return new Phong
            {
                RoomId = reader.GetInt32(0),
                RoomName = reader.GetString(1),
                RoomTypeId = reader.GetInt32(2),
                HomestayId = reader.GetInt32(3),
                Status = reader.GetString(4),
                IsActive = reader.GetBoolean(5),
                TypeName = reader.GetString(6),
                BasePrice = reader.GetDecimal(7),
                HomestayName = reader.GetString(8)
            };
        }
    }
}
