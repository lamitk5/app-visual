namespace Quan_ly_Homestay.Model
{
    /// <summary>
    /// Lưu thông tin người dùng đang đăng nhập (dùng để truyền giữa các form)
    /// </summary>
    public class CurrentUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }   // "Admin" hoặc "Staff"

        public bool IsAdmin => Role == "Admin";

        public CurrentUser() { }

        public CurrentUser(int userId, string username, string fullName, string role)
        {
            UserId   = userId;
            Username = username;
            FullName = fullName;
            Role     = role;
        }

        public override string ToString() => $"{FullName} ({Role})";
    }
}
