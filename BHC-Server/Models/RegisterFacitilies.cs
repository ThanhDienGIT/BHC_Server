namespace BHC_Server.Models
{
    public class RegisterFacitilies
    {
        public int IdxacThucDangKyMoCoSoYte { get; set; }
        public int IdnguoiDung { get; set; }
        public string TenCoSo { get; set; } = null!;
        public int? LoaiHinhDangKy { get; set; }
        public string AnhBangCap { get; set; } = null!;
        public string AnhGiayChungNhanKinhDoanh { get; set; } = null!;
        public string AnhCccdmatTruoc { get; set; } = null!;
        public string AnhCccdmatSau { get; set; } = null!;
        public string AnhCoSo { get; set; } = null!;
        public string AnhDangKyAnhBacSi { get; set; } = null!;
        public string AnhChungChiHanhNghe { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public bool? LoaiPhongKham { get; set; }
        public int? XaPhuong { get; set; }
    }
}
