using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class QuanTriVien
    {
        public QuanTriVien()
        {
            XacThucDangKyMoCoSoYtes = new HashSet<XacThucDangKyMoCoSoYte>();
        }

        public int IdquanTriVien { get; set; }
        public string HoQtv { get; set; } = null!;
        public string TenQtv { get; set; } = null!;
        public bool? TrangThai { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Cccd { get; set; } = null!;
        public bool GioiTinh { get; set; }
        public string SoDienThoai { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string? AnhQtv { get; set; }
        public string TaiKhoanQtv { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public bool? Chucvu { get; set; }

        public virtual ICollection<XacThucDangKyMoCoSoYte> XacThucDangKyMoCoSoYtes { get; set; }
    }
}
