using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.DAL
{
    /// <summary>
    /// Xử lý xác thực người dùng từ database
    /// </summary>
    public class UserDAL
    {
        private readonly string connectionString;

        public UserDAL()
        {
            connectionString = DatabaseConfig.ConnectionString;
        }

        /// <summary>
        /// Kiểm tra đăng nhập. Trả về CurrentUser nếu hợp lệ, null nếu sai.
        /// </summary>
        public CurrentUser Login(string username, string password)
        {
            const string sql = @"SELECT UserId, Username, FullName, Role
                                 FROM Users
                                 WHERE Username = @Username
                                   AND Password = @Password
                                   AND IsActive = 1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CurrentUser
                            {
                                UserId   = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                FullName = reader.IsDBNull(2) ? username : reader.GetString(2),
                                Role     = reader.IsDBNull(3) ? "Staff"  : reader.GetString(3)
                            };
                        }
                    }
                }
            }

            return null; // Sai thông tin
        }

        /// <summary>
        /// Kiểm tra username đã tồn tại chưa
        /// </summary>
        public bool UsernameExists(string username)
        {
            const string sql = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND IsActive = 1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        // ── CRUD cho Quản lý nhân sự ─────────────────────────────────────────

        /// <summary>Lấy tất cả nhân viên dạng DataTable (cho DataGridView)</summary>
        public DataTable GetAllAsDataTable()
        {
            const string sql = @"SELECT UserId, Username, FullName, Role, IsActive
                                 FROM Users
                                 WHERE IsActive = 1
                                 ORDER BY Role DESC, FullName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        /// <summary>Lấy user theo ID</summary>
        public CurrentUser GetById(int userId)
        {
            const string sql = @"SELECT UserId, Username, FullName, Role
                                 FROM Users
                                 WHERE UserId = @UserId AND IsActive = 1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CurrentUser
                            {
                                UserId   = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                FullName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Role     = reader.IsDBNull(3) ? "Staff" : reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>Thêm nhân viên mới</summary>
        public bool Insert(string username, string password, string fullName, string role)
        {
            const string sql = @"INSERT INTO Users (Username, Password, FullName, Role, IsActive)
                                 VALUES (@Username, @Password, @FullName, @Role, 1)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@FullName", (object)fullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role", role);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>Cập nhật thông tin nhân viên</summary>
        public bool Update(int userId, string fullName, string role, string password = null)
        {
            string sql;
            if (string.IsNullOrEmpty(password))
            {
                sql = @"UPDATE Users SET FullName = @FullName, Role = @Role
                        WHERE UserId = @UserId AND IsActive = 1";
            }
            else
            {
                sql = @"UPDATE Users SET FullName = @FullName, Role = @Role, Password = @Password
                        WHERE UserId = @UserId AND IsActive = 1";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@FullName", (object)fullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role", role);
                    if (!string.IsNullOrEmpty(password))
                        cmd.Parameters.AddWithValue("@Password", password);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>Xóa mềm nhân viên (không xóa Admin cuối cùng)</summary>
        public bool SoftDelete(int userId)
        {
            // Kiểm tra không phải admin cuối cùng
            const string checkSql = @"SELECT COUNT(1) FROM Users 
                                       WHERE Role = 'Admin' AND IsActive = 1";
            const string deleteSql = "UPDATE Users SET IsActive = 0 WHERE UserId = @UserId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                // Kiểm tra có phải admin cuối không
                using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                {
                    int adminCount = (int)checkCmd.ExecuteScalar();
                    
                    // Lấy role của user cần xóa
                    const string getRoleSql = "SELECT Role FROM Users WHERE UserId = @UserId AND IsActive = 1";
                    using (SqlCommand getRoleCmd = new SqlCommand(getRoleSql, conn))
                    {
                        getRoleCmd.Parameters.AddWithValue("@UserId", userId);
                        object roleObj = getRoleCmd.ExecuteScalar();
                        if (roleObj != null && roleObj.ToString() == "Admin" && adminCount <= 1)
                            throw new InvalidOperationException("Không thể xóa Admin cuối cùng!");
                    }
                }

                using (SqlCommand cmd = new SqlCommand(deleteSql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
