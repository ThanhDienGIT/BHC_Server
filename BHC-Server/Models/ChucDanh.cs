using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class ChucDanh
    {
        public ChucDanh()
        {
            ChucDanhBacSis = new HashSet<ChucDanhBacSi>();
        }

        public int IdchucDanh { get; set; }
        public string TenChucDanh { get; set; } = null!;

        public virtual ICollection<ChucDanhBacSi> ChucDanhBacSis { get; set; }
    }
}
