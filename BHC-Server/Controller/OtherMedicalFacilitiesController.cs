using BHC_Server.Models;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BHC_Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherMedicalFacilitiesController : ControllerBase
    {

        private readonly DB_BHCContext _Context;
        public OtherMedicalFacilitiesController(DB_BHCContext context)
        {
            _Context = context;
        }

        [HttpGet("LayDanhSachChuyenMon")]
        public IActionResult LayDanhSachChuyenMon()
        {
            var danhsach = _Context.ChuyenMons.ToList();
            return Ok(danhsach);
        }

        [HttpGet("LayThongtincoso/{idnhanvien}")]
        public IActionResult LayThongtincoso(string idnhanvien)
        {
            var infofacitilies = from x in _Context.CoSoDichVuKhacs
                                 join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                                 where c.IdnhanVienCoSo == idnhanvien
                                 select new { 
                                     x,
                                     c.HoTenNhanVien,
                                     c.Idnguoidung,
                                 };
            return Ok(infofacitilies);
        }

        [HttpGet("LayIdNhanVienByIdNguoiDung/{idnguoidung}")]
        public IActionResult LayIdNhanVienByIdNguoiDung(int idnguoidung)
        {
            var idnhanvien = _Context.NhanVienCoSos.FirstOrDefault(x => x.Idnguoidung == idnguoidung);
            if (idnhanvien != null)
            {
                return Ok(idnhanvien.IdnhanVienCoSo);
            }
            else
            {
                return Ok("Khongco");
            }
           
        }

        [HttpGet("LayLichKhamBacSi/{idbacsi}")]
        public IActionResult LayLichKhamBacSi(string idbacsi)
        {

            var List = (from a in _Context.DatLichNhanVienCoSos
                        join c in _Context.KeHoachNhanVienCoSos on a.IdkeHoachNhanVienCoSo equals c.IdkeHoachNhanVienCoSo
                        join d in _Context.TaoLichNhanVienCoSos on a.IddatLichNhanVienCoSo equals d.IddatLichNhanVienCoSo into dept
                        from k in dept.DefaultIfEmpty()
                        select new
                        {
                            c.IdnhanVienCoSo,
                            c.NgayDatLich,
                            c.TrangThaiKeHoachNhanVienCoSo,
                            a.ThoiGianDatLich,
                            a.IddatLichNhanVienCoSo,
                            a.SoLuongToiDa,
                            a.TrangThaiDatLich,
                            LyDoKham = k.LyDoKham != null ? k.LyDoKham : null,
                            IdtaoLich = k.IdtaoLichNhanVienCoSo != null ? k.IdtaoLichNhanVienCoSo : 0,
                            User = k.IdnguoiDungDatLichNavigation != null ? k.IdnguoiDungDatLichNavigation : null,
                            TrangThaiTaoLich = k.TrangThaiTaoLich != null ? k.TrangThaiTaoLich : null,
                        }).Where(c => c.IdnhanVienCoSo == idbacsi).OrderBy(c => c.NgayDatLich);

            return Ok(List);
        }

        [HttpGet("GetstaffById/{idbacsi}")]
        public IActionResult GetDoctorById(string idbacsi)
        {
            var infomation = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == idbacsi);
            
            return Ok(infomation);
        }

        [HttpGet("Getbookbystaff/{idbacsi}")]
        public IActionResult Getbookbyiddoctor(string idbacsi)
        {

            var lichcuabacsi = (from p in _Context.NhanVienCoSos
                                join b in _Context.KeHoachNhanVienCoSos on p.IdnhanVienCoSo equals b.IdnhanVienCoSo
                                join c in _Context.DatLichNhanVienCoSos on b.IdkeHoachNhanVienCoSo equals c.IdkeHoachNhanVienCoSo
                                where p.IdnhanVienCoSo == idbacsi && c.IddatLichNhanVienCoSo != null
                                select new
                                {
                                    b.NgayDatLich
                                }).Distinct();
            return Ok(lichcuabacsi);

        }

        [HttpGet("GetDiaChi/{idbacsi}")]
        public IActionResult getdiachi(string idbacsi)
        {
            var diachi = from x in _Context.CoSoDichVuKhacs
                         join d in _Context.XaPhuongs on x.IdxaPhuong equals d.IdxaPhuong
                         join c in _Context.QuanHuyens on d.IdquanHuyen equals c.IdquanHuyen
                         join e in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals e.IdcoSoDichVuKhac
                         where e.IdnhanVienCoSo == idbacsi
                         select new
                         {
                             x.DiaChi,
                             d.TenXaPhuong,
                             c.TenQuanHuyen,
                         };
            if (diachi.Count() > 0)
            {
                return Ok(diachi);
            }
            else
            {
                return BadRequest("No data");
            }
        }

        [HttpGet("Laythongtincuacosoupdate/{idnhanvien}")]
        public IActionResult Laythongtincuacoso(string idnhanvien)
        {
            var coso = (from x in _Context.CoSoDichVuKhacs
                        join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                        where c.IdnhanVienCoSo == idnhanvien
                        select x).FirstOrDefault();
            if (coso != null)
            {
                return Ok(coso);
            }
            else
            {
                return BadRequest("cơ sở không tồn tại");
            }
        }


        [HttpGet("laydiachiphongkhambangidphongkham/{idcoso}")]
        public IActionResult laydiachiphongkhambangidphongkham(string idcoso)
        {
            var diachi = (from x in _Context.CoSoDichVuKhacs
                         join d in _Context.XaPhuongs on x.IdxaPhuong equals d.IdxaPhuong
                         join c in _Context.QuanHuyens on d.IdquanHuyen equals c.IdquanHuyen
                         where x.IdcoSoDichVuKhac == idcoso
                         select new
                         {
                             x.DiaChi,
                             d.TenXaPhuong,
                             c.TenQuanHuyen,
                         }).FirstOrDefault();
            if (diachi != null)
            {
                return Ok(diachi);
            }
            else
            {
                return BadRequest("No data");
            }

        }

        [HttpGet("Laythongtincosobangidcoso/{idcoso}")]
        public IActionResult Laythongtincosobangidcoso(string idcoso)
        {
            var cosoyte = _Context.CoSoDichVuKhacs.FirstOrDefault(x => x.IdcoSoDichVuKhac == idcoso);

            if(cosoyte != null)
            {
                return Ok(cosoyte);
            }
            else
            {
                return BadRequest("Cơ sở y tế không tồn tại");
            }
            
        }



        [HttpPost("CheckKeHoach")]
        public IActionResult CheckKeHoach(CreateSchedule check)
        {
            var checkkehoach = _Context.KeHoachNhanVienCoSos
                               .FirstOrDefault(x => x.NgayDatLich == check.NgayDatLich && x.IdnhanVienCoSo == check.IdbacSi);
            if (checkkehoach != null)
            {
                return Ok(checkkehoach.IdkeHoachNhanVienCoSo);
            }
            else
            {
                var taokehoach = new KeHoachNhanVienCoSo
                {
                    IdnhanVienCoSo = check.IdbacSi,
                    NgayDatLich = Convert.ToDateTime(check.NgayDatLich),
                };
                _Context.KeHoachNhanVienCoSos.Add(taokehoach);
                _Context.SaveChanges();
                return Ok(taokehoach.IdkeHoachNhanVienCoSo);
            }
        }

        [HttpPost("ThemChuyenMonChoNhanVien/{idnhanvien}/{idchuyenmoncoso}")]
        public IActionResult ThemChuyenMonChoNhanVien(string idnhanvien,int idchuyenmoncoso) 
            {
            var checkstaff = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == idnhanvien);

          
                if(checkstaff != null)
                {
                    var addchuyenkhoa = new PhanLoaiChuyenKhoaNhanVien
                    {
                        IdnhanVienCoSo = idnhanvien,
                        ChuyenMoncoSo = idchuyenmoncoso,
                    };

                    _Context.PhanLoaiChuyenKhoaNhanViens.Add(addchuyenkhoa);
                    _Context.SaveChanges();
                    return Ok("Success");
                }
                else
                {
                    return BadRequest("Failed");
                }
   
            } 

        [HttpPost("TaoLich/{idkehoach}")]
        public IActionResult TaoLich(CreateSchedule create, int idkehoach)
        {

            var DatLich = new DatLichNhanVienCoSo
            {
                IdkeHoachNhanVienCoSo = idkehoach,
                ThoiGianDatLich = create.ThoiGianDatLich,
                SoLuongToiDa = create.SoLuongToiDa,
            };
            _Context.DatLichNhanVienCoSos.Add(DatLich);
            _Context.SaveChanges();
            return Ok("createdatlich");
        }


        [HttpPost("CheckDatLich")]
        public async Task<IActionResult> CheckDatLich(TaoLich taoLich)
        {
            var CheckDatLich = _Context.TaoLichNhanVienCoSos
                               .FirstOrDefault(x => x.IddatLichNhanVienCoSo == taoLich.IddatLich && x.IdnguoiDungDatLich == taoLich.IdnguoiDungDatLich);
            var datlich = _Context.DatLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == taoLich.IddatLich);
            var nguoidung = _Context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == taoLich.IdnguoiDungDatLich);

            if (CheckDatLich == null && datlich != null && nguoidung != null)
            {
                if (datlich.SoLuongHienTai >= datlich.SoLuongToiDa)
                {
                    return Ok("Đã hết chỗ");
                } 
                else
                {
                    return Ok("Success");
                }
            }
            else
            {
                return BadRequest("Đã book");
            }
        }

        [HttpGet("Laychuyenmoncoso/{idcoso}")]
        public IActionResult Laychuyenmoncoso(string idcoso)
        {
            var chuyenmoncoso = from x in _Context.CoSoDichVuKhacs
                                join q in _Context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals q.IdcoSoDichVuKhac
                                join e in _Context.ChuyenMons on q.IdchuyenMon equals e.IdChuyenMon
                                where x.IdcoSoDichVuKhac == idcoso
                                select e;
            return Ok(chuyenmoncoso);
        }

        [HttpGet("LaydanhsachnhanvienByidcoso/{idcoso}")]
        public IActionResult LaydanhsachnhanvienByidcoso(string idcoso)
        {
            var list = _Context.NhanVienCoSos.Where(x => x.IdcoSoDichVuKhac == idcoso);

            return Ok(list);
        }

        [HttpGet("Laydanhsachnhanvien/{idnhanvien}")]
        public IActionResult Laydanhsachnhanvien(string idnhanvien)
        {
            var staff = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == idnhanvien);
            if(staff != null)
            {
                var liststaff = _Context.NhanVienCoSos.Where(x=>x.IdcoSoDichVuKhac == staff.IdcoSoDichVuKhac).ToList();
                return Ok(liststaff);
            }
            else
            {
                return Ok("nodata");
            }
        }

        [HttpPost("DatLich")]
        public async Task<IActionResult> DatLich(TaoLich taoLich)
        {
            var CheckDatLich = _Context.TaoLichNhanVienCoSos
                               .FirstOrDefault(x => x.IddatLichNhanVienCoSo == taoLich.IddatLich && x.IdnguoiDungDatLich == taoLich.IdnguoiDungDatLich);
            var datlich = _Context.DatLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == taoLich.IddatLich);
            var nguoidung = _Context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == taoLich.IdnguoiDungDatLich);

            if (CheckDatLich == null && datlich != null && nguoidung != null)
            {
                var Newbook = new TaoLichNhanVienCoSo
                {
                    IdnguoiDungDatLich = taoLich.IdnguoiDungDatLich,
                    IddatLichNhanVienCoSo = taoLich.IddatLich,
                    LyDoKham = taoLich.LyDoKham,
                    TrangThaiTaoLich = taoLich.TrangThaiTaoLich,
                };
                _Context.TaoLichNhanVienCoSos.Add(Newbook);
                if (datlich.SoLuongHienTai >= datlich.SoLuongToiDa)
                {
                    return Ok("Đã hết chỗ");
                }
                else
                {
                    datlich.SoLuongHienTai++;
                    _Context.SaveChanges();
                    return Ok("Success");
                }
            }
            else
            {
                return BadRequest("Đã book");
            }
        }

        [HttpPut("ApprovalMedical")]
        public IActionResult ApprovalMedical(int iddatlich)
        {
            var a = _Context.DatLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == iddatlich);
            if (a == null)
            {
                return BadRequest("No Success");
            }
            else
            {
                a.TrangThaiDatLich = 3;
                _Context.SaveChanges();
                return Ok("Success");
            }
        }
        [HttpPut("Cancel")]
        public IActionResult CancelBooking(int iddatlich)
        {
            var a = _Context.DatLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == iddatlich);
            if (a == null)
            {
                return BadRequest("No Success");
            }
            else
            {
                a.TrangThaiDatLich = 0;
                _Context.SaveChanges();
                return Ok("Success");
            }
        }

        [HttpGet("LayTongSoLanKhamCuaBacSi/{idbacsi}")]
        public IActionResult LayTongSoLanKhamCuaBacSi(string idbacsi)
        {
            var solankham = from x in _Context.BacSis
                            join c in _Context.KeHoachKhams on x.IdbacSi equals c.IdbacSi
                            join d in _Context.DatLiches on c.IdkeHoachKham equals d.IdkeHoachKham
                            join q in _Context.TaoLiches on d.IddatLich equals q.IddatLich
                            where x.IdbacSi == idbacsi
                            select q;

            if(solankham.Count() > 0)
            {
                return Ok(solankham.Count());
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpPut("CapNhatMoTa/{idnhanviencoso}")]
        public IActionResult CapNhatMoTa(CapNhatMoTabacSi mota,string idnhanviencoso)
        {
            var checknhanvien = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == idnhanviencoso);
            if(checknhanvien != null)
            {
                checknhanvien.MoTa = mota.mota;
                _Context.SaveChanges();
            }

            return Ok(checknhanvien);
        }

        [HttpPost("EditNhanVienCoSo")]
        public IActionResult EditNhanVienCoSo(NhanVienCoSo nhanvien)
        {
            var checknhanvien = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == nhanvien.IdnhanVienCoSo);

            if(checknhanvien != null)
            {
                checknhanvien.HoTenNhanVien = nhanvien.HoTenNhanVien;
                checknhanvien.AnhNhanVien = nhanvien.AnhNhanVien;
                checknhanvien.AnhChungChiHanhNgheNhanVien = nhanvien.AnhChungChiHanhNgheNhanVien;
                checknhanvien.Cccd = nhanvien.Cccd;
                checknhanvien.SoDienThoaiNhanVienCoSo = nhanvien.SoDienThoaiNhanVienCoSo;
                checknhanvien.EmailNhanVienCoSo = nhanvien.EmailNhanVienCoSo;
                checknhanvien.GiaDatLich = nhanvien.GiaDatLich;
                checknhanvien.Idquyen = nhanvien.Idquyen;
                checknhanvien.GioiTinh = nhanvien.GioiTinh;
                checknhanvien.MatKhau = nhanvien.MatKhau;

                _Context.SaveChanges();
                return Ok("success");
            }
            return BadRequest("Failed");
            
        }

        [HttpGet("LayThongTinNhanVien/{idnhanvien}")]
        public IActionResult LayThongTinNhanVien(string idnhanvien)
        {
            var nhanvien = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == idnhanvien);

            if(nhanvien != null)
            {
                return Ok(nhanvien);
            }
            else
            {
                return BadRequest("failed");
            }
            
        }

        [HttpGet("LayChuyenMonCuaCoSo/{idbacsi}")]
        public IActionResult LayChuyenMonCuaCoSo(string idbacsi)
        {
            var list = from x in _Context.CoSoDichVuKhacs
                       join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                       join d in _Context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals d.IdcoSoDichVuKhac
                       join q in _Context.ChuyenMons on d.IdchuyenMon equals q.IdChuyenMon
                       where c.IdnhanVienCoSo == idbacsi
                       select new { 
                           d.IdchuyenMonCoSo,
                           q.IdChuyenMon,
                           q.TenChuyenMon,
                       };

            return Ok(list);
        }

        [HttpGet("LayChuyenMonCuaMotBacSi/{idbacsi}")]
        public IActionResult LayChuyenKhoaCuaMotBacSi(string idbacsi)
        {
            var ListChuyenMon = from x in _Context.NhanVienCoSos
                                 join c in _Context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                                 join d in _Context.PhanLoaiChuyenKhoaNhanViens on c.IdchuyenMonCoSo equals d.ChuyenMoncoSo
                                 join e in _Context.ChuyenMons on c.IdchuyenMon equals e.IdChuyenMon
                                 where x.IdnhanVienCoSo == idbacsi
                                 select new
                                 {
                                     x.IdnhanVienCoSo,
                                     e.TenChuyenMon,
                                     e.IdChuyenMon,
                                 };
                                
            return Ok(ListChuyenMon);
        }


        [HttpGet("getGiodattLichByNgayDatLich/{ngaydatlich}/{idbacsi}")]
        public IActionResult getGiodattLichByNgayDatLich(DateTime ngaydatlich, string idbacsi)
        {
            var thoigianlichkham = from x in _Context.KeHoachNhanVienCoSos
                                   join c in _Context.DatLichNhanVienCoSos on x.IdkeHoachNhanVienCoSo equals c.IdkeHoachNhanVienCoSo
                                   where x.NgayDatLich == ngaydatlich && x.IdnhanVienCoSo == idbacsi
                                   select new
                                   {
                                       c.ThoiGianDatLich,
                                       c.IddatLichNhanVienCoSo,
                                       c.SoLuongHienTai,
                                       c.SoLuongToiDa,
                                       x.NgayDatLich,
                                       c.TrangThaiDatLich,
                                       x.TrangThaiKeHoachNhanVienCoSo
                                   };

            return Ok(thoigianlichkham);
        }

        [HttpPut("EditKeHoach")]
        public IActionResult EditKeHoach(ThayDoiKeHoach edit)
        {
            var datlich = _Context.DatLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == edit.iddatlich);
            if (datlich != null)
            {
                datlich.ThoiGianDatLich = edit.thoiGianDatLich;
                datlich.SoLuongToiDa = edit.soluongdatlich;
                _Context.SaveChanges();
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
            var Delete = _Context.DatLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == iddatlich);
            if (Delete != null)
            {
                _Context.DatLichNhanVienCoSos.Remove(Delete);
                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("Have User");
            }
        }

        [HttpPost("ThemNhanVien/{idbacsi}")]
        public IActionResult AddDoctor(AddDoctor doctor, string idbacsi)
        {
            var checkEmail = _Context.NhanVienCoSos.FirstOrDefault(x => x.EmailNhanVienCoSo == doctor.EmailBacSi);
            var checkSDT = _Context.NhanVienCoSos.FirstOrDefault(x => x.SoDienThoaiNhanVienCoSo == doctor.SoDienThoaiBacSi);
            var checkCCCD = _Context.NhanVienCoSos.FirstOrDefault(x => x.Cccd == doctor.Cccd);
            string messemail = ""; string messSDT = ""; string messCCCD = "";
            string mess = "";
            var ramdom = new Ramdom.Ramdom();

            if (checkEmail != null)
            {
                messemail = "đã có email ";
            }
            if (checkSDT != null)
            {
                messSDT = "Đã có số điện thoại ";
            }
            if (checkCCCD != null)
            {
                messCCCD = "Đã có căn cước công dân";
            }

            mess += messemail + messSDT + messCCCD;

            if (mess.Length != 0)
            {
                return BadRequest(mess);
            }
            // Xu ly ID Bac Si
            var IDBacSihientai = idbacsi;

            string IDPhongKham = IDBacSihientai.Substring(0, 4);
            var IDBacSi = from a in _Context.CoSoDichVuKhacs
                          join b in _Context.NhanVienCoSos on a.IdcoSoDichVuKhac equals b.IdcoSoDichVuKhac
                          where b.IdcoSoDichVuKhac == IDPhongKham
                          select b;
            var IDBacSi2 = IDBacSi.OrderBy(idbacsi => idbacsi).Last().IdnhanVienCoSo;
            int numberPlus = Convert.ToInt32(IDBacSi2.Substring(3));
            string TypeID = IDBacSi2.ToString().Substring(0, 3);
            numberPlus++;
            string ID = TypeID + numberPlus;
            // Xu ly TaiKhoan Cua BacSi
            int indexName = doctor.EmailBacSi.IndexOf("@");
            string nameDoctor = doctor.EmailBacSi.Substring(0, indexName);
            string TaiKhoanDoctor = nameDoctor + ID;

            var bacsi = new NhanVienCoSo
            {
                IdnhanVienCoSo = ID,
                IdcoSoDichVuKhac = IDPhongKham,
                TaiKhoan = TaiKhoanDoctor,
                MatKhau = ramdom.GetPassword(),
                HoTenNhanVien = doctor.HoTenBacSi,
                Cccd = doctor.Cccd,
                SoDienThoaiNhanVienCoSo = doctor.SoDienThoaiBacSi,
                EmailNhanVienCoSo = doctor.EmailBacSi,
                Idquyen = doctor.Idquyen,
                GiaDatLich = doctor.GiaKham,
                AnhNhanVien = ID + doctor.AnhBacSi,
                AnhChungChiHanhNgheNhanVien = ID + doctor.AnhChungChiHanhNgheBacSi,
                GioiTinh = doctor.GioiTinh,
            };
            _Context.NhanVienCoSos.Add(bacsi);
            _Context.SaveChanges();
            return Ok(bacsi.IdnhanVienCoSo);
        }

        [HttpGet("Laychuyenmoncuanhanvien/{idnhanvien}")]
        public IActionResult Laychuyenmoncuanhanvien(string idnhanvien)
        {
            var nhanvien = _Context.NhanVienCoSos.Where(x => x.IdnhanVienCoSo == idnhanvien).FirstOrDefault();
            if(nhanvien != null)
            {
                var chuyenmonnhanvien = (from x in _Context.NhanVienCoSos
                                        join c in _Context.PhanLoaiChuyenKhoaNhanViens on x.IdnhanVienCoSo equals c.IdnhanVienCoSo
                                        join z in _Context.ChuyenMoncoSos on c.ChuyenMoncoSo equals z.IdchuyenMonCoSo
                                        join d in _Context.ChuyenMons on z.IdchuyenMon equals d.IdChuyenMon
                                        where x.IdnhanVienCoSo == idnhanvien
                                        select new {
                                            d.IdChuyenMon,
                                            d.TenChuyenMon,
                                            x.IdnhanVienCoSo,
                                        }).FirstOrDefault();
                if(chuyenmonnhanvien != null)
                {
                    return Ok(chuyenmonnhanvien);
                }
                else
                {
                    return BadRequest("failed");
                }
            }
            
            var chuyenmon = from x in _Context.NhanVienCoSos
                            join c in _Context.PhanLoaiChuyenKhoaNhanViens on x.IdnhanVienCoSo equals c.IdnhanVienCoSo
                            join d in _Context.ChuyenMons on c.ChuyenMoncoSo equals d.IdChuyenMon
                            where x.IdnhanVienCoSo == idnhanvien
                            select new
                            {
                                x.IdnhanVienCoSo,
                                d.TenChuyenMon,
                                d.IdChuyenMon,
                            };
            return Ok(chuyenmon);
        }


        [HttpPut("themchuyenmonchonhanvien/{idbacsi}")]
        public IActionResult themchucdanhbacsi(string idbacsi, List<PhanLoaiChuyenKhoaNhanVien> phanloai)
        {
            var listchuyenkhoa = _Context.PhanLoaiChuyenKhoaNhanViens.Where(x => x.IdnhanVienCoSo == idbacsi);
            if (listchuyenkhoa != null)
            {
                _Context.PhanLoaiChuyenKhoaNhanViens.RemoveRange(listchuyenkhoa);
                var list = new List<PhanLoaiChuyenKhoaNhanVien>();
                phanloai.ForEach(ele =>
                {
                    list.Add(
                      new PhanLoaiChuyenKhoaNhanVien() { IdnhanVienCoSo = idbacsi, ChuyenMoncoSo = ele.ChuyenMoncoSo }
                    );
                });
                _Context.PhanLoaiChuyenKhoaNhanViens.AddRange(list);
                _Context.SaveChanges();
                return Ok(list);
            }
            else
            {
                var list = new List<PhanLoaiChuyenKhoaNhanVien>();
                phanloai.ForEach(ele =>
                {
                    list.Add(
                      new PhanLoaiChuyenKhoaNhanVien() { IdnhanVienCoSo = idbacsi, ChuyenMoncoSo = ele.ChuyenMoncoSo }
                    );
                });
                _Context.PhanLoaiChuyenKhoaNhanViens.AddRange(list);
                _Context.SaveChanges();
                return Ok(list);
            }
        }


        [HttpPut("EditDoctor")]
        public IActionResult EditDoctor(EditDoctor edit)
        {
            var doctor = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == edit.idbacSi);
            var checkEmail = _Context.NhanVienCoSos.FirstOrDefault(x => x.EmailNhanVienCoSo == edit.EmailBacSi);
            var checkSDT = _Context.NhanVienCoSos.FirstOrDefault(x => x.SoDienThoaiNhanVienCoSo == edit.SoDienThoaiBacSi);
            var checkCCCD = _Context.NhanVienCoSos.FirstOrDefault(x => x.Cccd == edit.Cccd);
            string messemail = ""; string messSDT = ""; string messCCCD = "";
            string mess = "";

            if (checkEmail != null && checkEmail.EmailNhanVienCoSo != edit.EmailBacSi)
            {
                messemail = "đã có email ";
            }
            if (checkSDT != null && checkSDT.SoDienThoaiNhanVienCoSo != edit.SoDienThoaiBacSi)
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
                    doctor.HoTenNhanVien = edit.HoTenBacSi;
                    doctor.Cccd = edit.Cccd;
                    doctor.EmailNhanVienCoSo = edit.EmailBacSi;
                    doctor.SoDienThoaiNhanVienCoSo = edit.SoDienThoaiBacSi;
                    doctor.Idquyen = edit.Idquyen;
                    doctor.GiaDatLich = edit.GiaKham;
                    doctor.GioiTinh = edit.GioiTinh;
                    doctor.AnhNhanVien = edit.AnhBacSi;
                    doctor.AnhChungChiHanhNgheNhanVien = edit.AnhChungChiHanhNgheBacSi;
                    _Context.SaveChanges();
                    return Ok("success");
                }
            }
            return BadRequest("Failed");
        }

        [HttpPut("XoaNhanVien/{idnhanvien}")]
        public IActionResult XoaBacSi(string idnhanvien)
        {
            var nhanvien = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == idnhanvien);

            if(nhanvien != null)
            {
                nhanvien.TrangThai = false;
                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }
        [HttpPut("KhoiPhucNhanVien/{idnhanvien}")]
        public IActionResult KhoiPhucNhanVien(string idnhanvien)
        {
            var nhanvien = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == idnhanvien);

            if (nhanvien != null)
            {
                nhanvien.TrangThai = true;
                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }
        [HttpGet("LayDanhSachLichDangChoDuyet/{idbacsi}")]
        public IActionResult LayDanhSachLichDangChoDuyet(string idbacsi)
        {
            var a = from c in _Context.KeHoachNhanVienCoSos
                    join d in _Context.DatLichNhanVienCoSos on c.IdkeHoachNhanVienCoSo equals d.IdkeHoachNhanVienCoSo
                    join e in _Context.TaoLichNhanVienCoSos on d.IddatLichNhanVienCoSo equals e.IddatLichNhanVienCoSo
                    join q in _Context.NguoiDungs on e.IdnguoiDungDatLich equals q.IdNguoiDung
                    where c.IdnhanVienCoSo == idbacsi && e.TrangThaiTaoLich == 1
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
                        d.IddatLichNhanVienCoSo,
                        e.NgayGioDatLich,
                    };
            return Ok(a);
        }

        [HttpPost("DuyetLich")]
        public IActionResult XacNhanDatLich(DuyetLich duyetlich)
        {
            var a = _Context.TaoLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            if (a != null)
            {
                a.TrangThaiTaoLich = 2;
                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }

        }

        [HttpPost("Taikham")]
        public IActionResult Taikham(DuyetLich duyetlich)
        {
            var a = _Context.TaoLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            if (a != null)
            {
                a.TrangThaiTaoLich = 4;
                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("Failed");
            }

        }



        [HttpPost("HuyLichchokham")]
        public IActionResult HuyLichchokham(DuyetLich duyetlich)
        {
            var a = _Context.TaoLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            var datlich = _Context.DatLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == duyetlich.IDDatLich);
            if (a != null && datlich != null)
            {
                _Context.TaoLichNhanVienCoSos.Remove(a);
                datlich.SoLuongHienTai--;
                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }

        [HttpPost("HuyLich")]
        public IActionResult HuyLich(DuyetLich duyetlich)
        {
            var a = _Context.TaoLichNhanVienCoSos.FirstOrDefault(x => x.IddatLichNhanVienCoSo == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            var nguoidung = _Context.NguoiDungs.FirstOrDefault(x => x.IdNguoiDung == duyetlich.IDNguoiDung);
            if (a != null && nguoidung != null)
            {
                a.TrangThaiTaoLich = 0;
                nguoidung.HuyLich++;
                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }


        [HttpPut("Capnhatthongtincoso")]
        public IActionResult Capnhatthongtinphongkham(CapNhatThongTinPhongKham phongkham)
        {
            var checkphongkham = _Context.CoSoDichVuKhacs.FirstOrDefault(x => x.IdcoSoDichVuKhac == phongkham.IdphongKham);

            if (checkphongkham != null)
            {
                checkphongkham.TenCoSo = phongkham.TenPhongKham;
                checkphongkham.AnhDaiDienCoSo = phongkham.AnhDaiDienPhongKham;
                checkphongkham.HinhAnhCoSo = phongkham.HinhAnh;
                checkphongkham.DiaChi = phongkham.DiaChi;
                checkphongkham.IdxaPhuong = phongkham.IdxaPhuong;

                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return BadRequest("failed");
            }
        }

        [HttpPut("Capnhatmotacoso")]
        public IActionResult Capnhatmotaphongkham(CapNhatMoTaPhongKham mota)
        {
            var checkphongkham = _Context.CoSoDichVuKhacs.FirstOrDefault(x => x.IdcoSoDichVuKhac == mota.idphongkham);
            if (checkphongkham != null)
            {
                checkphongkham.LoiGioiThieu = mota.loigoithieu;
                checkphongkham.ChuyenMon = mota.chuyenmon;
                checkphongkham.TrangThietBi = mota.trangthietbi;
                checkphongkham.ViTri = mota.vitri;
                _Context.SaveChanges();
                return Ok("Success");
            };
            return BadRequest("Not found");
        }

        [HttpGet("laychuyenkhoaphongkhambyidnhanvien/{idnhanvien}")]
        public IActionResult laychuyenkhoaphongkham(string idnhanvien)
        {
            var chuyenmoncoso = from x in _Context.CoSoDichVuKhacs
                                join q in _Context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals q.IdcoSoDichVuKhac
                                join e in _Context.ChuyenMons on q.IdchuyenMon equals e.IdChuyenMon
                                join k in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals k.IdcoSoDichVuKhac
                                where k.IdnhanVienCoSo == idnhanvien
                                select e;
            return Ok(chuyenmoncoso);
        }

        [HttpPost("XacNhanKham")]
        public IActionResult XacNhanKham(DuyetLich duyetlich)
        {
            var a = _Context.TaoLichNhanVienCoSos.FirstOrDefault(x => x.IdtaoLichNhanVienCoSo == duyetlich.IDDatLich && x.IdnguoiDungDatLich == duyetlich.IDNguoiDung);
            var giakham = (from x in _Context.NhanVienCoSos
                           join c in _Context.KeHoachNhanVienCoSos on x.IdnhanVienCoSo equals c.IdnhanVienCoSo
                           join d in _Context.DatLichNhanVienCoSos on c.IdkeHoachNhanVienCoSo equals d.IdkeHoachNhanVienCoSo
                           where d.IddatLichNhanVienCoSo == duyetlich.IDDatLich
                           select x).FirstOrDefault();
            var checkcoso = (from x in _Context.NhanVienCoSos
                           join c in _Context.KeHoachNhanVienCoSos on x.IdnhanVienCoSo equals c.IdnhanVienCoSo
                           join d in _Context.DatLichNhanVienCoSos on c.IdkeHoachNhanVienCoSo equals d.IdkeHoachNhanVienCoSo
                           join j in _Context.CoSoDichVuKhacs on x.IdcoSoDichVuKhac equals j.IdcoSoDichVuKhac
                           where d.IddatLichNhanVienCoSo == duyetlich.IDDatLich
                           select j).FirstOrDefault();

            if (a != null && giakham != null && checkcoso != null)
            {
                a.TrangThaiTaoLich = 3;
                a.GiaKham = Convert.ToDouble(giakham.GiaDatLich);
                giakham.Solandatlich++;
                checkcoso.Solandatlich++;
                _Context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }

        [HttpGet("Laylichtrongcoso/{nhanviencoso}")]
        public IActionResult Laylichtrongcoso(string nhanviencoso)
        {
            var List = (from a in _Context.DatLichNhanVienCoSos
                        join c in _Context.KeHoachNhanVienCoSos on a.IdkeHoachNhanVienCoSo equals c.IdkeHoachNhanVienCoSo
                        join d in _Context.TaoLichNhanVienCoSos on a.IddatLichNhanVienCoSo equals d.IddatLichNhanVienCoSo into dept
                        from k in dept.DefaultIfEmpty()
                        select new
                        {
                            c.IdnhanVienCoSo,
                            c.NgayDatLich,
                            c.TrangThaiKeHoachNhanVienCoSo,
                            a.ThoiGianDatLich,
                            a.IddatLichNhanVienCoSo,
                            a.SoLuongToiDa,
                            a.SoLuongHienTai,
                            a.TrangThaiDatLich,
                            LyDoKham = k.LyDoKham != null ? k.LyDoKham : null,
                            IdtaoLich = k.IdtaoLichNhanVienCoSo != null ? k.IdtaoLichNhanVienCoSo : 0,
                            User = k.IdnguoiDungDatLichNavigation != null ? k.IdnguoiDungDatLichNavigation : null,
                            TrangThaiTaoLich = k.TrangThaiTaoLich != null ? k.TrangThaiTaoLich : null,
                        }).Where(c => c.IdnhanVienCoSo == nhanviencoso && c.TrangThaiTaoLich == null && c.SoLuongToiDa > c.SoLuongHienTai).OrderBy(c => c.NgayDatLich);


            return Ok(List);
        }

        [HttpGet("CheckTaiKham/{idnguoidung}/{idbacsi}")]
        public IActionResult CheckTaiKham(int idnguoidung,string idbacsi)
        {
            DateTime a = DateTime.Now;

            var check = (from x in _Context.NguoiDungs
                        join c in _Context.TaoLichNhanVienCoSos on x.IdNguoiDung equals c.IdnguoiDungDatLich
                        join d in _Context.DatLichNhanVienCoSos on c.IddatLichNhanVienCoSo equals d.IddatLichNhanVienCoSo
                        join z in _Context.KeHoachNhanVienCoSos on d.IdkeHoachNhanVienCoSo equals z.IdkeHoachNhanVienCoSo
                        where z.IdnhanVienCoSo == idbacsi && c.IdnguoiDungDatLich == idnguoidung && c.TrangThaiTaoLich == 2 || c.TrangThaiTaoLich == 4
                         select c).FirstOrDefault();
            if(check == null)
            {
                return Ok("success");
            }
            else
            {
                return BadRequest("Đã có lịch tái khám");
            }
        }

        [HttpGet("layidcosobangidnhanvien/{idnhanvien}")]
        public IActionResult layidcosobangidnhanvien(string idnhanvien)
        {
            var idphongkham = (from x in _Context.CoSoDichVuKhacs
                              join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                              where c.IdnhanVienCoSo == idnhanvien
                              select x.IdcoSoDichVuKhac).FirstOrDefault();

            if(idphongkham != null)
            {
                return Ok(idphongkham);
            }
            else
            {
                return BadRequest("Cơ sở không tồn tại");
            }
           
        }

        [HttpGet("Statisticalotherfacitilies/{idphongkham}")]
        public IActionResult StatisticalClinic(string idphongkham)
        {
            var danhsach = from x in _Context.CoSoDichVuKhacs
                           join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                           join q in _Context.KeHoachNhanVienCoSos on c.IdnhanVienCoSo equals q.IdnhanVienCoSo
                           join w in _Context.DatLichNhanVienCoSos on q.IdkeHoachNhanVienCoSo equals w.IdkeHoachNhanVienCoSo
                           join y in _Context.TaoLichNhanVienCoSos on w.IddatLichNhanVienCoSo equals y.IddatLichNhanVienCoSo
                           join k in _Context.NguoiDungs on y.IdnguoiDungDatLich equals k.IdNguoiDung
                           where x.IdcoSoDichVuKhac == idphongkham && y.TrangThaiTaoLich == 3
                           select new
                           {
                               c.HoTenNhanVien,
                               y.GiaKham,
                               c.IdnhanVienCoSo,
                               q.NgayDatLich,
                               w.ThoiGianDatLich,
                               k.HoNguoiDung,
                               k.TenNguoiDung,
                           };

            return Ok(danhsach);
        }


        [HttpGet("LayDanhSachLichdakham/{idbacsi}")]
        public IActionResult LayDanhSachLichdakham(string idbacsi)
        {
            var a = from c in _Context.KeHoachNhanVienCoSos
                    join d in _Context.DatLichNhanVienCoSos on c.IdkeHoachNhanVienCoSo equals d.IdkeHoachNhanVienCoSo
                    join e in _Context.TaoLichNhanVienCoSos on d.IddatLichNhanVienCoSo equals e.IddatLichNhanVienCoSo
                    join q in _Context.NguoiDungs on e.IdnguoiDungDatLich equals q.IdNguoiDung
                    where c.IdnhanVienCoSo == idbacsi && e.TrangThaiTaoLich != 1 && e.TrangThaiTaoLich !=2
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
                        d.IddatLichNhanVienCoSo,
                        e.NgayGioDatLich,
                    };
            return Ok(a);
        }


        [HttpGet("Laydanhsachcosotuongtu/{idphongkham}/{idchuyenkhoa}")]
        public IActionResult Laydanhsachphongkhamtuongtu(string idphongkham, int idchuyenkhoa)
        {
            var list = from x in _Context.CoSoDichVuKhacs
                       join c in _Context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                       where x.IdcoSoDichVuKhac != idphongkham && c.IdchuyenMon == idchuyenkhoa
                       select x;


            return Ok(list);
        }

        [HttpGet("Laydanhsachchuyenvientuongtu/{idnhanvien}/{idchuyenkhoa}")]
        public IActionResult Laydanhsachchuyenvientuongtu(string idnhanvien, int idchuyenkhoa)
        {
            var list = from x in _Context.NhanVienCoSos
                       join d in _Context.PhanLoaiChuyenKhoaNhanViens on x.IdnhanVienCoSo equals d.IdnhanVienCoSo
                       join q in _Context.ChuyenMoncoSos on d.ChuyenMoncoSo equals q.IdchuyenMonCoSo
                       join k in _Context.ChuyenMons on q.IdchuyenMon equals k.IdChuyenMon
                       where x.IdnhanVienCoSo != idnhanvien && k.IdChuyenMon == idchuyenkhoa
                       select x;


            return Ok(list);
        }

        [HttpGet("ThongKeDanhGiaNguoiDung/{idbacsi}")]
        public IActionResult ThongKeDanhGiaNguoiDung(string idbacsi)
        {
            var idphongkham = (from x in _Context.CoSoDichVuKhacs
                               join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                               where c.IdnhanVienCoSo == idbacsi
                               select x.IdcoSoDichVuKhac).FirstOrDefault();

            var list = from x in _Context.CoSoDichVuKhacs
                       join c in _Context.DanhGiaCosos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                       join d in _Context.NguoiDungs on c.Idnguoidanhgia equals d.IdNguoiDung
                       where x.IdcoSoDichVuKhac == idphongkham
                       select new
                       {
                           d,
                           x.Solandatlich,
                           x.Danhgia,
                           c.NhanXet,
                       };
            return Ok(list);
        }


        [HttpGet("Laydanhsachselect/{idbacsi}")]
        public IActionResult Laydanhsachselect(string idbacsi)
        {
            var idphongkham = (from x in _Context.CoSoDichVuKhacs
                               join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                               where c.IdnhanVienCoSo == idbacsi
                               select x.IdcoSoDichVuKhac).FirstOrDefault();

            var list = (from x in _Context.CoSoDichVuKhacs
                        join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                        join d in _Context.KeHoachNhanVienCoSos on c.IdnhanVienCoSo equals d.IdnhanVienCoSo
                        join q in _Context.DatLichNhanVienCoSos on d.IdkeHoachNhanVienCoSo equals q.IdkeHoachNhanVienCoSo
                        join v in _Context.TaoLichNhanVienCoSos on q.IddatLichNhanVienCoSo equals v.IddatLichNhanVienCoSo
                        where x.IdcoSoDichVuKhac == idphongkham
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
            var idphongkham = (from x in _Context.CoSoDichVuKhacs
                               join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                               where c.IdnhanVienCoSo == idbacsi
                               select x.IdcoSoDichVuKhac).FirstOrDefault();

            var list = (from x in _Context.CoSoDichVuKhacs
                        join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                        join d in _Context.KeHoachNhanVienCoSos on c.IdnhanVienCoSo equals d.IdnhanVienCoSo
                        select new
                        {
                            d.NgayDatLich.Year
                        }).Distinct();

            return Ok(list);
        }


        [HttpGet("LayDanhSachDashBoard/{idbacsi}")]
        public IActionResult LayDanhSachDashBoard(string idbacsi)
        {
            var idphongkham = (from x in _Context.CoSoDichVuKhacs
                               join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                               where c.IdnhanVienCoSo == idbacsi
                               select x.IdcoSoDichVuKhac).FirstOrDefault();

            var list = from x in _Context.CoSoDichVuKhacs
                        join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                        join d in _Context.KeHoachNhanVienCoSos on c.IdnhanVienCoSo equals d.IdnhanVienCoSo
                        join q in _Context.DatLichNhanVienCoSos on d.IdkeHoachNhanVienCoSo equals q.IdkeHoachNhanVienCoSo
                        join v in _Context.TaoLichNhanVienCoSos on q.IddatLichNhanVienCoSo equals v.IddatLichNhanVienCoSo
                        where x.IdcoSoDichVuKhac == idphongkham
                        select new
                        {
                            v,
                           d.NgayDatLich
                       };
            return Ok(list);
        }

        [HttpPost("LayDanhSachDashBoardselect/{idbacsi}")]
        public IActionResult LayDanhSachDashBoard(string idbacsi, ngaythanglaythongke ngaythang)
        {
            var idphongkham = (from x in _Context.CoSoDichVuKhacs
                               join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                               where c.IdnhanVienCoSo == idbacsi
                               select x.IdcoSoDichVuKhac).FirstOrDefault();

            var list = from x in _Context.CoSoDichVuKhacs
                       join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                       join d in _Context.KeHoachNhanVienCoSos on c.IdnhanVienCoSo equals d.IdnhanVienCoSo
                       join q in _Context.DatLichNhanVienCoSos on d.IdkeHoachNhanVienCoSo equals q.IdkeHoachNhanVienCoSo
                       join v in _Context.TaoLichNhanVienCoSos on q.IddatLichNhanVienCoSo equals v.IddatLichNhanVienCoSo
                       where x.IdcoSoDichVuKhac == idphongkham
                       select new
                       {
                           v,
                           d.NgayDatLich
                       };

            return Ok(list);
        }

        [HttpGet("Laynguoidungdanhgiaphongkham/{idphongkham}")]
        public IActionResult Laynguoidungdanhgiaphongkham(string idphongkham)
        {
            var list = from x in _Context.DanhGiaCosos
                       join d in _Context.NguoiDungs on x.Idnguoidanhgia equals d.IdNguoiDung
                       where x.IdcoSoDichVuKhac == idphongkham
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
            var list = from x in _Context.DanhGiaCosos
                       join d in _Context.NguoiDungs on x.Idnguoidanhgia equals d.IdNguoiDung
                       join z in _Context.NhanVienCoSos on x.IdnhanVienCoSo equals z.IdnhanVienCoSo
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


        [HttpGet("ThongKeHoatDongCacBacSiCuaMotPhongKham/{idbacsi}")]
        public IActionResult ThongKeHoatDongCacBacSiCuaMotPhongKham(string idbacsi)
        {
            var idphongkham = (from x in _Context.CoSoDichVuKhacs
                               join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                               where c.IdnhanVienCoSo == idbacsi
                               select x.IdcoSoDichVuKhac).FirstOrDefault();

            var list = from x in _Context.CoSoDichVuKhacs
                       join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                       where x.IdcoSoDichVuKhac == idphongkham
                       select new
                       {
                           c.IdnhanVienCoSo,
                           c.HoTenNhanVien,
                           c.DanhGiaCosos,
                           c.Danhgia,
                           c.Solandatlich,
                       };
            return Ok(list);
        }
    }
}
