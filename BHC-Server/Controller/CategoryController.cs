using BHC_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace BHC_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
           private readonly DB_BHCContext _context;
           public CategoryController(DB_BHCContext context)
           {
                _context = context;
           }

        [HttpGet]
        public IActionResult GetTypeSpecialist()
        {
            var ListSpecia = from s in _context.ChuyenKhoas
                             select new
                             {
                                 s.IdchuyenKhoa,
                                 s.TenChuyenKhoa,
                                 s.MoTa,
                                 s.Anh,
                             };
       
            return Ok(ListSpecia);
        }


        [HttpGet("bacsi")]
        public IActionResult GetTypeDoctor()
        {
            var Listdoctor = from s in _context.BacSis
                             select new {
                                 s.IdbacSi , s.HoTenBacSi ,s.AnhBacSi,s.Idnguoidung};
            return Ok(Listdoctor);
        }

        [HttpGet("GetTypechuyenkhoabacsi/{idbacsi}")]
        public IActionResult GetTypechuyenkhoabacsi(string idbacsi)
        {
            var chuyenkhoa = from a in _context.ChuyenKhoas
                             join c in _context.PhanLoaiBacSiChuyenKhoas on a.IdchuyenKhoa equals c.IdchuyenKhoa
                             join d in _context.BacSis on c.IdbacSi equals d.IdbacSi
                             where d.IdbacSi == idbacsi
                             select a.TenChuyenKhoa;
            if(chuyenkhoa.Count() > 0)
            {
                return Ok(chuyenkhoa);
            }
            else
            {
                return BadRequest(chuyenkhoa);
            }
            
        }

        [HttpGet("GetDiaChi/{idbacsi}")]
        public IActionResult getdiachi (string idbacsi)
        {
            var diachi = from x in _context.PhongKhams
                         join d in _context.XaPhuongs on x.IdxaPhuong equals d.IdxaPhuong
                         join c in _context.QuanHuyens on d.IdquanHuyen equals c.IdquanHuyen
                         join e in _context.BacSis on x.IdphongKham equals e.IdphongKham
                         where e.IdbacSi == idbacsi
                         select new
                         {
                             x.DiaChi,
                             d.TenXaPhuong,
                             c.TenQuanHuyen,
                         };
            if(diachi.Count() > 0)
            {
                return Ok(diachi);
            }
            else
            {
                return BadRequest("No data");
            }
         
        }
       
        [HttpGet("GetAllClinic")]
        public IActionResult GetAllClinic()
        {
            var a = from s in _context.PhongKhams
                    select new
                    {
                        s.TenPhongKham,
                        s.IdphongKham,
                        s.IdchuyenKhoas,
                        s.HinhAnh,
                    };
            return Ok(a);
        }

        [HttpGet("GetDoctorById/{idbacsi}")]
        public IActionResult GetDoctorById(string idbacsi)
        {
            var bacsi = from a in _context.BacSis
                        join b in _context.PhongKhams on a.IdphongKham equals b.IdphongKham
                        where a.IdbacSi == idbacsi
                        select new
                        {
                            a.HoTenBacSi,
                            a.AnhBacSi,
                            a.ChucDanhBacSis,
                            a.GiaKham,
                            a.MoTa,
                            a.PhanLoaiBacSiChuyenKhoas,
                            b.DiaChi,
                            b.IdxaPhuong,
                            a.TrangThai,
                            b.BaoHiem,
                            a.Idnguoidung,
                        };
            if (bacsi == null)
            {
                return BadRequest("No data");
            }
            else
            {
                return Ok(bacsi);
            }
        }


        [HttpGet("Laychucdanhbacsi/{idbacsi}")]
        public IActionResult Laychucdanhbacsi(string idbacsi)
        {
            var listchucdanh = from x in _context.ChucDanhBacSis
                               join c in _context.ChucDanhs on x.IdchucDanh equals c.IdchucDanh
                               join d in _context.BacSis on x.IdbacSi equals d.IdbacSi
                               where d.IdbacSi == idbacsi
                               select c.TenChucDanh;

            if(listchucdanh.Count() == null)
            {
                return BadRequest("no data");
            }
            else
            {
                return Ok(listchucdanh);
            }

        }





        [HttpGet("Getbookbyiddoctor/{idbacsi}")]
        public IActionResult Getbookbyiddoctor(string idbacsi)
        {

            var lichcuabacsi = (from p in _context.BacSis
                               join b in _context.KeHoachKhams on p.IdbacSi equals b.IdbacSi
                               join c in _context.DatLiches on b.IdkeHoachKham equals c.IdkeHoachKham
                               where p.IdbacSi == idbacsi && c.IddatLich != null
                               select new
                               {
                                   b.NgayDatLich
                               }).Distinct();  
            return Ok(lichcuabacsi);

        }
        [HttpGet("getGiodattLichByNgayDatLich/{ngaydatlich}/{idbacsi}")]
        public IActionResult getGiodattLichByNgayDatLich(DateTime ngaydatlich,string idbacsi)
        {
        
            var thoigianlichkham = from x in _context.KeHoachKhams
                                   join c in _context.DatLiches on x.IdkeHoachKham equals c.IdkeHoachKham
                                   where x.NgayDatLich == ngaydatlich && x.IdbacSi == idbacsi
                                   select new { c.ThoiGianDatLich, c.IddatLich };

            if(thoigianlichkham.Count() > 0)
            {
                return Ok(thoigianlichkham);
            }
            else
            {
                return BadRequest("No data");
            }

          

        }

        [HttpGet("getphongkhamByIdBacSi/{idbacsi}")]
        public IActionResult getphongkhamByIdBacSi(string idbacsi)
        {
            var diachibacsi = from a in _context.BacSis
                                join b in _context.PhongKhams on a.IdphongKham equals b.IdphongKham
                                join c in _context.XaPhuongs on b.IdxaPhuong equals c.IdxaPhuong
                                join d in _context.QuanHuyens on c.IdquanHuyen equals d.IdquanHuyen
                                where a.IdbacSi == idbacsi
                                select new
                                {
                                    b.DiaChi,
                                    c.TenXaPhuong,
                                    d.TenQuanHuyen,
                                    b.BaoHiem,
                                };
            if(diachibacsi.Count() == 0)
            {
                return BadRequest("Not found");
            }
            else
            {
                return Ok(diachibacsi);
            }

        }



    }
}
