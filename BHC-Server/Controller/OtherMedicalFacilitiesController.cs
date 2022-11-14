using BHC_Server.Models;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("ThemChuyenMonChoNhanVien/{idnhanvien}/{idcoso}")]
        public IActionResult ThemChuyenMonChoNhanVien(string idnhanvien,string idcoso) 
            {
            var checkstaff = _Context.NhanVienCoSos.FirstOrDefault(x => x.IdnhanVienCoSo == idnhanvien);

            var idchuyenmoncoso = (from x in _Context.CoSoDichVuKhacs
                                   join d in _Context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals d.IdcoSoDichVuKhac
                                   where x.IdcoSoDichVuKhac == idcoso
                                   select d.IdchuyenMonCoSo).FirstOrDefault();

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

        [HttpGet("LayChuyenMonCuaCoSo/{idbacsi}")]
        public IActionResult LayChuyenMonCuaCoSo(string idbacsi)
        {
            var list = from x in _Context.CoSoDichVuKhacs
                       join c in _Context.NhanVienCoSos on x.IdcoSoDichVuKhac equals c.IdcoSoDichVuKhac
                       join d in _Context.ChuyenMoncoSos on x.IdcoSoDichVuKhac equals d.IdcoSoDichVuKhac
                       join q in _Context.ChuyenMons on d.IdchuyenMon equals q.IdChuyenMon
                       where c.IdnhanVienCoSo == idbacsi
                       select q;

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
            return Ok(bacsi.IdcoSoDichVuKhac);
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
    }
}
