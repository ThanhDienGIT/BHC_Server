using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class NhaThuoc
    {
        public NhaThuoc()
        {
            NhanVienNhaThuocs = new HashSet<NhanVienNhaThuoc>();
            SanPhams = new HashSet<SanPham>();
        }

        public string IdNhaThuoc { get; set; } = null!;
        public string TenNhaThuoc { get; set; } = null!;
        public int? IdnguoiDung { get; set; }
        public int? IdxaPhuong { get; set; }
        public string DiaChi { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? Anhnhathuoc { get; set; }
        public int? IdLoaiHinhDichVu { get; set; }
        public DateTime? NgayMoNhaThuoc { get; set; }
        public double? DanhGia { get; set; }
        public int? SoLanDatMua { get; set; }
        public bool? TrangThai { get; set; }

        public virtual LoaiHinhDichVu? IdLoaiHinhDichVuNavigation { get; set; }
        public virtual NguoiDung? IdnguoiDungNavigation { get; set; }
        public virtual XaPhuong? IdxaPhuongNavigation { get; set; }
        public virtual ICollection<NhanVienNhaThuoc> NhanVienNhaThuocs { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
