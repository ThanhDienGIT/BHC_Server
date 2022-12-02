using BHC_Server.Models;
using Com.CloudRail.SI.Extensions;
using Com.CloudRail.SI.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

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

        [HttpGet("locbacsitheochuyenkhoa/{idchuyenkhoa}")]
        public IActionResult locbacsitheochuyenkhoa(int idchuyenkhoa)
        {
            var listbacsi = from x in _context.ChuyenKhoas
                            join c in _context.ChuyenKhoaPhongKhams on x.IdchuyenKhoa equals c.IdchuyenKhoa
                            join k in _context.PhanLoaiBacSiChuyenKhoas on c.IdchuyenKhoaPhongKham equals k.IdchuyenKhoa
                            join d in _context.BacSis on k.IdbacSi equals d.IdbacSi
                            where x.IdchuyenKhoa == idchuyenkhoa && d.TrangThai == true
                            select d;
            return Ok(listbacsi);
        }

        [HttpGet("loccosotheochuyenmon/{idchuyenmon}")]
        public IActionResult loccosotheochuyenmon(int idchuyenmon)
        {
            var listcoso = from c in _context.CoSoDichVuKhacs
                           join d in _context.ChuyenMoncoSos on c.IdcoSoDichVuKhac equals d.IdcoSoDichVuKhac
                           join x in _context.ChuyenMons on d.IdchuyenMon equals x.IdChuyenMon
                           where x.IdChuyenMon == idchuyenmon && c.TrangThai == true
                           select c;

            return Ok(listcoso);
        }

        [HttpGet("locnhanvientheochuyenmon/{idchuyenmon}")]
        public IActionResult locnhanvientheochuyenmon(int idchuyenmon)
        {
            var listnhanvien = from x in _context.NhanVienCoSos
                               join z in _context.PhanLoaiChuyenKhoaNhanViens on x.IdnhanVienCoSo equals z.IdnhanVienCoSo
                               join c in _context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                               join d in _context.ChuyenMons on c.IdchuyenMon equals d.IdChuyenMon
                               where d.IdChuyenMon == idchuyenmon && x.TrangThai == true
                               select x;
                               
            return Ok(listnhanvien);
        }

        [HttpGet("bacsi")]
        public IActionResult GetTypeDoctor()
        {
            var listnhanvienytekhac = from x in _context.BacSis
                                      join c in _context.PhongKhams on x.IdphongKham equals c.IdphongKham
                                      where x.TrangThai == true && c.TrangThai == true
                                      select x;

            var Listdoctor = from s in _context.BacSis
                             join k in _context.KeHoachKhams on s.IdbacSi equals k.IdbacSi
                             join c in _context.DatLiches on k.IdkeHoachKham equals c.IdkeHoachKham
                             join q in _context.TaoLiches on c.IddatLich equals q.IddatLich
                             join d in _context.PhongKhams on s.IdphongKham equals d.IdphongKham
                             where s.TrangThai == true && d.TrangThai == true && q.TrangThaiTaoLich == 3
                             select new {
                                 s.IdbacSi , s.HoTenBacSi ,s.AnhBacSi,s.Idnguoidung,s.DanhGiaCosos,s.Danhgia,c.TaoLiches};
            return Ok(listnhanvienytekhac);
        }

        [HttpGet("GetTypechuyenkhoabacsi/{idbacsi}")]
        public IActionResult GetTypechuyenkhoabacsi(string idbacsi)
        {
            var chuyenkhoa = from a in _context.ChuyenKhoas
                             join c in _context.PhanLoaiBacSiChuyenKhoas on a.IdchuyenKhoa equals c.IdchuyenKhoa
                             join d in _context.BacSis on c.IdbacSi equals d.IdbacSi
                             where d.IdbacSi == idbacsi
                             select a.TenChuyenKhoa;
          
                return Ok(chuyenkhoa);       
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
                    join d in _context.ChuyenKhoaPhongKhams on s.IdphongKham equals d.IdphongKham
                    join c in _context.ChuyenKhoas on d.IdchuyenKhoa equals c.IdchuyenKhoa
                    where s.TrangThai == true
                    select new
                    {
                        s
                    };
            return Ok(a);
        }

        [HttpGet("SoLanKhamThanhCongCuaBacSi/{idbacsi}")]
        public IActionResult SoLanKhamThanhCongCuaBacSi(string idbacsi)
        {
            var solankham = from x in _context.KeHoachKhams
                            join c in _context.DatLiches on x.IdkeHoachKham equals c.IdkeHoachKham
                            join d in _context.TaoLiches on c.IddatLich equals d.IddatLich
                            where x.IdbacSi == idbacsi && d.TrangThaiTaoLich == 3 || d.TrangThaiTaoLich == 4
                            select new
                            {
                                d
                            };

            return Ok(solankham.Count());
        }


        [HttpGet("LayThongTinBacSiBangID/{idbacsi}")]
        public IActionResult LayThongTinBacSiBangID(string idbacsi)
        {
            var info = _context.BacSis.FirstOrDefault(x => x.IdbacSi == idbacsi);
            return Ok(info);
        }

        [HttpGet("GetDoctorById/{idbacsi}")]
        public IActionResult GetDoctorById(string idbacsi)
        {
            var bacsi = from a in _context.BacSis
                        join b in _context.PhongKhams on a.IdphongKham equals b.IdphongKham
                        where a.IdbacSi == idbacsi
                        select new
                        {
                            a.Danhgia,
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

          
                return Ok(listchucdanh);
    

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
                                   select new { 
                                       c.ThoiGianDatLich, c.IddatLich , c.SoLuongHienTai , 
                                       c.SoLuongToiDa, x.NgayDatLich,c.TrangThaiDatLich,x.TrangThaiKeHoachKham};

                return Ok(thoigianlichkham);
        }


        [HttpGet("LayTatCaNhanVienYTeKHac")]
        public IActionResult LayTatCaNhanVienYTeKHac()
        {
            var listnhanvienytekhac = from x in _context.NhanVienCoSos
                                      join c in _context.CoSoDichVuKhacs on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                                      where x.TrangThai == true && c.TrangThai == true
                                      select x;

            return Ok(listnhanvienytekhac);
        }

        

        [HttpGet("LayTatCaPhongKham")]
        public IActionResult LaytatcaPhongKham()
        {
            var listClinic = _context.PhongKhams.Where(x=>x.TrangThai == true).ToList();
            return Ok(listClinic);
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

        [HttpGet("LayDanhSachBacsiChuyenKhoa/{idchuyenkhoa}")]
        public IActionResult LayDanhSachBacsiChuyenKhoa (int idchuyenkhoa)
        {
            var list = from x in _context.ChuyenKhoas
                       join c in _context.ChuyenKhoaPhongKhams on x.IdchuyenKhoa equals c.IdchuyenKhoa
                       join d in _context.PhanLoaiBacSiChuyenKhoas on c.IdchuyenKhoaPhongKham equals d.IdchuyenKhoa
                       join e in _context.BacSis on d.IdbacSi equals e.IdbacSi
                       join k in _context.PhongKhams on e.IdphongKham equals k.IdphongKham
                       where x.IdchuyenKhoa == idchuyenkhoa && e.TrangThai == true && k.TrangThai== true
                       select e
                       ;
            return Ok(list);
        }

        [HttpGet("Laythongtinchuyenkhoa/{idchuyenkhoa}")]
        public IActionResult Laythongtinchuyenkhoa (int idchuyenkhoa)
        {
            var chuyenkhoa = _context.ChuyenKhoas.FirstOrDefault(x => x.IdchuyenKhoa == idchuyenkhoa);

            if (chuyenkhoa != null)
            {
                return Ok(chuyenkhoa);
            }
            else
            {
                return BadRequest("Chuyên khoa không tồn tại");
            }

        }

        [HttpGet("Laydanhsachchuyenmon")]
        public IActionResult Laydanhsachchuyenmon(int idchuyenmon)
        {
            var list = _context.ChuyenMons.ToList();

            return Ok(list);
        }

        [HttpGet("LayDanhSachNhanVienChuyenMon/{idchuyenkhoa}")]
        public IActionResult LayDanhSachNhanVienChuyenMon(int idchuyenkhoa)
        {
            var list = from x in _context.ChuyenMons
                       join c in _context.ChuyenMoncoSos on x.IdChuyenMon equals c.IdchuyenMon
                       join d in _context.PhanLoaiChuyenKhoaNhanViens on c.IdchuyenMonCoSo equals d.ChuyenMoncoSo
                       join e in _context.NhanVienCoSos on d.IdnhanVienCoSo equals e.IdnhanVienCoSo
                       join k in _context.CoSoDichVuKhacs on e.IdcoSoDichVuKhac equals k.IdcoSoDichVuKhac
                       where x.IdChuyenMon == idchuyenkhoa && e.TrangThai == true && k.TrangThai == true
                       select e;
            return Ok(list);
        }

        [HttpGet("Laythongtinchuyenmon/{idchuyenmon}")]
        public IActionResult Laythongtinchuyenmon(int idchuyenmon)
        {
            var chuyenkhoa = _context.ChuyenMons.FirstOrDefault(x => x.IdChuyenMon == idchuyenmon);

            if (chuyenkhoa != null)
            {
                return Ok(chuyenkhoa);
            }
            else
            {
                return BadRequest("Chuyên môn không tồn tại");
            }

        }

        [HttpGet("getcosoByIdnhanvien/{idnhanvien}")]
        public IActionResult getcosoByIdnhanvien(string idnhanvien)
        {
            var diachibacsi = from a in _context.NhanVienCoSos
                              join b in _context.CoSoDichVuKhacs on a.IdcoSoDichVuKhac equals b.IdcoSoDichVuKhac
                              join c in _context.XaPhuongs on b.IdxaPhuong equals c.IdxaPhuong
                              join d in _context.QuanHuyens on c.IdquanHuyen equals d.IdquanHuyen
                              where a.IdnhanVienCoSo == idnhanvien
                              select new
                              {
                                  b.DiaChi,
                                  c.TenXaPhuong,
                                  d.TenQuanHuyen,
                              };
            if (diachibacsi.Count() == 0)
            {
                return BadRequest("Not found");
            }
            else
            {
                return Ok(diachibacsi);
            }

        }

        [HttpGet("LayDanhSachCoSoYTe")]
        public IActionResult LayDanhSachCoSoYTe()
        {
            var list = _context.CoSoDichVuKhacs.Where(x=>x.TrangThai == true);

            return Ok(list);
        }

        [HttpGet("Laychuyenmoncoso/{idcoso}")]
        public IActionResult Laychuyenmoncoso(string idcoso)
        {
            var chuyenmoncoso = from x in _context.CoSoDichVuKhacs
                                join q in _context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals q.IdcoSoDichVuKhac
                                join e in _context.ChuyenMons on q.IdchuyenMon equals e.IdChuyenMon
                                where x.IdcoSoDichVuKhac == idcoso
                                select e;
            return Ok(chuyenmoncoso);
        }

    }
}
