using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class LoaiHinhDichVu
    {
        public LoaiHinhDichVu()
        {
            NhaThuocs = new HashSet<NhaThuoc>();
            XacThucDangKyMoCoSoYtes = new HashSet<XacThucDangKyMoCoSoYte>();
        }

        public int IdLoaiHinhDichVu { get; set; }
        public string TenLoaiHinhDichVu { get; set; } = null!;

        public virtual ICollection<NhaThuoc> NhaThuocs { get; set; }
        public virtual ICollection<XacThucDangKyMoCoSoYte> XacThucDangKyMoCoSoYtes { get; set; }
    }
}
