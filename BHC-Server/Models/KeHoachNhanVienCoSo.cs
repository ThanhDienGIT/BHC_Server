using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class KeHoachNhanVienCoSo
    {
        public KeHoachNhanVienCoSo()
        {
            DatLichNhanVienCoSos = new HashSet<DatLichNhanVienCoSo>();
        }

        public int IdkeHoachNhanVienCoSo { get; set; }
        public string? IdnhanVienCoSo { get; set; }
        public DateTime NgayDatLich { get; set; }
        public int? TrangThaiKeHoachNhanVienCoSo { get; set; }

        public virtual NhanVienCoSo? IdnhanVienCoSoNavigation { get; set; }
        public virtual ICollection<DatLichNhanVienCoSo> DatLichNhanVienCoSos { get; set; }
    }
}
