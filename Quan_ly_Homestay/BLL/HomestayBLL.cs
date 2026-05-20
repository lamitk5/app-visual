using System;
using System.Collections.Generic;
using System.Data;
using Quan_ly_Homestay.DAL;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.BLL
{
    public class HomestayBLL
    {
        private readonly HomestayDAL homestayDAL;

        public HomestayBLL()
        {
            homestayDAL = new HomestayDAL();
        }

        // ── Dùng cho Room.cs (trả về List) ──────────────────────────────────

        public List<Homestay> LayDanhSachHomestay()
        {
            return homestayDAL.LayDanhSachHomestay();
        }

        public Homestay LayHomestayTheoId(int homestayId)
        {
            return homestayDAL.LayHomestayTheoId(homestayId);
        }

        public List<Phong> LayDanhSachPhongTheoHomestay(int homestayId)
        {
            return homestayDAL.LayDanhSachPhongTheoHomestay(homestayId);
        }

        // ── Dùng cho FormHomestay.cs (CRUD) ─────────────────────────────────

        /// <summary>Lấy tất cả homestay dưới dạng DataTable để hiển thị DataGridView</summary>
        public DataTable GetAll()
        {
            return homestayDAL.GetAllAsDataTable();
        }

        public DataTable GetActiveForSelection()
        {
            return homestayDAL.GetActiveForSelection();
        }

        public DataTable GetActiveNames()
        {
            return homestayDAL.GetActiveNames();
        }

        /// <summary>Thêm homestay mới</summary>
        public bool Add(string name, string address, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên Homestay không được trống!");

            return homestayDAL.ThemHomestay(name.Trim(), address?.Trim(), phone?.Trim());
        }

        /// <summary>Sửa thông tin homestay</summary>
        public bool Update(int homestayId, string name, string address, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên Homestay không được trống!");

            return homestayDAL.SuaHomestay(homestayId, name.Trim(), address?.Trim(), phone?.Trim());
        }

        /// <summary>Xóa mềm homestay (IsActive = 0)</summary>
        public bool SoftDelete(int homestayId)
        {
            return homestayDAL.XoaMem(homestayId);
        }
    }
}
