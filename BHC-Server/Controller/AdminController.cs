using BHC_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BHC_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DB_BHCContext _context;
        public AdminController(DB_BHCContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult LoginAdmin(Login loginadmin)
        {
            var a = _context.QuanTriViens.FirstOrDefault(x => x.TaiKhoanQtv == loginadmin.UserName);
            if (a != null)
            {
                if (a.MatKhau == loginadmin.Password)
                {
                    return Ok(a.IdquanTriVien);
                }
                else
                {
                    return BadRequest("Sai tài khoản hoặc mật khẩu");
                }
            }
            else
            {
                return BadRequest("Tài khoản không tồn tạis");
            }

        }

        [HttpGet("LaydanhsachDatHen")]
        public IActionResult LaydanhsachDatHen()
        {
            var list = from x in _context.LichHens
                       join c in _context.NguoiDungs on x.IdNguoiDungHenLich equals c.IdNguoiDung
                       join d in _context.XacThucDangKyMoCoSoYtes on c.IdNguoiDung equals d.IdnguoiDung
                       select new{
                           x.IdLichHen,
                           x.IdNguoiDungHenLich,
                           x.NgayHen,
                           x.GioHen,
                           x.LinkHen,
                           x.TrangThaiLichHen,
                           c,
                           d.LoaiHinhDangKy,
                       };
            return Ok(list);    
        }

        [HttpPost("LuuLichHen")]
        public IActionResult LuuLichHen(LichHen lichhen)
        {
            var checklichhen = _context.LichHens.FirstOrDefault(x => x.IdLichHen == lichhen.IdLichHen);

            if(checklichhen == null)
            {
                var a = new LichHen
                {
                    IdNguoiDungHenLich = lichhen.IdNguoiDungHenLich,
                    GioHen = lichhen.GioHen,
                    NgayHen = lichhen.NgayHen,
                    LinkHen = lichhen.LinkHen,
                };
                _context.LichHens.Add(a);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("failed");
            }


            
        }

    }
}
