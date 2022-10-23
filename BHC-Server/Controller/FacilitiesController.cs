﻿using BHC_Server.Models;
using Microsoft.AspNetCore.Mvc;

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
                                              where xacThuc.TrangThaiXacThuc == 0
                                              select new
                                              {
                                                  xacThuc,
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
            if (SelectAllRegisterFacilities.Count() > 0)
            {
                return Ok(SelectAllRegisterFacilities);
            }
            return BadRequest("No Data");
        }


        [HttpPost]
        public IActionResult RegisterFacitilies(RegisterFacitilies dangky)
        {

            var phongkham = _context.XacThucDangKyMoCoSoYtes.FirstOrDefault(x => x.IdnguoiDung == dangky.IdnguoiDung && x.LoaiHinhDangKy == 1);
            var nhathuoc = _context.XacThucDangKyMoCoSoYtes.FirstOrDefault(x => x.IdnguoiDung == dangky.IdnguoiDung && x.LoaiHinhDangKy == 2);
            
            if (phongkham != null)
            {
                if(nhathuoc != null)
                {
                    return BadRequest("Đã đăng ký tất cả cơ sở");
                }
                if(nhathuoc == null)
                {
                    if(dangky.LoaiHinhDangKy == 1)
                    {
                       return Ok("Đã có phòng khám");
                    }

                    if(dangky.LoaiHinhDangKy == 2)
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
                }
            }
            else
            {
                if (dangky.LoaiHinhDangKy == 1)
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
                    return Ok("Register Clinic Success");
                }
                if (nhathuoc != null)
                {
                    if(dangky.LoaiHinhDangKy == 2)
                    {
                        return Ok("Đã có nhà thuốc");
                    }
                }
            }        
            return BadRequest("Failed");
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
            var CheckNhaThuoc = from xacThuc in _context.XacThucDangKyMoCoSoYtes
                                 join x in _context.NguoiDungs on xacThuc.IdnguoiDung equals x.IdNguoiDung
                                 where xacThuc.TrangThaiXacThuc == 1 && xacThuc.LoaiHinhDangKy == 2 && xacThuc.IdnguoiDung == Xacthuccoso.IdnguoiDung
                                select new
                                 {
                                     xacThuc,
                                 };

            if (CheckPhongKham.Count() > 0 && Xacthuccoso.LoaiHinhDangKy == 1)
            {
                return BadRequest(CheckPhongKham);
            }

            if(CheckNhaThuoc.Count() > 0 && Xacthuccoso.LoaiHinhDangKy == 1)
            {
                return BadRequest(CheckNhaThuoc);
            }

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
                    IDCoSo = "DDK" + Xacthuccoso.IdnguoiDung;
                    IDNhanVienCoSo = IDCoSo + "1";
                }

                XacThuc.NgayXetDuyet = DateTime.Now;
                XacThuc.TrangThaiXacThuc = 1;
                XacThuc.IdquanTriVien = idquantrivien;

                if(Xacthuccoso.LoaiHinhDangKy == 1)
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
                        HoTenBacSi = NguoiDung.TenNguoiDung,
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
                    _context.BacSis.Add(BacSi2);
                    _context.PhongKhams.Add(PhongKham2);
                    NguoiDung.TrangThaiPhongKham = true;
                }
               
                if(Xacthuccoso.LoaiHinhDangKy == 2)
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
                    _context.NhaThuocs.Add(NhaThuoc2);
                    _context.NhanVienNhaThuocs.Add(NhanVienNhaThuoc2);
                }
            }
            _context.SaveChanges();
            return Ok("Success");
        }
    }
}