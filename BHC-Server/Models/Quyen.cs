﻿using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class Quyen
    {
        public Quyen()
        {
            BacSis = new HashSet<BacSi>();
            NhanVienCoSos = new HashSet<NhanVienCoSo>();
            NhanVienNhaThuocs = new HashSet<NhanVienNhaThuoc>();
        }

        public int Idquyen { get; set; }
        public string TenQuyen { get; set; } = null!;

        public virtual ICollection<BacSi> BacSis { get; set; }
        public virtual ICollection<NhanVienCoSo> NhanVienCoSos { get; set; }
        public virtual ICollection<NhanVienNhaThuoc> NhanVienNhaThuocs { get; set; }
    }
}
