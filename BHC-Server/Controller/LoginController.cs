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
                    return BadRequest("Sai tài khoản hoặc mật khẩu");
                }
            }
            else
            {
                return BadRequest("Người dùng không tồn tại");
            }
        }

        [HttpPost("LoginFacilities")]
        public IActionResult LoginFacilities(Login login)
        {
            var a = _Context.NhanVienNhaThuocs.FirstOrDefault(p => p.TaiKhoan == login.UserName);
            var b = _Context.BacSis.FirstOrDefault(p => p.TaiKhoan == login.UserName);
            var c = _Context.NhanVienCoSos.FirstOrDefault(p => p.TaiKhoan == login.UserName);
            
            if(a!= null)
            {
                if (a.MatKhau == login.Password)
                {
                    return Ok(a.IdnhanVienNhaThuoc);
                }
                else
                {
                    return BadRequest("Sai tài khoản hoặc mật khẩu");
                }
            }
            
            if(b!= null)
            {
                if (b != null)
                {
                    if (b.MatKhau == login.Password)
                    {
                        return Ok(b.IdbacSi);
                    }
                    else
                    {
                        return BadRequest("Sai tài khoản hoặc mật khẩu");
                    }
                }
            }

            if (c != null)
            {
                if (c != null)
                {
                    if (c.MatKhau == login.Password)
                    {
                        return Ok(c.IdnhanVienCoSo);
                    }
                    else
                    {
                        return BadRequest("Sai tài khoản hoặc mật khẩu");
                    }
                }
            }
            return BadRequest("Tài khoản không tồn tại");
        }


        [HttpPost("xacthucnguoidung")]
        public IActionResult xacthucnguoidung(XacThucNguoiDung xacthuc)
        {
            var a = _Context.NguoiDungs.FirstOrDefault(p => p.TaiKhoan == xacthuc.UserName);
            if (a != null)
            {
                if(a.XacThuc == true)
                {
                    return BadRequest("Tài khoản đã xác thực");
                }

                if (a.MatKhau == xacthuc.Password)
                {
                    if(a.Cccd == xacthuc.Cccd)
                    {
                        a.DangNhapLanCuoi = DateTime.Now;
                        a.XacThuc = true;
                        _Context.SaveChanges();
                        return Ok("Success");
                    }
                    else
                    {
                        return BadRequest("Sai căn cước công dân");
                    }
                   
                }
                else
                {
                    return BadRequest("Sai Tài khoản hoặc Mật khẩu");
                }
            }
            else
            {
                return BadRequest("Người dùng không tồn tại");
            }
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
                messemail = "Email đã được sử dụng";
            }
            if (sodienthoai != null)
            {
                messsodienthoai = "Số điện thoại đã được sử dụng";
            }
            if (taikhoan != null)
            {
                messtaikhoan = "Tài khoản đã được sử dụng";
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
                return BadRequest(mess);
            }
        }
    }
}
