using BHC_Server.Models;
using BHC_Server.Ramdom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;

namespace BHC_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DB_BHCContext _context;
        public AdminController(DB_BHCContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult LoginAdmin(Login loginadmin)
        {
            var a = _context.QuanTriViens.FirstOrDefault(x => x.TaiKhoanQtv == loginadmin.UserName);
            if (a != null)
            {
                if (a.MatKhau == loginadmin.Password)
                {
                    return Ok(a.IdquanTriVien);
                }
                else
                {
                    return BadRequest("Sai tài khoản hoặc mật khẩu");
                }
            }
            else
            {
                return BadRequest("Tài khoản không tồn tạis");
            }

        }

        [HttpGet("Laythongtincanhanbyidadmin/{idadmin}")]
        public IActionResult Laythongtincanhanbyidadmin(int idadmin)
        {
            var info = _context.QuanTriViens.FirstOrDefault(x => x.IdquanTriVien == idadmin);
            if (info != null)
            {
                return Ok(info);
            }
            return BadRequest("Quản trị viên không tồn tại");
        }


        [HttpGet("LaydanhsachDatHen")]
        public IActionResult LaydanhsachDatHen()
        {
            var list = from x in _context.LichHens
                       join c in _context.NguoiDungs on x.IdNguoiDungHenLich equals c.IdNguoiDung
                       join d in _context.XacThucDangKyMoCoSoYtes on c.IdNguoiDung equals d.IdnguoiDung
                       select new{
                           x.IdLichHen,
                           x.IdNguoiDungHenLich,
                           x.NgayHen,
                           x.GioHen,
                           x.LinkHen,
                           x.TrangThaiLichHen,
                           c,
                           d.LoaiHinhDangKy,
                       };
            return Ok(list);    
        }

        [HttpPost("LuuLichHen")]
        public IActionResult LuuLichHen(LichHen lichhen)
        {
            var checklichhen = _context.LichHens.FirstOrDefault(x => x.IdLichHen == lichhen.IdLichHen);

            if(checklichhen == null)
            {
                var a = new LichHen
                {
                    IdNguoiDungHenLich = lichhen.IdNguoiDungHenLich,
                    GioHen = lichhen.GioHen,
                    NgayHen = lichhen.NgayHen,
                    LinkHen = lichhen.LinkHen,
                };
                _context.LichHens.Add(a);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("failed");
            }  
        }

        [HttpDelete("xoanhanvien/{idnhanvien}")]
        public IActionResult xoanhanvien(int idnhanvien)
        {
            var idnhanvienxoa = _context.QuanTriViens.FirstOrDefault(x => x.IdquanTriVien == idnhanvien);

            if(idnhanvien != 1)
            {
                if(idnhanvienxoa != null)
                {
                    idnhanvienxoa.TrangThai = false;
                    _context.SaveChanges();
                    return Ok("Xóa thành công");
                }
                else
                {
                    return BadRequest("Nhân viên xóa không tồn tại");
                }
            }
            else
            {
                return Ok("Không thể xóa nhân viên này");
            }
        }

        [HttpPut("KhoiPhucNhanVien/{idnhanvien}")]
        public IActionResult KhoiPhucNhanVien(int idnhanvien)
        {
            var idnhanvienxoa = _context.QuanTriViens.FirstOrDefault(x => x.IdquanTriVien == idnhanvien);

                if (idnhanvienxoa != null)
                {
                    idnhanvienxoa.TrangThai = true;
                    _context.SaveChanges();
                    return Ok("Khôi phục thành công");
                }
                else
                {
                    return BadRequest("Nhân viên này không tồn tại");
                }
            
        }

        [HttpPost("Themnhanvien")]
        public IActionResult Themnhanvien(QuanTriVien nhanvien)
        {
            var ramdom = new Ramdom.Ramdom();
            var check = _context.QuanTriViens
                .FirstOrDefault(x => x.Email == nhanvien.Email || x.SoDienThoai == nhanvien.SoDienThoai ||
                x.Cccd == nhanvien.Cccd);
            if(check == null)
            {
                string taikhoan = "";
                int index = nhanvien.Email.IndexOf("@");
                string firstusername = nhanvien.Email.Substring(0, index);
                taikhoan = "qtv" + firstusername;
                var Add = new QuanTriVien
                {
                    AnhQtv = nhanvien.AnhQtv,
                    HoQtv = nhanvien.HoQtv,
                    TenQtv = nhanvien.TenQtv,
                    Email = nhanvien.Email,
                    SoDienThoai = nhanvien.SoDienThoai,
                    Cccd = nhanvien.Cccd,
                    DiaChi = nhanvien.DiaChi,
                    NgaySinh = nhanvien.NgaySinh,
                    Chucvu = nhanvien.Chucvu,
                    TaiKhoanQtv = taikhoan,
                    MatKhau = ramdom.GetPassword(),
                    TrangThai = true
                };
                _context.QuanTriViens.Add(Add);
                _context.SaveChanges();
                return Ok(Add.IdquanTriVien);
            }
            else
            {
                return BadRequest("Email, số điện thoại hoặc CCCD đã tồn tại");
            }

        }


        [HttpPut("EditStaff")]
        public IActionResult EditStaff(QuanTriVien quantrivien)
        {
            var Checknhanvien = _context.QuanTriViens.FirstOrDefault(x => x.IdquanTriVien == quantrivien.IdquanTriVien);
            if (Checknhanvien != null)
            {
                Checknhanvien.AnhQtv = quantrivien.AnhQtv;
                Checknhanvien.HoQtv = quantrivien.HoQtv;
                Checknhanvien.TenQtv = quantrivien.TenQtv;
                Checknhanvien.Email = quantrivien.Email;
                Checknhanvien.SoDienThoai = quantrivien.SoDienThoai;
                Checknhanvien.NgaySinh = quantrivien.NgaySinh;
                Checknhanvien.DiaChi = quantrivien.DiaChi;
                Checknhanvien.GioiTinh = quantrivien.GioiTinh;
                Checknhanvien.Chucvu = quantrivien.Chucvu;
                Checknhanvien.Cccd = quantrivien.Cccd;

                _context.SaveChanges();
                return Ok("Success");
            }

            return BadRequest("Failed");
        }


        [HttpGet("LayDanhSachNhanVienQuanTri")]
        public IActionResult LayDanhSachNhanVienQuanTri()
        {
            var list = _context.QuanTriViens.ToList();

            return Ok(list);
        }

        [HttpGet("LayDanhSachNguoiDung")]
        public IActionResult LayDanhSachNguoiDung()
        {
            var list = _context.NguoiDungs.ToList();

            return Ok(list);
        }

        [HttpPut("khoanguoidung/{iduser}")]
        public IActionResult lockuser(int iduser)
        {
            var checkuser = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == iduser);
            if (checkuser == null)
            {
                return BadRequest("Người dùng không tồn tại");
            }
            else
            {
                checkuser.TrangThaiNguoiDung = false;
                _context.SaveChanges();
                return Ok("Khóa tài khoản thành công");
            }
        }

        [HttpPut("mokhoanguoidung/{iduser}")]
        public IActionResult mokhoanguoidung(int iduser)
        {
            var checkuser = _context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == iduser);
            if (checkuser == null)
            {
                return BadRequest("Người dùng không tồn tại");
            }
            else
            {
                checkuser.TrangThaiNguoiDung = true;
                _context.SaveChanges();
                return Ok("Mở khóa tài khoản thành công");
            }
        }

        [HttpGet("Laytatcaphongkham")]
        public IActionResult Laytatcaphongkham()
        {
            var list = from x in _context.PhongKhams
                       join c in _context.NguoiDungs on x.IdnguoiDung equals c.IdNguoiDung
                       join d in _context.XaPhuongs on x.IdxaPhuong equals d.IdxaPhuong
                       join e in _context.QuanHuyens on d.IdquanHuyen equals e.IdquanHuyen
                       select new
                       {
                           x.IdphongKham,
                           x.AnhDaiDienPhongKham,
                           x.LoaiPhongKham,
                           x.TenPhongKham,
                           x.TrangThai,
                           x.HinhAnh,
                           x.NgayMoPhongKham,
                           x.DiaChi,
                           d.TenXaPhuong,
                           e.TenQuanHuyen,
                           c.HoNguoiDung,
                           c.TenNguoiDung
                       };
            return Ok(list);
        }

        [HttpGet("Laytatcacosoyte")]
        public IActionResult Laytatcacosoyte()
        {
            var list = from x in _context.CoSoDichVuKhacs
                       join q in _context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals q.IdcoSoDichVuKhac
                       join k in _context.ChuyenMons on q.IdchuyenMon equals k.IdChuyenMon
                       join c in _context.NguoiDungs on x.IdnguoiDung equals c.IdNguoiDung
                       join d in _context.XaPhuongs on x.IdxaPhuong equals d.IdxaPhuong
                       join e in _context.QuanHuyens on d.IdquanHuyen equals e.IdquanHuyen
                       select new
                       {
                           x.IdcoSoDichVuKhac,
                           x.AnhDaiDienCoSo,
                           x.TenCoSo,
                           x.TrangThai,
                           x.HinhAnhCoSo,
                           x.NgayMoCoSo,
                           x.DiaChi,
                           d.TenXaPhuong,
                           e.TenQuanHuyen,
                           c.HoNguoiDung,
                           c.TenNguoiDung,
                           k.TenChuyenMon,
                       };
            return Ok(list);
        }



        [HttpDelete("LockClinic/{idphongkham}")]
        public IActionResult LockClinic(string idphongkham)
        {
            var phongkham = _context.PhongKhams.FirstOrDefault(x => x.IdphongKham == idphongkham);

            if(phongkham == null)
            {
                return BadRequest("Phòng khám không tồn tại");
            }
            else
            {
                phongkham.TrangThai = false;
                _context.SaveChanges();
                return Ok("success");
            }
        }

        [HttpPut("UnLockClinic/{idphongkham}")]
        public IActionResult UnLockClinic(string idphongkham)
        {
            var phongkham = _context.PhongKhams.FirstOrDefault(x => x.IdphongKham == idphongkham);

            if (phongkham == null)
            {
                return BadRequest("Phòng khám không tồn tại");
            }
            else
            {
                phongkham.TrangThai = true;
                _context.SaveChanges();
                return Ok("success");
            }
        }

        [HttpDelete("LockFacitilies/{idcoso}")]
        public IActionResult LockFacitilies(string idcoso)
        {
            var coso = _context.CoSoDichVuKhacs.FirstOrDefault(x => x.IdcoSoDichVuKhac == idcoso);
            if (coso == null)
            {
                return BadRequest("Phòng khám không tồn tại");
            }
            else
            {
                coso.TrangThai = false;
                _context.SaveChanges();
                return Ok("success");
            }
        }

        [HttpPut("UnLockFacitilies/{idcoso}")]
        public IActionResult UnLockFacitilies(string idcoso)
        {
            var coso = _context.CoSoDichVuKhacs.FirstOrDefault(x => x.IdcoSoDichVuKhac == idcoso);
            if (coso == null)
            {
                return BadRequest("Phòng khám không tồn tại");
            }
            else
            {
                coso.TrangThai = true;
                _context.SaveChanges();
                return Ok("success");
            }
        }

        [HttpGet("LayDanhSachChuyenKhoa")]
        public IActionResult LayDanhSachChuyenKhoa()
        {
            var list = _context.ChuyenKhoas.ToList();

            return Ok(list);
        }

        [HttpPost("AddSpecialist")]
        public IActionResult AddSpecialist(ChuyenKhoa chuyenkhoa)
        {
            var checkchuyenkhoa = _context.ChuyenKhoas.FirstOrDefault(x => x.TenChuyenKhoa == chuyenkhoa.TenChuyenKhoa);
            
            if(checkchuyenkhoa == null)
            {
                var addspecialist = new ChuyenKhoa
                {
                    IdchuyenKhoa = chuyenkhoa.IdchuyenKhoa,
                    TenChuyenKhoa = chuyenkhoa.TenChuyenKhoa,
                    Anh = chuyenkhoa.Anh,
                    MoTa = chuyenkhoa.MoTa,
                };
                _context.ChuyenKhoas.Add(addspecialist);
                _context.SaveChanges();
                return Ok(addspecialist.IdchuyenKhoa);
            }
            else
            {
                return BadRequest("Đã có chuyên khoa");
            }
        }
        [HttpPut("EditSpecialist")]
        public IActionResult EditSpecialist(ChuyenKhoa chuyenkhoa)
        {
            var checkchuyenkhoa = _context.ChuyenKhoas.FirstOrDefault(x => x.IdchuyenKhoa == chuyenkhoa.IdchuyenKhoa);
            var k = _context.ChuyenKhoas.ToList();

            foreach(var x in k)
            {
                if(chuyenkhoa.TenChuyenKhoa == x.TenChuyenKhoa && chuyenkhoa.TenChuyenKhoa != checkchuyenkhoa.TenChuyenKhoa)
                {
                    return BadRequest("Chuyên khoa đã tồn tại");
                }
            }


            if (checkchuyenkhoa != null)
            {

                checkchuyenkhoa.TenChuyenKhoa = chuyenkhoa.TenChuyenKhoa;
                checkchuyenkhoa.Anh = chuyenkhoa.Anh;
                checkchuyenkhoa.MoTa = chuyenkhoa.MoTa;

                _context.SaveChanges();
                return Ok(checkchuyenkhoa.IdchuyenKhoa);
            }
            else
            {
                return BadRequest("Chuyên khoa không tồn tại");
            }
        }



        [HttpDelete("xoachuyenkhoa/{idchuyenkhoa}")]
        public IActionResult xoachuyenkhoa(int idchuyenkhoa)
        {
            var checkchuyenkhoa = _context.ChuyenKhoas.FirstOrDefault(x => x.IdchuyenKhoa == idchuyenkhoa);

            if(checkchuyenkhoa != null)
            {
                var getlistdoctorforchuyenkhoa = (from x in _context.ChuyenKhoas
                                                  join c in _context.ChuyenKhoaPhongKhams on x.IdchuyenKhoa equals c.IdchuyenKhoa
                                                  select x.IdchuyenKhoa).Distinct();
                if(getlistdoctorforchuyenkhoa != null)
                {
                    foreach(var x in getlistdoctorforchuyenkhoa)
                    {
                        if(x == idchuyenkhoa)
                        {
                            return BadRequest("Chuyên khoa đã có phòng khám và bác sĩ đăng ký");
                        }
                    }
                    _context.ChuyenKhoas.Remove(checkchuyenkhoa);
                    _context.SaveChanges();
                    return Ok("Xóa thành công");
                }
                else
                {
                    return BadRequest("Chưa có chuyên khoa nào sao mà xóa");
                }
            }
            else
            {
                return BadRequest("Chuyên khoa không tồn tại");
            }
        }


        [HttpGet("Laydanhsachchuyenmon")]
        public IActionResult Laydanhsachchuyenmon()
        {
            var list = _context.ChuyenMons.ToList();

            return Ok(list);
        }


        [HttpPost("AddSpecializelist")]
        public IActionResult AddSpecializelist(ChuyenMon chuyenmon)
        {
            var checkchuyenkhoa = _context.ChuyenMons.FirstOrDefault(x => x.TenChuyenMon == chuyenmon.TenChuyenMon);

            if (checkchuyenkhoa == null)
            {
                var addspecialist = new ChuyenMon
                {
                    IdChuyenMon = chuyenmon.IdChuyenMon,
                    TenChuyenMon = chuyenmon.TenChuyenMon,
                    AnhChuyeMon = chuyenmon.AnhChuyeMon,
                    MoTaChuyenMon = chuyenmon.MoTaChuyenMon,
                };
                _context.ChuyenMons.Add(addspecialist);
                _context.SaveChanges();
                return Ok(addspecialist.IdChuyenMon);
            }
            else
            {
                return BadRequest("Đã có chuyên khoa");
            }
        }
        [HttpPut("EditSpecializelist")]
        public IActionResult EditSpecializelist(ChuyenMon chuyenmon)
        {
            var checkchuyenkhoa = _context.ChuyenMons.FirstOrDefault(x => x.IdChuyenMon == chuyenmon.IdChuyenMon);
            var k = _context.ChuyenKhoas.ToList();

            foreach (var x in k)
            {
                if (chuyenmon.TenChuyenMon == x.TenChuyenKhoa && chuyenmon.TenChuyenMon != checkchuyenkhoa.TenChuyenMon)
                {
                    return BadRequest("Chuyên khoa đã tồn tại");
                }
            }


            if (checkchuyenkhoa != null)
            {

                checkchuyenkhoa.TenChuyenMon = chuyenmon.TenChuyenMon;
                checkchuyenkhoa.AnhChuyeMon = chuyenmon.AnhChuyeMon;
                checkchuyenkhoa.MoTaChuyenMon = chuyenmon.MoTaChuyenMon;

                _context.SaveChanges();
                return Ok(checkchuyenkhoa.IdChuyenMon);
            }
            else
            {
                return BadRequest("Chuyên khoa không tồn tại");
            }
        }



        [HttpDelete("xoachuyenmon/{idchuyenmon}")]
        public IActionResult xoachuyenmon(int idchuyenmon)
        {
            var checkchuyenkhoa = _context.ChuyenMons.FirstOrDefault(x => x.IdChuyenMon == idchuyenmon);

            if (checkchuyenkhoa != null)
            {
                var getlistdoctorforchuyenkhoa = (from x in _context.ChuyenMons
                                                  join c in _context.ChuyenMoncoSos on x.IdChuyenMon equals c.IdchuyenMon
                                                  select x.IdChuyenMon).Distinct();
                if (getlistdoctorforchuyenkhoa != null)
                {
                    foreach (var x in getlistdoctorforchuyenkhoa)
                    {
                        if (x == idchuyenmon)
                        {
                            return BadRequest("Chuyên môn đã có cơ sở đăng ký");
                        }
                    }
                    _context.ChuyenMons.Remove(checkchuyenkhoa);
                    _context.SaveChanges();
                    return Ok("Xóa thành công");
                }
                else
                {
                    return BadRequest("Chưa có chuyên môn nào sao mà xóa");
                }
            }
            else
            {
                return BadRequest("Chuyên môn không tồn tại");
            }
        }


        [HttpGet("laytatcaphongkhamstatistical")]
        public IActionResult laytatcaphongkhamstatistical()
        {
            var list = from x in _context.PhongKhams
                       join k in _context.NguoiDungs on x.IdnguoiDung equals k.IdNguoiDung
                       select new
                       {
                           x,
                           k.HoNguoiDung,
                           k.TenNguoiDung,
                       };

            return Ok(list);
        }

        [HttpGet("LayTatCaSoLanDatLichCuaPhongKham/{idphongkham}")]
        public IActionResult LayTatCaSoLanDatLichCuaPhongKham(string idphongkham)
        {
            var danhsach = from x in _context.PhongKhams
                           join c in _context.BacSis on x.IdphongKham equals c.IdphongKham
                           join q in _context.KeHoachKhams on c.IdbacSi equals q.IdbacSi
                           join w in _context.DatLiches on q.IdkeHoachKham equals w.IdkeHoachKham
                           join y in _context.TaoLiches on w.IddatLich equals y.IddatLich
                           where x.IdphongKham == idphongkham && y.TrangThaiTaoLich == 3
                           select new
                           {
                               c.HoTenBacSi,
                               c.GiaKham,
                           };
            return Ok(danhsach);
        }

        [HttpGet("laytatcacosostatistical")]
        public IActionResult laytatcacosostatistical()
        {
            var list = from x in _context.CoSoDichVuKhacs
                       join c in _context.NguoiDungs on x.IdnguoiDung equals c.IdNguoiDung
                       select new
                       {
                           x,
                           c.HoNguoiDung,
                           c.TenNguoiDung,
                       };

            return Ok(list);
        }



    }
}
