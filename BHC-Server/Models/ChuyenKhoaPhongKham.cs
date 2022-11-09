using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class ChuyenKhoaPhongKham
    {
        public ChuyenKhoaPhongKham()
        {
            PhanLoaiBacSiChuyenKhoas = new HashSet<PhanLoaiBacSiChuyenKhoa>();
        }

        public int IdchuyenKhoaPhongKham { get; set; }
        public int? IdchuyenKhoa { get; set; }
        public string? IdphongKham { get; set; }

        public virtual ChuyenKhoa? IdchuyenKhoaNavigation { get; set; }
        public virtual PhongKham? IdphongKhamNavigation { get; set; }
        public virtual ICollection<PhanLoaiBacSiChuyenKhoa> PhanLoaiBacSiChuyenKhoas { get; set; }
    }
}
