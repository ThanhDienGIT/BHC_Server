using BHC_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingHealthCare_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DB_BHCContext _Context;
        public LoginController(DB_BHCContext context)
        {
            _Context = context;
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            var a = _Context.NguoiDungs.FirstOrDefault(p => p.TaiKhoan == login.UserName);
            if (a != null)
            {
                if (a.MatKhau == login.Password)
                {
                    a.DangNhapLanCuoi = DateTime.Now;
                    _Context.SaveChanges();
                    return Ok(a.IdNguoiDung);
                }
                else
                {
                    return BadRequest("failed");
                }
            }
            else
            {
                return BadRequest("failed");
            }
        }

        [HttpPost("LoginFacilities")]
        public IActionResult LoginFacilities(Login login)
        {
            var a = _Context.NhanVienNhaThuocs.FirstOrDefault(p => p.TaiKhoan == login.UserName);
            var b = _Context.BacSis.FirstOrDefault(p => p.TaiKhoan == login.UserName);
            
            if(a != null)
            {
                if (a.MatKhau == login.Password)
                {
                    return Ok(a.IdnhanVienNhaThuoc);
                }
                else
                {
                    return BadRequest("Wrong Passworld");
                }
            }
            else
            {
                if(b != null)
                {
                    if (b.MatKhau == login.Password)
                    {
                        return Ok(b.IdbacSi);
                    }
                    else
                    {
                        return BadRequest("Wrong Passworld");
                    }
                }
              
            }
            return BadRequest("Failed");
        }



        [HttpPost("register")]

        public IActionResult Register(Register register)
        {

            var email = _Context.NguoiDungs.FirstOrDefault(x => x.Email == register.Email);
            var sodienthoai = _Context.NguoiDungs.FirstOrDefault(x => x.SoDienThoai == register.SoDienThoai);
            var taikhoan = _Context.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == register.TaiKhoan);

            string mess = "";
            string messemail = "";
            string messsodienthoai = "";
            string messtaikhoan = "";

            if (email != null)
            {
                messemail = "dacoemail";
            }
            if (sodienthoai != null)
            {
                messsodienthoai = "dacosodienthoai";
            }
            if (taikhoan != null)
            {
                messtaikhoan = "dacotaikhoan";
            }
            mess += messemail + " " + messsodienthoai + " " + messtaikhoan;

            if (email == null && sodienthoai == null && taikhoan == null)
            {
                NguoiDung useradd = new NguoiDung
                {
                    HoNguoiDung = register.HoNguoiDung,
                    TenNguoiDung = register.TenNguoiDung,
                    TaiKhoan = register.TaiKhoan,
                    MatKhau = register.MatKhau,
                    SoDienThoai = register.SoDienThoai,
                    Email = register.Email,
                };
                _Context.NguoiDungs.Add(useradd);
                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest(email);
            }
        }
    }
}
