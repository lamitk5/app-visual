using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.DAL
{
    public class HomestayDAL
    {
        private readonly string connectionString;

        public HomestayDAL()
        {
            connectionString = DatabaseConfig.ConnectionString;
        }

        // Lấy tất cả homestays đang hoạt động
        public List<Homestay> LayDanhSachHomestay()
        {
            List<Homestay> danhSach = new List<Homestay>();

            const string sql = @"SELECT HomestayId, HomestayName, Address, Phone, IsActive 
                                 FROM Homestays 
                                 WHERE IsActive = 1
                                 ORDER BY HomestayName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new Homestay
                            {
                                HomestayId = reader.GetInt32(0),
                                HomestayName = reader.GetString(1),
                                Address = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                IsActive = reader.GetBoolean(4)
                            });
                        }
                    }
                }
            }

            return danhSach;
        }

        // Lấy homestay theo ID
        public Homestay LayHomestayTheoId(int homestayId)
        {
            const string sql = @"SELECT HomestayId, HomestayName, Address, Phone, IsActive 
                                 FROM Homestays 
                                 WHERE HomestayId = @HomestayId AND IsActive = 1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HomestayId", homestayId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Homestay
                            {
                                HomestayId = reader.GetInt32(0),
                                HomestayName = reader.GetString(1),
                                Address = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                IsActive = reader.GetBoolean(4)
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Lấy phòng theo HomestayId
        public List<Phong> LayDanhSachPhongTheoHomestay(int homestayId)
        {
            List<Phong> danhSachPhong = new List<Phong>();

            const string sql = @"SELECT r.RoomId, r.RoomName, r.RoomTypeId, r.HomestayId, 
                                         r.Status, r.IsActive,
                                         rt.TypeName, rt.BasePrice,
                                         h.HomestayName
                                  FROM Rooms r
                                  INNER JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                                  INNER JOIN Homestays h ON r.HomestayId = h.HomestayId
                                  WHERE r.HomestayId = @HomestayId AND r.IsActive = 1
                                  ORDER BY r.RoomName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HomestayId", homestayId);
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

        private Phong MapReaderToPhong(SqlDataReader reader)
        {
            return new Phong
            {
                RoomId      = reader.GetInt32(0),
                RoomName    = reader.GetString(1),
                RoomTypeId  = reader.GetInt32(2),
                HomestayId  = reader.GetInt32(3),
                Status      = reader.GetString(4),
                IsActive    = reader.GetBoolean(5),
                TypeName    = reader.GetString(6),
                BasePrice   = reader.GetDecimal(7),
                HomestayName = reader.GetString(8)
            };
        }

        // ── CRUD cho FormHomestay ────────────────────────────────────────────

        /// <summary>Lấy tất cả homestay dưới dạng DataTable (cho DataGridView)</summary>
        public DataTable GetAllAsDataTable()
        {
            const string sql = @"SELECT HomestayId, HomestayName, Address, Phone
                                 FROM Homestays
                                 WHERE IsActive = 1
                                 ORDER BY HomestayName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable GetActiveForSelection()
        {
            const string sql = @"SELECT HomestayId, HomestayName, Address
                                 FROM Homestays
                                 WHERE IsActive = 1
                                 ORDER BY HomestayName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable GetActiveNames()
        {
            const string sql = @"SELECT HomestayId, HomestayName
                                 FROM Homestays
                                 WHERE IsActive = 1
                                 ORDER BY HomestayName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        /// <summary>Thêm homestay mới</summary>
        public bool ThemHomestay(string name, string address, string phone)
        {
            const string sql = @"INSERT INTO Homestays (HomestayName, Address, Phone, IsActive)
                                 VALUES (@Name, @Address, @Phone, 1)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name",    name);
                    cmd.Parameters.AddWithValue("@Address", (object)address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone",   (object)phone   ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>Sửa thông tin homestay</summary>
        public bool SuaHomestay(int homestayId, string name, string address, string phone)
        {
            const string sql = @"UPDATE Homestays
                                 SET HomestayName = @Name,
                                     Address      = @Address,
                                     Phone        = @Phone
                                 WHERE HomestayId = @HomestayId AND IsActive = 1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HomestayId", homestayId);
                    cmd.Parameters.AddWithValue("@Name",       name);
                    cmd.Parameters.AddWithValue("@Address",    (object)address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone",      (object)phone   ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>Xóa mềm homestay (IsActive = 0)</summary>
        public bool XoaMem(int homestayId)
        {
            const string sql = "UPDATE Homestays SET IsActive = 0 WHERE HomestayId = @HomestayId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HomestayId", homestayId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
