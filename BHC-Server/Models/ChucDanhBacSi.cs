using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class ChucDanhBacSi
    {
        public int IdChucDanhBacSi { get; set; }
        public string? IdbacSi { get; set; }
        public int? IdchucDanh { get; set; }

        public virtual BacSi? IdbacSiNavigation { get; set; }
        public virtual ChucDanh? IdchucDanhNavigation { get; set; }
    }
}
