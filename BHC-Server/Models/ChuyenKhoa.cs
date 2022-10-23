using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class ChuyenKhoa
    {
        public ChuyenKhoa()
        {
            PhanLoaiBacSiChuyenKhoas = new HashSet<PhanLoaiBacSiChuyenKhoa>();
            IdphongKhams = new HashSet<PhongKham>();
        }

        public int IdchuyenKhoa { get; set; }
        public string TenChuyenKhoa { get; set; } = null!;
        public string? Anh { get; set; }
        public string? MoTa { get; set; }

        public virtual ICollection<PhanLoaiBacSiChuyenKhoa> PhanLoaiBacSiChuyenKhoas { get; set; }

        public virtual ICollection<PhongKham> IdphongKhams { get; set; }
    }
}
