using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class CacChuyenKhoaChuyenMonDangKy
    {
        public int IdCacChuyenKhoaChuyenMonDangKy { get; set; }
        public int? Idxacthucdatlich { get; set; }
        public int? Idchuyenmon { get; set; }

        public virtual XacThucDangKyMoCoSoYte? IdxacthucdatlichNavigation { get; set; }
    }
}
