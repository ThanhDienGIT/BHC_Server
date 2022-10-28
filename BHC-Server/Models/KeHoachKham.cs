using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class KeHoachKham
    {
        public KeHoachKham()
        {
            DatLiches = new HashSet<DatLich>();
        }

        public int IdkeHoachKham { get; set; }
        public string? IdbacSi { get; set; }
        public DateTime NgayDatLich { get; set; }
        public int? TrangThaiKeHoachKham { get; set; }

        public virtual BacSi? IdbacSiNavigation { get; set; }
        public virtual ICollection<DatLich> DatLiches { get; set; }
    }
}
