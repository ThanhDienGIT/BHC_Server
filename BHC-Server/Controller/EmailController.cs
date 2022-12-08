using BHC_Server.Models;
using EmailSend.Core.HostServices;
using Microsoft.AspNetCore.Mvc;
using BHC_Server.Ramdom;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

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
                    EmailAddress = "thanhdiensett@gmail.com",
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
                    Body = "<h1> Chào " + email.HoNguoiDung + " " + email.TenNguoiDung + " </h1> \r\n " +
                    "<h2 style=\"color:green;\">&emsp; Link xác thực đặt lịch khám bệnh của bạn là : " + stringXacThuc + " </h2> " +
                    "<h2 style=\"color:red;\">&emsp; Lưu ý : nếu bạn cố ý đặt lịch nhưng không đi khám bệnh hoặc không tuân thủ quy định của website sẽ bị khóa tài khoản !  </h2> " +
                    " <h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h2> \r\n  " +
                    " <h3>&emsp;&emsp; Email : thanhdiensett@gmail.com </h3>\r\n " +
                    " <h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n  " +
                    " <h2> BOOKINGHEALTHCARE </h2>"
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
                    Body = "<h1> Chào " + email.HoNguoiDung + " " + email.TenNguoiDung + " </h1> \r\n " +
                    "<h2 style=\"color:green;\">&emsp; Link xác thực đặt lịch sử dụng dịch vụ y tế của bạn là : " + stringXacThuc + " </h2> " +
                    "<h2 style=\"color:red;\">&emsp; Lưu ý : nếu bạn cố ý đặt lịch nhưng không sử dụng dịch vụ hoặc không tuân thủ quy định của website sẽ bị khóa tài khoản !  </h2> " +
                    " <h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h2> \r\n  " +
                    " <h3>&emsp;&emsp; Email : thanhdiensett@gmail.com </h3>\r\n " +
                    " <h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n  " +
                    " <h2> BOOKINGHEALTHCARE </h2>"
                    ,
                    Attachments = null
                });
                return Ok(email.Email);
            }

            return BadRequest("failed");
        }

        [HttpPost("Guitaikhoanmatkhauchobacsimoithem/{email}")]
        public async Task<IActionResult> Guitaikhoanmatkhauchobacsimoithem(string email)
        {
            var checkbacsi = _context.BacSis.FirstOrDefault(x => x.EmailBacSi == email);
            if (checkbacsi != null)
            {
                if (email != null)
                {
                    await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                    {
                        EmailAddress = "thanhdiensett@gmail.com",
                        Subject = "BOOKINGHEALTHCARE : Tài khoản bác sĩ",
                        Body = "<h1> Chào " + checkbacsi.HoTenBacSi + " </h1> \r\n " +
                        "<h2 style=\"color:green;\">&emsp; Tài khoản của bạn là : " + checkbacsi.TaiKhoan + " </h2> " +
                         "<h2 style=\"color:green;\">&emsp; Mật khẩu của bạn là : " + checkbacsi.MatKhau + " </h2> " +
                        "<h2 style=\"color:red;\">&emsp; Lưu ý : Khi đăng nhập nhấn vào link bên dưới form đăng nhập nhấn vào (Đăng nhập dành cho cơ sở y tế ) để tiến hành đăng nhập vào bằng tài khoản này  </h2> " +
                        " <h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h2> \r\n  " +
                        " <h3>&emsp;&emsp; Email : thanhdiensett@gmail.com </h3>\r\n " +
                        " <h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n  " +
                        " <h2> BOOKINGHEALTHCARE </h2>"
                        ,
                        Attachments = null
                    });
                    return Ok(checkbacsi.EmailBacSi);
                }
            }

            return Ok();
        }


        [HttpPost("XacthucDatLichcosoThanhCong/{idnguoidung}/{iddatlich}")]
        public async Task<IActionResult> XacthucDatLichcosoThanhCong(int idnguoidung, int iddatlich)
        {
            var checkdatlich = (from x in _context.DatLichNhanVienCoSos
                                join c in _context.KeHoachNhanVienCoSos on x.IdkeHoachNhanVienCoSo equals c.IdkeHoachNhanVienCoSo
                                join d in _context.NhanVienCoSos on c.IdnhanVienCoSo equals d.IdnhanVienCoSo
                                join k in _context.CoSoDichVuKhacs on d.IdcoSoDichVuKhac equals k.IdcoSoDichVuKhac
                                join q in _context.XaPhuongs on k.IdxaPhuong equals q.IdxaPhuong
                                join z in _context.QuanHuyens on q.IdquanHuyen equals z.IdquanHuyen
                                where x.IddatLichNhanVienCoSo == iddatlich
                                select new
                                {
                                    x.ThoiGianDatLich,
                                    c.NgayDatLich,
                                    d.HoTenNhanVien,
                                    k.DiaChi,
                                    q.TenXaPhuong,
                                    z.TenQuanHuyen,
                                }).FirstOrDefault();
            var nguoidung = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);

            if (nguoidung != null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "<h1 style=\"font-size: 25px;\">Xin chào " + nguoidung.HoNguoiDung + " " + nguoidung.TenNguoiDung + "</h1> <br>\r\n" +
                    "<h2 style=\"color:green; font-size: 18px\">&emsp; Lịch của bạn đã được duyệt khi sử dụng dịch vụ vui lòng đem theo CCCD để sử dụng dịch vụ  </h2><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Lịch đặt : " + checkdatlich.ThoiGianDatLich + "  ngày " + checkdatlich.NgayDatLich + " </b>  </h3><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Chuyên viên : " + checkdatlich.HoTenNhanVien + " </b> </h3><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Địa chỉ khám bệnh : <i> " + checkdatlich.DiaChi + ", " + checkdatlich.TenXaPhuong + ", " + checkdatlich.TenQuanHuyen + " </i> </h3><br>\r\n" +
                    "<h3 style=\"color:red;font-size: 16pxpx \">&emsp; <b> LƯU Ý : nếu bạn cố ý đặt lịch mà không sử dụng dịch vụ hoặc không tuân thủ theo quy định của website sẽ bị khóa tài khoản </b> </h3><br>\r\n " +
                    "<h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ để biết thêm thông tin chi tiết  :</h2> <br>\r\n" +
                    "<h3>&emsp;&emsp; Email : thanhdiensett@gmail.com hoặc </h3>\r\n" +
                    "<h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n" +
                    "<h2 style=\"font-size: 30px;\"> BOOKINGHEALTHCARE  </h2> "
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

        [HttpPost("XacthucDatLichThanhCong/{idnguoidung}/{iddatlich}")]
        public async Task<IActionResult> XacthucDatLichThanhCong(int idnguoidung,int iddatlich)
        {
            var checkdatlich = (from x in _context.DatLiches
                                join c in _context.KeHoachKhams on x.IdkeHoachKham equals c.IdkeHoachKham
                                join d in _context.BacSis on c.IdbacSi equals d.IdbacSi
                                join k in _context.PhongKhams on d.IdphongKham equals k.IdphongKham
                                join q in _context.XaPhuongs on k.IdxaPhuong equals q.IdxaPhuong
                                join z in _context.QuanHuyens on q.IdquanHuyen equals z.IdquanHuyen
                                where x.IddatLich == iddatlich
                                select new
                                {
                                    x.ThoiGianDatLich,
                                    c.NgayDatLich,
                                    d.HoTenBacSi,
                                    k.DiaChi,
                                    q.TenXaPhuong,
                                    z.TenQuanHuyen,
                                }).FirstOrDefault();
            var nguoidung = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);

            if (nguoidung != null )
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "<h1 style=\"font-size: 25px;\">Xin chào "+ nguoidung.HoNguoiDung +" "+ nguoidung.TenNguoiDung+"</h1> <br>\r\n" +
                    "<h2 style=\"color:green; font-size: 18px\">&emsp; Lịch của bạn đã được duyệt khi đi khám vui lòng đem theo CCCD để khám bệnh  </h2><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Lịch khám : "+checkdatlich.ThoiGianDatLich+"  ngày "+ checkdatlich.NgayDatLich +" </b>  </h3><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Bác sĩ khám bệnh : "+ checkdatlich.HoTenBacSi +" </b> </h3><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Địa chỉ khám bệnh : <i> "+ checkdatlich.DiaChi + ", " + checkdatlich.TenXaPhuong + ", " + checkdatlich.TenQuanHuyen +" </i> </h3><br>\r\n" +
                    "<h3 style=\"color:red;font-size: 16pxpx \">&emsp; <b> LƯU Ý : nếu bạn cố ý đặt lịch mà không đi khám bệnh hoặc không tuân thủ theo quy định của website sẽ bị khóa tài khoản </b> </h3><br>\r\n " +
                    "<h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ để biết thêm thông tin chi tiết  :</h2> <br>\r\n" +
                    "<h3>&emsp;&emsp; Email : thanhdiensett@gmail.com hoặc </h3>\r\n" +
                    "<h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n" +
                    "<h2 style=\"font-size: 30px;\"> BOOKINGHEALTHCARE  </h2> "
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
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "<h2 style=\"font-size: 25px;\"> Chào " + nguoidung.HoNguoiDung + " " + nguoidung.TenNguoiDung + "</h2> <br>\r\n " +
                    "<h2 style=\"color:green;font-size: 18px;\">&emsp; " +
                    "Mật khẩu của bạn là : " + c + " </h2>  <br>\r\n " +
                    "<h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h2> <br>\r\n " +
                    " <h3>&emsp;&emsp; Email : thanhdiensett@gmail.com hoặc </h3>\r\n " +
                    " <h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n " +
                    "<h2> BookingHealthCare</h2>"
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

        [HttpPost("LinkApproval/{idnguoidung}")]
        public async Task<IActionResult> Approval(int idnguoidung)
        {
            var nguoidung = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);
            string link = "http://localhost:3000/notifi";
            if (nguoidung!= null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "<h1 style=\"font-size: 25px;\">Xin chào "+nguoidung.HoNguoiDung + " " +nguoidung.TenNguoiDung + " </h1> <br>\r\n" +
                    "<h2 style=\"color:rgb(59, 171, 179); font-size: 18px\">&emsp; Link xác thực của bạn là : bấm vào <u>" + link+"</u> để chuyển đến trang xác thực </h2><br>\r\n" +
                    "<h2 style=\"color:green;font-size: 18px \">&emsp;  Bạn có biết :<i> nếu bạn xác thực và tuân thủ quy định của website bạn sẽ được ưu tiên khi đăng ký dịch vụ y tế của website </i> </h2><br>\r\n" +
                    "<h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ để biết thêm thông tin chi tiết  :</h2> <br>\r\n" +
                    "<h3>&emsp;&emsp; Email : thanhdiensett@gmail.com hoặc </h3>\r\n" +
                    "<h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n" +
                    "<h2 style=\"font-size: 25px;\"> BOOKINGHEALTHCARE  </h2>",
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
            string pathxacthuc = "http://localhost:3000/notifi";
            if (nguoidung != null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BOOKINGHEALTHCARE nền tảng y tế chăm sóc sức khỏe toàn diện",
                    Body = "<h1 style=\"font-size: 25px;\">Xin chào "+ nguoidung.HoNguoiDung +" "+nguoidung.TenNguoiDung +"</h1> <br>\r\n" +
                    "<h2 style=\"color:rgb(59, 171, 179); font-size: 22px\">&emsp; Link xác thực tài khoản của của bạn là : bấm vào "+ pathxacthuc + " để chuyển đến trang xác thực </h2><br>\r\n" +
                    "<h2 style=\"color:green;font-size: 18px \">&emsp; <b> Bạn có biết : nếu bạn xác thực tài khoản và tuân thủ quy định của website bạn sẽ được ưu tiên khi đăng ký dịch vụ y tế của website </b> </h2><br>\r\n" +
                    "<h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ để biết thêm thông tin chi tiết  :</h2> <br>\r\n" +
                    "<h3>&emsp;&emsp; Email : thanhdiensett@gmail.com hoặc </h3>\r\n" +
                    "<h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n" +
                    "<h2 style=\"font-size: 30px;\"> BOOKINGHEALTHCARE  </h2>",
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
            var checktaikhoan = _context.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == email.TaiKhoan);
            if(checktaikhoan == null)
            {
                return BadRequest("Tài khoản không tồn tại");
            }
            else
            {
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
                        EmailAddress = "thanhdiensett@gmail.com",
                        Subject = "BOOKINGHEALTHCARE nền tảng y tế chăm sóc sức khỏe toàn diện",
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


        [HttpPost("XacThucTaiKham/{idnguoidung}/{iddatlich}")]
        public async Task<IActionResult> XacThucTaiKham(int idnguoidung, int iddatlich)
        {
            var checkdatlich = (from x in _context.DatLichNhanVienCoSos
                                join c in _context.KeHoachNhanVienCoSos on x.IdkeHoachNhanVienCoSo equals c.IdkeHoachNhanVienCoSo
                                join d in _context.NhanVienCoSos on c.IdnhanVienCoSo equals d.IdnhanVienCoSo
                                join k in _context.CoSoDichVuKhacs on d.IdcoSoDichVuKhac equals k.IdcoSoDichVuKhac
                                join q in _context.XaPhuongs on k.IdxaPhuong equals q.IdxaPhuong
                                join z in _context.QuanHuyens on q.IdquanHuyen equals z.IdquanHuyen
                                where x.IddatLichNhanVienCoSo == iddatlich
                                select new
                                {
                                    x.ThoiGianDatLich,
                                    c.NgayDatLich,
                                    d.HoTenNhanVien,
                                    k.DiaChi,
                                    q.TenXaPhuong,
                                    z.TenQuanHuyen,
                                }).FirstOrDefault();

            var email = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);
            var stringXacThuc = "http://localhost:3000/xacnhantaikham/" + idnguoidung + "/" + iddatlich;
            if (email != null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BOOKINGHEALTHCARE : LINK XÁC THỰC ĐẶT LỊCH CƠ SỞ Y TẾ",
                    Body = "<h1> Chào " + email.HoNguoiDung + " " + email.TenNguoiDung + " </h1> \r\n " +
                    "<h2 style=\"color:green;\">&emsp; Link xác thực tiếp tục sử dụng dịch vụ y tế của bạn là : " + stringXacThuc + " </h2> " +
                     "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Lịch đặt : " + checkdatlich.ThoiGianDatLich + "  ngày " + checkdatlich.NgayDatLich + " </b>  </h3><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Chuyên viên : " + checkdatlich.HoTenNhanVien + " </b> </h3><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Địa chỉ : <i> " + checkdatlich.DiaChi + ", " + checkdatlich.TenXaPhuong + ", " + checkdatlich.TenQuanHuyen + " </i> </h3><br>\r\n" +
                    "<h2 style=\"color:red;\">&emsp; Lưu ý : Bạn sẽ nằm trong danh sách chờ sử dụng dịch vụ y tế mà không cần chờ duyệt  </h2> " +
                    " <h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h2> \r\n  " +
                    " <h3>&emsp;&emsp; Email : thanhdiensett@gmail.com </h3>\r\n " +
                    " <h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n  " +
                    " <h2> BOOKINGHEALTHCARE </h2>"
                    ,
                    Attachments = null
                });
                return Ok("Success");
            }
            return BadRequest("failed");
        }


        [HttpPost("XacThucTaiKhamphongkham/{idnguoidung}/{iddatlich}")]
        public async Task<IActionResult> XacThucTaiKhamphongkham(int idnguoidung, int iddatlich)
        {
            var checkdatlich = (from x in _context.DatLiches
                                join c in _context.KeHoachKhams on x.IdkeHoachKham equals c.IdkeHoachKham
                                join d in _context.BacSis on c.IdbacSi equals d.IdbacSi
                                join k in _context.PhongKhams on d.IdphongKham equals k.IdphongKham
                                join q in _context.XaPhuongs on k.IdxaPhuong equals q.IdxaPhuong
                                join z in _context.QuanHuyens on q.IdquanHuyen equals z.IdquanHuyen
                                where x.IddatLich == iddatlich
                                select new
                                {
                                    x.ThoiGianDatLich,
                                    c.NgayDatLich,
                                    d.HoTenBacSi,
                                    k.DiaChi,
                                    q.TenXaPhuong,
                                    z.TenQuanHuyen,
                                }).FirstOrDefault();

            var email = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);
            var stringXacThuc = "http://localhost:3000/xacnhantaikhamphongkham/" + idnguoidung + "/" + iddatlich;
            if (email != null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = "thanhdiensett@gmail.com",
                    Subject = "BOOKINGHEALTHCARE : LINK XÁC THỰC ĐẶT LỊCH CƠ SỞ Y TẾ",
                    Body = "<h1> Chào " + email.HoNguoiDung + " " + email.TenNguoiDung + " </h1> \r\n " +
                    "<h2 style=\"color:green;\">&emsp; Link xác thực tiếp tục sử dụng dịch vụ y tế của bạn là : " + stringXacThuc + " </h2> " +
                     "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Lịch đặt : " + checkdatlich.ThoiGianDatLich + "  ngày " + checkdatlich.NgayDatLich + " </b>  </h3><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Chuyên viên : " + checkdatlich.HoTenBacSi + " </b> </h3><br>\r\n" +
                    "<h3 style=\"color:rgb(74, 178, 192);font-size: 18px \">&emsp; <b> Địa chỉ : <i> " + checkdatlich.DiaChi + ", " + checkdatlich.TenXaPhuong + ", " + checkdatlich.TenQuanHuyen + " </i> </h3><br>\r\n" +
                    "<h2 style=\"color:red;\">&emsp; Lưu ý : Bạn sẽ nằm trong danh sách chờ sử dụng dịch vụ y tế mà không cần chờ duyệt  </h2> " +
                    " <h2>&emsp; Nếu bạn có thắc mắc gì hãy liên hệ :</h2> \r\n  " +
                    " <h3>&emsp;&emsp; Email : thanhdiensett@gmail.com </h3>\r\n " +
                    " <h3>&emsp;&emsp; Số điện thoại : 0966631453 </h3> <br>\r\n  " +
                    " <h2> BOOKINGHEALTHCARE </h2>"
                    ,
                    Attachments = null
                });
                return Ok("Success");
            }
            return BadRequest("failed");
        }

    }
}
