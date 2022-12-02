using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class PhongKham
    {
        public PhongKham()
        {
            BacSis = new HashSet<BacSi>();
            ChuyenKhoaPhongKhams = new HashSet<ChuyenKhoaPhongKham>();
            DanhGiaCosos = new HashSet<DanhGiaCoso>();
        }

        public string IdphongKham { get; set; } = null!;
        public int? IdnguoiDung { get; set; }
        public string DiaChi { get; set; } = null!;
        public string TenPhongKham { get; set; } = null!;
        public string HinhAnh { get; set; } = null!;
        public string? AnhDaiDienPhongKham { get; set; }
        public string? LoiGioiThieu { get; set; }
        public string? ChuyenMon { get; set; }
        public string? TrangThietBi { get; set; }
        public string? ViTri { get; set; }
        public int IdxaPhuong { get; set; }
        public bool? BaoHiem { get; set; }
        public bool LoaiPhongKham { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; }
        public double? Danhgia { get; set; }
        public int? Solandatlich { get; set; }
        public DateTime? NgayMoPhongKham { get; set; }

        public virtual NguoiDung? IdnguoiDungNavigation { get; set; }
        public virtual XaPhuong IdxaPhuongNavigation { get; set; } = null!;
        public virtual ICollection<BacSi> BacSis { get; set; }
        public virtual ICollection<ChuyenKhoaPhongKham> ChuyenKhoaPhongKhams { get; set; }
        public virtual ICollection<DanhGiaCoso> DanhGiaCosos { get; set; }
    }
}
