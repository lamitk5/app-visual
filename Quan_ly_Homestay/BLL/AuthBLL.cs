using System;
using Quan_ly_Homestay.DAL;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.BLL
{
    /// <summary>
    /// Business logic cho xác thực và quản lý phiên đăng nhập
    /// </summary>
    public class AuthBLL
    {
        private readonly UserDAL userDAL;

        public AuthBLL()
        {
            userDAL = new UserDAL();
        }

        /// <summary>
        /// Đăng nhập. Trả về CurrentUser nếu hợp lệ, null nếu sai.
        /// </summary>
        public CurrentUser Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Tên đăng nhập không được trống!");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Mật khẩu không được trống!");

            return userDAL.Login(username.Trim(), password.Trim());
        }
    }
}
