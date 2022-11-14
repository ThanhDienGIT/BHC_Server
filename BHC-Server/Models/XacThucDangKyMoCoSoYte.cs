using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class XacThucDangKyMoCoSoYte
    {
        public XacThucDangKyMoCoSoYte()
        {
            CacChuyenKhoaChuyenMonDangKies = new HashSet<CacChuyenKhoaChuyenMonDangKy>();
        }

        public int IdxacThucDangKyMoCoSoYte { get; set; }
        public int IdnguoiDung { get; set; }
        public int? IdquanTriVien { get; set; }
        public string TenCoSo { get; set; } = null!;
        public int? LoaiHinhDangKy { get; set; }
        public string AnhBangCap { get; set; } = null!;
        public string AnhGiayChungNhanKinhDoanh { get; set; } = null!;
        public string AnhCccdmatTruoc { get; set; } = null!;
        public string AnhCccdmatSau { get; set; } = null!;
        public string AnhCoSo { get; set; } = null!;
        public string AnhDangKyAnhBacSi { get; set; } = null!;
        public string AnhChungChiHanhNghe { get; set; } = null!;
        public DateTime? NgayDangKy { get; set; }
        public DateTime? NgayXetDuyet { get; set; }
        public string DiaChi { get; set; } = null!;
        public bool? LoaiPhongKham { get; set; }
        public int? LoaiCoSo { get; set; }
        public int? XaPhuong { get; set; }
        public int? TrangThaiXacThuc { get; set; }

        public virtual NguoiDung IdnguoiDungNavigation { get; set; } = null!;
        public virtual QuanTriVien? IdquanTriVienNavigation { get; set; }
        public virtual LoaiHinhDichVu? LoaiHinhDangKyNavigation { get; set; }
        public virtual XaPhuong? XaPhuongNavigation { get; set; }
        public virtual ICollection<CacChuyenKhoaChuyenMonDangKy> CacChuyenKhoaChuyenMonDangKies { get; set; }
    }
}
