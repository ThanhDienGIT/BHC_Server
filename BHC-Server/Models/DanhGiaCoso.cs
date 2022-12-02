using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class DanhGiaCoso
    {
        public int IdDanhGiaCoso { get; set; }
        public int Idnguoidanhgia { get; set; }
        public string? IdbacSi { get; set; }
        public string? IdnhanVienCoSo { get; set; }
        public string? IdcoSoDichVuKhac { get; set; }
        public string? IdphongKham { get; set; }
        public double SoSao { get; set; }
        public string? NhanXet { get; set; }

        public virtual BacSi? IdbacSiNavigation { get; set; }
        public virtual CoSoDichVuKhac? IdcoSoDichVuKhacNavigation { get; set; }
        public virtual NguoiDung IdnguoidanhgiaNavigation { get; set; } = null!;
        public virtual NhanVienCoSo? IdnhanVienCoSoNavigation { get; set; }
        public virtual PhongKham? IdphongKhamNavigation { get; set; }
    }
}
