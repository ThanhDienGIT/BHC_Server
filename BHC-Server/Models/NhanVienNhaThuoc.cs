using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class NhanVienNhaThuoc
    {
        public NhanVienNhaThuoc()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public string IdnhanVienNhaThuoc { get; set; } = null!;
        public string? IdNhaThuoc { get; set; }
        public string HoTenNhanVien { get; set; } = null!;
        public string EmailNhanvien { get; set; } = null!;
        public string SdtnhanVien { get; set; } = null!;
        public DateTime NgaySinh { get; set; }
        public string? AnhNhanVien { get; set; }
        public string? Cccd { get; set; }
        public string AnhChungChiHanhNgheNhanVien { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public int? ChucVu { get; set; }
        public string TaiKhoan { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public bool? TrangThai { get; set; }

        public virtual Quyen? ChucVuNavigation { get; set; }
        public virtual NhaThuoc? IdNhaThuocNavigation { get; set; }
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
