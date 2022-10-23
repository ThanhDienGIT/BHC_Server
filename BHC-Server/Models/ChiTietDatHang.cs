using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class ChiTietDatHang
    {
        public int IdchiTietDatHang { get; set; }
        public int? IddonHang { get; set; }
        public int? IdsanPham { get; set; }
        public int SoLuongSanPham { get; set; }
        public decimal Giatien { get; set; }

        public virtual DonHang? IddonHangNavigation { get; set; }
        public virtual SanPham? IdsanPhamNavigation { get; set; }
    }
}
