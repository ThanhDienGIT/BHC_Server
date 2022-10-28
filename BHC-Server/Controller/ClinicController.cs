using BHC_Server.Models;
using Com.CloudRail.SI.Types;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BHC_Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly DB_BHCContext _context;

        public ClinicController (DB_BHCContext context, IConfiguration configuration)
        {
            _context = context;
        }

        [HttpGet("{idnguoidung}")]
        public IActionResult GetIdDoctorByIdUser(int idnguoidung)
        {
            var BacSi = _context.BacSis.FirstOrDefault(x=>x.Idnguoidung == idnguoidung);

            if(BacSi == null)
            {
                return Ok("No data");
            }
            else
            {
                return Ok(BacSi.IdbacSi);
            }
        }
        [HttpGet("doctor/{idbacsi}")]
        public IActionResult GetIdDocTorByIdBacSi(string idbacsi)
        {
            var BacSi = _context.BacSis.FirstOrDefault(x => x.IdbacSi == idbacsi);

            if(BacSi == null)
            {
                return BadRequest("Not Found");
            }
            else
            {
                return Ok(BacSi);
            }
        }

        [HttpGet("Clinic/{idbacsi}")]
        public IActionResult GetClinicByIdDocTor(string idbacsi)
        {
            var PhongKham = from p in _context.BacSis
                            join x in _context.PhongKhams on p.IdphongKham equals x.IdphongKham
                            where p.IdbacSi == idbacsi
                            select new
                            {
                                x,
                                p.Idnguoidung,
                                p.HoTenBacSi,
                               
                            };
                if(PhongKham.Count()  == 0)
                {
                    return BadRequest("No Data");
                }
                else
                {
                    return Ok(PhongKham);

                }
                
            
        }

        [HttpGet("Clinic/user/{iduser}")]
        public IActionResult GetClinicByIdUser(int iduser)
        {
            var PhongKham = from p in _context.NguoiDungs
                            join x in _context.PhongKhams on p.IdNguoiDung equals x.IdnguoiDung
                            where p.IdNguoiDung == iduser
                            select new
                            {
                                x,
                                p.IdNguoiDung,
                                p.HoNguoiDung,
                                p.TenNguoiDung,

                            };
            if (PhongKham.Count() == 0)
            {
                return BadRequest("No Data");
            }
            else
            {
                return Ok(PhongKham);
            }
        }

        [HttpGet("ListDoctor/{idbacsi}")]
        public IActionResult GetListDoctor(string idbacsi)
        {
            var idphongkham = from x in _context.BacSis
                              join p in _context.PhongKhams on x.IdphongKham equals p.IdphongKham
                              where p.IdphongKham == x.IdphongKham && x.IdbacSi == idbacsi
                              select p.IdphongKham;
            string a = "";
            a = string.Join("U+002C", idphongkham);
            var listdoctor = from x in _context.BacSis
                             join p in _context.PhongKhams on x.IdphongKham equals p.IdphongKham
                             where p.IdphongKham == a
                             select x;
                            
            if(listdoctor.Count() == 0)
            {
                return BadRequest("no data");
            }
            else
            {
                return Ok(listdoctor);
            }
           
        }


        [HttpPost("PhanLoaiBacSi/{idbacsi}")]
        public IActionResult PhanLoaiBacSi(List<PhanLoaiBacSiChuyenKhoa> phanloai,string idbacsi)
        {
            var list = new List<PhanLoaiBacSiChuyenKhoa>();
            phanloai.ForEach(ele =>
            {
                list.Add(
                  new PhanLoaiBacSiChuyenKhoa(){ IdbacSi = idbacsi , IdchuyenKhoa = ele.IdchuyenKhoa}
                );
            });
           _context.PhanLoaiBacSiChuyenKhoas.AddRange(list);
           _context.SaveChanges();
            return Ok(list);     
        }


        [HttpPost("AddDoctor/{idbacsi}")]
        public IActionResult AddDoctor(AddDoctor doctor,string idbacsi)
        {
            var checkEmail = _context.BacSis.FirstOrDefault(x => x.EmailBacSi == doctor.EmailBacSi);
            var checkSDT = _context.BacSis.FirstOrDefault(x => x.SoDienThoaiBacSi == doctor.SoDienThoaiBacSi);
            var checkCCCD = _context.BacSis.FirstOrDefault(x => x.Cccd == doctor.Cccd);
            string messemail = ""; string messSDT = ""; string messCCCD = "";
            string mess = "";
            var ramdom = new Ramdom.Ramdom();
          
            if (checkEmail != null)
            {
                messemail = "đã có email ";
            }
            if(checkSDT != null)
            {
                messSDT = "Đã có số điện thoại ";
            }
            if(checkCCCD != null)
            {
                messCCCD = "Đã có căn cước công dân";
            }

            mess += messemail + messSDT + messCCCD;

            if(mess.Length != 0)
            {
                return BadRequest(mess);
            }
            // Xu ly ID Bac Si
            var IDBacSihientai = idbacsi;
           
            string IDPhongKham = IDBacSihientai.Substring(0,3);
            var IDBacSi = from a in _context.PhongKhams
                          join b in _context.BacSis on a.IdphongKham equals b.IdphongKham
                          where b.IdphongKham == IDPhongKham
                          select b;
            var IDBacSi2 = IDBacSi.OrderBy(idbacsi => idbacsi).Last().IdbacSi;
            int numberPlus = Convert.ToInt32(IDBacSi2.Substring(2));
            string TypeID = IDBacSi2.ToString().Substring(0, 2);
            numberPlus++;
            string ID = TypeID + numberPlus;
            // Xu ly TaiKhoan Cua BacSi
            int indexName = doctor.EmailBacSi.IndexOf("@");
            string nameDoctor = doctor.EmailBacSi.Substring(0,indexName);
            string TaiKhoanDoctor = nameDoctor + ID;

            var bacsi = new BacSi
            {
                IdbacSi = ID,
                IdphongKham = IDPhongKham,
                TaiKhoan = TaiKhoanDoctor,
                MatKhau = ramdom.GetPassword(),
                HoTenBacSi = doctor.HoTenBacSi,
                Cccd = doctor.Cccd,
                SoDienThoaiBacSi = doctor.SoDienThoaiBacSi,
                EmailBacSi = doctor.EmailBacSi,
                Idquyen = doctor.Idquyen,
                GiaKham = doctor.GiaKham,
                AnhBacSi = ID + doctor.AnhBacSi,
                AnhChungChiHanhNgheBacSi = ID + doctor.AnhChungChiHanhNgheBacSi,
                GioiTinh = doctor.GioiTinh,
            };
            _context.BacSis.Add(bacsi);
            _context.SaveChanges();
          
            return Ok(bacsi.IdbacSi);
        }






        [HttpGet("GetTypeListSpecialist")]
        public IActionResult GetTypeListSpecialist()
        {
            var listtype = from x in _context.ChuyenKhoas
                           select new
                           {
                               x.IdchuyenKhoa,
                               x.TenChuyenKhoa,
                               x.MoTa,
                               x.Anh,
                           };
            if(listtype.Count() == 0)
            {
                return BadRequest("No data");
            }
            else
            {
                return Ok(listtype);
            }
              
        }

        [HttpGet("laychuyenkhoabyidbacsi/{idbacsi}")]
        public IActionResult laychuyenkhoabyidbacsi(string idbacsi)
        {
            var danhsachchuyenkhoa = from a in _context.ChuyenKhoas
                                     join c in _context.PhanLoaiBacSiChuyenKhoas on a.IdchuyenKhoa equals c.IdchuyenKhoa
                                     join b in _context.BacSis on c.IdbacSi equals b.IdbacSi
                                     where c.IdbacSi == idbacsi
                                     select new
                                     {
                                         a.IdchuyenKhoa,
                                         a.TenChuyenKhoa,
                                         a.Anh,
                                         a.MoTa
                                     };
            if(danhsachchuyenkhoa.Count() == 0)
            {
                return BadRequest("Nodata");
            }
            else
            {
                return Ok(danhsachchuyenkhoa);
            }
                     
        }

        [HttpPut("DeleteDoctor/{iddoctor}")]
        public IActionResult DeleteDocTor(string iddoctor)
        {
            var deletefrom = _context.BacSis.FirstOrDefault(x => x.IdbacSi == iddoctor);
            if(deletefrom != null)
            {
                deletefrom.TrangThai = false;
                _context.SaveChanges();
                return Ok("Success");
            }
            return NotFound("Not Found");
        }

        [HttpPut("EditKeHoach")]
        public IActionResult EditKeHoach(ThayDoiKeHoach edit)
        {
            var datlich = _context.DatLiches.FirstOrDefault(x => x.IddatLich == edit.iddatlich);
            if (datlich != null)
            {
                datlich.ThoiGianDatLich = edit.thoiGianDatLich;
                datlich.SoLuongToiDa = edit.soluongdatlich;
                _context.SaveChanges();
                return Ok("success");
            }
            else
            {
                return BadRequest("failed");
            }   
        }
        // delete chinh sua
        [HttpDelete("DeleteKeHoach/{iddatlich}")]
        public IActionResult DeleteKeHoach(int iddatlich)
        {
          var  Delete = _context.DatLiches.FirstOrDefault(x=>x.IddatLich == iddatlich);
                    
            if(Delete != null)
            {
                _context.DatLiches.Remove(Delete);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("Have User");
            }
        }


        [HttpPut("EditDoctor")]
        public IActionResult EditDoctor(EditDoctor edit)
        {
            var doctor = _context.BacSis.FirstOrDefault(x => x.IdbacSi == edit.idbacSi);
            var checkEmail = _context.BacSis.FirstOrDefault(x => x.EmailBacSi == edit.EmailBacSi);
            var checkSDT = _context.BacSis.FirstOrDefault(x => x.SoDienThoaiBacSi == edit.SoDienThoaiBacSi);
            var checkCCCD = _context.BacSis.FirstOrDefault(x => x.Cccd == edit.Cccd);
            string messemail = ""; string messSDT = ""; string messCCCD = "";
            string mess = "";
            var ramdom = new Ramdom.Ramdom();

            if (checkEmail != null && checkEmail.EmailBacSi != edit.EmailBacSi)
            {
                messemail = "đã có email ";
            }
            if (checkSDT != null && checkSDT.SoDienThoaiBacSi != edit.SoDienThoaiBacSi)
            {
                messSDT = "Đã có số điện thoại ";
            }
            if (checkCCCD != null && checkCCCD.Cccd != edit.Cccd)
            {
                messCCCD = "Đã có căn cước công dân";
            }

            mess += messemail + messSDT + messCCCD;

            if (mess.Length != 0)
            {
                return BadRequest(mess);
            }
            else
            {
                if (doctor != null)
                {
                    doctor.IdbacSi = edit.idbacSi;
                    doctor.HoTenBacSi = edit.HoTenBacSi;
                    doctor.Cccd = edit.Cccd;
                    doctor.SoDienThoaiBacSi = edit.SoDienThoaiBacSi;
                    doctor.Idquyen = edit.Idquyen;
                    doctor.GiaKham = edit.GiaKham;
                    doctor.GioiTinh = edit.GioiTinh;
                    doctor.AnhBacSi = edit.AnhBacSi;
                    doctor.AnhChungChiHanhNgheBacSi = edit.AnhChungChiHanhNgheBacSi;
                  //  _context.SaveChanges();
                    return Ok(messCCCD);
                }
            }
            return BadRequest("Failed");
        }

        [HttpGet("LayDanhSachLichDangChoDuyet/{idbacsi}")]
        public IActionResult LayDanhSachLichDangChoDuyet(string idbacsi)
        {
            var a = from c in _context.KeHoachKhams
                    join d in _context.DatLiches on c.IdkeHoachKham equals d.IdkeHoachKham
                    join e in _context.TaoLiches on d.IddatLich equals e.IddatLich
                    join q in _context.NguoiDungs on  e.IdnguoiDungDatLich equals q.IdNguoiDung
                    where c.IdbacSi == idbacsi && e.TrangThaiTaoLich == 1
                    select new
                    {
                        q.HoNguoiDung,
                        q.TenNguoiDung,
                        q.XacThuc,
                        q.GioiTinh,
                        q.SoDienThoai,
                        q.Email,
                        q.IdNguoiDung,
                        q.NgaySinh,
                        e.LyDoKham,
                        c.NgayDatLich,
                        d.ThoiGianDatLich,
                        d.IddatLich,
                        e.NgayGioDatLich,
                    };

       
                return Ok(a);
        
        }

        [HttpGet("LayDanhSachUserDatLichCuaMotBacSi/{idbacsi}")]
        public IActionResult LayDanhSachUserDatLichCuaMotBacSi(string idbacsi)
        {
            var List = from x in _context.KeHoachKhams
                       join c in _context.DatLiches on x.IdkeHoachKham equals c.IdkeHoachKham
                       join d in _context.TaoLiches on c.IddatLich equals d.IddatLich
                       join z in _context.NguoiDungs on d.IdnguoiDungDatLich equals z.IdNguoiDung
                       where x.IdbacSi == idbacsi
                       select new
                       {
                           d.IdnguoiDungDatLichNavigation
                       };
            
                return Ok(List);
        }

        [HttpGet("LayLichKhamBacSi/{idbacsi}")]
        public IActionResult LayLichKhamBacSi(string idbacsi)
        {

            var List = (from a in _context.DatLiches
                       join c in _context.KeHoachKhams on a.IdkeHoachKham equals c.IdkeHoachKham
                       join d in _context.TaoLiches on a.IddatLich equals d.IddatLich into dept
                       from k in dept.DefaultIfEmpty()
                       select new
                       {
                           c.IdbacSi,
                           c.NgayDatLich,
                           c.TrangThaiKeHoachKham,
                           a.ThoiGianDatLich,
                           a.IddatLich,
                           a.SoLuongToiDa,
                           a.TrangThaiDatLich,
                           LyDoKham = k.LyDoKham != null ? k.LyDoKham : null,
                           IdtaoLich = k.IdtaoLich != null ? k.IdtaoLich : 0,
                           User = k.IdnguoiDungDatLichNavigation != null ? k.IdnguoiDungDatLichNavigation : null,
                           TrangThaiTaoLich = k.TrangThaiTaoLich != null ? k.TrangThaiTaoLich : null,
                       }).Where(c=>c.IdbacSi == idbacsi).OrderBy(c => c.NgayDatLich);

            return Ok(List);
        }


        [HttpPost("HuyLich")]
        public IActionResult HuyLich(DuyetLich duyetlich)
        {
            var a = _context.TaoLiches.FirstOrDefault(x => x.IdtaoLich == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            var nguoidung = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == duyetlich.IDNguoiDung);
            if (a != null && nguoidung != null)
            {
                a.TrangThaiTaoLich = 0;
                nguoidung.HuyLich++;
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }

        [HttpPost("XacNhanKham")]
        public IActionResult XacNhanKham(DuyetLich duyetlich)
        {
            var a = _context.TaoLiches.FirstOrDefault(x => x.IdtaoLich == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            if (a != null)
            {
                a.TrangThaiTaoLich = 3;
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }

        [HttpPost("HenTaiKham")]
        public IActionResult HenTaiKham(DuyetLich duyetlich)
        {
            var a = _context.TaoLiches.FirstOrDefault(x => x.IdtaoLich == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            if (a != null)
            {
                a.TrangThaiTaoLich = 4;
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }


        [HttpPost("DuyetLich")]
        public IActionResult XacNhanDatLich(DuyetLich duyetlich)
        {
            var a = _context.TaoLiches.FirstOrDefault(x => x.IddatLich == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            if(a != null)
            {
                a.TrangThaiTaoLich = 2;
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
          
        }

        [HttpPost("HuyLichchokham")]
        public IActionResult HuyLichchokham(DuyetLich duyetlich)
        {
            var a = _context.TaoLiches.FirstOrDefault(x => x.IddatLich == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            var datlich = _context.DatLiches.FirstOrDefault(x => x.IddatLich == duyetlich.IDDatLich);
            if (a != null && datlich != null)
            {
                _context.TaoLiches.Remove(a);
                datlich.SoLuongHienTai--;
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }

        }

    }
}
