using System;
using System.Collections.Generic;
using Quan_ly_Homestay.DAL;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.BLL
{
    public class PhongBLL
    {
        private readonly PhongDAL phongDAL;

        public PhongBLL()
        {
            phongDAL = new PhongDAL();
        }

        public List<Phong> LayDanhSachTatCaPhong()
        {
            return phongDAL.LayDanhSachPhong();
        }

        public List<Phong> LayDanhSachPhongTrong()
        {
            return phongDAL.LayDanhSachPhongTheoTrangThai(TrangThaiPhong.Trong);
        }

        public List<Phong> LayDanhSachPhongDangThue()
        {
            return phongDAL.LayDanhSachPhongTheoTrangThai(TrangThaiPhong.DangThue);
        }

        public List<Phong> LayDanhSachPhongDangDonDep()
        {
            return phongDAL.LayDanhSachPhongTheoTrangThai(TrangThaiPhong.DangDonDep);
        }

        public List<Phong> LayDanhSachPhongBaoTri()
        {
            return phongDAL.LayDanhSachPhongTheoTrangThai(TrangThaiPhong.BaoTri);
        }

        public Phong LayThongTinPhong(int maPhong)
        {
            return phongDAL.LayPhongTheoMa(maPhong);
        }

        public Phong LayThongTinPhong(string roomName)
        {
            return phongDAL.LayPhongTheoTen(roomName);
        }

        public int ThemPhongMoi(Phong phong)
        {
            if (!KiemTraDuLieuPhong(phong, out string loi))
            {
                throw new ArgumentException(loi);
            }

            if (phongDAL.LayPhongTheoTen(phong.RoomName) != null)
            {
                throw new InvalidOperationException($"Phòng {phong.RoomName} đã tồn tại!");
            }

            phong.Status = "Available";
            return phongDAL.ThemPhong(phong);
        }

        public bool CapNhatThongTinPhong(Phong phong)
        {
            if (!KiemTraDuLieuPhong(phong, out string loi))
            {
                throw new ArgumentException(loi);
            }

            Phong phongHienTai = phongDAL.LayPhongTheoMa(phong.RoomId);
            if (phongHienTai == null)
            {
                throw new InvalidOperationException("Không tìm thấy phòng cần cập nhật!");
            }

            if (phong.RoomName != phongHienTai.RoomName)
            {
                Phong phongTrung = phongDAL.LayPhongTheoTen(phong.RoomName);
                if (phongTrung != null)
                {
                    throw new InvalidOperationException($"Phòng {phong.RoomName} đã được sử dụng!");
                }
            }

            return phongDAL.SuaPhong(phong);
        }

        public bool XoaPhong(int roomId)
        {
            Phong phong = phongDAL.LayPhongTheoMa(roomId);
            if (phong == null)
            {
                throw new InvalidOperationException("Không tìm thấy phòng cần xóa!");
            }

            if (phong.TrangThai == TrangThaiPhong.DangThue)
            {
                throw new InvalidOperationException("Không thể xóa phòng đang được thuê!");
            }

            return phongDAL.XoaPhong(roomId);
        }

        public bool ChuyenTrangThaiSangTrong(int roomId)
        {
            Phong phong = phongDAL.LayPhongTheoMa(roomId);
            if (phong == null)
            {
                throw new InvalidOperationException("Không tìm thấy phòng!");
            }

            if (phong.TrangThai != TrangThaiPhong.DangDonDep && phong.TrangThai != TrangThaiPhong.BaoTri)
            {
                throw new InvalidOperationException("Chỉ có thể chuyển sang trống từ trạng thái đang dọn hoặc bảo trì!");
            }

            return phongDAL.CapNhatTrangThaiPhong(roomId, TrangThaiPhong.Trong);
        }

        public bool ChuyenTrangThaiSangDangThue(int roomId)
        {
            Phong phong = phongDAL.LayPhongTheoMa(roomId);
            if (phong == null)
            {
                throw new InvalidOperationException("Không tìm thấy phòng!");
            }

            if (phong.TrangThai != TrangThaiPhong.Trong)
            {
                throw new InvalidOperationException("Chỉ có thể cho thuê phòng đang trống!");
            }

            return phongDAL.CapNhatTrangThaiPhong(roomId, TrangThaiPhong.DangThue);
        }

        public bool ChuyenTrangThaiSangDangDonDep(int roomId)
        {
            Phong phong = phongDAL.LayPhongTheoMa(roomId);
            if (phong == null)
            {
                throw new InvalidOperationException("Không tìm thấy phòng!");
            }

            if (phong.TrangThai != TrangThaiPhong.DangThue)
            {
                throw new InvalidOperationException("Chỉ có thể dọn dẹp phòng sau khi khách trả!");
            }

            return phongDAL.CapNhatTrangThaiPhong(roomId, TrangThaiPhong.DangDonDep);
        }

        public bool ChuyenTrangThaiSangBaoTri(int roomId)
        {
            Phong phong = phongDAL.LayPhongTheoMa(roomId);
            if (phong == null)
            {
                throw new InvalidOperationException("Không tìm thấy phòng!");
            }

            if (phong.TrangThai == TrangThaiPhong.DangThue)
            {
                throw new InvalidOperationException("Không thể bảo trì phòng đang có khách!");
            }

            return phongDAL.CapNhatTrangThaiPhong(roomId, TrangThaiPhong.BaoTri);
        }

        public bool CapNhatGiaPhong(int roomTypeId, decimal giaMoi)
        {
            if (giaMoi < 0)
            {
                throw new ArgumentException("Giá phòng không được âm!");
            }

            return phongDAL.CapNhatGiaPhong(roomTypeId, giaMoi);
        }

        public bool CapNhatTrangThaiPhong(int roomId, TrangThaiPhong trangThaiMoi)
        {
            Phong phong = phongDAL.LayPhongTheoMa(roomId);
            if (phong == null)
            {
                throw new InvalidOperationException("Không tìm thấy phòng!");
            }

            // Kiểm tra logic chuyển trạng thái
            switch (trangThaiMoi)
            {
                case TrangThaiPhong.Trong:
                    if (phong.TrangThai != TrangThaiPhong.DangDonDep && phong.TrangThai != TrangThaiPhong.BaoTri)
                        throw new InvalidOperationException("Chỉ có thể chuyển sang trống từ đang dọn hoặc bảo trì!");
                    break;
                case TrangThaiPhong.DangThue:
                    if (phong.TrangThai != TrangThaiPhong.Trong)
                        throw new InvalidOperationException("Chỉ có thể cho thuê phòng đang trống!");
                    break;
                case TrangThaiPhong.DangDonDep:
                    if (phong.TrangThai != TrangThaiPhong.DangThue)
                        throw new InvalidOperationException("Chỉ có thể dọn dẹp phòng sau khi khách trả!");
                    break;
                case TrangThaiPhong.BaoTri:
                    if (phong.TrangThai == TrangThaiPhong.DangThue)
                        throw new InvalidOperationException("Không thể bảo trì phòng đang có khách!");
                    break;
            }

            return phongDAL.CapNhatTrangThaiPhong(roomId, trangThaiMoi);
        }

        /// <summary>
        /// Tự động cập nhật trạng thái phòng đã quá ngày check-out.
        /// Trả về số lượng booking được cập nhật.
        /// </summary>
        public int CapNhatPhongQuaHanCheckout()
        {
            return phongDAL.CapNhatPhongQuaHanCheckout();
        }

        public List<Phong> TimKiemPhong(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                return LayDanhSachTatCaPhong();
            }
            return phongDAL.TimKiemPhong(tuKhoa.Trim());
        }

        public Dictionary<TrangThaiPhong, int> LayThongKePhong()
        {
            return phongDAL.ThongKePhongTheoTrangThai();
        }

        public int DemSoPhongTrong()
        {
            return phongDAL.LayDanhSachPhongTheoTrangThai(TrangThaiPhong.Trong).Count;
        }

        public int DemSoPhongDangThue()
        {
            return phongDAL.LayDanhSachPhongTheoTrangThai(TrangThaiPhong.DangThue).Count;
        }

        public int DemTongSoPhong()
        {
            return phongDAL.LayDanhSachPhong().Count;
        }

        public List<RoomType> LayDanhSachLoaiPhong()
        {
            return phongDAL.LayDanhSachLoaiPhong();
        }

        private bool KiemTraDuLieuPhong(Phong phong, out string loi)
        {
            loi = string.Empty;

            if (string.IsNullOrWhiteSpace(phong.RoomName))
            {
                loi = "Tên phòng không được để trống!";
                return false;
            }

            if (phong.RoomTypeId <= 0)
            {
                loi = "Vui lòng chọn loại phòng!";
                return false;
            }

            if (phong.HomestayId <= 0)
            {
                loi = "Vui lòng chọn homestay!";
                return false;
            }

            return true;
        }
    }
}
