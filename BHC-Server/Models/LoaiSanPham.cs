using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class LoaiSanPham
    {
        public LoaiSanPham()
        {
            DonViBans = new HashSet<DonViBan>();
            SanPhams = new HashSet<SanPham>();
        }

        public int IdloaiSanPham { get; set; }
        public int? IddanhMucSanPham { get; set; }
        public string TenLoaiSanPham { get; set; } = null!;

        public virtual DanhMucSanPham? IddanhMucSanPhamNavigation { get; set; }
        public virtual ICollection<DonViBan> DonViBans { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
