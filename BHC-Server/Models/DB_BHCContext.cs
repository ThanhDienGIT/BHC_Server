using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BHC_Server.Models
{
    public partial class DB_BHCContext : DbContext
    {
        public DB_BHCContext()
        {
        }

        public DB_BHCContext(DbContextOptions<DB_BHCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BacSi> BacSis { get; set; } = null!;
        public virtual DbSet<CacChuyenKhoaChuyenMonDangKy> CacChuyenKhoaChuyenMonDangKies { get; set; } = null!;
        public virtual DbSet<ChiTietDatHang> ChiTietDatHangs { get; set; } = null!;
        public virtual DbSet<ChucDanh> ChucDanhs { get; set; } = null!;
        public virtual DbSet<ChucDanhBacSi> ChucDanhBacSis { get; set; } = null!;
        public virtual DbSet<ChuyenKhoa> ChuyenKhoas { get; set; } = null!;
        public virtual DbSet<ChuyenKhoaPhongKham> ChuyenKhoaPhongKhams { get; set; } = null!;
        public virtual DbSet<ChuyenMon> ChuyenMons { get; set; } = null!;
        public virtual DbSet<ChuyenMoncoSo> ChuyenMoncoSos { get; set; } = null!;
        public virtual DbSet<CoSoDichVuKhac> CoSoDichVuKhacs { get; set; } = null!;
        public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; } = null!;
        public virtual DbSet<DatLich> DatLiches { get; set; } = null!;
        public virtual DbSet<DatLichNhanVienCoSo> DatLichNhanVienCoSos { get; set; } = null!;
        public virtual DbSet<DonHang> DonHangs { get; set; } = null!;
        public virtual DbSet<DonViBan> DonViBans { get; set; } = null!;
        public virtual DbSet<KeHoachKham> KeHoachKhams { get; set; } = null!;
        public virtual DbSet<KeHoachNhanVienCoSo> KeHoachNhanVienCoSos { get; set; } = null!;
        public virtual DbSet<LichHen> LichHens { get; set; } = null!;
        public virtual DbSet<LoaiHinhDichVu> LoaiHinhDichVus { get; set; } = null!;
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<NhaThuoc> NhaThuocs { get; set; } = null!;
        public virtual DbSet<NhanVienCoSo> NhanVienCoSos { get; set; } = null!;
        public virtual DbSet<NhanVienNhaThuoc> NhanVienNhaThuocs { get; set; } = null!;
        public virtual DbSet<PhanLoaiBacSiChuyenKhoa> PhanLoaiBacSiChuyenKhoas { get; set; } = null!;
        public virtual DbSet<PhanLoaiChuyenKhoaNhanVien> PhanLoaiChuyenKhoaNhanViens { get; set; } = null!;
        public virtual DbSet<PhongKham> PhongKhams { get; set; } = null!;
        public virtual DbSet<QuanHuyen> QuanHuyens { get; set; } = null!;
        public virtual DbSet<QuanTriVien> QuanTriViens { get; set; } = null!;
        public virtual DbSet<Quyen> Quyens { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;
        public virtual DbSet<TaoLich> TaoLiches { get; set; } = null!;
        public virtual DbSet<TaoLichNhanVienCoSo> TaoLichNhanVienCoSos { get; set; } = null!;
        public virtual DbSet<XaPhuong> XaPhuongs { get; set; } = null!;
        public virtual DbSet<XacThucDangKyMoCoSoYte> XacThucDangKyMoCoSoYtes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BacSi>(entity =>
            {
                entity.HasKey(e => e.IdbacSi)
                    .HasName("PK__BacSi__32D03F8477F2265A");

                entity.ToTable("BacSi");

                entity.HasIndex(e => e.SoDienThoaiBacSi, "UQ__BacSi__2C67339DAE711D7B")
                    .IsUnique();

                entity.HasIndex(e => e.Cccd, "UQ__BacSi__A955A0AA34FF1338")
                    .IsUnique();

                entity.HasIndex(e => e.EmailBacSi, "UQ__BacSi__A9B055F19C99C5B4")
                    .IsUnique();

                entity.Property(e => e.IdbacSi)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDBacSi");

                entity.Property(e => e.AnhBacSi).HasColumnType("text");

                entity.Property(e => e.AnhChungChiHanhNgheBacSi).HasColumnType("text");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CCCD")
                    .IsFixedLength();

                entity.Property(e => e.EmailBacSi)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GiaKham)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.HoTenBacSi).HasMaxLength(30);

                entity.Property(e => e.Idnguoidung).HasColumnName("IDNguoidung");

                entity.Property(e => e.IdphongKham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDPhongKham");

                entity.Property(e => e.Idquyen).HasColumnName("IDQuyen");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.SoDienThoaiBacSi)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdnguoidungNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.Idnguoidung)
                    .HasConstraintName("FK__BacSi__IDNguoidu__6477ECF3");

                entity.HasOne(d => d.IdphongKhamNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.IdphongKham)
                    .HasConstraintName("FK__BacSi__IDPhongKh__619B8048");

                entity.HasOne(d => d.IdquyenNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.Idquyen)
                    .HasConstraintName("FK__BacSi__IDQuyen__628FA481");
            });

            modelBuilder.Entity<CacChuyenKhoaChuyenMonDangKy>(entity =>
            {
                entity.HasKey(e => e.IdCacChuyenKhoaChuyenMonDangKy)
                    .HasName("PK__CacChuye__A93C4F99C74F109F");

                entity.ToTable("CacChuyenKhoaChuyenMonDangKy");

                entity.Property(e => e.IdCacChuyenKhoaChuyenMonDangKy).HasColumnName("idCacChuyenKhoaChuyenMonDangKy");

                entity.Property(e => e.Idchuyenmon).HasColumnName("idchuyenmon");

                entity.Property(e => e.Idcoso)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idcoso");
            });

            modelBuilder.Entity<ChiTietDatHang>(entity =>
            {
                entity.HasKey(e => e.IdchiTietDatHang)
                    .HasName("PK__ChiTietD__279D92CAE615443F");

                entity.ToTable("ChiTietDatHang");

                entity.Property(e => e.IdchiTietDatHang).HasColumnName("IDChiTietDatHang");

                entity.Property(e => e.Giatien).HasColumnType("money");

                entity.Property(e => e.IddonHang).HasColumnName("IDDonHang");

                entity.Property(e => e.IdsanPham).HasColumnName("IDSanPham");

                entity.HasOne(d => d.IddonHangNavigation)
                    .WithMany(p => p.ChiTietDatHangs)
                    .HasForeignKey(d => d.IddonHang)
                    .HasConstraintName("FK__ChiTietDa__IDDon__2645B050");

                entity.HasOne(d => d.IdsanPhamNavigation)
                    .WithMany(p => p.ChiTietDatHangs)
                    .HasForeignKey(d => d.IdsanPham)
                    .HasConstraintName("FK__ChiTietDa__IDSan__2739D489");
            });

            modelBuilder.Entity<ChucDanh>(entity =>
            {
                entity.HasKey(e => e.IdchucDanh)
                    .HasName("PK__ChucDanh__BF5D15EC18CFC04D");

                entity.ToTable("ChucDanh");

                entity.HasIndex(e => e.TenChucDanh, "UQ__ChucDanh__BAAFE715B552361A")
                    .IsUnique();

                entity.Property(e => e.IdchucDanh).HasColumnName("IDChucDanh");

                entity.Property(e => e.TenChucDanh).HasMaxLength(50);
            });

            modelBuilder.Entity<ChucDanhBacSi>(entity =>
            {
                entity.HasKey(e => e.IdChucDanhBacSi)
                    .HasName("PK__ChucDanh__17DA8846BB11BFD7");

                entity.ToTable("ChucDanhBacSi");

                entity.Property(e => e.IdbacSi)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDBacSi");

                entity.Property(e => e.IdchucDanh).HasColumnName("IDChucDanh");

                entity.HasOne(d => d.IdbacSiNavigation)
                    .WithMany(p => p.ChucDanhBacSis)
                    .HasForeignKey(d => d.IdbacSi)
                    .HasConstraintName("FK__ChucDanhB__IDBac__693CA210");

                entity.HasOne(d => d.IdchucDanhNavigation)
                    .WithMany(p => p.ChucDanhBacSis)
                    .HasForeignKey(d => d.IdchucDanh)
                    .HasConstraintName("FK__ChucDanhB__IDChu__6A30C649");
            });

            modelBuilder.Entity<ChuyenKhoa>(entity =>
            {
                entity.HasKey(e => e.IdchuyenKhoa)
                    .HasName("PK__ChuyenKh__8929FBE5A505DBE5");

                entity.ToTable("ChuyenKhoa");

                entity.HasIndex(e => e.TenChuyenKhoa, "UQ__ChuyenKh__E7F9B928D44064B1")
                    .IsUnique();

                entity.Property(e => e.IdchuyenKhoa).HasColumnName("IDChuyenKhoa");

                entity.Property(e => e.Anh).HasColumnType("text");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.TenChuyenKhoa).HasMaxLength(50);
            });

            modelBuilder.Entity<ChuyenKhoaPhongKham>(entity =>
            {
                entity.HasKey(e => e.IdchuyenKhoaPhongKham)
                    .HasName("PK__ChuyenKh__9F1349A69B828256");

                entity.ToTable("ChuyenKhoaPhongKham");

                entity.Property(e => e.IdchuyenKhoaPhongKham).HasColumnName("IDChuyenKhoaPhongKham");

                entity.Property(e => e.IdchuyenKhoa).HasColumnName("IDChuyenKhoa");

                entity.Property(e => e.IdphongKham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDPhongKham");

                entity.HasOne(d => d.IdchuyenKhoaNavigation)
                    .WithMany(p => p.ChuyenKhoaPhongKhams)
                    .HasForeignKey(d => d.IdchuyenKhoa)
                    .HasConstraintName("FK__ChuyenKho__IDChu__5535A963");

                entity.HasOne(d => d.IdphongKhamNavigation)
                    .WithMany(p => p.ChuyenKhoaPhongKhams)
                    .HasForeignKey(d => d.IdphongKham)
                    .HasConstraintName("FK__ChuyenKho__IDPho__5629CD9C");
            });

            modelBuilder.Entity<ChuyenMon>(entity =>
            {
                entity.HasKey(e => e.IdChuyenMon)
                    .HasName("PK__ChuyenMo__314169CBFCD1AC4D");

                entity.ToTable("ChuyenMon");

                entity.Property(e => e.AnhChuyeMon).HasColumnType("text");

                entity.Property(e => e.MoTaChuyenMon).HasColumnType("ntext");

                entity.Property(e => e.TenChuyenMon).HasMaxLength(50);
            });

            modelBuilder.Entity<ChuyenMoncoSo>(entity =>
            {
                entity.HasKey(e => e.IdchuyenMonCoSo)
                    .HasName("PK__ChuyenMo__75E12AF9C83CA5B8");

                entity.ToTable("ChuyenMoncoSo");

                entity.Property(e => e.IdchuyenMonCoSo).HasColumnName("IDChuyenMonCoSo");

                entity.Property(e => e.IdchuyenMon).HasColumnName("IDChuyenMon");

                entity.Property(e => e.IdcoSoDichVuKhac)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDCoSoDichVuKhac");

                entity.HasOne(d => d.IdchuyenMonNavigation)
                    .WithMany(p => p.ChuyenMoncoSos)
                    .HasForeignKey(d => d.IdchuyenMon)
                    .HasConstraintName("FK__ChuyenMon__IDChu__32AB8735");

                entity.HasOne(d => d.IdcoSoDichVuKhacNavigation)
                    .WithMany(p => p.ChuyenMoncoSos)
                    .HasForeignKey(d => d.IdcoSoDichVuKhac)
                    .HasConstraintName("FK__ChuyenMon__IDCoS__339FAB6E");
            });

            modelBuilder.Entity<CoSoDichVuKhac>(entity =>
            {
                entity.HasKey(e => e.IdcoSoDichVuKhac)
                    .HasName("PK__CoSoDich__12A97EE527298BD3");

                entity.ToTable("CoSoDichVuKhac");

                entity.Property(e => e.IdcoSoDichVuKhac)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDCoSoDichVuKhac");

                entity.Property(e => e.AnhDaiDienCoSo).HasColumnType("text");

                entity.Property(e => e.ChuyenMon).HasColumnType("ntext");

                entity.Property(e => e.DiaChi).HasColumnType("ntext");

                entity.Property(e => e.HinhAnhCoSo).HasColumnType("text");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdxaPhuong).HasColumnName("IDXaPhuong");

                entity.Property(e => e.LoiGioiThieu).HasColumnType("ntext");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.NgayMoCoSo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenCoSo).HasMaxLength(50);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.Property(e => e.TrangThietBi).HasColumnType("ntext");

                entity.Property(e => e.ViTri).HasColumnType("text");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.CoSoDichVuKhacs)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK__CoSoDichV__IDNgu__2B0A656D");

                entity.HasOne(d => d.IdxaPhuongNavigation)
                    .WithMany(p => p.CoSoDichVuKhacs)
                    .HasForeignKey(d => d.IdxaPhuong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSoDichV__IDXaP__2BFE89A6");
            });

            modelBuilder.Entity<DanhMucSanPham>(entity =>
            {
                entity.HasKey(e => e.IddanhMucSanPham)
                    .HasName("PK__DanhMucS__3E7D2E36773DA3F7");

                entity.ToTable("DanhMucSanPham");

                entity.HasIndex(e => e.TenDanhMuc, "UQ__DanhMucS__650CAE4EC99E9918")
                    .IsUnique();

                entity.Property(e => e.IddanhMucSanPham).HasColumnName("IDDanhMucSanPham");

                entity.Property(e => e.TenDanhMuc).HasMaxLength(50);
            });

            modelBuilder.Entity<DatLich>(entity =>
            {
                entity.HasKey(e => e.IddatLich)
                    .HasName("PK__DatLich__CE66721C74C0B29A");

                entity.ToTable("DatLich");

                entity.Property(e => e.IddatLich).HasColumnName("IDDatLich");

                entity.Property(e => e.IdkeHoachKham).HasColumnName("IDKeHoachKham");

                entity.Property(e => e.SoLuongHienTai).HasDefaultValueSql("((0))");

                entity.Property(e => e.ThoiGianDatLich)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThaiDatLich).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdkeHoachKhamNavigation)
                    .WithMany(p => p.DatLiches)
                    .HasForeignKey(d => d.IdkeHoachKham)
                    .HasConstraintName("FK__DatLich__IDKeHoa__74AE54BC");
            });

            modelBuilder.Entity<DatLichNhanVienCoSo>(entity =>
            {
                entity.HasKey(e => e.IddatLichNhanVienCoSo)
                    .HasName("PK__DatLichN__BE648159733CA5B0");

                entity.ToTable("DatLichNhanVienCoSo");

                entity.Property(e => e.IddatLichNhanVienCoSo).HasColumnName("IDDatLichNhanVienCoSo");

                entity.Property(e => e.IdkeHoachNhanVienCoSo).HasColumnName("IDKeHoachNhanVienCoSo");

                entity.Property(e => e.SoLuongHienTai).HasDefaultValueSql("((0))");

                entity.Property(e => e.ThoiGianDatLich)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThaiDatLich).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdkeHoachNhanVienCoSoNavigation)
                    .WithMany(p => p.DatLichNhanVienCoSos)
                    .HasForeignKey(d => d.IdkeHoachNhanVienCoSo)
                    .HasConstraintName("FK__DatLichNh__IDKeH__489AC854");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.IddonHang)
                    .HasName("PK__DonHang__9CA232F79DC9F83F");

                entity.ToTable("DonHang");

                entity.Property(e => e.IddonHang).HasColumnName("IDDonHang");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdnhanVien)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDNhanVien");

                entity.Property(e => e.NgayDat).HasColumnType("date");

                entity.Property(e => e.NgayDuyet).HasColumnType("date");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK__DonHang__IDNguoi__2180FB33");

                entity.HasOne(d => d.IdnhanVienNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.IdnhanVien)
                    .HasConstraintName("FK__DonHang__IDNhanV__22751F6C");
            });

            modelBuilder.Entity<DonViBan>(entity =>
            {
                entity.HasKey(e => e.IddonViBan)
                    .HasName("PK__DonViBan__112EE94AAF7E48A5");

                entity.ToTable("DonViBan");

                entity.Property(e => e.IddonViBan).HasColumnName("IDDonViBan");

                entity.Property(e => e.IdloaiSanPham).HasColumnName("IDLoaiSanPham");

                entity.Property(e => e.TenDonViBan).HasMaxLength(30);

                entity.HasOne(d => d.IdloaiSanPhamNavigation)
                    .WithMany(p => p.DonViBans)
                    .HasForeignKey(d => d.IdloaiSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonViBan__IDLoai__160F4887");
            });

            modelBuilder.Entity<KeHoachKham>(entity =>
            {
                entity.HasKey(e => e.IdkeHoachKham)
                    .HasName("PK__KeHoachK__1312058994D91A46");

                entity.ToTable("KeHoachKham");

                entity.Property(e => e.IdkeHoachKham).HasColumnName("IDKeHoachKham");

                entity.Property(e => e.IdbacSi)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDBacSi");

                entity.Property(e => e.NgayDatLich).HasColumnType("date");

                entity.Property(e => e.TrangThaiKeHoachKham).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdbacSiNavigation)
                    .WithMany(p => p.KeHoachKhams)
                    .HasForeignKey(d => d.IdbacSi)
                    .HasConstraintName("FK__KeHoachKh__IDBac__70DDC3D8");
            });

            modelBuilder.Entity<KeHoachNhanVienCoSo>(entity =>
            {
                entity.HasKey(e => e.IdkeHoachNhanVienCoSo)
                    .HasName("PK__KeHoachN__54395B910454713C");

                entity.ToTable("KeHoachNhanVienCoSo");

                entity.Property(e => e.IdkeHoachNhanVienCoSo).HasColumnName("IDKeHoachNhanVienCoSo");

                entity.Property(e => e.IdnhanVienCoSo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDNhanVienCoSo");

                entity.Property(e => e.NgayDatLich).HasColumnType("date");

                entity.Property(e => e.TrangThaiKeHoachNhanVienCoSo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdnhanVienCoSoNavigation)
                    .WithMany(p => p.KeHoachNhanVienCoSos)
                    .HasForeignKey(d => d.IdnhanVienCoSo)
                    .HasConstraintName("FK__KeHoachNh__IDNha__44CA3770");
            });

            modelBuilder.Entity<LichHen>(entity =>
            {
                entity.HasKey(e => e.IdLichHen)
                    .HasName("PK__LichHen__C82CFE9BDF35CA6E");

                entity.ToTable("LichHen");

                entity.Property(e => e.IdLichHen).HasColumnName("idLichHen");

                entity.Property(e => e.GioHen)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdNguoiDungHenLich).HasColumnName("idNguoiDungHenLich");

                entity.Property(e => e.LinkHen).HasColumnType("text");

                entity.Property(e => e.NgayHen).HasColumnType("date");

                entity.HasOne(d => d.IdNguoiDungHenLichNavigation)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.IdNguoiDungHenLich)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LichHen__idNguoi__634EBE90");
            });

            modelBuilder.Entity<LoaiHinhDichVu>(entity =>
            {
                entity.HasKey(e => e.IdLoaiHinhDichVu)
                    .HasName("PK__LoaiHinh__73E1A76AC1C8652F");

                entity.ToTable("LoaiHinhDichVu");

                entity.HasIndex(e => e.TenLoaiHinhDichVu, "UQ__LoaiHinh__83E3BF1D23FC1FCB")
                    .IsUnique();

                entity.Property(e => e.IdLoaiHinhDichVu).HasColumnName("idLoaiHinhDichVu");

                entity.Property(e => e.TenLoaiHinhDichVu).HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.IdloaiSanPham)
                    .HasName("PK__LoaiSanP__6CB987C5704041F9");

                entity.ToTable("LoaiSanPham");

                entity.HasIndex(e => e.TenLoaiSanPham, "UQ__LoaiSanP__FD39E6056256040D")
                    .IsUnique();

                entity.Property(e => e.IdloaiSanPham).HasColumnName("IDLoaiSanPham");

                entity.Property(e => e.IddanhMucSanPham).HasColumnName("IDDanhMucSanPham");

                entity.Property(e => e.TenLoaiSanPham).HasMaxLength(50);

                entity.HasOne(d => d.IddanhMucSanPhamNavigation)
                    .WithMany(p => p.LoaiSanPhams)
                    .HasForeignKey(d => d.IddanhMucSanPham)
                    .HasConstraintName("FK__LoaiSanPh__IDDan__1332DBDC");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.IdNguoiDung)
                    .HasName("PK__NguoiDun__BE010FC9ED447438");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.SoDienThoai, "UQ__NguoiDun__0389B7BDB056BF5A")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534872777A1")
                    .IsUnique();

                entity.HasIndex(e => e.TaiKhoan, "UQ__NguoiDun__D5B8C7F05F492766")
                    .IsUnique();

                entity.Property(e => e.IdNguoiDung).HasColumnName("idNguoiDung");

                entity.Property(e => e.AnhNguoidung).HasColumnType("text");

                entity.Property(e => e.Bmi)
                    .HasColumnType("decimal(20, 2)")
                    .HasColumnName("BMI");

                entity.Property(e => e.CanNang).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CCCD")
                    .IsFixedLength();

                entity.Property(e => e.ChieuCao).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.DangNhapLanCuoi).HasColumnType("date");

                entity.Property(e => e.Diachi).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HoNguoiDung).HasMaxLength(50);

                entity.Property(e => e.HuyLich).HasDefaultValueSql("((0))");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoaiNguoiThan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TenNguoiDung).HasMaxLength(50);

                entity.Property(e => e.TienSuBenh).HasMaxLength(200);

                entity.Property(e => e.TrangThaiCoSoYte)
                    .HasColumnName("TrangThaiCoSoYTe")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TrangThaiNguoiDung).HasDefaultValueSql("((1))");

                entity.Property(e => e.TrangThaiNhaThuoc).HasDefaultValueSql("((0))");

                entity.Property(e => e.TrangThaiPhongKham).HasDefaultValueSql("((0))");

                entity.Property(e => e.XacThuc).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<NhaThuoc>(entity =>
            {
                entity.HasKey(e => e.IdNhaThuoc)
                    .HasName("PK__NhaThuoc__6C21C360E852486B");

                entity.ToTable("NhaThuoc");

                entity.HasIndex(e => e.TenNhaThuoc, "UQ__NhaThuoc__744A5273F8BFF51F")
                    .IsUnique();

                entity.Property(e => e.IdNhaThuoc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("idNhaThuoc");

                entity.Property(e => e.DiaChi).HasColumnType("ntext");

                entity.Property(e => e.IdLoaiHinhDichVu)
                    .HasColumnName("idLoaiHinhDichVu")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdxaPhuong).HasColumnName("IDXaPhuong");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.NgayMoNhaThuoc)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenNhaThuoc).HasMaxLength(50);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdLoaiHinhDichVuNavigation)
                    .WithMany(p => p.NhaThuocs)
                    .HasForeignKey(d => d.IdLoaiHinhDichVu)
                    .HasConstraintName("FK__NhaThuoc__idLoai__02084FDA");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.NhaThuocs)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK__NhaThuoc__IDNguo__00200768");

                entity.HasOne(d => d.IdxaPhuongNavigation)
                    .WithMany(p => p.NhaThuocs)
                    .HasForeignKey(d => d.IdxaPhuong)
                    .HasConstraintName("FK__NhaThuoc__IDXaPh__01142BA1");
            });

            modelBuilder.Entity<NhanVienCoSo>(entity =>
            {
                entity.HasKey(e => e.IdnhanVienCoSo)
                    .HasName("PK__NhanVien__45007A4F1368D439");

                entity.ToTable("NhanVienCoSo");

                entity.HasIndex(e => e.SoDienThoaiNhanVienCoSo, "UQ__NhanVien__A8A79DF418F888C9")
                    .IsUnique();

                entity.HasIndex(e => e.Cccd, "UQ__NhanVien__A955A0AAB6EFD03A")
                    .IsUnique();

                entity.HasIndex(e => e.EmailNhanVienCoSo, "UQ__NhanVien__D16177E489B3C477")
                    .IsUnique();

                entity.Property(e => e.IdnhanVienCoSo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDNhanVienCoSo");

                entity.Property(e => e.AnhChungChiHanhNgheNhanVien).HasColumnType("text");

                entity.Property(e => e.AnhNhanVien).HasColumnType("text");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CCCD")
                    .IsFixedLength();

                entity.Property(e => e.EmailNhanVienCoSo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GiaDatLich)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.HoTenNhanVien).HasMaxLength(30);

                entity.Property(e => e.IdcoSoDichVuKhac)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDCoSoDichVuKhac");

                entity.Property(e => e.Idnguoidung).HasColumnName("IDNguoidung");

                entity.Property(e => e.Idquyen).HasColumnName("IDQuyen");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.SoDienThoaiNhanVienCoSo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdcoSoDichVuKhacNavigation)
                    .WithMany(p => p.NhanVienCoSos)
                    .HasForeignKey(d => d.IdcoSoDichVuKhac)
                    .HasConstraintName("FK__NhanVienC__IDCoS__395884C4");

                entity.HasOne(d => d.IdnguoidungNavigation)
                    .WithMany(p => p.NhanVienCoSos)
                    .HasForeignKey(d => d.Idnguoidung)
                    .HasConstraintName("FK__NhanVienC__IDNgu__3C34F16F");

                entity.HasOne(d => d.IdquyenNavigation)
                    .WithMany(p => p.NhanVienCoSos)
                    .HasForeignKey(d => d.Idquyen)
                    .HasConstraintName("FK__NhanVienC__IDQuy__3A4CA8FD");
            });

            modelBuilder.Entity<NhanVienNhaThuoc>(entity =>
            {
                entity.HasKey(e => e.IdnhanVienNhaThuoc)
                    .HasName("PK__NhanVien__9FD1BD3040BAD069");

                entity.ToTable("NhanVienNhaThuoc");

                entity.HasIndex(e => e.EmailNhanvien, "UQ__NhanVien__4DEE8FFC4563167D")
                    .IsUnique();

                entity.HasIndex(e => e.SdtnhanVien, "UQ__NhanVien__7542FE02743A0C6E")
                    .IsUnique();

                entity.HasIndex(e => e.TaiKhoan, "UQ__NhanVien__D5B8C7F05216F115")
                    .IsUnique();

                entity.Property(e => e.IdnhanVienNhaThuoc)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDNhanVienNhaThuoc");

                entity.Property(e => e.DiaChi).HasColumnType("ntext");

                entity.Property(e => e.EmailNhanvien)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HoTenNhanVien).HasMaxLength(50);

                entity.Property(e => e.IdNhaThuoc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("idNhaThuoc");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SdtnhanVien)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SDTNhanVien");

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ChucVuNavigation)
                    .WithMany(p => p.NhanVienNhaThuocs)
                    .HasForeignKey(d => d.ChucVu)
                    .HasConstraintName("FK__NhanVienN__ChucV__0B91BA14");

                entity.HasOne(d => d.IdNhaThuocNavigation)
                    .WithMany(p => p.NhanVienNhaThuocs)
                    .HasForeignKey(d => d.IdNhaThuoc)
                    .HasConstraintName("FK__NhanVienN__idNha__0A9D95DB");
            });

            modelBuilder.Entity<PhanLoaiBacSiChuyenKhoa>(entity =>
            {
                entity.HasKey(e => e.IdphanLoaiBacSiChuyenKhoa)
                    .HasName("PK__PhanLoai__ABD2E476D6B82F50");

                entity.ToTable("PhanLoaiBacSiChuyenKhoa");

                entity.Property(e => e.IdphanLoaiBacSiChuyenKhoa).HasColumnName("IDPhanLoaiBacSiChuyenKHoa");

                entity.Property(e => e.IdbacSi)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDBacSi");

                entity.Property(e => e.IdchuyenKhoa).HasColumnName("IDChuyenKhoa");

                entity.HasOne(d => d.IdbacSiNavigation)
                    .WithMany(p => p.PhanLoaiBacSiChuyenKhoas)
                    .HasForeignKey(d => d.IdbacSi)
                    .HasConstraintName("FK__PhanLoaiB__IDBac__6D0D32F4");

                entity.HasOne(d => d.IdchuyenKhoaNavigation)
                    .WithMany(p => p.PhanLoaiBacSiChuyenKhoas)
                    .HasForeignKey(d => d.IdchuyenKhoa)
                    .HasConstraintName("FK__PhanLoaiB__IDChu__6E01572D");
            });

            modelBuilder.Entity<PhanLoaiChuyenKhoaNhanVien>(entity =>
            {
                entity.HasKey(e => e.IdphanLoaiBacSiChuyenKhoa)
                    .HasName("PK__PhanLoai__ABD2E476FF3BD6A7");

                entity.ToTable("PhanLoaiChuyenKhoaNhanVien");

                entity.Property(e => e.IdphanLoaiBacSiChuyenKhoa).HasColumnName("IDPhanLoaiBacSiChuyenKHoa");

                entity.Property(e => e.IdnhanVienCoSo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDNhanVienCoSo");

                entity.HasOne(d => d.ChuyenMoncoSoNavigation)
                    .WithMany(p => p.PhanLoaiChuyenKhoaNhanViens)
                    .HasForeignKey(d => d.ChuyenMoncoSo)
                    .HasConstraintName("FK__PhanLoaiC__Chuye__41EDCAC5");

                entity.HasOne(d => d.IdnhanVienCoSoNavigation)
                    .WithMany(p => p.PhanLoaiChuyenKhoaNhanViens)
                    .HasForeignKey(d => d.IdnhanVienCoSo)
                    .HasConstraintName("FK__PhanLoaiC__IDNha__40F9A68C");
            });

            modelBuilder.Entity<PhongKham>(entity =>
            {
                entity.HasKey(e => e.IdphongKham)
                    .HasName("PK__PhongKha__32AA370A4804DE64");

                entity.ToTable("PhongKham");

                entity.HasIndex(e => e.TenPhongKham, "UQ__PhongKha__013DD7AA0232B077")
                    .IsUnique();

                entity.Property(e => e.IdphongKham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDPhongKham");

                entity.Property(e => e.AnhDaiDienPhongKham).HasColumnType("text");

                entity.Property(e => e.BaoHiem).HasDefaultValueSql("((0))");

                entity.Property(e => e.ChuyenMon).HasColumnType("ntext");

                entity.Property(e => e.DiaChi).HasColumnType("ntext");

                entity.Property(e => e.HinhAnh).HasColumnType("text");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdxaPhuong).HasColumnName("IDXaPhuong");

                entity.Property(e => e.LoiGioiThieu).HasColumnType("ntext");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.NgayMoPhongKham)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenPhongKham).HasMaxLength(100);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.Property(e => e.TrangThietBi).HasColumnType("ntext");

                entity.Property(e => e.ViTri).HasColumnType("text");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.PhongKhams)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK__PhongKham__IDNgu__4E88ABD4");

                entity.HasOne(d => d.IdxaPhuongNavigation)
                    .WithMany(p => p.PhongKhams)
                    .HasForeignKey(d => d.IdxaPhuong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhongKham__IDXaP__4F7CD00D");
            });

            modelBuilder.Entity<QuanHuyen>(entity =>
            {
                entity.HasKey(e => e.IdquanHuyen)
                    .HasName("PK__QuanHuye__29AC36EEEE870AFF");

                entity.ToTable("QuanHuyen");

                entity.HasIndex(e => e.TenQuanHuyen, "UQ__QuanHuye__7A15A2B8A6BC2277")
                    .IsUnique();

                entity.Property(e => e.IdquanHuyen).HasColumnName("IDQuanHuyen");

                entity.Property(e => e.TenQuanHuyen).HasMaxLength(50);
            });

            modelBuilder.Entity<QuanTriVien>(entity =>
            {
                entity.HasKey(e => e.IdquanTriVien)
                    .HasName("PK__QuanTriV__C7DEE233D7EA7EF3");

                entity.ToTable("QuanTriVien");

                entity.HasIndex(e => e.SoDienThoai, "UQ__QuanTriV__0389B7BDE65A6DFC")
                    .IsUnique();

                entity.HasIndex(e => e.TaiKhoanQtv, "UQ__QuanTriV__630AB4F3739C05E8")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__QuanTriV__A9D105348250F41D")
                    .IsUnique();

                entity.Property(e => e.IdquanTriVien).HasColumnName("IDQuanTriVien");

                entity.Property(e => e.AnhQtv)
                    .HasColumnType("text")
                    .HasColumnName("AnhQTV");

                entity.Property(e => e.Chucvu)
                    .HasColumnName("chucvu")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DiaChi).HasColumnType("ntext");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HoQtv)
                    .HasMaxLength(30)
                    .HasColumnName("HoQTV");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TaiKhoanQtv)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TaiKhoanQTV");

                entity.Property(e => e.TenQtv)
                    .HasMaxLength(30)
                    .HasColumnName("TenQTV");

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Quyen>(entity =>
            {
                entity.HasKey(e => e.Idquyen)
                    .HasName("PK__Quyen__FB764FA1B4237286");

                entity.ToTable("Quyen");

                entity.HasIndex(e => e.TenQuyen, "UQ__Quyen__5637EE79BA734B01")
                    .IsUnique();

                entity.Property(e => e.Idquyen).HasColumnName("IDquyen");

                entity.Property(e => e.TenQuyen).HasMaxLength(20);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.IdsanPham)
                    .HasName("PK__SanPham__9D45E58AD1908759");

                entity.ToTable("SanPham");

                entity.HasIndex(e => e.TenSanPham, "UQ__SanPham__FCA80469846EA1E3")
                    .IsUnique();

                entity.Property(e => e.IdsanPham).HasColumnName("IDSanPham");

                entity.Property(e => e.AnhSanPham).HasColumnType("text");

                entity.Property(e => e.Gia)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IddonViBan).HasColumnName("IDDonViBan");

                entity.Property(e => e.IdloaiSanPham).HasColumnName("IDLoaiSanPham");

                entity.Property(e => e.IdnhaThuoc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDNhaThuoc");

                entity.Property(e => e.Mota).HasColumnType("ntext");

                entity.Property(e => e.TenSanPham).HasMaxLength(50);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IddonViBanNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IddonViBan)
                    .HasConstraintName("FK__SanPham__IDDonVi__1EA48E88");

                entity.HasOne(d => d.IdloaiSanPhamNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdloaiSanPham)
                    .HasConstraintName("FK__SanPham__IDLoaiS__1AD3FDA4");

                entity.HasOne(d => d.IdnhaThuocNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdnhaThuoc)
                    .HasConstraintName("FK__SanPham__IDNhaTh__19DFD96B");
            });

            modelBuilder.Entity<TaoLich>(entity =>
            {
                entity.HasKey(e => e.IdtaoLich)
                    .HasName("PK__TaoLich__4CD8CD8D1DC9A294");

                entity.ToTable("TaoLich");

                entity.Property(e => e.IdtaoLich).HasColumnName("IDTaoLich");

                entity.Property(e => e.IddatLich).HasColumnName("IDDatLich");

                entity.Property(e => e.IdnguoiDungDatLich).HasColumnName("IDNguoiDungDatLich");

                entity.Property(e => e.LyDoKham).HasMaxLength(250);

                entity.Property(e => e.NgayGioDatLich)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrangThaiTaoLich).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IddatLichNavigation)
                    .WithMany(p => p.TaoLiches)
                    .HasForeignKey(d => d.IddatLich)
                    .HasConstraintName("FK__TaoLich__IDDatLi__7A672E12");

                entity.HasOne(d => d.IdnguoiDungDatLichNavigation)
                    .WithMany(p => p.TaoLiches)
                    .HasForeignKey(d => d.IdnguoiDungDatLich)
                    .HasConstraintName("FK__TaoLich__IDNguoi__797309D9");
            });

            modelBuilder.Entity<TaoLichNhanVienCoSo>(entity =>
            {
                entity.HasKey(e => e.IdtaoLichNhanVienCoSo)
                    .HasName("PK__TaoLichN__1B3A9FD96051E7B7");

                entity.ToTable("TaoLichNhanVienCoSo");

                entity.Property(e => e.IdtaoLichNhanVienCoSo).HasColumnName("IDTaoLichNhanVienCoSo");

                entity.Property(e => e.IddatLichNhanVienCoSo).HasColumnName("IDDatLichNhanVienCoSo");

                entity.Property(e => e.IdnguoiDungDatLich).HasColumnName("IDNguoiDungDatLich");

                entity.Property(e => e.LyDoKham).HasMaxLength(250);

                entity.Property(e => e.NgayGioDatLich)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrangThaiTaoLich).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IddatLichNhanVienCoSoNavigation)
                    .WithMany(p => p.TaoLichNhanVienCoSos)
                    .HasForeignKey(d => d.IddatLichNhanVienCoSo)
                    .HasConstraintName("FK__TaoLichNh__IDDat__4E53A1AA");

                entity.HasOne(d => d.IdnguoiDungDatLichNavigation)
                    .WithMany(p => p.TaoLichNhanVienCoSos)
                    .HasForeignKey(d => d.IdnguoiDungDatLich)
                    .HasConstraintName("FK__TaoLichNh__IDNgu__4D5F7D71");
            });

            modelBuilder.Entity<XaPhuong>(entity =>
            {
                entity.HasKey(e => e.IdxaPhuong)
                    .HasName("PK__XaPhuong__9B0E87294F68E3E1");

                entity.ToTable("XaPhuong");

                entity.Property(e => e.IdxaPhuong).HasColumnName("IDXaPhuong");

                entity.Property(e => e.IdquanHuyen).HasColumnName("IDQuanHuyen");

                entity.Property(e => e.TenXaPhuong).HasMaxLength(50);

                entity.HasOne(d => d.IdquanHuyenNavigation)
                    .WithMany(p => p.XaPhuongs)
                    .HasForeignKey(d => d.IdquanHuyen)
                    .HasConstraintName("FK__XaPhuong__IDQuan__44FF419A");
            });

            modelBuilder.Entity<XacThucDangKyMoCoSoYte>(entity =>
            {
                entity.HasKey(e => e.IdxacThucDangKyMoCoSoYte)
                    .HasName("PK__XacThucD__D7D33CA5F259E7CE");

                entity.ToTable("XacThucDangKyMoCoSoYTe");

                entity.Property(e => e.IdxacThucDangKyMoCoSoYte).HasColumnName("IDXacThucDangKyMoCoSoYTe");

                entity.Property(e => e.AnhBangCap).HasColumnType("text");

                entity.Property(e => e.AnhCccdmatSau)
                    .HasColumnType("text")
                    .HasColumnName("AnhCCCDMatSau");

                entity.Property(e => e.AnhCccdmatTruoc)
                    .HasColumnType("text")
                    .HasColumnName("AnhCCCDMatTruoc");

                entity.Property(e => e.AnhChungChiHanhNghe).HasColumnType("text");

                entity.Property(e => e.AnhCoSo).HasColumnType("text");

                entity.Property(e => e.AnhDangKyAnhBacSi).HasColumnType("text");

                entity.Property(e => e.AnhGiayChungNhanKinhDoanh).HasColumnType("text");

                entity.Property(e => e.DiaChi).HasColumnType("ntext");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdquanTriVien).HasColumnName("IDQuanTriVien");

                entity.Property(e => e.NgayDangKy)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgayXetDuyet).HasColumnType("date");

                entity.Property(e => e.TenCoSo).HasMaxLength(50);

                entity.Property(e => e.TrangThaiXacThuc).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__XacThucDa__IDNgu__59C55456");

                entity.HasOne(d => d.IdquanTriVienNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.IdquanTriVien)
                    .HasConstraintName("FK__XacThucDa__IDQua__5AB9788F");

                entity.HasOne(d => d.LoaiHinhDangKyNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.LoaiHinhDangKy)
                    .HasConstraintName("FK__XacThucDa__LoaiH__5BAD9CC8");

                entity.HasOne(d => d.XaPhuongNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.XaPhuong)
                    .HasConstraintName("FK__XacThucDa__XaPhu__5D95E53A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
