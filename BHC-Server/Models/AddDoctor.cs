namespace BHC_Server.Models
{
    public class AddDoctor
    {
        public string HoTenBacSi { get; set; } = null!;
        public string Cccd { get; set; } = null!;
        public string SoDienThoaiBacSi { get; set; } = null!;
        public string EmailBacSi { get; set; } = null!;
        public int? Idquyen { get; set; }
        public decimal? GiaKham { get; set; }
        public bool GioiTinh { get; set; }
        public string? AnhBacSi { get; set; }
        public string AnhChungChiHanhNgheBacSi { get; set; } = null!;
    }
}
