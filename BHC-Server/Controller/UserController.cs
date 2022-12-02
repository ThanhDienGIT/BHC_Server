using BHC_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookingHealthCare_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DB_BHCContext _Context;
        public UserController(DB_BHCContext context)
        {
            _Context = context;
        }

        [HttpGet("{id}")]
           public IActionResult GetUserById(int id)
           {

            var InfoUserById = _Context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == id);

                if(InfoUserById != null)
                {
                 return Ok(InfoUserById);
            }
            else
            {
                return BadRequest("Not found");
            }
 
            
            }

        [HttpPut]
        public IActionResult ChangeInfoUser(ChangeUser user)
        {
            var nguoidung = _Context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == user.IdNguoiDung);
            var a = _Context.NguoiDungs.ToList();
            bool checkEmail = false;
            bool checkNumber = false;
            bool checkCCCD  =  false;
            bool sodienthoainguoithan = false;
            string messemail = "";
            string messesdt = "";
            string messesdtnt = "";
            string messecccd = "";
            string mess = "";
            if (nguoidung != null)
            {
                if (user.Email != nguoidung.Email || user.SoDienThoai != nguoidung.SoDienThoai
                || user.Cccd != nguoidung.Cccd || user.SoDienThoaiNguoiThan != nguoidung.SoDienThoaiNguoiThan)
                {
                    foreach (var x in a)
                    {
                        if (x.Email == user.Email && x.Email != nguoidung.Email)
                        {
                            checkEmail = true;
                            messemail = "Đã tồn tại Email ";
                        }
                        if(x.SoDienThoai == user.SoDienThoai && x.SoDienThoai != nguoidung.SoDienThoai)
                        {
                            checkNumber = true;
                            messesdt = "Đã tồn tại số điện thoại ";
                        }
                        if(x.SoDienThoaiNguoiThan == user.SoDienThoaiNguoiThan && x.SoDienThoaiNguoiThan != nguoidung.SoDienThoaiNguoiThan)
                        {
                            sodienthoainguoithan = true;
                            messesdtnt = "Đã tồn tại số điện thoại của người thân ";
                        }
                        if(x.Cccd == user.Cccd && x.Cccd != nguoidung.Cccd)
                        {
                            checkCCCD = true;
                            messecccd = "Đã tồn tại căn cước công dân";
                        }
                    }
                }
            }
            else
            {
                return BadRequest("No User");
            }
            mess += messemail + messesdt + messesdtnt + messecccd;
            if (checkNumber == false && checkCCCD == false && sodienthoainguoithan==false && checkEmail == false)
            {
                if (nguoidung != null && user.AnhNguoidung != null)
                {
                    nguoidung.HoNguoiDung = user.HoNguoiDung;
                    nguoidung.TenNguoiDung = user.TenNguoiDung;
                    nguoidung.Email = user.Email;
                    nguoidung.Cccd = user.Cccd;
                    nguoidung.NgaySinh = user.NgaySinh;
                    nguoidung.MatKhau = user.MatKhau;
                    nguoidung.SoDienThoai = user.SoDienThoai;
                    nguoidung.GioiTinh = user.GioiTinh;
                    nguoidung.AnhNguoidung = user.AnhNguoidung;
                    nguoidung.SoDienThoaiNguoiThan = user.SoDienThoaiNguoiThan;
                    nguoidung.Diachi = user.Diachi;
                    nguoidung.TienSuBenh = user.TienSuBenh;
                    nguoidung.CanNang = user.CanNang;
                    nguoidung.ChieuCao = user.ChieuCao;
                    nguoidung.Bmi = user.Bmi;
                    nguoidung.QuocTich = user.QuocTich;
                    _Context.SaveChanges();
                    return Ok("Change Success");
                }
                else
                {
                    nguoidung.HoNguoiDung = user.HoNguoiDung;
                    nguoidung.TenNguoiDung = user.TenNguoiDung;
                    nguoidung.Email = user.Email;
                    nguoidung.Cccd = user.Cccd;
                    nguoidung.NgaySinh = user.NgaySinh;
                    nguoidung.MatKhau = user.MatKhau;
                    nguoidung.SoDienThoai = user.SoDienThoai;
                    nguoidung.GioiTinh = user.GioiTinh;
                    nguoidung.SoDienThoaiNguoiThan = user.SoDienThoaiNguoiThan;
                    nguoidung.Diachi = user.Diachi;
                    nguoidung.TienSuBenh = user.TienSuBenh;
                    nguoidung.CanNang = user.CanNang;
                    nguoidung.ChieuCao = user.ChieuCao;
                    nguoidung.Bmi = user.Bmi;
                    nguoidung.QuocTich = user.QuocTich;
                    _Context.SaveChanges();
                    return Ok("Change Success");
                }
            }
            else
            {
                return BadRequest(mess);
            }
          
        }

        [HttpGet("layidbacsibangiduser/{iduser}")]
        public IActionResult Checklichkham(int iduser)
        {
            var checkdortor = from x in _Context.BacSis
                              join c in _Context.PhongKhams on x.IdphongKham equals c.IdphongKham
                              where c.IdnguoiDung == iduser
                              select x.IdbacSi;
            if(checkdortor.Count() > 0)
            {
                return Ok(checkdortor);
            }
            else
            {
                return BadRequest("Nodata");
            }     
        }

        [HttpGet("LayLichSuDatLich/{iduser}")]
        public IActionResult LayLichSuDatLich(int iduser)
        {
            var ListScheduleUser = (from x in _Context.KeHoachKhams
                                   join c in _Context.DatLiches on x.IdkeHoachKham equals c.IdkeHoachKham
                                   join d in _Context.TaoLiches on c.IddatLich equals d.IddatLich
                                   join e in _Context.BacSis on x.IdbacSi equals e.IdbacSi
                                   join k in _Context.PhongKhams on e.IdphongKham equals k.IdphongKham
                                   where d.IdnguoiDungDatLich == iduser
                                   select new
                                   {
                                       x.NgayDatLich,
                                       x.TrangThaiKeHoachKham,
                                       c.ThoiGianDatLich,
                                       c.TrangThaiDatLich,
                                       c.IddatLich,
                                       d.NgayGioDatLich,
                                       d.LyDoKham,
                                       d.TrangThaiTaoLich,
                                       e.SoDienThoaiBacSi,
                                       e.IdbacSi,
                                       e.HoTenBacSi,
                                       e.GiaKham,
                                       k.DiaChi,
                                   }).OrderBy(x=>x.NgayDatLich);

            return Ok(ListScheduleUser);
        }

        [HttpPut("thaydoilich/{iddatlichcu}/{iddatlichmoi}")]
        public IActionResult thaydoilich(int iddatlichcu,int iddatlichmoi)
        {
            var thaydoilich = _Context.TaoLiches.FirstOrDefault(x => x.IddatLich == iddatlichcu);
            var lichcu = _Context.DatLiches.FirstOrDefault(x => x.IddatLich == iddatlichcu);
            var lichmoi = _Context.DatLiches.FirstOrDefault(x => x.IddatLich == iddatlichmoi);
            if(thaydoilich != null && lichcu != null && lichmoi != null)
            {
                lichmoi.SoLuongHienTai++;
                lichcu.SoLuongHienTai--;
                thaydoilich.IddatLich = iddatlichmoi;
                _Context.SaveChanges();
                return Ok("success");
            }
            else
            {
                return BadRequest("Lịch không tồn tại");

            }
        }


        [HttpGet("checkdanhgiabacsi/{idnguoidung}/{idbacsi}")]
        public IActionResult checkdanhgiabacsi(int idnguoidung, string idbacsi)
        {
            var check = from x in _Context.NguoiDungs
                        join d in _Context.TaoLiches on x.IdNguoiDung equals d.IdnguoiDungDatLich
                        join e in _Context.DatLiches on d.IddatLich equals e.IddatLich
                        join b in _Context.KeHoachKhams on e.IdkeHoachKham equals b.IdkeHoachKham
                        join c in _Context.BacSis on b.IdbacSi equals c.IdbacSi
                        where x.IdNguoiDung == idnguoidung && c.IdbacSi == idbacsi && d.TrangThaiTaoLich == 3
                        select d;
            if (check.Count() > 0)
            {
                return Ok("success");
            }
            else
            {
                return BadRequest("Không được đánh giá");
            }
        }

        [HttpGet("checkdanhgiaphongkham/{idnguoidung}/{idphongkham}")]
        public IActionResult checkdanhgiaphongkham(int idnguoidung, string idphongkham)
        {
            var check = from x in _Context.NguoiDungs
                        join d in _Context.TaoLiches on x.IdNguoiDung equals d.IdnguoiDungDatLich
                        join e in _Context.DatLiches on d.IddatLich equals e.IddatLich
                        join b in _Context.KeHoachKhams on e.IdkeHoachKham equals b.IdkeHoachKham
                        join c in _Context.BacSis on b.IdbacSi equals c.IdbacSi
                        join n in _Context.PhongKhams on c.IdphongKham equals n.IdphongKham
                        where x.IdNguoiDung == idnguoidung && n.IdphongKham == idphongkham && d.TrangThaiTaoLich == 3
                        select d;
            if (check.Count() > 0)
            {
                return Ok("success");
            }
            else
            {
                return BadRequest("Không được đánh giá");
            }
        }


        [HttpGet("checkdanhgianhanviencoso/{idnguoidung}/{idnhanvien}")]
        public IActionResult checkdanhgianhanviencoso(int idnguoidung, string idnhanvien)
        {
            var check = from x in _Context.NguoiDungs
                        join d in _Context.TaoLichNhanVienCoSos on x.IdNguoiDung equals d.IdnguoiDungDatLich
                        join e in _Context.DatLichNhanVienCoSos on d.IddatLichNhanVienCoSo equals e.IddatLichNhanVienCoSo
                        join b in _Context.KeHoachNhanVienCoSos on e.IdkeHoachNhanVienCoSo equals b.IdkeHoachNhanVienCoSo
                        join c in _Context.NhanVienCoSos on b.IdnhanVienCoSo equals c.IdnhanVienCoSo
                        where x.IdNguoiDung == idnguoidung && c.IdnhanVienCoSo == idnhanvien && d.TrangThaiTaoLich == 3
                        select d;
            if (check.Count() > 0)
            {
                return Ok("success");
            }
            else
            {
                return BadRequest("Không được đánh giá");
            }
        }

        [HttpGet("checkdanhgiacoso/{idnguoidung}/{idcoso}")]
        public IActionResult checkdanhgiacoso(int idnguoidung, string idcoso)
        {
            var check = from x in _Context.NguoiDungs
                        join d in _Context.TaoLichNhanVienCoSos on x.IdNguoiDung equals d.IdnguoiDungDatLich
                        join e in _Context.DatLichNhanVienCoSos on d.IddatLichNhanVienCoSo equals e.IddatLichNhanVienCoSo
                        join b in _Context.KeHoachNhanVienCoSos on e.IdkeHoachNhanVienCoSo equals b.IdkeHoachNhanVienCoSo
                        join c in _Context.NhanVienCoSos on b.IdnhanVienCoSo equals c.IdnhanVienCoSo
                        join q in _Context.CoSoDichVuKhacs on c.IdcoSoDichVuKhac equals q.IdcoSoDichVuKhac
                        where x.IdNguoiDung == idnguoidung && q.IdcoSoDichVuKhac == idcoso && d.TrangThaiTaoLich == 3
                        select d;
            
            if (check.Count() > 0)
            {
                return Ok("success");
            }
            else
            {
                return BadRequest("Không được đánh giá");
            }
        }

        [HttpPost("danhgia/{id}/{idnguoidung}")]
        public IActionResult danhgia(DanhGiaCuaNguoiDung sosao,string id,int idnguoidung)
        {
            var checkbacsi = _Context.BacSis.FirstOrDefault(x => x.IdbacSi == id);
            var checknhanviencoso = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == id);
            var checkphongkham = _Context.PhongKhams.FirstOrDefault(x => x.IdphongKham == id);
            var checkcoso = _Context.CoSoDichVuKhacs.FirstOrDefault(x => x.IdcoSoDichVuKhac == id);

            var checkdanhgiabacsi = _Context.DanhGiaCosos
                .FirstOrDefault(x => x.IdbacSi == id && x.Idnguoidanhgia == idnguoidung);
            var checkdanhgianhanvien = _Context.DanhGiaCosos
                .FirstOrDefault(x => x.IdnhanVienCoSo == id && x.Idnguoidanhgia == idnguoidung);
            var checkdanhgiaphongkham = _Context.DanhGiaCosos
                .FirstOrDefault(x => x.IdphongKham == id && x.Idnguoidanhgia == idnguoidung);
            var checkdanhgiacoso = _Context.DanhGiaCosos
                .FirstOrDefault(x => x.IdcoSoDichVuKhac == id && x.Idnguoidanhgia == idnguoidung);

            if (checkbacsi != null && checkdanhgiabacsi == null)
            {
              if(checkbacsi.Danhgia == 0)
              {
                    var danhgia = new DanhGiaCoso
                    {
                        IdbacSi = id,
                        Idnguoidanhgia = idnguoidung,
                        SoSao = sosao.sosao,
                        NhanXet = sosao.nhanxet,
                    };
                    _Context.DanhGiaCosos.Add(danhgia);
                    checkbacsi.Danhgia += sosao.sosao;
                    _Context.SaveChanges();
                    return Ok("success");
              }
              if(checkbacsi.Danhgia > 0)
                {
                    var listdanhgia = _Context.DanhGiaCosos.Where(x=>x.IdbacSi == id).ToList();
                    int tongso = listdanhgia.Count();
                    double tonggiatri = 0.0;
                    foreach(var x in listdanhgia)
                    {
                        tonggiatri += x.SoSao;
                    }
                    tonggiatri += sosao.sosao;
                    double danhgiabacsi = tonggiatri / (tongso + 1);

                    var danhgia = new DanhGiaCoso
                    {
                        IdbacSi = id,
                        Idnguoidanhgia = idnguoidung,
                        SoSao = sosao.sosao,
                        NhanXet = sosao.nhanxet,
                    };
                    _Context.DanhGiaCosos.Add(danhgia);
                    checkbacsi.Danhgia = danhgiabacsi;
                    _Context.SaveChanges();
                    return Ok("success");
                }
            }
            // nhanvien
            if (checknhanviencoso != null && checkdanhgianhanvien == null)
            {
                if (checknhanviencoso.Danhgia == 0)
                {
                    var danhgia = new DanhGiaCoso
                    {
                        IdnhanVienCoSo = id,
                        Idnguoidanhgia = idnguoidung,
                        SoSao = sosao.sosao,
                        NhanXet= sosao.nhanxet,
                    };
                    _Context.DanhGiaCosos.Add(danhgia);
                    checknhanviencoso.Danhgia += sosao.sosao;
                    _Context.SaveChanges();
                    return Ok("success");
                }
                if (checknhanviencoso.Danhgia > 0)
                {
                    var listdanhgia = _Context.DanhGiaCosos.Where(x => x.IdnhanVienCoSo == id).ToList();
                    int tongso = listdanhgia.Count();
                    double tonggiatri = 0.0;
                    foreach (var x in listdanhgia)
                    {
                        tonggiatri += x.SoSao;
                    }
                    tonggiatri += sosao.sosao;
                    double danhgiabacsi = tonggiatri / (tongso + 1);

                    var danhgia = new DanhGiaCoso
                    {
                        IdnhanVienCoSo = id,
                        Idnguoidanhgia = idnguoidung,
                        SoSao = sosao.sosao,
                        NhanXet = sosao.nhanxet
                    };
                    _Context.DanhGiaCosos.Add(danhgia);
                    checknhanviencoso.Danhgia = danhgiabacsi;
                    _Context.SaveChanges();
                    return Ok("success");
                }
            }
            if (checkphongkham != null && checkdanhgiaphongkham == null)
            {
                if (checkphongkham.Danhgia == 0)
                {
                    var danhgia = new DanhGiaCoso
                    {
                        IdphongKham = id,
                        Idnguoidanhgia = idnguoidung,
                        SoSao = sosao.sosao,
                        NhanXet = sosao.nhanxet,
                    };
                    _Context.DanhGiaCosos.Add(danhgia);
                    checkphongkham.Danhgia += sosao.sosao;
                    _Context.SaveChanges();
                    return Ok("success");
                }
                if (checkphongkham.Danhgia > 0)
                {
                    var listdanhgia = _Context.DanhGiaCosos.Where(x => x.IdphongKham == id).ToList();
                    int tongso = listdanhgia.Count();
                    double tonggiatri = 0.0;
                    foreach (var x in listdanhgia)
                    {
                        tonggiatri += x.SoSao;
                    }
                    tonggiatri += sosao.sosao;
                    double danhgiabacsi = tonggiatri / (tongso + 1);

                    var danhgia = new DanhGiaCoso
                    {
                        IdphongKham = id,
                        Idnguoidanhgia = idnguoidung,
                        SoSao = sosao.sosao,
                        NhanXet = sosao.nhanxet,
                    };
                    _Context.DanhGiaCosos.Add(danhgia);
                    checkphongkham.Danhgia = danhgiabacsi;
                    _Context.SaveChanges();
                    return Ok("success");
                }
            }
            if (checkcoso != null && checkdanhgiacoso == null)
            {
                if (checkcoso.Danhgia == 0)
                {
                    var danhgia = new DanhGiaCoso
                    {
                        IdcoSoDichVuKhac = id,
                        Idnguoidanhgia = idnguoidung,
                        SoSao = sosao.sosao,
                        NhanXet = sosao.nhanxet,
                    };
                    _Context.DanhGiaCosos.Add(danhgia);
                    checkcoso.Danhgia += sosao.sosao;
                    _Context.SaveChanges();
                    return Ok("success");
                }
                if (checkcoso.Danhgia > 0)
                {
                    var listdanhgia = _Context.DanhGiaCosos.Where(x => x.IdcoSoDichVuKhac == id).ToList();
                    int tongso = listdanhgia.Count();
                    double tonggiatri = 0.0;
                    foreach (var x in listdanhgia)
                    {
                        tonggiatri += x.SoSao;
                    }
                    tonggiatri += sosao.sosao;
                    double danhgiabacsi = tonggiatri / (tongso + 1);

                    var danhgia = new DanhGiaCoso
                    {
                        IdcoSoDichVuKhac = id,
                        Idnguoidanhgia = idnguoidung,
                        SoSao = sosao.sosao,
                        NhanXet = sosao.nhanxet,
                    };
                    _Context.DanhGiaCosos.Add(danhgia);
                    checkcoso.Danhgia = danhgiabacsi;
                    _Context.SaveChanges();
                    return Ok("success");
                }
            }

            return BadRequest("failed");
        }

        [HttpPost("Thaydoimatkhaunguoidung/{idnguoidung}/{matkhaumoi}")]
        public IActionResult Thaydoimatkhaunguoidung(int idnguoidung,string matkhaumoi)
        {
            var nguoidung = _Context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == idnguoidung);

            if(nguoidung != null)
            {
                nguoidung.MatKhau = matkhaumoi;
                _Context.SaveChanges();
                return Ok("Success");
            }


            return BadRequest("Failed");
        }

        [HttpGet("laytatcabacsi")]
        public IActionResult laytatcabacsi()
        {
            var list = from x in _Context.BacSis
                       join c in _Context.PhongKhams on x.IdphongKham equals c.IdphongKham
                       where x.TrangThai == true && c.TrangThai == true
                       select x;
                       
            return Ok(list);
        }

        [HttpGet("laytatcanhanvienyte")]
        public IActionResult laytatcanhanvienyte()
        {
            var list = from x in _Context.NhanVienCoSos
                       join c in _Context.CoSoDichVuKhacs on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                       where x.TrangThai == true && c.TrangThai == true
                       select x;

            return Ok(list);
        }

        [HttpGet("laytatcaphongkham")]
        public IActionResult laytatcaphongkham()
        {
            var list = _Context.PhongKhams.Where(x => x.TrangThai == true);

            return Ok(list);
        }

        [HttpGet("laytatcacoso")]
        public IActionResult laytatcacoso()
        {
            var list = _Context.CoSoDichVuKhacs.Where(x => x.TrangThai == true);
                       

            return Ok(list);
        }

        [HttpGet("laytatcachuyenkhoa")]
        public IActionResult laytatcachuyenkhoa()
        {
            var list = _Context.ChuyenKhoas.ToList();

            return Ok(list);
        }

        [HttpGet("laytatcachuyenmon")]
        public IActionResult laytatcachuyenmon()
        {
            var list = _Context.ChuyenMons.ToList();

            return Ok(list);
        }

    }
}
