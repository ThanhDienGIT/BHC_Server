using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class ChuyenKhoa
    {
        public ChuyenKhoa()
        {
            ChuyenKhoaPhongKhams = new HashSet<ChuyenKhoaPhongKham>();
        }

        public int? IdchuyenKhoa { get; set; } = null;
        public string TenChuyenKhoa { get; set; } = null!;
        public string? Anh { get; set; }
        public string? MoTa { get; set; }

        public virtual ICollection<ChuyenKhoaPhongKham>? ChuyenKhoaPhongKhams { get; set; } = null;
    }
}
