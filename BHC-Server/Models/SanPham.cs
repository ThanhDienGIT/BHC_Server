using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietDatHangs = new HashSet<ChiTietDatHang>();
        }

        public int IdsanPham { get; set; }
        public string? IdnhaThuoc { get; set; }
        public string TenSanPham { get; set; } = null!;
        public int? IdloaiSanPham { get; set; }
        public string Mota { get; set; } = null!;
        public decimal Gia { get; set; }
        public bool? TrangThai { get; set; }
        public int SoLuong { get; set; }
        public string AnhSanPham { get; set; } = null!;
        public int? SoLuongDatMua { get; set; }
        public double? DanhGia { get; set; }
        public int? IddonViBan { get; set; }

        public virtual DonViBan? IddonViBanNavigation { get; set; }
        public virtual LoaiSanPham? IdloaiSanPhamNavigation { get; set; }
        public virtual NhaThuoc? IdnhaThuocNavigation { get; set; }
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }
    }
}
