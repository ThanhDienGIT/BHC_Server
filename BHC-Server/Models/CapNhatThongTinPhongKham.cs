namespace BHC_Server.Models
{
    public class CapNhatThongTinPhongKham
    {
        public string IdphongKham { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string TenPhongKham { get; set; } = null!;
        public string HinhAnh { get; set; } = null!;
        public string? AnhDaiDienPhongKham { get; set; }
        public int IdxaPhuong { get; set; }
        public bool? BaoHiem { get; set; }
    }
}
