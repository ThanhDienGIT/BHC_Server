using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class PhanLoaiChuyenKhoaNhanVien
    {
        public int? IdphanLoaiBacSiChuyenKhoa { get; set; } = null;
        public string? IdnhanVienCoSo { get; set; }
        public int? ChuyenMoncoSo { get; set; }

        public virtual ChuyenMoncoSo? ChuyenMoncoSoNavigation { get; set; }
        public virtual NhanVienCoSo? IdnhanVienCoSoNavigation { get; set; }
    }
}
