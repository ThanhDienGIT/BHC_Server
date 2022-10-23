namespace BHC_Server.Models
{
    public class CreateSchedule
    {
        public string? ThoiGianDatLich { get; set; }
        public string? IdbacSi { get; set; }
        public DateTime? NgayDatLich { get; set; } = DateTime.Now;
        public int? SoLuongToiDa { get; set; } = 0;
    }
}
