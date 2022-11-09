using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class NhanVienCoSo
    {
        public NhanVienCoSo()
        {
            KeHoachNhanVienCoSos = new HashSet<KeHoachNhanVienCoSo>();
            PhanLoaiChuyenKhoaNhanViens = new HashSet<PhanLoaiChuyenKhoaNhanVien>();
        }

        public string IdnhanVienCoSo { get; set; } = null!;
        public string? IdcoSoDichVuKhac { get; set; }
        public string HoTenNhanVien { get; set; } = null!;
        public string Cccd { get; set; } = null!;
        public string SoDienThoaiNhanVienCoSo { get; set; } = null!;
        public string EmailNhanVienCoSo { get; set; } = null!;
        public int? Idquyen { get; set; }
        public decimal? GiaDatLich { get; set; }
        public string TaiKhoan { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public bool GioiTinh { get; set; }
        public string? AnhNhanVien { get; set; }
        public string AnhChungChiHanhNgheNhanVien { get; set; } = null!;
        public int? Idnguoidung { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; }

        public virtual CoSoDichVuKhac? IdcoSoDichVuKhacNavigation { get; set; }
        public virtual NguoiDung? IdnguoidungNavigation { get; set; }
        public virtual Quyen? IdquyenNavigation { get; set; }
        public virtual ICollection<KeHoachNhanVienCoSo> KeHoachNhanVienCoSos { get; set; }
        public virtual ICollection<PhanLoaiChuyenKhoaNhanVien> PhanLoaiChuyenKhoaNhanViens { get; set; }
    }
}
