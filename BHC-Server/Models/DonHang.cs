using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            ChiTietDatHangs = new HashSet<ChiTietDatHang>();
        }

        public int IddonHang { get; set; }
        public int? IdnguoiDung { get; set; }
        public string? IdnhanVien { get; set; }
        public int? TrangThai { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayDuyet { get; set; }
        public decimal TongTien { get; set; }
        public int PhuongThucThanhToan { get; set; }

        public virtual NguoiDung? IdnguoiDungNavigation { get; set; }
        public virtual NhanVienNhaThuoc? IdnhanVienNavigation { get; set; }
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }
    }
}
