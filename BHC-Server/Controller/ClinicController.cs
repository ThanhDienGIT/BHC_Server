using BHC_Server.Models;
using Com.CloudRail.SI.Types;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks.Dataflow;

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

        [HttpGet("Facitilies/{iduser}")]
        public IActionResult Facitilies(int iduser)
        {
            var facitilies = from x in _context.CoSoDichVuKhacs
                             join c in _context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                             where x.IdnguoiDung == iduser
                             select c.IdnhanVienCoSo;
            return Ok(facitilies);

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

        [HttpGet("Laychuyenkhoacuamotphongkham/{idbacsi}")]
        public IActionResult Laychuyenkhoacuamotphongkham(string idbacsi)
        {
            var list = from x in _context.PhongKhams
                       join c in _context.ChuyenKhoaPhongKhams on x.IdphongKham equals c.IdphongKham
                       join d in _context.ChuyenKhoas on c.IdchuyenKhoa equals d.IdchuyenKhoa
                       join e in _context.BacSis on x.IdphongKham equals e.IdphongKham
                       where e.IdbacSi == idbacsi
                       select new
                       {
                           c.IdchuyenKhoaPhongKham,
                           x.IdphongKham,
                           d.TenChuyenKhoa,
                           d.IdchuyenKhoa,
                       };
            return Ok(list);
        }

        [HttpGet("layphongkhambangidbacsi/{idbacsi}")]
        public IActionResult layphongkhambangidbacsi(string idbacsi)
        {
            var phongkham = (from x in _context.BacSis
                            join d in _context.PhongKhams on x.IdphongKham equals d.IdphongKham
                            where x.IdbacSi == idbacsi
                            select d).FirstOrDefault();
            return Ok(phongkham);
        }

        [HttpGet("layphongkhambangidphongkham/{idphongkham}")]
        public IActionResult layphongkhambangidphongkham(string idphongkham)
        {
            var phongkham = _context.PhongKhams.FirstOrDefault(x => x.IdphongKham == idphongkham);
                            
            return Ok(phongkham);
        }

        [HttpGet("laychuyenkhoaphongkhambangidphongkham/{idphongkham}")]
        public IActionResult laychuyenkhoaphongkhambangidphongkham(string idphongkham)
        {
            var list = from x in _context.PhongKhams
                       join c in _context.ChuyenKhoaPhongKhams on x.IdphongKham equals c.IdphongKham
                       join d in _context.ChuyenKhoas on c.IdchuyenKhoa equals d.IdchuyenKhoa
                       where x.IdphongKham == idphongkham
                       select new
                       {
                           x.IdphongKham,
                           d.TenChuyenKhoa,
                           d.IdchuyenKhoa,
                           c.IdchuyenKhoaPhongKham
                       };
            return Ok(list);
        }

        [HttpGet("locchuyenkhoaphongkhambangidphongkham/{idphongkham}/{idchuyenkhoa}")]
        public IActionResult locchuyenkhoaphongkhambangidphongkham(string idphongkham,int idchuyenkhoa)
        {
            var list = from x in _context.PhongKhams
                       join c in _context.ChuyenKhoaPhongKhams on x.IdphongKham equals c.IdphongKham
                       join d in _context.ChuyenKhoas on c.IdchuyenKhoa equals d.IdchuyenKhoa
                       where x.IdphongKham == idphongkham && d.IdchuyenKhoa == idchuyenkhoa
                       select new
                       {
                           x.IdphongKham,
                           d.TenChuyenKhoa,
                           d.IdchuyenKhoa,
                       };
            return Ok(list);
        }


        [HttpPost("ThemChuyenKhoaChoPhongKham/{idphongkham}")]
        public IActionResult ThemChuyenKhoaChoPhongKham(string idphongkham,List<ChuyenKhoaPhongKham> chuyenkhoa)
        {
            var checkchuyenkhoa = _context.ChuyenKhoaPhongKhams.Where(x => x.IdphongKham == idphongkham).ToList();
            var list = new List<ChuyenKhoaPhongKham>();

            if (checkchuyenkhoa.Count() > 0)
            {
                chuyenkhoa.ForEach(ele =>
                {
                    checkchuyenkhoa.ForEach(ala =>
                    {
                        if (ele.IdchuyenKhoa != ala.IdchuyenKhoa)
                        {
                           list.Add(
                           new ChuyenKhoaPhongKham()
                           {
                               IdchuyenKhoa = ele.IdchuyenKhoa,
                               IdphongKham = idphongkham,
                           });
                        }
                    });
                });
                if(list.Count() > 0)
                {
                    _context.ChuyenKhoaPhongKhams.AddRange(list);
                    _context.SaveChanges();
                    return Ok("Success");
                }
                else
                {
                    return Ok("Không có sự thay đổi chuyên khoa");
                }
            }
            else
            {
                chuyenkhoa.ForEach(ele =>
                {
                    list.Add(
                        new ChuyenKhoaPhongKham()
                        {
                            IdchuyenKhoa = ele.IdchuyenKhoa,
                            IdphongKham = idphongkham,
                        });
                });
                _context.ChuyenKhoaPhongKhams.AddRange(list);
                _context.SaveChanges();
                return Ok("Success");
            } 
           
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


        [HttpGet("LayChucDanhCuaMotBacSi/{idbacsi}")]
        public IActionResult LayChucDanhCuaMotBacSi(string idbacsi)
        {
            var listchucdanh = from x in _context.BacSis
                          join c in _context.ChucDanhBacSis on x.IdbacSi equals c.IdbacSi
                          join d in _context.ChucDanhs on c.IdchucDanh equals d.IdchucDanh
                          where x.IdbacSi == idbacsi
                          select new
                          {
                              x.IdbacSi,
                              d.TenChucDanh,
                              d.IdchucDanh,
                          };
            return Ok(listchucdanh);
        }

        [HttpGet("LayChuyenKhoaCuaMotBacSi/{idbacsi}")]
        public IActionResult LayChuyenKhoaCuaMotBacSi(string idbacsi)
        {
            var ListChuyenKhoa = from x in _context.BacSis
                                 join c in _context.PhanLoaiBacSiChuyenKhoas on x.IdbacSi equals c.IdbacSi
                                 join k in _context.ChuyenKhoaPhongKhams on c.IdchuyenKhoa equals k.IdchuyenKhoaPhongKham
                                 join d in _context.ChuyenKhoas on k.IdchuyenKhoa equals d.IdchuyenKhoa
                                 where x.IdbacSi == idbacsi
                                 select new
                                 {
                                     k.IdchuyenKhoaPhongKham,
                                     k.IdphongKham,       
                                     d.TenChuyenKhoa,  
                                     d.IdchuyenKhoa,
                                 };        

            return Ok(ListChuyenKhoa);
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

        [HttpGet("LayDanhSachBacSiCuaMotPhongKham/{idphongkham}")]
        public IActionResult DanhSachBacSi(string idphongkham)
        {
            var danhsachbacsi = from x in _context.PhongKhams
                                join BacSi in _context.BacSis on x.IdphongKham equals BacSi.IdphongKham
                                where x.IdphongKham == idphongkham && BacSi.TrangThai == true
                                select BacSi;

            return Ok(danhsachbacsi);
        }

        [HttpGet("LayDanhSachBacSiCuaMotPhongKham/{idphongkham}/{idchuyenkhoa}")]
        public IActionResult DanhSachBacSi(string idphongkham,int idchuyenkhoa)
        {

            var danhsachbacsi2 = from a in _context.BacSis
                                 join c in _context.PhanLoaiBacSiChuyenKhoas on a.IdbacSi equals c.IdbacSi                             
                                 where a.IdphongKham == idphongkham && c.IdchuyenKhoa == idchuyenkhoa
                                 select a;

            return Ok(danhsachbacsi2);
        }

        [HttpPut("Capnhatmotaphongkham")]
        public IActionResult Capnhatmotaphongkham(CapNhatMoTaPhongKham mota)
        {
            var checkphongkham = _context.PhongKhams.FirstOrDefault(x => x.IdphongKham == mota.idphongkham);
            if(checkphongkham != null)
            {
                checkphongkham.LoiGioiThieu = mota.loigoithieu;
                checkphongkham.ChuyenMon = mota.chuyenmon;
                checkphongkham.TrangThietBi = mota.trangthietbi;
                checkphongkham.ViTri = mota.vitri;
                _context.SaveChanges();
                return Ok("Success");
            }
;            return BadRequest("Not found");
        }

        [HttpGet("XaPhuongQuanHuyenPhongKham/{idphongkham}")]
        public IActionResult XaPhuongQuanHuyenPhongKham(string idphongkham)
        {
            var address = from x in _context.XaPhuongs
                          join c in _context.QuanHuyens on x.IdquanHuyen equals c.IdquanHuyen
                          join d in _context.PhongKhams on x.IdxaPhuong equals d.IdxaPhuong
                          where d.IdphongKham == idphongkham
                          select new
                          {
                              x.TenXaPhuong,
                              c.TenQuanHuyen,
                              d.IdphongKham,
                              d.DiaChi,
                          };
            return Ok(address);
        }

        [HttpGet("XaPhuongQuanHuyenPhongKhambacsi/{idbacsi}")]
        public IActionResult XaPhuongQuanHuyenPhongKhambacsi(string idbacsi)
        {
            var address = from x in _context.XaPhuongs
                          join c in _context.QuanHuyens on x.IdquanHuyen equals c.IdquanHuyen
                          join d in _context.PhongKhams on x.IdxaPhuong equals d.IdxaPhuong
                          join q in _context.BacSis on d.IdphongKham equals q.IdphongKham
                          where q.IdbacSi == idbacsi
                          select new
                          {
                              x.TenXaPhuong,
                              c.TenQuanHuyen,
                              c.IdquanHuyen,
                              x.IdxaPhuong,
                              d.IdphongKham,
                              d.DiaChi,
                          };
            return Ok(address);
        }

        [HttpGet("Laytatcachucdanhbacsi/{idphongkham}")]
        public IActionResult Laytatcachucdanhbacsi(string idphongkham)
        {
            var listchucdanh = from x in _context.PhongKhams
                               join z in _context.BacSis on x.IdphongKham equals z.IdphongKham
                               join c in _context.ChucDanhBacSis on z.IdbacSi equals c.IdbacSi
                               join d in _context.ChucDanhs on c.IdchucDanh equals d.IdchucDanh
                               where x.IdphongKham == idphongkham
                               select new
                               {
                                   z.IdbacSi,
                                   d.TenChucDanh,
                               };
            return Ok(listchucdanh);
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

        [HttpPut("EditChuyenKhoaBacSi/{idbacsi}")]
        public IActionResult EditChuyenKhoaBacSi(string idbacsi,List<ChuyenKhoaPhongKham> phanloai)
        {
            var listchuyenkhoa = _context.PhanLoaiBacSiChuyenKhoas.Where(x => x.IdbacSi == idbacsi);
            if(listchuyenkhoa != null)
            {
                _context.PhanLoaiBacSiChuyenKhoas.RemoveRange(listchuyenkhoa);
                var list = new List<PhanLoaiBacSiChuyenKhoa>();
                phanloai.ForEach(ele =>
                {
                    list.Add(
                      new PhanLoaiBacSiChuyenKhoa() { IdbacSi = idbacsi, IdchuyenKhoa = ele.IdchuyenKhoaPhongKham }
                    );
                });
                _context.PhanLoaiBacSiChuyenKhoas.AddRange(list);
                  _context.SaveChanges();
                return Ok(phanloai);
            }
            else
            {
                var list = new List<PhanLoaiBacSiChuyenKhoa>();
                phanloai.ForEach(ele =>
                {
                    list.Add(
                      new PhanLoaiBacSiChuyenKhoa() { IdbacSi = idbacsi, IdchuyenKhoa = ele.IdchuyenKhoaPhongKham  }
                    );
                });
                _context.PhanLoaiBacSiChuyenKhoas.AddRange(list);
                _context.SaveChanges();
                return Ok(phanloai);
            }
            return BadRequest("Failed");
        }

        [HttpPut("themchucdanhbacsi/{idbacsi}")]
        public async Task<IActionResult> themchucdanhbacsi(string idbacsi, List<ChucDanhBacSi> phanloai)
        {
            var listchuyenkhoa = _context.ChucDanhBacSis.Where(x => x.IdbacSi == idbacsi);


            if(listchuyenkhoa != null)
            {
                 _context.ChucDanhBacSis.RemoveRange(listchuyenkhoa);
                var list = new List<ChucDanhBacSi>();
                phanloai.ForEach(ele =>
                {
                    list.Add(
                      new ChucDanhBacSi() { IdbacSi = idbacsi, IdchucDanh = ele.IdchucDanh }
                    );
                });
                await _context.ChucDanhBacSis.AddRangeAsync(list);
                _context.SaveChanges();
                return Ok("Có xóa chức danh cũ");
            }
            else
            {
                var list = new List<ChucDanhBacSi>();
                phanloai.ForEach(ele =>
                {
                    list.Add(
                      new ChucDanhBacSi() { IdbacSi = idbacsi, IdchucDanh = ele.IdchucDanh }
                    );
                });
                _context.ChucDanhBacSis.AddRange(list);
                _context.SaveChanges();
                return Ok("Chỉ thêm");
            }     
        }

        [HttpGet("Danhsachchucdanh")]
        public IActionResult Danhsachchucdanh()
        {
            var list = _context.ChucDanhs.ToList();
            return Ok(list);
        }

        [HttpGet("laychucdanhcuabacsi/{idbacsi}")]
        public IActionResult laychucdanhcuabacsi(string idbacsi)
        {
            var list = from x in _context.BacSis
                       join c in _context.ChucDanhBacSis on x.IdbacSi equals c.IdbacSi
                       join d in _context.ChucDanhs on c.IdchucDanh equals d.IdchucDanh
                       where x.IdbacSi == idbacsi
                       select new
                       {
                           x.IdbacSi,
                           d.TenChucDanh,
                           d.IdchucDanh
                       };

            return Ok(list);
        }

        [HttpPost("Capnhatmota/{idbacsi}")]
        public IActionResult Capnhatmota(string idbacsi,CapNhatMoTabacSi mota)
        {
            var bacsi = _context.BacSis.FirstOrDefault(x => x.IdbacSi == idbacsi);
            if(bacsi != null)
            {
                bacsi.MoTa = mota.mota;
                _context.SaveChanges();
                return Ok("Success");
            };
            return BadRequest("failed");
        }

        [HttpGet("laymotabacsi/{idbacsi}")]
        public IActionResult laymotabacsi(string idbacsi)
        {
            var bacsi = _context.BacSis.FirstOrDefault(x => x.IdbacSi == idbacsi);
            if (bacsi != null)
            {
                return Ok(bacsi.MoTa);
            };
            return BadRequest("failed");
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

        [HttpGet("LayThongTinPHongKhamByIdPhongKham/{idphongkham}")]
        public IActionResult LayThongTinPHongKhamByIdPhongKham(string idphongkham)
        {
            var info = _context.PhongKhams.FirstOrDefault(x => x.IdphongKham == idphongkham);

            return Ok(info);
        }

        [HttpGet("LayChuyenKhoaPhongKham/{idphongkham}")]
        public IActionResult LayChuyenKhoaPhongKham(string idphongkham)
        {
            var a = from s in _context.PhongKhams
                    join d in _context.ChuyenKhoaPhongKhams on s.IdphongKham equals d.IdphongKham
                    join c in _context.ChuyenKhoas on d.IdchuyenKhoa equals c.IdchuyenKhoa
                    where s.IdphongKham == idphongkham
                    select new
                    {
                        s.IdphongKham,
                        c.TenChuyenKhoa,
                        c.MoTa,
                        c.Anh
                    };
            return Ok(a);
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
                    doctor.HoTenBacSi = edit.HoTenBacSi;
                    doctor.Cccd = edit.Cccd;
                    doctor.EmailBacSi = edit.EmailBacSi;
                    doctor.SoDienThoaiBacSi = edit.SoDienThoaiBacSi;
                    doctor.Idquyen = edit.Idquyen;
                    doctor.GiaKham = edit.GiaKham;
                    doctor.GioiTinh = edit.GioiTinh;
                    doctor.AnhBacSi = edit.AnhBacSi;
                    doctor.AnhChungChiHanhNgheBacSi = edit.AnhChungChiHanhNgheBacSi;
                    _context.SaveChanges();
                    return Ok("success");
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
                        e.TrangThaiTaoLich,
                        d.IddatLich,
                        e.NgayGioDatLich,

                    };
                return Ok(a);
        }

        [HttpPut("KhoiPhucBacSi/{idbacsi}")]
        public IActionResult KhoiPhucBacSi(string idbacsi)
        {
            var a = _context.BacSis.FirstOrDefault(x => x.IdbacSi == idbacsi);

            if(a!= null)
            {
                a.TrangThai = true;
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("Failed");
            }
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
            var a = _context.TaoLiches.FirstOrDefault(x => x.IddatLich == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
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
            var a = _context.TaoLiches.FirstOrDefault(x => x.IddatLich == duyetlich.IDDatLich);
            var checkbacsikham = (from x in _context.BacSis
                          join c in _context.KeHoachKhams on x.IdbacSi equals c.IdbacSi
                          join d in _context.DatLiches on c.IdkeHoachKham equals d.IdkeHoachKham
                          where d.IddatLich == duyetlich.IDDatLich
                          select x).FirstOrDefault();
            var checkphongkhamkham = (
                           from x in _context.BacSis
                           join q in _context.PhongKhams on x.IdphongKham equals q.IdphongKham
                           join c in _context.KeHoachKhams on x.IdbacSi equals c.IdbacSi
                           join d in _context.DatLiches on c.IdkeHoachKham equals d.IdkeHoachKham
                           where d.IddatLich == duyetlich.IDDatLich
                           select q).FirstOrDefault();
            if (a != null && checkbacsikham != null && checkphongkhamkham != null)
            {
                a.TrangThaiTaoLich = 3;
                a.GiaKham =Convert.ToDouble(checkbacsikham.GiaKham);
                checkbacsikham.Solandatlich++;
                checkphongkhamkham.Solandatlich++;
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok(a);
            }
        }


        [HttpPost("HenTaiKham")]
        public IActionResult HenTaiKham(DuyetLich duyetlich)
        {
            var a = _context.TaoLiches.FirstOrDefault(x => x.IddatLich == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
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

        [HttpGet("LayDanhSachQuanHuyen")]
        public IActionResult LayDanhSachQuanHuyen()
        {
            var Danhsachquanhuyen = _context.QuanHuyens.ToList();

            return Ok(Danhsachquanhuyen);
        }

        [HttpGet("LayDanhSachXaPhuong/{idquanhuyen}")]
        public IActionResult LayDanhSachQuanHuyen(int idquanhuyen)
        {
            var Danhsachxaphuong = _context.XaPhuongs.Where(x => x.IdquanHuyen == idquanhuyen).ToList();

            return Ok(Danhsachxaphuong);
        }

        [HttpGet("idquanhuyentheoidxaphuong/{idxaphuong}")]
        public IActionResult idquanhuyentheoidxaphuong(int idxaphuong)
        {
            var xaphuong = _context.XaPhuongs.FirstOrDefault(x => x.IdxaPhuong == idxaphuong);
            if(xaphuong != null)
            {
                return Ok(xaphuong.IdquanHuyen);
            }
            else
            {
                return Ok(0);
            }      
        }

        [HttpPut("Capnhatthongtinphongkham")]
        public IActionResult Capnhatthongtinphongkham(CapNhatThongTinPhongKham phongkham)
        {
            var checkphongkham = _context.PhongKhams.FirstOrDefault(x => x.IdphongKham == phongkham.IdphongKham);

            if(checkphongkham != null)
            {
                checkphongkham.BaoHiem = phongkham.BaoHiem;
                checkphongkham.TenPhongKham = phongkham.TenPhongKham;
                checkphongkham.AnhDaiDienPhongKham = phongkham.AnhDaiDienPhongKham;
                checkphongkham.HinhAnh = phongkham.HinhAnh;
                checkphongkham.DiaChi = phongkham.DiaChi;
                checkphongkham.IdxaPhuong = phongkham.IdxaPhuong;
              
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("failed");
            }
        }

        [HttpGet("laydanhsachdakhamcuamotbacsi/{idbacsi}")]
        public IActionResult laydanhsachdakhamcuamotbacsi(string idbacsi)
        {
            var list = (from x in _context.BacSis
                       join d in _context.KeHoachKhams on x.IdbacSi equals d.IdbacSi
                       join c in _context.DatLiches on d.IdkeHoachKham equals c.IdkeHoachKham
                       join e in _context.TaoLiches on c.IddatLich equals e.IddatLich
                       join k in _context.NguoiDungs on e.IdnguoiDungDatLich equals k.IdNguoiDung
                       where x.IdbacSi == idbacsi && e.TrangThaiTaoLich != 1 && e.TrangThaiTaoLich != 2
                        select new
                       {
                           k.IdNguoiDung,
                           k.NgaySinh,
                           e.LyDoKham,
                           k.GioiTinh,
                           k.HoNguoiDung,
                           k.TenNguoiDung,
                           k.Email,
                           k.SoDienThoai,
                           d.NgayDatLich,
                           c.ThoiGianDatLich,
                           e.TrangThaiTaoLich,
                       }).OrderBy(x=>x.NgayDatLich);
            return Ok(list);
        }

        [HttpGet("StatisticalClinic/{idphongkham}")]
        public IActionResult StatisticalClinic(string idphongkham)
        {
            var danhsach = from x in _context.PhongKhams
                           join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                           join q in _context.KeHoachKhams on c.IdbacSi equals q.IdbacSi
                           join w in _context.DatLiches on q.IdkeHoachKham equals w.IdkeHoachKham
                           join y in _context.TaoLiches on w.IddatLich equals y.IddatLich
                           join k in _context.NguoiDungs on y.IdnguoiDungDatLich equals k.IdNguoiDung
                           where x.IdphongKham == idphongkham && y.TrangThaiTaoLich == 3
                           select new
                           {
                               c.HoTenBacSi,
                               y.GiaKham,
                               c.IdbacSi,
                               q.NgayDatLich,
                               w.ThoiGianDatLich,
                               k.HoNguoiDung,
                               k.TenNguoiDung,
                           };

            return Ok(danhsach);
        }

        [HttpGet("layidphongkhambangidbacsi/{idbacsi}")]
        public IActionResult layidphongkhambangidbacsi(string idbacsi)
        {
            var a = (from x in _context.PhongKhams
                    join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                    where c.IdbacSi == idbacsi
                    select x.IdphongKham).FirstOrDefault();
            return Ok(a);
        }


        [HttpGet("Laylichtrongcoso/{idbacsi}")]
        public IActionResult Laylichtrongcoso(string idbacsi)
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
                            a.SoLuongHienTai,
                            a.TrangThaiDatLich,
                            LyDoKham = k.LyDoKham != null ? k.LyDoKham : null,
                            IdtaoLich = k.IdtaoLich != null ? k.IdtaoLich : 0,
                            User = k.IdnguoiDungDatLichNavigation != null ? k.IdnguoiDungDatLichNavigation : null,
                            TrangThaiTaoLich = k.TrangThaiTaoLich != null ? k.TrangThaiTaoLich : null,
                        }).Where(c => c.IdbacSi == idbacsi && c.TrangThaiTaoLich == null && c.SoLuongToiDa > c.SoLuongHienTai).OrderBy(c => c.NgayDatLich);


            return Ok(List);
        }

        [HttpGet("CheckTaiKham/{idnguoidung}/{idbacsi}")]
        public IActionResult CheckTaiKham(int idnguoidung, string idbacsi)
        {
            DateTime a = DateTime.Now;

            var check = (from x in _context.NguoiDungs
                         join c in _context.TaoLiches on x.IdNguoiDung equals c.IdnguoiDungDatLich
                         join d in _context.DatLiches on c.IddatLich equals d.IddatLich
                         join z in _context.KeHoachKhams on d.IdkeHoachKham equals z.IdkeHoachKham
                         where z.IdbacSi == idbacsi && c.IdnguoiDungDatLich == idnguoidung && c.TrangThaiTaoLich == 2 || c.TrangThaiTaoLich == 4
                         select c).FirstOrDefault();
            if (check == null)
            {
                return Ok("success");
            }
            else
            {
                return BadRequest("Đã có lịch tái khám");
            }
        }


        [HttpPost("Taikham")]
        public IActionResult Taikham(DuyetLich duyetlich)
        {
            var a = _context.TaoLiches.FirstOrDefault(x => x.IddatLich == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            if (a != null)
            {
                a.TrangThaiTaoLich = 4;
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("Failed");
            }

        }

        [HttpGet("Laydanhsachphongkhamtuongtu/{idphongkham}/{idchuyenkhoa}")]
        public IActionResult Laydanhsachphongkhamtuongtu(string idphongkham,int idchuyenkhoa)
        {
            var list = from x in _context.PhongKhams
                       join c in _context.ChuyenKhoaPhongKhams on x.IdphongKham equals c.IdphongKham
                       where x.IdphongKham != idphongkham && c.IdchuyenKhoa == idchuyenkhoa && x.TrangThai == true
                       select x;


            return Ok(list);
        }


        [HttpGet("LayDanhSachBacsiChuyenKhoa/{idchuyenkhoa}/{idbacsi}")]
        public IActionResult LayDanhSachBacsiChuyenKhoa(int idchuyenkhoa, string idbacsi)
        {
            var list = from x in _context.ChuyenKhoas
                       join c in _context.ChuyenKhoaPhongKhams on x.IdchuyenKhoa equals c.IdchuyenKhoa
                       join d in _context.PhanLoaiBacSiChuyenKhoas on c.IdchuyenKhoaPhongKham equals d.IdchuyenKhoa
                       join e in _context.BacSis on d.IdbacSi equals e.IdbacSi
                       join k in _context.PhongKhams on e.IdphongKham equals k.IdphongKham
                       where x.IdchuyenKhoa == idchuyenkhoa && e.TrangThai == true && k.TrangThai == true &&
                       e.IdbacSi != idbacsi
                       select e
                       ;
            return Ok(list);
        }

        [HttpGet("Laydanhsachselect/{idbacsi}")]
        public IActionResult Laydanhsachselect(string idbacsi)
        {
            var idphongkham = (from x in _context.PhongKhams
                               join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                               where c.IdbacSi == idbacsi
                               select x.IdphongKham).FirstOrDefault();

            var list =(from x in _context.PhongKhams
                       join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                       join d in _context.KeHoachKhams on c.IdbacSi equals d.IdbacSi
                       join q in _context.DatLiches on d.IdkeHoachKham equals q.IdkeHoachKham
                       join v in _context.TaoLiches on q.IddatLich equals v.IddatLich
                       where x.IdphongKham == idphongkham
                       select new
                       {
                           d.NgayDatLich.Month,
                           d.NgayDatLich.Year,
                       }).Distinct();

            return Ok(list);
        }


        [HttpGet("laynamhoatdongphongkham/{idbacsi}")]
        public IActionResult laynamhoatdongphongkham(string idbacsi)
        {
            var idphongkham = (from x in _context.PhongKhams
                               join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                               where c.IdbacSi == idbacsi
                               select x.IdphongKham).FirstOrDefault();

            var list = (from x in _context.PhongKhams
                        join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                        join d in _context.KeHoachKhams on c.IdbacSi equals d.IdbacSi
                        select new
                        {
                            d.NgayDatLich.Year
                        }).Distinct();

            return Ok(list);
        }


        [HttpGet("LayDanhSachDashBoard/{idbacsi}")]
        public IActionResult LayDanhSachDashBoard(string idbacsi)
        {
            var idphongkham = (from x in _context.PhongKhams
                               join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                               where c.IdbacSi == idbacsi
                               select x.IdphongKham).FirstOrDefault();

            var list = from x in _context.PhongKhams
                       join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                       join d in _context.KeHoachKhams on c.IdbacSi equals d.IdbacSi
                       join q in _context.DatLiches on d.IdkeHoachKham equals q.IdkeHoachKham
                       join v in _context.TaoLiches on q.IddatLich equals v.IddatLich
                       where x.IdphongKham == idphongkham
                       select new
                       {
                           v,
                           d.NgayDatLich
                       };
            return Ok(list);
        }

        [HttpPost("LayDanhSachDashBoardselect/{idbacsi}")]
        public IActionResult LayDanhSachDashBoard(string idbacsi,ngaythanglaythongke ngaythang)
        {
            var idphongkham = (from x in _context.PhongKhams
                              join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                              where c.IdbacSi == idbacsi
                              select x.IdphongKham).FirstOrDefault();

            var list = from x in _context.PhongKhams
                       join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                       join d in _context.KeHoachKhams on c.IdbacSi equals d.IdbacSi
                       join q in _context.DatLiches on d.IdkeHoachKham equals q.IdkeHoachKham
                       join v in _context.TaoLiches on q.IddatLich equals v.IddatLich
                       where x.IdphongKham == idphongkham && d.NgayDatLich.Month == ngaythang.thang &&
                       d.NgayDatLich.Year == ngaythang.nam
                       select new {
                             v,
                             d.NgayDatLich
                       };

            return Ok(list);
        }

        [HttpGet("Laynguoidungdanhgiaphongkham/{idphongkham}")]
        public IActionResult Laynguoidungdanhgiaphongkham (string idphongkham)
        {
            var list = from x in _context.DanhGiaCosos
                       join d in _context.NguoiDungs on x.Idnguoidanhgia equals d.IdNguoiDung
                       where x.IdphongKham == idphongkham
                       select new
                       {
                           x.NhanXet,
                           x.SoSao,
                           d.HoNguoiDung,
                           d.TenNguoiDung,
                           d.Email,
                           d.SoDienThoai,
                       };

            return Ok(list);
        }

        [HttpGet("laydanhsachnguoidungdanhgia1bacsi/{idbacsi}")]
        public IActionResult laydanhsachnguoidungdanhgia1bacsi(string idbacsi)
        {
            var list = from x in _context.DanhGiaCosos
                       join d in _context.NguoiDungs on x.Idnguoidanhgia equals d.IdNguoiDung
                       join z in _context.BacSis on x.IdbacSi equals z.IdbacSi
                       where z.IdbacSi == idbacsi
                       select new
                       {
                           d.IdNguoiDung,
                           x.NhanXet,
                           x.SoSao,
                           d.HoNguoiDung,
                           d.TenNguoiDung,
                           d.Email,
                           d.SoDienThoai,
                       };

            return Ok(list);
        }

        [HttpGet("laydanhsachnguoidungdanhgia1nhanviencoso/{idbacsi}")]
        public IActionResult laydanhsachnguoidungdanhgia1nhanviencoso(string idbacsi)
        {
            var list = from x in _context.DanhGiaCosos
                       join d in _context.NguoiDungs on x.Idnguoidanhgia equals d.IdNguoiDung
                       join z in _context.NhanVienCoSos on x.IdnhanVienCoSo equals z.IdnhanVienCoSo
                       where z.IdnhanVienCoSo == idbacsi
                       select new
                       {
                           d.IdNguoiDung,
                           x.NhanXet,
                           x.SoSao,
                           d.HoNguoiDung,
                           d.TenNguoiDung,
                           d.Email,
                           d.SoDienThoai,
                       };

            return Ok(list);
        }

        [HttpGet("laydanhsachnguoidungdanhgia1phongkham/{idphongkham}")]
        public IActionResult laydanhsachnguoidungdanhgia1phongkham(string idphongkham)
        {
            var list = from x in _context.DanhGiaCosos
                       join d in _context.NguoiDungs on x.Idnguoidanhgia equals d.IdNguoiDung
                       join z in _context.PhongKhams on x.IdphongKham equals z.IdphongKham
                       where z.IdphongKham == idphongkham
                       select new
                       {
                           d.IdNguoiDung,
                           x.NhanXet,
                           x.SoSao,
                           d.HoNguoiDung,
                           d.TenNguoiDung,
                           d.Email,
                           d.SoDienThoai,
                       };
            return Ok(list);
        }

        [HttpGet("laydanhsachnguoidungdanhgia1coso/{idcoso}")]
        public IActionResult laydanhsachnguoidungdanhgia1coso(string idcoso)
        {
            var list = from x in _context.DanhGiaCosos
                       join d in _context.NguoiDungs on x.Idnguoidanhgia equals d.IdNguoiDung
                       join z in _context.CoSoDichVuKhacs on x.IdcoSoDichVuKhac equals z.IdcoSoDichVuKhac
                       where z.IdcoSoDichVuKhac == idcoso
                       select new
                       {
                           d.IdNguoiDung,
                           x.NhanXet,
                           x.SoSao,
                           d.HoNguoiDung,
                           d.TenNguoiDung,
                           d.Email,
                           d.SoDienThoai,
                       };
            return Ok(list);
        }

        [HttpGet("ThongKeDanhGiaNguoiDung/{idbacsi}")]
        public IActionResult ThongKeDanhGiaNguoiDung(string idbacsi)
        {
            var idphongkham = (from x in _context.PhongKhams
                               join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                               where c.IdbacSi == idbacsi
                               select x.IdphongKham).FirstOrDefault();

            var list = from x in _context.PhongKhams
                       join c in _context.DanhGiaCosos on x.IdphongKham equals c.IdphongKham
                       join d in _context.NguoiDungs on c.Idnguoidanhgia equals d.IdNguoiDung
                       where x.IdphongKham == idphongkham
                       select new
                       {
                           d,
                           x.Solandatlich,
                           x.Danhgia,
                           c.NhanXet,
                       };
            return Ok(list);
        }

        [HttpGet("ThongKeHoatDongCacBacSiCuaMotPhongKham/{idbacsi}")]
        public IActionResult ThongKeHoatDongCacBacSiCuaMotPhongKham(string idbacsi)
        {
            var idphongkham = (from x in _context.PhongKhams
                               join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                               where c.IdbacSi == idbacsi
                               select x.IdphongKham).FirstOrDefault();

            var list = from x in _context.PhongKhams
                       join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                       where x.IdphongKham == idphongkham
                       select new
                       {
                           c.IdbacSi,
                           c.HoTenBacSi,
                           c.DanhGiaCosos,
                           c.Danhgia,
                           c.Solandatlich,
                       };
            return Ok(list);
        }
    }
}
