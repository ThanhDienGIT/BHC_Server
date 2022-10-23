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

        [HttpGet("LayEmailTuIDNguoiDung/{idnguoidung}")]
        public async Task<IActionResult> LayEmailTuIDNguoiDungAsync(int idnguoidung)
        {
            var email = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);
            
            if(email != null)
            {
                await _hostedService.SendEmailAsync(new EmailSend.Core.Common.Email.EmailModel
                {
                    EmailAddress = email.Email,
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "Link xác nhận đăng ký lịch của bạn là :" ,
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
                    EmailAddress = email,
                    Subject = "BookingHealcare xin chào bạn",
                    Body = "Link xác thực đặt lịch của bạn là : <a href=\"http://localhost:3000/notifi\" style=\"color:green;\">Chuyển đến trang xác thực.</a>",
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


    }
}
