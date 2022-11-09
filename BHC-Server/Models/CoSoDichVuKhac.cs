using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class CoSoDichVuKhac
    {
        public CoSoDichVuKhac()
        {
            ChuyenMoncoSos = new HashSet<ChuyenMoncoSo>();
            NhanVienCoSos = new HashSet<NhanVienCoSo>();
        }

        public string IdcoSoDichVuKhac { get; set; } = null!;
        public int? IdnguoiDung { get; set; }
        public string TenCoSo { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string HinhAnhCoSo { get; set; } = null!;
        public string? AnhDaiDienCoSo { get; set; }
        public string? LoiGioiThieu { get; set; }
        public string? ChuyenMon { get; set; }
        public string? TrangThietBi { get; set; }
        public string? ViTri { get; set; }
        public int IdxaPhuong { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; }
        public DateTime? NgayMoCoSo { get; set; }

        public virtual NguoiDung? IdnguoiDungNavigation { get; set; }
        public virtual XaPhuong IdxaPhuongNavigation { get; set; } = null!;
        public virtual ICollection<ChuyenMoncoSo> ChuyenMoncoSos { get; set; }
        public virtual ICollection<NhanVienCoSo> NhanVienCoSos { get; set; }
    }
}
