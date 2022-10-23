using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class QuanHuyen
    {
        public QuanHuyen()
        {
            XaPhuongs = new HashSet<XaPhuong>();
        }

        public int IdquanHuyen { get; set; }
        public string TenQuanHuyen { get; set; } = null!;

        public virtual ICollection<XaPhuong> XaPhuongs { get; set; }
    }
}
