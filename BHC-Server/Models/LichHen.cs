using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class LichHen
    {
        public int IdLichHen { get; set; }
        public int IdNguoiDungHenLich { get; set; }
        public string GioHen { get; set; } = null!;
        public DateTime NgayHen { get; set; }
        public string LinkHen { get; set; } = null!;

        public virtual NguoiDung IdNguoiDungHenLichNavigation { get; set; } = null!;
    }
}
