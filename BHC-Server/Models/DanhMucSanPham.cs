using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class DanhMucSanPham
    {
        public DanhMucSanPham()
        {
            LoaiSanPhams = new HashSet<LoaiSanPham>();
        }

        public int IddanhMucSanPham { get; set; }
        public string TenDanhMuc { get; set; } = null!;

        public virtual ICollection<LoaiSanPham> LoaiSanPhams { get; set; }
    }
}
