using BHC_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace BHC_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {

        private readonly DB_BHCContext _context;

        public FacilitiesController(DB_BHCContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllRegisterFacilities()
        {

            var SelectAllRegisterFacilities = from xacThuc in _context.XacThucDangKyMoCoSoYtes
                                              join x in _context.NguoiDungs on xacThuc.IdnguoiDung equals x.IdNguoiDung
                                              join c in _context.XaPhuongs on xacThuc.XaPhuong equals c.IdxaPhuong
                                              join d in _context.QuanHuyens on c.IdquanHuyen equals d.IdquanHuyen
                                              where xacThuc.TrangThaiXacThuc == 0
                                              select new
                                              {
                                                  xacThuc,
                                                  c.TenXaPhuong,
                                                  d.TenQuanHuyen,
                                                  x.IdNguoiDung,
                                                  x.HoNguoiDung,
                                                  x.TenNguoiDung,
                                                  x.Email,
                                                  x.SoDienThoai,
                                                  x.NgaySinh,
                                                  x.Diachi,
                                                  x.NgayTao,
                                                  x.QuocTich,
                                                  x.GioiTinh,
                                                  x.Cccd,
                                                  x.AnhNguoidung
                                              };
       
                return Ok(SelectAllRegisterFacilities);
           
        }

        [HttpPost("LuuLichHen")]
        public IActionResult LuuLichHen(LichHen lichhen)
        {
            var hen = new LichHen
            {
                LinkHen = lichhen.LinkHen,
                NgayHen = lichhen.NgayHen,
                GioHen = lichhen.GioHen,
                IdNguoiDungHenLich = lichhen.IdNguoiDungHenLich,
            };
            _context.LichHens.Add(hen);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("XacNhanDaHop/{idlichhen}")]
        public IActionResult XacNhanDaHop(int idlichhen)
        {
            var xacnhan = _context.LichHens.FirstOrDefault(x => x.IdLichHen == idlichhen);
            if (xacnhan != null)
            {
                xacnhan.TrangThaiLichHen = 2;
                _context.SaveChanges();
                return Ok("Success");
            }

            return Ok("failed");
        }


        [HttpPost("HuyHop/{idlichhen}")]
        public IActionResult HuyHop(int idlichhen)
        {
            var xacnhan = _context.LichHens.FirstOrDefault(x => x.IdLichHen == idlichhen);
            if (xacnhan != null)
            {
                xacnhan.TrangThaiLichHen = 0;
                _context.SaveChanges();
                return Ok("Success");
            }

            return Ok("failed");
        }



        [HttpGet("Laytenchuyenmon/{idloaicoso}")]
        public IActionResult Laytenchuyenmon(int idloaicoso)
        {
            var a = _context.ChuyenMons.FirstOrDefault(x => x.IdChuyenMon == idloaicoso);
            return Ok(a);
        }

        [HttpGet("Laydanhsachchuyenkhoadangky/{idxacthuc}")]
        public IActionResult Laydanhsachchuyenkhoadangky(int idxacthuc)
        {
            var a = from x in _context.CacChuyenKhoaChuyenMonDangKies
                    join c in _context.ChuyenKhoas on x.Idchuyenmon equals c.IdchuyenKhoa
                    where x.Idxacthucdatlich == idxacthuc
                    select new
                    {
                        x,
                        c.TenChuyenKhoa
                    };
            return Ok(a);
        }


        [HttpPost("RegisterFacitilies")]
        public IActionResult RegisterFacitilies2(RegisterFacitilies dangky)
        {
            var dacophongkham = _context.NguoiDungs.Where(x => x.IdNguoiDung == dangky.IdnguoiDung && x.TrangThaiPhongKham == true);
            var daconhathuoc = _context.NguoiDungs.Where(x => x.IdNguoiDung == dangky.IdnguoiDung && x.TrangThaiNhaThuoc == true);
            var dacocoso = _context.NguoiDungs.Where(x => x.IdNguoiDung == dangky.IdnguoiDung && x.TrangThaiCoSoYte == true);
            var phongkham = _context.XacThucDangKyMoCoSoYtes.FirstOrDefault(x => x.IdnguoiDung == dangky.IdnguoiDung && x.LoaiHinhDangKy == 1);
            var nhathuoc = _context.XacThucDangKyMoCoSoYtes.FirstOrDefault(x => x.IdnguoiDung == dangky.IdnguoiDung && x.LoaiHinhDangKy == 2);
            var cosoyte = _context.XacThucDangKyMoCoSoYtes.FirstOrDefault(x => x.IdnguoiDung == dangky.IdnguoiDung && x.LoaiHinhDangKy == 3);
            string messphongkham = "";
            string messnhathuoc = "";
            string messcosoyte = "";
            string mess = "";

            if (phongkham == null && dangky.LoaiHinhDangKy == 1 && dacophongkham.Count() == 0)
            {
                var a = new XacThucDangKyMoCoSoYte
                {
                    TenCoSo = dangky.TenCoSo,
                    IdnguoiDung = dangky.IdnguoiDung,
                    LoaiHinhDangKy = dangky.LoaiHinhDangKy,
                    AnhBangCap = dangky.AnhBangCap,
                    AnhCccdmatSau = dangky.AnhCccdmatSau,
                    AnhCccdmatTruoc = dangky.AnhCccdmatTruoc,
                    DiaChi = dangky.DiaChi,
                    XaPhuong = dangky.XaPhuong,
                    AnhGiayChungNhanKinhDoanh = dangky.AnhGiayChungNhanKinhDoanh,
                    AnhDangKyAnhBacSi = dangky.AnhDangKyAnhBacSi,
                    AnhChungChiHanhNghe = dangky.AnhChungChiHanhNghe,
                    AnhCoSo = dangky.AnhCoSo,
                    LoaiPhongKham = dangky.LoaiPhongKham,
                };
                _context.XacThucDangKyMoCoSoYtes.Add(a);
                _context.SaveChanges();
                return Ok(a.IdxacThucDangKyMoCoSoYte);
            }
            else
            {
                messphongkham = "Đã có phòng khám ";
            }

            if (nhathuoc == null && dangky.LoaiHinhDangKy == 2 && daconhathuoc.Count() == 0)
            {
                var a = new XacThucDangKyMoCoSoYte
                {
                    TenCoSo = dangky.TenCoSo,
                    IdnguoiDung = dangky.IdnguoiDung,
                    LoaiHinhDangKy = dangky.LoaiHinhDangKy,
                    AnhBangCap = dangky.AnhBangCap,
                    AnhCccdmatSau = dangky.AnhCccdmatSau,
                    AnhCccdmatTruoc = dangky.AnhCccdmatTruoc,
                    DiaChi = dangky.DiaChi,
                    XaPhuong = dangky.XaPhuong,
                    AnhGiayChungNhanKinhDoanh = dangky.AnhGiayChungNhanKinhDoanh,
                    AnhDangKyAnhBacSi = dangky.AnhDangKyAnhBacSi,
                    AnhChungChiHanhNghe = dangky.AnhChungChiHanhNghe,
                    AnhCoSo = dangky.AnhCoSo,
                };
                _context.XacThucDangKyMoCoSoYtes.Add(a);
                _context.SaveChanges();
                return Ok("Register Pharma Success");
            }
            else
            {
                messnhathuoc = "Đã có nhà thuốc";
            }

            if (cosoyte == null && dangky.LoaiHinhDangKy == 3 && dacocoso.Count() == 0)
            {
                var a = new XacThucDangKyMoCoSoYte
                {
                    TenCoSo = dangky.TenCoSo,
                    IdnguoiDung = dangky.IdnguoiDung,
                    LoaiHinhDangKy = dangky.LoaiHinhDangKy,
                    AnhBangCap = dangky.AnhBangCap,
                    AnhCccdmatSau = dangky.AnhCccdmatSau,
                    AnhCccdmatTruoc = dangky.AnhCccdmatTruoc,
                    DiaChi = dangky.DiaChi,
                    XaPhuong = dangky.XaPhuong,
                    LoaiCoSo = dangky.IdLoaiCoSoYTe,
                    AnhGiayChungNhanKinhDoanh = dangky.AnhGiayChungNhanKinhDoanh,
                    AnhDangKyAnhBacSi = dangky.AnhDangKyAnhBacSi,
                    AnhChungChiHanhNghe = dangky.AnhChungChiHanhNghe,
                    AnhCoSo = dangky.AnhCoSo,
                };
                _context.XacThucDangKyMoCoSoYtes.Add(a);
                _context.SaveChanges();
                return Ok("Register Facitilies Success");
            }
            else
            {
                messcosoyte = "Đã có cơ sở y tế ";
            }
            mess += messcosoyte + messnhathuoc + messphongkham;

            return Ok(mess);
        }


        [HttpPost("dangkychuyenkhoa/{idxacthuc}")]
        public IActionResult dangkychuyenkhoa(int idxacthuc, List<CacChuyenKhoaChuyenMonDangKy> chuyenkhoa)
        {
            var xacthuc = _context.XacThucDangKyMoCoSoYtes.FirstOrDefault(x => x.IdxacThucDangKyMoCoSoYte == idxacthuc);

            if (xacthuc != null)
            {
                var list = new List<CacChuyenKhoaChuyenMonDangKy>();
                chuyenkhoa.ForEach(ele =>
                {
                    list.Add(
                      new CacChuyenKhoaChuyenMonDangKy() { Idxacthucdatlich = idxacthuc, Idchuyenmon = ele.Idchuyenmon }
                    );
                });
                _context.CacChuyenKhoaChuyenMonDangKies.AddRange(chuyenkhoa);
                _context.SaveChanges();
                return Ok(idxacthuc);
            }
            return Ok("Failed");
        }

        [HttpPost("Themchuyenkhoachobacsichuyenkhoa")]
        public IActionResult Themchuyenkhoachophongkhamchuyenkhoa(ChuyenKhoaPhongKham chuyenkhoa)
        {
            var nhanvien = _context.BacSis.FirstOrDefault(x => x.IdphongKham == chuyenkhoa.IdphongKham);
            var idchuyenkhoa = (from x in _context.PhongKhams
                                join c in _context.ChuyenKhoaPhongKhams on x.IdphongKham equals c.IdphongKham
                                where x.IdphongKham == chuyenkhoa.IdphongKham
                                select c.IdchuyenKhoaPhongKham).FirstOrDefault();

            if (nhanvien != null)
            {

                var a = new PhanLoaiBacSiChuyenKhoa
                {
                    IdbacSi = nhanvien.IdbacSi,
                    IdchuyenKhoa = idchuyenkhoa
                };
               
                _context.PhanLoaiBacSiChuyenKhoas.Add(a);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("Bác sĩ không tồn tại");
            }

        }

        [HttpPost("Themchuyenkhoachophongkham/{idphongkham}")]
        public IActionResult Themchuyenkhoachophongkham(string idphongkham, List<ChuyenKhoaPhongKham> chuyenkhoa)
        {
            var checkclinic = _context.PhongKhams.FirstOrDefault(x => x.IdphongKham == idphongkham);

            if (checkclinic != null)
            {
                var list = new List<ChuyenKhoaPhongKham>();
                chuyenkhoa.ForEach(ele =>
                {
                    list.Add(
                      new ChuyenKhoaPhongKham() { IdphongKham = idphongkham, IdchuyenKhoa = ele.IdchuyenKhoa }
                    );
                });
                _context.ChuyenKhoaPhongKhams.AddRange(chuyenkhoa);
                _context.SaveChanges();
                return Ok(idphongkham);
            }
            else
            {
                return BadRequest("Phòng khám không tồn tại");
            }

        }

        [HttpPost("ThemChuyenMonChoCoSo/{idcoso}")]
        public IActionResult ThemChuyenMonChoCoSo(string idcoso, List<ChuyenMoncoSo> chuyenmon)
        {
            var checkclinic = _context.CoSoDichVuKhacs.FirstOrDefault(x => x.IdcoSoDichVuKhac == idcoso);

            if (checkclinic != null)
            {
                var list = new List<ChuyenMoncoSo>();
                chuyenmon.ForEach(ele =>
                {
                    list.Add(
                      new ChuyenMoncoSo() { IdcoSoDichVuKhac = idcoso, IdchuyenMon = ele.IdchuyenMon }
                    );
                });
                _context.ChuyenMoncoSos.AddRange(chuyenmon);
                _context.SaveChanges();
                return Ok("Add success");
            }
            else
            {
                return BadRequest("Failed");
            }
        }

        [HttpPut("HuyXacThuc/{idxacthuc}")]
        public IActionResult HuyXacThuc(int idxacthuc)
        {
            var dangkychuyemon = _context.CacChuyenKhoaChuyenMonDangKies.Where(x => x.Idxacthucdatlich == idxacthuc);
            var xacthuc = _context.XacThucDangKyMoCoSoYtes.FirstOrDefault(x => x.IdxacThucDangKyMoCoSoYte == idxacthuc);

            if (dangkychuyemon.Count() > 0 && xacthuc != null)
            {
                _context.CacChuyenKhoaChuyenMonDangKies.RemoveRange(dangkychuyemon);
                _context.XacThucDangKyMoCoSoYtes.Remove(xacthuc);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("Failed");
            }

        }

        [HttpGet("Layidchuyenmoncoso/{idcoso}")]
        public IActionResult Laychuyenmoncoso(string idcoso)
        {
            var chuyenmon = _context.ChuyenMoncoSos.FirstOrDefault(x => x.IdcoSoDichVuKhac == idcoso);
            if(chuyenmon != null)
            {
                return Ok(chuyenmon.IdchuyenMonCoSo);
            }
            else
            {
                return BadRequest("not found");
            }
           
        }

        [HttpPost("ThemChuyenMonChoNhanVien")]
        public IActionResult ThemChuyenMonChoNhanVien(PhanLoaiChuyenKhoaNhanVien nhanvien)
        {
            var checknhanvien = _context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == nhanvien.IdnhanVienCoSo);
            var checkchuyenmon = _context.PhanLoaiChuyenKhoaNhanViens.FirstOrDefault(x => x.IdnhanVienCoSo == nhanvien.IdnhanVienCoSo);
            if(checknhanvien != null)
            { 
                if(checkchuyenmon == null)
                {
                    var themchuyenmon = new PhanLoaiChuyenKhoaNhanVien
                    {
                        IdnhanVienCoSo = nhanvien.IdnhanVienCoSo,
                        ChuyenMoncoSo = nhanvien.ChuyenMoncoSo,
                    };
                    _context.PhanLoaiChuyenKhoaNhanViens.Add(themchuyenmon);
                    _context.SaveChanges();
                    return Ok("Success");
                }
                else
                {
                    return Ok("dacochuyenmon");
                }
               
            }
            else
            {
                return BadRequest("Not found staff");
            }

        }

        [HttpPost("Themchuyenmonchochucosoyte/{idcoso}")]
        public IActionResult Themchuyenmonchochucosoyte(string idcoso)
        {
           var nhanvien = _context.NhanVienCoSos.Where(x => x.IdcoSoDichVuKhac == idcoso).FirstOrDefault();

           if(nhanvien != null)
            {
                var chuyenmoncoso = (from x in _context.CoSoDichVuKhacs
                                     join c in _context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                                     join d in _context.ChuyenMons on c.IdchuyenMon equals d.IdChuyenMon
                                     where x.IdcoSoDichVuKhac == idcoso
                                     select c).FirstOrDefault();
                if(chuyenmoncoso != null)
                {
                    var a = new PhanLoaiChuyenKhoaNhanVien
                    {
                        IdnhanVienCoSo = nhanvien.IdnhanVienCoSo,
                        ChuyenMoncoSo = chuyenmoncoso.IdchuyenMonCoSo,
                    };

                    _context.PhanLoaiChuyenKhoaNhanViens.Add(a);
                    _context.SaveChanges();
                    return Ok("success");
                }
                else
                {
                    return BadRequest("Co so chua co chuyen mon");
                }

            }
            else
            {
                return BadRequest("Chua co nhan vien nay");
            } 

        }

        [HttpPost("Themchuyenkhoachochuphongkham/{idcoso}")]
        public IActionResult Themchuyenkhoachochuphongkham(string idcoso)
        {
            var nhanvien = _context.BacSis.Where(x => x.IdphongKham == idcoso).FirstOrDefault();

            if (nhanvien != null)
            {
                var chuyenmoncoso = (from x in _context.PhongKhams
                                     join c in _context.ChuyenKhoaPhongKhams on x.IdphongKham equals c.IdphongKham
                                     join d in _context.ChuyenKhoas on c.IdchuyenKhoa equals d.IdchuyenKhoa
                                     where x.IdphongKham == idcoso
                                     select c).FirstOrDefault();
                if (chuyenmoncoso != null)
                {
                    var a = new PhanLoaiBacSiChuyenKhoa
                    {
                        IdbacSi = nhanvien.IdbacSi,
                        IdphanLoaiBacSiChuyenKhoa = chuyenmoncoso.IdchuyenKhoaPhongKham,
                    };

                    _context.PhanLoaiBacSiChuyenKhoas.Add(a);
                    _context.SaveChanges();
                    return Ok("success");
                }
                else
                {
                    return BadRequest("Co so chua co chuyen mon");
                }

            }
            else
            {
                return BadRequest("Chua co nhan vien nay");
            }

        }



        [HttpPut]
        public IActionResult ApprovalFacitilies(ApprovalClinic Xacthuccoso,int idquantrivien)
        {
            
            var CheckPhongKham = from xacThuc in _context.XacThucDangKyMoCoSoYtes
                                 join x in _context.NguoiDungs on xacThuc.IdnguoiDung equals x.IdNguoiDung
                                 where xacThuc.TrangThaiXacThuc == 1 && xacThuc.LoaiHinhDangKy == 1 && xacThuc.IdnguoiDung == Xacthuccoso.IdnguoiDung
                                 select new
                                 {
                                   xacThuc,
                                 };
            var CheckNhaThuoc =  from xacThuc in _context.XacThucDangKyMoCoSoYtes
                                 join x in _context.NguoiDungs on xacThuc.IdnguoiDung equals x.IdNguoiDung
                                 where xacThuc.TrangThaiXacThuc == 1 && xacThuc.LoaiHinhDangKy == 2 && xacThuc.IdnguoiDung == Xacthuccoso.IdnguoiDung
                                 select new
                                 {
                                     xacThuc,
                                 };
            var CheckCoSoYTeKhac = from xacThuc in _context.XacThucDangKyMoCoSoYtes
                                   join x in _context.NguoiDungs on xacThuc.IdnguoiDung equals x.IdNguoiDung
                                   where xacThuc.TrangThaiXacThuc == 1 && xacThuc.LoaiHinhDangKy == 3 && xacThuc.IdnguoiDung == Xacthuccoso.IdnguoiDung
                                   select new
                                   {
                                       xacThuc,
                                   };
          
            
            var XacThuc = _context.XacThucDangKyMoCoSoYtes.FirstOrDefault(x => x.IdxacThucDangKyMoCoSoYte == Xacthuccoso.IdxacThucDangKyMoCoSoYte);
            var NguoiDung = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == Xacthuccoso.IdnguoiDung);
            var ramdom = new Ramdom.Ramdom();

           
            if (NguoiDung == null || XacThuc == null)
            {
                return BadRequest("Failed");
            }
            else
            {
                string IDCoSo = "";
                string IDNhanVienCoSo = "";

                if (Xacthuccoso.LoaiHinhDangKy == 1)
                {
                    if (Xacthuccoso.LoaiPhongKham == false)
                    {
                        IDCoSo = "DK" + Xacthuccoso.IdnguoiDung;
                    }
                    else
                    {
                        IDCoSo = "CK" + Xacthuccoso.IdnguoiDung;
                    }
                    IDNhanVienCoSo = IDCoSo + "1";
                }

                if (Xacthuccoso.LoaiHinhDangKy == 2)
                {
                    IDCoSo = "NT" + Xacthuccoso.IdnguoiDung;
                    IDNhanVienCoSo = IDCoSo + "1";
                }

                if (Xacthuccoso.LoaiHinhDangKy == 3)

                {
                    IDCoSo = "CSK" + Xacthuccoso.IdnguoiDung;
                    IDNhanVienCoSo = IDCoSo + "1";
                }
                XacThuc.NgayXetDuyet = DateTime.Now;
                XacThuc.TrangThaiXacThuc = 1;
                XacThuc.IdquanTriVien = idquantrivien;

                if (Xacthuccoso.LoaiHinhDangKy == 1)
                {
                    var PhongKham2 = new PhongKham
                    {
                        IdphongKham = IDCoSo,
                        IdnguoiDung = Xacthuccoso.IdnguoiDung,
                        DiaChi = Xacthuccoso.DiaChi,
                        TenPhongKham = Xacthuccoso.TenCoSo.ToUpper(),
                        HinhAnh = Xacthuccoso.AnhCoSo,
                        IdxaPhuong = Convert.ToInt32(Xacthuccoso.XaPhuong),
                        LoaiPhongKham = Convert.ToBoolean(Xacthuccoso.LoaiPhongKham),
                    };
                    var BacSi2 = new BacSi
                    {
                        IdbacSi = IDNhanVienCoSo,
                        IdphongKham = IDCoSo,
                        HoTenBacSi = NguoiDung.HoNguoiDung + " " + NguoiDung.TenNguoiDung,
                        Cccd = NguoiDung.Cccd,
                        SoDienThoaiBacSi = NguoiDung.SoDienThoai,
                        EmailBacSi = NguoiDung.Email,
                        Idquyen = 2,
                        TaiKhoan = ramdom.GetPassword(),
                        MatKhau = ramdom.GetPassword(),
                        AnhBacSi = Xacthuccoso.AnhDangKyAnhBacSi,
                        AnhChungChiHanhNgheBacSi = Xacthuccoso.AnhChungChiHanhNghe,
                        Idnguoidung = Xacthuccoso.IdnguoiDung,
                    };
                    string path = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\XacThucImg\\";
                    string path2 = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\CoSoYTeImg\\";
                    var anhdelete1 = path + XacThuc.AnhBangCap;
                    var anhdelete2 = path + XacThuc.AnhChungChiHanhNghe;
                    var anhdelete3 = path + XacThuc.AnhDangKyAnhBacSi;
                    var anhdelete4 = path + XacThuc.AnhGiayChungNhanKinhDoanh;
                    var anhdelete5 = path + XacThuc.AnhCoSo;
                    var anhdelete6 = path + XacThuc.AnhCccdmatSau;
                    var anhdelete7 = path + XacThuc.AnhCccdmatTruoc;
                    var anhcreate1 = path2 + XacThuc.AnhBangCap;
                    var anhcreate2 = path2+ XacThuc.AnhChungChiHanhNghe;
                    var anhcreate3 = path2 + XacThuc.AnhDangKyAnhBacSi;
                    var anhcreate4 = path2 + XacThuc.AnhGiayChungNhanKinhDoanh;
                    var anhcreate5 = path2 + XacThuc.AnhCoSo;
                    var anhcreate6 = path2 + XacThuc.AnhCccdmatSau;
                    var anhcreate7 = path2 + XacThuc.AnhCccdmatTruoc;

                    if(System.IO.File.Exists(anhcreate1) == false ){
                        System.IO.File.Copy(anhdelete1, anhcreate1);
                    }
                    if (System.IO.File.Exists(anhcreate2) == false)
                    {
                        System.IO.File.Copy(anhdelete2, anhcreate2);
                    }
                    if (System.IO.File.Exists(anhcreate3) == false)
                    {
                        System.IO.File.Copy(anhdelete3, anhcreate3);
                    }
                    if (System.IO.File.Exists(anhcreate4) == false)
                    {
                        System.IO.File.Copy(anhdelete4, anhcreate4);
                    }
                    if (System.IO.File.Exists(anhcreate5) == false)
                    {
                        System.IO.File.Copy(anhdelete5, anhcreate5);
                    }
                    if (System.IO.File.Exists(anhcreate6) == false)
                    {
                        System.IO.File.Copy(anhdelete6, anhcreate6);
                    }
                    if (System.IO.File.Exists(anhcreate7) == false)
                    {
                        System.IO.File.Copy(anhdelete7, anhcreate7);
                    }
                    _context.PhongKhams.Add(PhongKham2);
                    _context.BacSis.Add(BacSi2);
                    NguoiDung.TrangThaiPhongKham = true;
                    _context.SaveChanges();
                    return Ok(IDCoSo);
                }

                if (Xacthuccoso.LoaiHinhDangKy == 3)
                {
                    var Coso = new CoSoDichVuKhac
                    {
                        IdcoSoDichVuKhac = IDCoSo,
                        IdnguoiDung = Xacthuccoso.IdnguoiDung,
                        DiaChi = Xacthuccoso.DiaChi,
                        TenCoSo = Xacthuccoso.TenCoSo.ToUpper(),
                        HinhAnhCoSo = Xacthuccoso.AnhCoSo,
                        IdxaPhuong = Convert.ToInt32(Xacthuccoso.XaPhuong),
                    };
                    var NhanVienCoSo = new NhanVienCoSo
                    {
                        IdnhanVienCoSo = IDNhanVienCoSo,
                        IdcoSoDichVuKhac = IDCoSo,
                        HoTenNhanVien = NguoiDung.HoNguoiDung + " " + NguoiDung.TenNguoiDung,
                        Cccd = NguoiDung.Cccd,
                        SoDienThoaiNhanVienCoSo = NguoiDung.SoDienThoai,
                        EmailNhanVienCoSo = NguoiDung.Email,
                        Idquyen = 2,
                        TaiKhoan = ramdom.GetPassword(),
                        MatKhau = ramdom.GetPassword(),
                        AnhNhanVien = Xacthuccoso.AnhDangKyAnhBacSi,
                        AnhChungChiHanhNgheNhanVien = Xacthuccoso.AnhChungChiHanhNghe,
                        Idnguoidung = Xacthuccoso.IdnguoiDung,
                    };
                    _context.CoSoDichVuKhacs.Add(Coso);
                    var themchuyenmon = new ChuyenMoncoSo
                    {
                        IdchuyenMon = Xacthuccoso.idLoaiCoSoYTe,
                        IdcoSoDichVuKhac = Coso.IdcoSoDichVuKhac,
                    };


                    string path = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\XacThucImg\\";
                    string path2 = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\CoSoYTeImg\\";
                    var anhdelete1 = path + XacThuc.AnhBangCap;
                    var anhdelete2 = path + XacThuc.AnhChungChiHanhNghe;
                    var anhdelete3 = path + XacThuc.AnhDangKyAnhBacSi;
                    var anhdelete4 = path + XacThuc.AnhGiayChungNhanKinhDoanh;
                    var anhdelete5 = path + XacThuc.AnhCoSo;
                    var anhdelete6 = path + XacThuc.AnhCccdmatSau;
                    var anhdelete7 = path + XacThuc.AnhCccdmatTruoc;
                    var anhcreate1 = path2 + XacThuc.AnhBangCap;
                    var anhcreate2 = path2 + XacThuc.AnhChungChiHanhNghe;
                    var anhcreate3 = path2 + XacThuc.AnhDangKyAnhBacSi;
                    var anhcreate4 = path2 + XacThuc.AnhGiayChungNhanKinhDoanh;
                    var anhcreate5 = path2 + XacThuc.AnhCoSo;
                    var anhcreate6 = path2 + XacThuc.AnhCccdmatSau;
                    var anhcreate7 = path2 + XacThuc.AnhCccdmatTruoc;

                    if (System.IO.File.Exists(anhcreate1) == false)
                    {
                        System.IO.File.Copy(anhdelete1, anhcreate1);
                    }
                    if (System.IO.File.Exists(anhcreate2) == false)
                    {
                        System.IO.File.Copy(anhdelete2, anhcreate2);
                    }
                    if (System.IO.File.Exists(anhcreate3) == false)
                    {
                        System.IO.File.Copy(anhdelete3, anhcreate3);
                    }
                    if (System.IO.File.Exists(anhcreate4) == false)
                    {
                        System.IO.File.Copy(anhdelete4, anhcreate4);
                    }
                    if (System.IO.File.Exists(anhcreate5) == false)
                    {
                        System.IO.File.Copy(anhdelete5, anhcreate5);
                    }
                    if (System.IO.File.Exists(anhcreate6) == false)
                    {
                        System.IO.File.Copy(anhdelete6, anhcreate6);
                    }
                    if (System.IO.File.Exists(anhcreate7) == false)
                    {
                        System.IO.File.Copy(anhdelete7, anhcreate7);
                    }
                    _context.ChuyenMoncoSos.Add(themchuyenmon);
                    _context.NhanVienCoSos.Add(NhanVienCoSo);
                    NguoiDung.TrangThaiCoSoYte = true;
                    _context.SaveChanges();
                  
                    return Ok(IDCoSo);
                }

                if (Xacthuccoso.LoaiHinhDangKy == 2)
                {
                    var NhaThuoc2 = new NhaThuoc
                    {
                        IdNhaThuoc = IDCoSo,
                        TenNhaThuoc = Xacthuccoso.TenCoSo,
                        IdnguoiDung = NguoiDung.IdNguoiDung,
                        IdxaPhuong = Xacthuccoso.XaPhuong,
                        DiaChi = Xacthuccoso.DiaChi,
                        IdLoaiHinhDichVu = Xacthuccoso.LoaiHinhDangKy,
                    };
                    var NhanVienNhaThuoc2 = new NhanVienNhaThuoc
                    {
                        IdnhanVienNhaThuoc = IDNhanVienCoSo,
                        HoTenNhanVien = NguoiDung.TenNguoiDung,
                        IdNhaThuoc = IDCoSo,
                        EmailNhanvien = NguoiDung.Email,
                        SdtnhanVien = NguoiDung.SoDienThoai,
                        NgaySinh = Convert.ToDateTime(NguoiDung.NgaySinh),
                        DiaChi = NguoiDung.Diachi,
                        ChucVu = 2,
                        TaiKhoan = ramdom.GetPassword(),
                        MatKhau = ramdom.GetPassword(),
                    };
                    NguoiDung.TrangThaiNhaThuoc = true;
                    string path = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\XacThucImg\\";
                    string path2 = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\CoSoYTeImg\\";
                    var anhdelete1 = path + XacThuc.AnhBangCap;
                    var anhdelete2 = path + XacThuc.AnhChungChiHanhNghe;
                    var anhdelete3 = path + XacThuc.AnhDangKyAnhBacSi;
                    var anhdelete4 = path + XacThuc.AnhGiayChungNhanKinhDoanh;
                    var anhdelete5 = path + XacThuc.AnhCoSo;
                    var anhdelete6 = path + XacThuc.AnhCccdmatSau;
                    var anhdelete7 = path + XacThuc.AnhCccdmatTruoc;
                    var anhcreate1 = path2 + XacThuc.AnhBangCap;
                    var anhcreate2 = path2 + XacThuc.AnhChungChiHanhNghe;
                    var anhcreate3 = path2 + XacThuc.AnhDangKyAnhBacSi;
                    var anhcreate4 = path2 + XacThuc.AnhGiayChungNhanKinhDoanh;
                    var anhcreate5 = path2 + XacThuc.AnhCoSo;
                    var anhcreate6 = path2 + XacThuc.AnhCccdmatSau;
                    var anhcreate7 = path2 + XacThuc.AnhCccdmatTruoc;

                    if (System.IO.File.Exists(anhcreate1) == false)
                    {
                        System.IO.File.Copy(anhdelete1, anhcreate1);
                    }
                    if (System.IO.File.Exists(anhcreate2) == false)
                    {
                        System.IO.File.Copy(anhdelete2, anhcreate2);
                    }
                    if (System.IO.File.Exists(anhcreate3) == false)
                    {
                        System.IO.File.Copy(anhdelete3, anhcreate3);
                    }
                    if (System.IO.File.Exists(anhcreate4) == false)
                    {
                        System.IO.File.Copy(anhdelete4, anhcreate4);
                    }
                    if (System.IO.File.Exists(anhcreate5) == false)
                    {
                        System.IO.File.Copy(anhdelete5, anhcreate5);
                    }
                    if (System.IO.File.Exists(anhcreate6) == false)
                    {
                        System.IO.File.Copy(anhdelete6, anhcreate6);
                    }
                    if (System.IO.File.Exists(anhcreate7) == false)
                    {
                        System.IO.File.Copy(anhdelete7, anhcreate7);
                    }

                    _context.NhaThuocs.Add(NhaThuoc2);
                    _context.NhanVienNhaThuocs.Add(NhanVienNhaThuoc2);
                    _context.SaveChanges();
                    return Ok("Success");
                }
                return BadRequest("Failed");
            }
        }
    }
}
