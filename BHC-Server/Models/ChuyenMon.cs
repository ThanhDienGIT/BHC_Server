using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class ChuyenMon
    {
        public ChuyenMon()
        {
            ChuyenMoncoSos = new HashSet<ChuyenMoncoSo>();
        }

        public int IdChuyenMon { get; set; }
        public string TenChuyenMon { get; set; } = null!;
        public string? AnhChuyeMon { get; set; }
        public string? MoTaChuyenMon { get; set; }

        public virtual ICollection<ChuyenMoncoSo> ChuyenMoncoSos { get; set; }
    }
}
