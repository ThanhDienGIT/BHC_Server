using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class XaPhuong
    {
        public XaPhuong()
        {
            CoSoDichVuKhacs = new HashSet<CoSoDichVuKhac>();
            PhongKhams = new HashSet<PhongKham>();
            XacThucDangKyMoCoSoYtes = new HashSet<XacThucDangKyMoCoSoYte>();
        }

        public int IdxaPhuong { get; set; }
        public string TenXaPhuong { get; set; } = null!;
        public int? IdquanHuyen { get; set; }

        public virtual QuanHuyen? IdquanHuyenNavigation { get; set; }
        public virtual ICollection<CoSoDichVuKhac> CoSoDichVuKhacs { get; set; }
        public virtual ICollection<PhongKham> PhongKhams { get; set; }
        public virtual ICollection<XacThucDangKyMoCoSoYte> XacThucDangKyMoCoSoYtes { get; set; }
    }
}
