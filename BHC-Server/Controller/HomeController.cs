using BHC_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;

namespace BHC_Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly DB_BHCContext _context;
        public HomeController(DB_BHCContext context)
        {
            _context = context;
        }

        [HttpGet("Danhsachchuyenkhoaphobien")]
        public IActionResult Danhsachchuyenkhoaphobien()
        {
            var listchuyenkhoa = _context.ChuyenKhoas.ToList();

            return Ok(listchuyenkhoa);
        }

        [HttpGet("Danhsachphongkhamphobien")]
        public IActionResult Danhsachphongkhamphobien()
        {
            var listchuyenkhoa = _context.PhongKhams.Where(x=>x.TrangThai == true);


            return Ok(listchuyenkhoa);
        }

        [HttpGet("Danhsachnhanvienphobien")]
        public IActionResult Danhsachnhanvienphobien()
        {
            var liststaff = from x in _context.NhanVienCoSos
                            join w in _context.PhanLoaiChuyenKhoaNhanViens on x.IdnhanVienCoSo equals w.IdnhanVienCoSo
                            join z in _context.ChuyenMoncoSos on w.ChuyenMoncoSo equals z.IdchuyenMonCoSo
                            join c in _context.CoSoDichVuKhacs on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                            join h in _context.ChuyenMons on z.IdchuyenMon equals h.IdChuyenMon
                            where c.TrangThai == true && x.TrangThai == true
                            select new { 
                                x, 
                                h.TenChuyenMon
                            };
            return Ok(liststaff);
        }

        [HttpGet("Danhsachbacsiphobien")]
        public IActionResult Danhsachbacsiphobien()
        {
            var liststaff = from x in _context.BacSis
                            join w in _context.PhanLoaiBacSiChuyenKhoas on x.IdbacSi equals w.IdbacSi
                            join z in _context.ChuyenKhoaPhongKhams on w.IdchuyenKhoa equals z.IdchuyenKhoaPhongKham
                            join c in _context.PhongKhams on x.IdphongKham equals c.IdphongKham
                            join h in _context.ChuyenKhoas on z.IdchuyenKhoa equals h.IdchuyenKhoa
                            where c.TrangThai == true && x.TrangThai == true
                            select new
                            {
                                x,
                                c.ChuyenKhoaPhongKhams
                            };
            return Ok(liststaff);
        }


        [HttpGet("Danhsachcosophobien")]
        public IActionResult Danhsachcosophobien()
        {
            var liststaff = from x in _context.CoSoDichVuKhacs
                            join c in _context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                            join d in _context.ChuyenMons on c.IdchuyenMon equals d.IdChuyenMon
                            where x.TrangThai == true
                            select new
                            {
                                x,
                                d.TenChuyenMon
                            };
            return Ok(liststaff);
        }


        [HttpGet("Danhsachchuyenmonphobien")]
        public IActionResult Danhsachchuyenmonphobien()
        {
            var list = _context.ChuyenMons.ToList();


            return Ok(list);
        }
    }
}
