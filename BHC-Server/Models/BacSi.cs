using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class BacSi
    {
        public BacSi()
        {
            ChucDanhBacSis = new HashSet<ChucDanhBacSi>();
            KeHoachKhams = new HashSet<KeHoachKham>();
            PhanLoaiBacSiChuyenKhoas = new HashSet<PhanLoaiBacSiChuyenKhoa>();
        }

        public string IdbacSi { get; set; } = null!;
        public string? IdphongKham { get; set; }
        public string HoTenBacSi { get; set; } = null!;
        public string Cccd { get; set; } = null!;
        public string SoDienThoaiBacSi { get; set; } = null!;
        public string EmailBacSi { get; set; } = null!;
        public int? Idquyen { get; set; }
        public decimal? GiaKham { get; set; }
        public string TaiKhoan { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public bool GioiTinh { get; set; }
        public string? AnhBacSi { get; set; }
        public string AnhChungChiHanhNgheBacSi { get; set; } = null!;
        public int? Idnguoidung { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; }

        public virtual NguoiDung? IdnguoidungNavigation { get; set; }
        public virtual PhongKham? IdphongKhamNavigation { get; set; }
        public virtual Quyen? IdquyenNavigation { get; set; }
        public virtual ICollection<ChucDanhBacSi> ChucDanhBacSis { get; set; }
        public virtual ICollection<KeHoachKham> KeHoachKhams { get; set; }
        public virtual ICollection<PhanLoaiBacSiChuyenKhoa> PhanLoaiBacSiChuyenKhoas { get; set; }
    }
}
