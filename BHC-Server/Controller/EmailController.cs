using BHC_Server.Models;
using EmailSend.Core.HostServices;
using Microsoft.AspNetCore.Mvc;
using BHC_Server.Ramdom;
using Microsoft.EntityFrameworkCore;

namespace BHC_Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly DB_BHCContext _context;
                
        private readonly EmailHostedService _hostedService;

        public EmailController(EmailHostedService hostedService,DB_BHCContext context)
        {
            _hostedService = hostedService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string email)
        {
            await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
            {
                EmailAddress = email,
                Subject = "Hello Thanh Dien",
                Body = "" +
                       "<strong> Hello la dien day </strong>" +
                       "<img src='https://image.shutterstock.com/image-photo/mountains-under-mist-morning-amazing-260nw-1725825019.jpg' alt='day la anh'/>" +
                       "",
                                Attachments = null
                            });

            return Ok("Send Success");
        }

        [HttpPost("guimatkhauchonhanvien/{idnhanvien}")]
        public async Task<IActionResult> guimatkhauchonhanvien(string idnhanvien)
        {
            var email = _context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == idnhanvien);
            var ramdom = new Ramdom.Ramdom();
            var c = ramdom.GetPassword();
           
            if (email != null)
            {
                email.MatKhau = c;
                _context.SaveChanges();
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BOOKINGHEALTHCARE : LINK HẸN XÁC THỰC MỞ CƠ SỞ Y TẾ",
                    Body = "<h2> Chào " + email.HoTenNhanVien  + "</h2> <br>\r\n " +
                    "<h3 style=\"color:green;\">&emsp; " +
                    "Mật khẩu của bạn là : "+ c + " </h3>  <br>\r\n " +
                    "<h3>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h3> <br>\r\n " +
                    " <h4>&emsp;&emsp; Email : thanhdiensett@gmail.com hoặc </h4>\r\n " +
                    " <h4>&emsp;&emsp; Số điện thoại : 0966631453 </h4> <br>\r\n " +
                    "<h3> BookingHealthCare</h3>"
                    ,
                    Attachments = null
                });
                return Ok(email.EmailNhanVienCoSo);
            }
            return BadRequest("Failed");
        }

        [HttpPost("GuiLichMeet")]
        public async Task<IActionResult> GuiLichMeet(LichHen nguoidung)
        {
            var email = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == nguoidung.IdNguoiDungHenLich);
            if (email != null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = email.Email,
                    Subject = "BOOKINGHEALTHCARE : LINK HẸN XÁC THỰC MỞ CƠ SỞ Y TẾ",
                    Body = "<h2> Chào "+  email.HoNguoiDung + " " + email.TenNguoiDung +" </h2> \r\n " +
                    "     <h3 style=\"color:green;\">&emsp; Link hẹn xác thực mở cơ sở y tế của bạn là : "+ nguoidung.LinkHen +" </h3> " +
                    " \r\n  <h3 style=\"color:gray\">&emsp;&emsp; Ngày họp là : "+ nguoidung.NgayHen +"</h3>\r\n  " +
                    " <h3 style=\"color:gray\">&emsp;&emsp; Giờ họp là : "+ nguoidung.GioHen +"</h3> <br>\r\n " +
                    " <h3>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h3> \r\n  " +
                    " <h4>&emsp;&emsp; Email : thanhdiensett@gmail.com </h4>\r\n " +
                    " <h4>&emsp;&emsp; Số điện thoại : 0966631453 </h4> <br>\r\n  " +
                    " <h3> BOOKINGHEALTHCARE </h3>"
                    ,
                    Attachments = null  
                });
                return Ok(email.Email);
            }
            return BadRequest("Failed");
        }

        [HttpPost("guiXacthucDatLich/{idnguoidung}/{iddatlich}")]
        public async Task<IActionResult> guiXacthucDatLich(int idnguoidung,int iddatlich)
        {
            var email = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);
            var stringXacThuc = "http://localhost:3000/confirmbooking/" + idnguoidung + "/" + iddatlich  ;
            if(email != null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BOOKINGHEALTHCARE : LINK XÁC THỰC ĐẶT LỊCH MỞ CƠ SỞ Y TẾ",
                    Body = "<h2> Chào " + email.HoNguoiDung + " " + email.TenNguoiDung + " </h2> \r\n " +
                     "     <h3 style=\"color:green;\">&emsp; Link hẹn xác thực mở cơ sở y tế của bạn là : " + stringXacThuc + " </h3> " +
                    " <h3>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h3> \r\n  " +
                    " <h4>&emsp;&emsp; Email : thanhdiensett@gmail.com </h4>\r\n " +
                    " <h4>&emsp;&emsp; Số điện thoại : 0966631453 </h4> <br>\r\n  " +
                    " <h3> BOOKINGHEALTHCARE </h3>"
                    ,
                    Attachments = null
                });
                return Ok(email.Email);
            }

            return Ok();
        }

        [HttpPost("guiXacthucDatLichCSYE/{idnguoidung}/{iddatlich}")]
        public async Task<IActionResult> guiXacthucDatLichCSYE(int idnguoidung, int iddatlich)
        {
            var email = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);
            var stringXacThuc = "http://localhost:3000/confirmOtherFacitilies/" + idnguoidung + "/" + iddatlich;
            if (email != null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BOOKINGHEALTHCARE : LINK XÁC THỰC ĐẶT LỊCH CƠ SỞ Y TẾ",
                    Body = "<h2> Chào " + email.HoNguoiDung + " " + email.TenNguoiDung + " </h2> \r\n " +
                     "     <h3 style=\"color:green;\">&emsp; Link hẹn xác thực mở cơ sở y tế của bạn là : " + stringXacThuc + " </h3> " +
                    " <h3>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h3> \r\n  " +
                    " <h4>&emsp;&emsp; Email : thanhdiensett@gmail.com </h4>\r\n " +
                    " <h4>&emsp;&emsp; Số điện thoại : 0966631453 </h4> <br>\r\n  " +
                    " <h3> BOOKINGHEALTHCARE </h3>"
                    ,
                    Attachments = null
                });
                return Ok(email.Email);
            }

            return Ok();
        }



        [HttpPost("resetPass")]
        public async Task<IActionResult> ResetPass(int idnguoidung,string email)
        {
            
            var nguoidung = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);

            if(nguoidung != null)
            {
                var newpass = new Ramdom.Ramdom();

                var c = newpass.GetPassword();

                nguoidung.MatKhau = c;
                _context.SaveChanges();

                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = nguoidung.Email,
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "<h2> Chào " + nguoidung.HoNguoiDung + " " + nguoidung.TenNguoiDung + "</h2> <br>\r\n " +
                    "<h3 style=\"color:green;\">&emsp; " +
                    "Mật khẩu của bạn là : " + c + " </h3>  <br>\r\n " +
                    "<h3>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h3> <br>\r\n " +
                    " <h4>&emsp;&emsp; Email : thanhdiensett@gmail.com hoặc </h4>\r\n " +
                    " <h4>&emsp;&emsp; Số điện thoại : 0966631453 </h4> <br>\r\n " +
                    "<h3> BookingHealthCare</h3>"
                    ,
                    Attachments = null
                });
                return Ok("Send Success");
            }
            else
            {
                return BadRequest("No user");
            }      
        }

        [HttpPost("LinkApproval")]
        public async Task<IActionResult> Approval(int idnguoidung, string email)
        {
            var nguoidung = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);

            if(nguoidung!= null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = email,
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "Link xác thực của bạn là : <a href=\"http://localhost:3000/notifi\" style=\"color:green;\">Chuyển đến trang xác thực.</a>",
                    Attachments = null
                });
                return Ok("Send Success");
            }
            else
            {
                return BadRequest("No user");
            }     
        }

        [HttpPost("LinkBook")]
        public async Task<IActionResult> LinkBook(int idnguoidung)
        {
            var nguoidung = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);

            if (nguoidung != null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = nguoidung.Email,
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "Link xác thực của bạn là : <a href=\"http://localhost:3000/notifi/ \" style=\"color:green;\">Chuyển đến trang xác thực.</a>",
                    Attachments = null
                });
                return Ok("Send Success");
            }
            else
            {
                return BadRequest("No user");
            }
        }

        [HttpPost("datlaimatkhaunew")]
        public async Task<IActionResult> datlaimatkhau(EmailResetPasswork email)
        {
            var ramdom = new Ramdom.Ramdom();
            var checkmail = _context.NguoiDungs.FirstOrDefault(x => x.Email == email.Email);
            if (checkmail == null)
            {
                return BadRequest("Email không tồn tại");
            }
            else
            {
                var c = ramdom.GetPassword();
                checkmail.MatKhau = c;
                _context.SaveChanges();
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = checkmail.Email,
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "<h2> Chào " + checkmail.HoNguoiDung + " " + checkmail.TenNguoiDung + "</h2> <br>\r\n " +
                    "<h3 style=\"color:green;\">&emsp; " +
                    "Mật khẩu của bạn là : " + c + " </h3>  <br>\r\n " +
                    "<h3>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h3> <br>\r\n " +
                    " <h4>&emsp;&emsp; Email : thanhdiensett@gmail.com hoặc </h4>\r\n " +
                    " <h4>&emsp;&emsp; Số điện thoại : 0966631453 </h4> <br>\r\n " +
                    "<h3> BookingHealthCare</h3>"
                    ,
                    Attachments = null
                });
                return Ok("success");
            }

           
        }




    }
}
