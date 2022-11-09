using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class ChuyenMoncoSo
    {
        public ChuyenMoncoSo()
        {
            PhanLoaiChuyenKhoaNhanViens = new HashSet<PhanLoaiChuyenKhoaNhanVien>();
        }

        public int IdchuyenMonCoSo { get; set; }
        public int? IdchuyenMon { get; set; }
        public string? IdcoSoDichVuKhac { get; set; }

        public virtual ChuyenMon? IdchuyenMonNavigation { get; set; }
        public virtual CoSoDichVuKhac? IdcoSoDichVuKhacNavigation { get; set; }
        public virtual ICollection<PhanLoaiChuyenKhoaNhanVien> PhanLoaiChuyenKhoaNhanViens { get; set; }
    }
}
