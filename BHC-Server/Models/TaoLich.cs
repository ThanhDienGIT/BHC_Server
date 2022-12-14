using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class TaoLich
    {
        public int IdtaoLich { get; set; }
        public int? IdnguoiDungDatLich { get; set; }
        public int? IddatLich { get; set; }
        public int? TrangThaiTaoLich { get; set; }
        public DateTime? NgayGioDatLich { get; set; }
        public double? GiaKham { get; set; }
        public string? LyDoKham { get; set; }

        public virtual DatLich? IddatLichNavigation { get; set; }
        public virtual NguoiDung? IdnguoiDungDatLichNavigation { get; set; }
    }
}
