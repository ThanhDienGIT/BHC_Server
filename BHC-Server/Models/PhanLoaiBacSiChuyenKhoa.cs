using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class PhanLoaiBacSiChuyenKhoa
    {
        public int IdphanLoaiBacSiChuyenKhoa { get; set; }
        public string? IdbacSi { get; set; }
        public int? IdchuyenKhoa { get; set; }

        public virtual BacSi? IdbacSiNavigation { get; set; }
        public virtual ChuyenKhoa? IdchuyenKhoaNavigation { get; set; }
    }
}
