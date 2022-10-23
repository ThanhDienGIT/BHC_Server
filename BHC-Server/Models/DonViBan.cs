using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class DonViBan
    {
        public DonViBan()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int IddonViBan { get; set; }
        public string TenDonViBan { get; set; } = null!;
        public int IdloaiSanPham { get; set; }

        public virtual LoaiSanPham IdloaiSanPhamNavigation { get; set; } = null!;
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
