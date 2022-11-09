using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class DatLich
    {
        public DatLich()
        {
            TaoLiches = new HashSet<TaoLich>();
        }

        public int IddatLich { get; set; }
        public int? IdkeHoachKham { get; set; }
        public int? TrangThaiDatLich { get; set; }
        public int? SoLuongToiDa { get; set; }
        public int? SoLuongHienTai { get; set; }
        public string? ThoiGianDatLich { get; set; }

        public virtual KeHoachKham? IdkeHoachKhamNavigation { get; set; }
        public virtual ICollection<TaoLich> TaoLiches { get; set; }
    }
}
