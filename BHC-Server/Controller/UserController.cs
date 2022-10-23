using BHC_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookingHealthCare_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DB_BHCContext _Context;
        public UserController(DB_BHCContext context)
        {
            _Context = context;
        }

        [HttpGet("{id}")]
           public IActionResult GetUserById(int id)
           {

            var InfoUserById = _Context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == id);

                if(InfoUserById != null)
                {
                 return Ok(InfoUserById);
            }
            else
            {
                return BadRequest("Not found");
            }
 
            
            }
        // xoa
        [HttpGet("GetListBooking/{iduser}")]
        public IActionResult GetListBookingByIDUser(int iduser)
        {

                return BadRequest("No booking");   
        }
        [HttpPut]
        public IActionResult ChangeInfoUser(ChangeUser user)
        {
            var nguoidung = _Context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == user.IdNguoiDung);
            var a = _Context.NguoiDungs.ToList();
            bool checkEmail = false;
            bool checkNumber = false;
            bool checkCCCD  =  false;
            bool sodienthoainguoithan = false;
            string messemail = "";
            string messesdt = "";
            string messesdtnt = "";
            string messecccd = "";
            string mess = "";
            if (nguoidung != null)
            {
                if (user.Email != nguoidung.Email || user.SoDienThoai != nguoidung.SoDienThoai
                || user.Cccd != nguoidung.Cccd || user.SoDienThoaiNguoiThan != nguoidung.SoDienThoaiNguoiThan)
                {
                    foreach (var x in a)
                    {
                        if (x.Email == user.Email && x.Email != nguoidung.Email)
                        {
                            checkEmail = true;
                            messemail = "Đã tồn tại Email ";
                        }
                        if(x.SoDienThoai == user.SoDienThoai && x.SoDienThoai != nguoidung.SoDienThoai)
                        {
                            checkNumber = true;
                            messesdt = "Đã tồn tại số điện thoại ";
                        }
                        if(x.SoDienThoaiNguoiThan == user.SoDienThoaiNguoiThan && x.SoDienThoaiNguoiThan != nguoidung.SoDienThoaiNguoiThan)
                        {
                            sodienthoainguoithan = true;
                            messesdtnt = "Đã tồn tại số điện thoại của người thân ";
                        }
                        if(x.Cccd == user.Cccd && x.Cccd != nguoidung.Cccd)
                        {
                            checkCCCD = true;
                            messecccd = "Đã tồn tại căn cước công dân";
                        }
                    }
                }
            }
            else
            {
                return BadRequest("No User");
            }
            mess += messemail + messesdt + messesdtnt + messecccd;
            if (checkNumber == false && checkCCCD == false && sodienthoainguoithan==false && checkEmail == false)
            {
                if (nguoidung != null && user.AnhNguoidung != null)
                {
                    nguoidung.HoNguoiDung = user.HoNguoiDung;
                    nguoidung.TenNguoiDung = user.TenNguoiDung;
                    nguoidung.Email = user.Email;
                    nguoidung.Cccd = user.Cccd;
                    nguoidung.NgaySinh = user.NgaySinh;
                    nguoidung.MatKhau = user.MatKhau;
                    nguoidung.SoDienThoai = user.SoDienThoai;
                    nguoidung.GioiTinh = user.GioiTinh;
                    nguoidung.AnhNguoidung = user.AnhNguoidung;
                    nguoidung.SoDienThoaiNguoiThan = user.SoDienThoaiNguoiThan;
                    nguoidung.Diachi = user.Diachi;
                    nguoidung.TienSuBenh = user.TienSuBenh;
                    nguoidung.CanNang = user.CanNang;
                    nguoidung.ChieuCao = user.ChieuCao;
                    nguoidung.Bmi = user.Bmi;
                    nguoidung.QuocTich = user.QuocTich;
                    _Context.SaveChanges();
                    return Ok("Change Success");
                }
                else
                {
                    nguoidung.HoNguoiDung = user.HoNguoiDung;
                    nguoidung.TenNguoiDung = user.TenNguoiDung;
                    nguoidung.Email = user.Email;
                    nguoidung.Cccd = user.Cccd;
                    nguoidung.NgaySinh = user.NgaySinh;
                    nguoidung.MatKhau = user.MatKhau;
                    nguoidung.SoDienThoai = user.SoDienThoai;
                    nguoidung.GioiTinh = user.GioiTinh;
                    nguoidung.SoDienThoaiNguoiThan = user.SoDienThoaiNguoiThan;
                    nguoidung.Diachi = user.Diachi;
                    nguoidung.TienSuBenh = user.TienSuBenh;
                    nguoidung.CanNang = user.CanNang;
                    nguoidung.ChieuCao = user.ChieuCao;
                    nguoidung.Bmi = user.Bmi;
                    nguoidung.QuocTich = user.QuocTich;
                    _Context.SaveChanges();
                    return Ok("Change Success");
                }
            }
            else
            {
                return BadRequest(mess);
            }
          
        }

        [HttpGet("layidbacsibangiduser/{iduser}")]
        public IActionResult Checklichkham(int iduser)
        {
            var checkdortor = from x in _Context.BacSis
                              join c in _Context.PhongKhams on x.IdphongKham equals c.IdphongKham
                              where c.IdnguoiDung == iduser
                              select x.IdbacSi;
            if(checkdortor.Count() > 0)
            {
                return Ok(checkdortor);
            }
            else
            {
                return BadRequest("Nodata");
            }     
        }
    }
}
