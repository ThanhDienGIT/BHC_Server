using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            BacSis = new HashSet<BacSi>();
            CoSoDichVuKhacs = new HashSet<CoSoDichVuKhac>();
            DonHangs = new HashSet<DonHang>();
            LichHens = new HashSet<LichHen>();
            NhaThuocs = new HashSet<NhaThuoc>();
            NhanVienCoSos = new HashSet<NhanVienCoSo>();
            PhongKhams = new HashSet<PhongKham>();
            TaoLichNhanVienCoSos = new HashSet<TaoLichNhanVienCoSo>();
            TaoLiches = new HashSet<TaoLich>();
            XacThucDangKyMoCoSoYtes = new HashSet<XacThucDangKyMoCoSoYte>();
        }

        public int IdNguoiDung { get; set; }
        public string HoNguoiDung { get; set; } = null!;
        public string TenNguoiDung { get; set; } = null!;
        public string TaiKhoan { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string SoDienThoai { get; set; } = null!;
        public DateTime? NgaySinh { get; set; }
        public bool? GioiTinh { get; set; }
        public string? AnhNguoidung { get; set; }
        public string? Cccd { get; set; }
        public string? Diachi { get; set; }
        public string? SoDienThoaiNguoiThan { get; set; }
        public string? TienSuBenh { get; set; }
        public bool? TrangThaiNguoiDung { get; set; }
        public bool? TrangThaiNhaThuoc { get; set; }
        public bool? TrangThaiPhongKham { get; set; }
        public bool? TrangThaiCoSoYte { get; set; }
        public decimal? CanNang { get; set; }
        public decimal? ChieuCao { get; set; }
        public decimal? Bmi { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? DangNhapLanCuoi { get; set; }
        public bool? QuocTich { get; set; }
        public int? HuyLich { get; set; }
        public bool? XacThuc { get; set; }

        public virtual ICollection<BacSi> BacSis { get; set; }
        public virtual ICollection<CoSoDichVuKhac> CoSoDichVuKhacs { get; set; }
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual ICollection<LichHen> LichHens { get; set; }
        public virtual ICollection<NhaThuoc> NhaThuocs { get; set; }
        public virtual ICollection<NhanVienCoSo> NhanVienCoSos { get; set; }
        public virtual ICollection<PhongKham> PhongKhams { get; set; }
        public virtual ICollection<TaoLichNhanVienCoSo> TaoLichNhanVienCoSos { get; set; }
        public virtual ICollection<TaoLich> TaoLiches { get; set; }
        public virtual ICollection<XacThucDangKyMoCoSoYte> XacThucDangKyMoCoSoYtes { get; set; }
    }
}
