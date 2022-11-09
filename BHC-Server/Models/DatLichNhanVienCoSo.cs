using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class DatLichNhanVienCoSo
    {
        public DatLichNhanVienCoSo()
        {
            TaoLichNhanVienCoSos = new HashSet<TaoLichNhanVienCoSo>();
        }

        public int IddatLichNhanVienCoSo { get; set; }
        public int? IdkeHoachNhanVienCoSo { get; set; }
        public int? TrangThaiDatLich { get; set; }
        public int? SoLuongToiDa { get; set; }
        public int? SoLuongHienTai { get; set; }
        public string? ThoiGianDatLich { get; set; }

        public virtual KeHoachNhanVienCoSo? IdkeHoachNhanVienCoSoNavigation { get; set; }
        public virtual ICollection<TaoLichNhanVienCoSo> TaoLichNhanVienCoSos { get; set; }
    }
}
