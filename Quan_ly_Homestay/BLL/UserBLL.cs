using System;
using System.Data;
using Quan_ly_Homestay.DAL;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.BLL
{
    /// <summary>
    /// Business logic cho Quản lý nhân sự (chỉ Admin)
    /// </summary>
    public class UserBLL
    {
        private readonly UserDAL userDAL;

        public UserBLL()
        {
            userDAL = new UserDAL();
        }

        // ── Phân quyền ────────────────────────────────────────────────────────

        /// <summary>Kiểm tra người dùng hiện tại có phải Admin không</summary>
        public static bool KiemTraAdmin(CurrentUser user)
        {
            return user != null && user.IsAdmin;
        }

        /// <summary>Throw exception nếu không phải Admin</summary>
        public static void YeuCauAdmin(CurrentUser user)
        {
            if (!KiemTraAdmin(user))
                throw new UnauthorizedAccessException("Chỉ Admin mới có quyền thực hiện thao tác này!");
        }

        // ── CRUD Operations ───────────────────────────────────────────────────

        /// <summary>Lấy tất cả nhân viên (DataTable cho DataGridView)</summary>
        public DataTable LayDanhSachNhanVien()
        {
            return userDAL.GetAllAsDataTable();
        }

        /// <summary>Lấy nhân viên theo ID</summary>
        public CurrentUser LayNhanVienTheoId(int userId)
        {
            return userDAL.GetById(userId);
        }

        /// <summary>Thêm nhân viên mới</summary>
        public bool ThemNhanVien(string username, string password, string fullName, string role, CurrentUser currentUser)
        {
            YeuCauAdmin(currentUser);

            // Validate
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Tên đăng nhập không được trống!");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Mật khẩu không được trống!");
            if (password.Length < 3)
                throw new ArgumentException("Mật khẩu phải có ít nhất 3 ký tự!");
            if (userDAL.UsernameExists(username.Trim()))
                throw new InvalidOperationException("Tên đăng nhập đã tồn tại!");

            return userDAL.Insert(username.Trim(), password.Trim(), fullName?.Trim(), role);
        }

        /// <summary>Cập nhật thông tin nhân viên</summary>
        public bool CapNhatNhanVien(int userId, string fullName, string role, string password, CurrentUser currentUser)
        {
            YeuCauAdmin(currentUser);

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Họ tên không được trống!");

            // Kiểm tra không tự hạ cấp chính mình nếu là admin cuối
            if (currentUser.UserId == userId && role != "Admin")
            {
                const string checkSql = "SELECT COUNT(1) FROM Users WHERE Role = 'Admin' AND IsActive = 1";
                using (var conn = new System.Data.SqlClient.SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new System.Data.SqlClient.SqlCommand(checkSql, conn))
                    {
                        int adminCount = (int)cmd.ExecuteScalar();
                        if (adminCount <= 1)
                            throw new InvalidOperationException("Bạn là Admin cuối cùng, không thể đổi vai trò!");
                    }
                }
            }

            return userDAL.Update(userId, fullName.Trim(), role, 
                string.IsNullOrWhiteSpace(password) ? null : password.Trim());
        }

        /// <summary>Xóa nhân viên</summary>
        public bool XoaNhanVien(int userId, CurrentUser currentUser)
        {
            YeuCauAdmin(currentUser);

            if (currentUser.UserId == userId)
                throw new InvalidOperationException("Không thể tự xóa chính mình!");

            return userDAL.SoftDelete(userId);
        }
    }
}
