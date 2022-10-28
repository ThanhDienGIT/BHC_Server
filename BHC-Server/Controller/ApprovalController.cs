using BHC_Server.Models;
using Com.CloudRail.SI.ServiceCode.Commands;
using EmailSend.Core.HostServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BHC_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private readonly DB_BHCContext _context;
        private readonly EmailHostedService _hostedService;
        public ApprovalController(DB_BHCContext context,EmailHostedService hostedService)
        {
            _context = context;
            _hostedService = hostedService;
        }

        [HttpPut]
        public IActionResult ApprovalBooking(int iddatlich)
        {
            var a = _context.DatLiches.FirstOrDefault(x => x.IddatLich == iddatlich);
            if(a == null)
            {
                return BadRequest("No Success");
            }
            else
            {
                 a.TrangThaiDatLich = 2;
                _context.SaveChanges();
                return Ok("Success");
            }

           
        }

        [HttpPost("CheckKeHoach")]
        public IActionResult CheckKeHoach(CreateSchedule check)
        {
            var checkkehoach = _context.KeHoachKhams
                               .FirstOrDefault(x => x.NgayDatLich == check.NgayDatLich && x.IdbacSi == check.IdbacSi);
            if (checkkehoach != null)
            {
                return Ok(checkkehoach.IdkeHoachKham);
            }
            else
            {
                var taokehoach = new KeHoachKham
                {
                    IdbacSi = check.IdbacSi,
                    NgayDatLich = Convert.ToDateTime(check.NgayDatLich),
                };
                _context.KeHoachKhams.Add(taokehoach);
                _context.SaveChanges();
                return Ok(taokehoach.IdkeHoachKham);
            }
        }

        [HttpPost("TaoLich/{idkehoach}")]
        public IActionResult TaoLich(CreateSchedule create,int idkehoach)
        {
          
                var DatLich = new DatLich
                {
                    IdkeHoachKham = idkehoach,
                    ThoiGianDatLich = create.ThoiGianDatLich,
                    SoLuongToiDa = create.SoLuongToiDa,
                };
                _context.DatLiches.Add(DatLich);
                _context.SaveChanges();
                return Ok("createdatlich");
        }

        [HttpPost("DatLich")]
        public async Task<IActionResult> DatLich(TaoLich taoLich)
        {
            var CheckDatLich = _context.TaoLiches
                               .FirstOrDefault(x => x.IddatLich == taoLich.IddatLich && x.IdnguoiDungDatLich == taoLich.IdnguoiDungDatLich);
            var datlich = _context.DatLiches.FirstOrDefault(x => x.IddatLich == taoLich.IddatLich);
            var nguoidung = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == taoLich.IdnguoiDungDatLich);
            
             if (CheckDatLich == null && datlich != null && nguoidung != null)
             {
                 var Newbook = new TaoLich
                  {
                      IdnguoiDungDatLich = taoLich.IdnguoiDungDatLich,
                      IddatLich = taoLich.IddatLich,
                      LyDoKham = taoLich.LyDoKham,
                      TrangThaiTaoLich = taoLich.TrangThaiTaoLich,
                  };
                  _context.TaoLiches.Add(Newbook);
                  if(datlich.SoLuongHienTai >= datlich.SoLuongToiDa)
                    {
                      return Ok("Đã hết chỗ");
                     }
                else
                {
                    datlich.SoLuongHienTai++;
                    _context.SaveChanges();
                    return Ok("Success");
                }      
            }
            else
            {
                return BadRequest("Đã book");
            }
        }

        [HttpGet("XacThucDatLich/{idnguoidung}")]
        public IActionResult Approhe(Login login)
        {
            var a = _context.NguoiDungs.FirstOrDefault(p => p.TaiKhoan == login.UserName);
            if (a != null)
            {
                if (a.MatKhau == login.Password)
                {
                    
                    _context.SaveChanges();
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

        [HttpPut("ApprovalMedical")]
        public IActionResult ApprovalMedical(int iddatlich)
        {
            var a = _context.DatLiches.FirstOrDefault(x => x.IddatLich == iddatlich);
            if (a == null)
            {
                return BadRequest("No Success");
            }
            else
            {
                a.TrangThaiDatLich = 3;
                _context.SaveChanges();
                return Ok("Success");
            }
        }
        [HttpPut("Cancel")]
        public IActionResult CancelBooking (int iddatlich)
        {
            var a = _context.DatLiches.FirstOrDefault(x => x.IddatLich == iddatlich);
            if (a == null)
            {
                return BadRequest("No Success");
            }
            else
            {
                a.TrangThaiDatLich = 0;
                _context.SaveChanges();
                return Ok("Success");
            }
        }
        [HttpPost]
        public IActionResult Approval(Login login)
        {
            var a = _context.NguoiDungs.FirstOrDefault(p => p.TaiKhoan == login.UserName);
            if (a != null)
            {
                if (a.MatKhau == login.Password)
                {
                    a.XacThuc = true;
                    _context.SaveChanges();
                    return Ok("Success");
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

        

    }
}
