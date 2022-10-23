namespace BHC_Server.Models
{
    public class ChangeUser
    {
        public int IdNguoiDung { get; set; }
        public string HoNguoiDung { get; set; } = null!;
        public string TenNguoiDung { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string SoDienThoai { get; set; } = null!;
        public DateTime? NgaySinh { get; set; }
        public bool? GioiTinh { get; set; }
        public string? AnhNguoidung { get; set; }
        public string? Cccd { get; set; }
        public string? Diachi { get; set; }
        public string? SoDienThoaiNguoiThan { get; set; }
        public string? TienSuBenh { get; set; }
        public decimal? CanNang { get; set; }
        public decimal? ChieuCao { get; set; }
        public decimal? Bmi { get; set; }
        public bool? QuocTich { get; set; }
    }
}
