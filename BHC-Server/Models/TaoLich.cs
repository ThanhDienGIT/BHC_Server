using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class TaoLich
    {
        public int? IdtaoLich { get; set; } = null;
        public int? IdnguoiDungDatLich { get; set; }
        public int? IddatLich { get; set; }
        public int? TrangThai { get; set; }
        public DateTime? NgayGioDatLich { get; set; }
        public string? LyDoKham { get; set; }

        public virtual DatLich? IddatLichNavigation { get; set; }
        public virtual NguoiDung? IdnguoiDungDatLichNavigation { get; set; }
    }
}
