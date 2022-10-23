using System;
using System.Collections.Generic;

namespace BHC_Server.Models
{
    public partial class PhongKham
    {
        public PhongKham()
        {
            BacSis = new HashSet<BacSi>();
            IdchuyenKhoas = new HashSet<ChuyenKhoa>();
        }

        public string IdphongKham { get; set; } = null!;
        public int? IdnguoiDung { get; set; }
        public string DiaChi { get; set; } = null!;
        public string TenPhongKham { get; set; } = null!;
        public string HinhAnh { get; set; } = null!;
        public int IdxaPhuong { get; set; }
        public bool? BaoHiem { get; set; }
        public bool LoaiPhongKham { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; }
        public DateTime? NgayMoPhongKham { get; set; }

        public virtual NguoiDung? IdnguoiDungNavigation { get; set; }
        public virtual XaPhuong IdxaPhuongNavigation { get; set; } = null!;
        public virtual ICollection<BacSi> BacSis { get; set; }

        public virtual ICollection<ChuyenKhoa> IdchuyenKhoas { get; set; }
    }
}
