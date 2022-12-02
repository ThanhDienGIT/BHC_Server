using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class TaoLichNhanVienCoSo
    {
        public int IdtaoLichNhanVienCoSo { get; set; }
        public int? IdnguoiDungDatLich { get; set; }
        public int? IddatLichNhanVienCoSo { get; set; }
        public int? TrangThaiTaoLich { get; set; }
        public DateTime? NgayGioDatLich { get; set; }
        public double? GiaKham { get; set; }
        public string? LyDoKham { get; set; }

        public virtual DatLichNhanVienCoSo? IddatLichNhanVienCoSoNavigation { get; set; }
        public virtual NguoiDung? IdnguoiDungDatLichNavigation { get; set; }
    }
}
