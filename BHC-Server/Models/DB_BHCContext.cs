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
        public virtual DbSet<ChiTietDatHang> ChiTietDatHangs { get; set; } = null!;
        public virtual DbSet<ChucDanh> ChucDanhs { get; set; } = null!;
        public virtual DbSet<ChucDanhBacSi> ChucDanhBacSis { get; set; } = null!;
        public virtual DbSet<ChuyenKhoa> ChuyenKhoas { get; set; } = null!;
        public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; } = null!;
        public virtual DbSet<DatLich> DatLiches { get; set; } = null!;
        public virtual DbSet<DonHang> DonHangs { get; set; } = null!;
        public virtual DbSet<DonViBan> DonViBans { get; set; } = null!;
        public virtual DbSet<KeHoachKham> KeHoachKhams { get; set; } = null!;
        public virtual DbSet<LoaiHinhDichVu> LoaiHinhDichVus { get; set; } = null!;
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<NhaThuoc> NhaThuocs { get; set; } = null!;
        public virtual DbSet<NhanVienNhaThuoc> NhanVienNhaThuocs { get; set; } = null!;
        public virtual DbSet<PhanLoaiBacSiChuyenKhoa> PhanLoaiBacSiChuyenKhoas { get; set; } = null!;
        public virtual DbSet<PhongKham> PhongKhams { get; set; } = null!;
        public virtual DbSet<QuanHuyen> QuanHuyens { get; set; } = null!;
        public virtual DbSet<QuanTriVien> QuanTriViens { get; set; } = null!;
        public virtual DbSet<Quyen> Quyens { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;
        public virtual DbSet<TaoLich> TaoLiches { get; set; } = null!;
        public virtual DbSet<XaPhuong> XaPhuongs { get; set; } = null!;
        public virtual DbSet<XacThucDangKyMoCoSoYte> XacThucDangKyMoCoSoYtes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=DB_BHC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BacSi>(entity =>
            {
                entity.HasKey(e => e.IdbacSi)
                    .HasName("PK__BacSi__32D03F8433FBF916");

                entity.ToTable("BacSi");

                entity.HasIndex(e => e.SoDienThoaiBacSi, "UQ__BacSi__2C67339D47895888")
                    .IsUnique();

                entity.HasIndex(e => e.Cccd, "UQ__BacSi__A955A0AA6A2F12B5")
                    .IsUnique();

                entity.HasIndex(e => e.EmailBacSi, "UQ__BacSi__A9B055F1906E3C51")
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
                    .HasConstraintName("FK__BacSi__IDNguoidu__6383C8BA");

                entity.HasOne(d => d.IdphongKhamNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.IdphongKham)
                    .HasConstraintName("FK__BacSi__IDPhongKh__60A75C0F");

                entity.HasOne(d => d.IdquyenNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.Idquyen)
                    .HasConstraintName("FK__BacSi__IDQuyen__619B8048");
            });

            modelBuilder.Entity<ChiTietDatHang>(entity =>
            {
                entity.HasKey(e => e.IdchiTietDatHang)
                    .HasName("PK__ChiTietD__279D92CA2BFF65AC");

                entity.ToTable("ChiTietDatHang");

                entity.Property(e => e.IdchiTietDatHang).HasColumnName("IDChiTietDatHang");

                entity.Property(e => e.Giatien).HasColumnType("money");

                entity.Property(e => e.IddonHang).HasColumnName("IDDonHang");

                entity.Property(e => e.IdsanPham).HasColumnName("IDSanPham");

                entity.HasOne(d => d.IddonHangNavigation)
                    .WithMany(p => p.ChiTietDatHangs)
                    .HasForeignKey(d => d.IddonHang)
                    .HasConstraintName("FK__ChiTietDa__IDDon__25518C17");

                entity.HasOne(d => d.IdsanPhamNavigation)
                    .WithMany(p => p.ChiTietDatHangs)
                    .HasForeignKey(d => d.IdsanPham)
                    .HasConstraintName("FK__ChiTietDa__IDSan__2645B050");
            });

            modelBuilder.Entity<ChucDanh>(entity =>
            {
                entity.HasKey(e => e.IdchucDanh)
                    .HasName("PK__ChucDanh__BF5D15EC35C40F9B");

                entity.ToTable("ChucDanh");

                entity.HasIndex(e => e.TenChucDanh, "UQ__ChucDanh__BAAFE7156E9EDB71")
                    .IsUnique();

                entity.Property(e => e.IdchucDanh).HasColumnName("IDChucDanh");

                entity.Property(e => e.TenChucDanh).HasMaxLength(50);
            });

            modelBuilder.Entity<ChucDanhBacSi>(entity =>
            {
                entity.HasKey(e => e.IdChucDanhBacSi)
                    .HasName("PK__ChucDanh__17DA8846F01E009C");

                entity.ToTable("ChucDanhBacSi");

                entity.Property(e => e.IdbacSi)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDBacSi");

                entity.Property(e => e.IdchucDanh).HasColumnName("IDChucDanh");

                entity.HasOne(d => d.IdbacSiNavigation)
                    .WithMany(p => p.ChucDanhBacSis)
                    .HasForeignKey(d => d.IdbacSi)
                    .HasConstraintName("FK__ChucDanhB__IDBac__68487DD7");

                entity.HasOne(d => d.IdchucDanhNavigation)
                    .WithMany(p => p.ChucDanhBacSis)
                    .HasForeignKey(d => d.IdchucDanh)
                    .HasConstraintName("FK__ChucDanhB__IDChu__693CA210");
            });

            modelBuilder.Entity<ChuyenKhoa>(entity =>
            {
                entity.HasKey(e => e.IdchuyenKhoa)
                    .HasName("PK__ChuyenKh__8929FBE566A43392");

                entity.ToTable("ChuyenKhoa");

                entity.HasIndex(e => e.TenChuyenKhoa, "UQ__ChuyenKh__E7F9B928DB3FF685")
                    .IsUnique();

                entity.Property(e => e.IdchuyenKhoa).HasColumnName("IDChuyenKhoa");

                entity.Property(e => e.Anh).HasColumnType("text");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.TenChuyenKhoa).HasMaxLength(50);

                entity.HasMany(d => d.IdphongKhams)
                    .WithMany(p => p.IdchuyenKhoas)
                    .UsingEntity<Dictionary<string, object>>(
                        "ChuyenKhoaPhongKham",
                        l => l.HasOne<PhongKham>().WithMany().HasForeignKey("IdphongKham").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ChuyenKho__IDPho__5535A963"),
                        r => r.HasOne<ChuyenKhoa>().WithMany().HasForeignKey("IdchuyenKhoa").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ChuyenKho__IDChu__5441852A"),
                        j =>
                        {
                            j.HasKey("IdchuyenKhoa", "IdphongKham").HasName("PK__ChuyenKh__3A035895EABF8299");

                            j.ToTable("ChuyenKhoaPhongKham");

                            j.IndexerProperty<int>("IdchuyenKhoa").HasColumnName("IDChuyenKhoa");

                            j.IndexerProperty<string>("IdphongKham").HasMaxLength(10).IsUnicode(false).HasColumnName("IDPhongKham");
                        });
            });

            modelBuilder.Entity<DanhMucSanPham>(entity =>
            {
                entity.HasKey(e => e.IddanhMucSanPham)
                    .HasName("PK__DanhMucS__3E7D2E36344A109C");

                entity.ToTable("DanhMucSanPham");

                entity.HasIndex(e => e.TenDanhMuc, "UQ__DanhMucS__650CAE4E65A1229F")
                    .IsUnique();

                entity.Property(e => e.IddanhMucSanPham).HasColumnName("IDDanhMucSanPham");

                entity.Property(e => e.TenDanhMuc).HasMaxLength(50);
            });

            modelBuilder.Entity<DatLich>(entity =>
            {
                entity.HasKey(e => e.IddatLich)
                    .HasName("PK__DatLich__CE66721C909D2AA4");

                entity.ToTable("DatLich");

                entity.Property(e => e.IddatLich).HasColumnName("IDDatLich");

                entity.Property(e => e.IdkeHoachKham).HasColumnName("IDKeHoachKham");

                entity.Property(e => e.ThoiGianDatLich)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdkeHoachKhamNavigation)
                    .WithMany(p => p.DatLiches)
                    .HasForeignKey(d => d.IdkeHoachKham)
                    .HasConstraintName("FK__DatLich__IDKeHoa__73BA3083");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.IddonHang)
                    .HasName("PK__DonHang__9CA232F71139D42E");

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
                    .HasConstraintName("FK__DonHang__IDNguoi__208CD6FA");

                entity.HasOne(d => d.IdnhanVienNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.IdnhanVien)
                    .HasConstraintName("FK__DonHang__IDNhanV__2180FB33");
            });

            modelBuilder.Entity<DonViBan>(entity =>
            {
                entity.HasKey(e => e.IddonViBan)
                    .HasName("PK__DonViBan__112EE94A349746DF");

                entity.ToTable("DonViBan");

                entity.Property(e => e.IddonViBan).HasColumnName("IDDonViBan");

                entity.Property(e => e.IdloaiSanPham).HasColumnName("IDLoaiSanPham");

                entity.Property(e => e.TenDonViBan).HasMaxLength(30);

                entity.HasOne(d => d.IdloaiSanPhamNavigation)
                    .WithMany(p => p.DonViBans)
                    .HasForeignKey(d => d.IdloaiSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonViBan__IDLoai__151B244E");
            });

            modelBuilder.Entity<KeHoachKham>(entity =>
            {
                entity.HasKey(e => e.IdkeHoachKham)
                    .HasName("PK__KeHoachK__131205896D488534");

                entity.ToTable("KeHoachKham");

                entity.Property(e => e.IdkeHoachKham).HasColumnName("IDKeHoachKham");

                entity.Property(e => e.IdbacSi)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDBacSi");

                entity.Property(e => e.NgayDatLich).HasColumnType("date");

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdbacSiNavigation)
                    .WithMany(p => p.KeHoachKhams)
                    .HasForeignKey(d => d.IdbacSi)
                    .HasConstraintName("FK__KeHoachKh__IDBac__6FE99F9F");
            });

            modelBuilder.Entity<LoaiHinhDichVu>(entity =>
            {
                entity.HasKey(e => e.IdLoaiHinhDichVu)
                    .HasName("PK__LoaiHinh__73E1A76AC4703573");

                entity.ToTable("LoaiHinhDichVu");

                entity.HasIndex(e => e.TenLoaiHinhDichVu, "UQ__LoaiHinh__83E3BF1D00E5AA7E")
                    .IsUnique();

                entity.Property(e => e.IdLoaiHinhDichVu).HasColumnName("idLoaiHinhDichVu");

                entity.Property(e => e.TenLoaiHinhDichVu).HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.IdloaiSanPham)
                    .HasName("PK__LoaiSanP__6CB987C5B9C383D4");

                entity.ToTable("LoaiSanPham");

                entity.HasIndex(e => e.TenLoaiSanPham, "UQ__LoaiSanP__FD39E605BF88D92E")
                    .IsUnique();

                entity.Property(e => e.IdloaiSanPham).HasColumnName("IDLoaiSanPham");

                entity.Property(e => e.IddanhMucSanPham).HasColumnName("IDDanhMucSanPham");

                entity.Property(e => e.TenLoaiSanPham).HasMaxLength(50);

                entity.HasOne(d => d.IddanhMucSanPhamNavigation)
                    .WithMany(p => p.LoaiSanPhams)
                    .HasForeignKey(d => d.IddanhMucSanPham)
                    .HasConstraintName("FK__LoaiSanPh__IDDan__123EB7A3");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.IdNguoiDung)
                    .HasName("PK__NguoiDun__BE010FC931F6FF89");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.SoDienThoai, "UQ__NguoiDun__0389B7BDFC302054")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534F23E851B")
                    .IsUnique();

                entity.HasIndex(e => e.TaiKhoan, "UQ__NguoiDun__D5B8C7F0AB7E3E04")
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

                entity.Property(e => e.TrangThaiNguoiDung).HasDefaultValueSql("((1))");

                entity.Property(e => e.TrangThaiNhaThuoc).HasDefaultValueSql("((0))");

                entity.Property(e => e.TrangThaiPhongKham).HasDefaultValueSql("((0))");

                entity.Property(e => e.XacThuc).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<NhaThuoc>(entity =>
            {
                entity.HasKey(e => e.IdNhaThuoc)
                    .HasName("PK__NhaThuoc__6C21C36033467B56");

                entity.ToTable("NhaThuoc");

                entity.HasIndex(e => e.TenNhaThuoc, "UQ__NhaThuoc__744A527348D12C14")
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
                    .HasConstraintName("FK__NhaThuoc__idLoai__01142BA1");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.NhaThuocs)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK__NhaThuoc__IDNguo__7F2BE32F");

                entity.HasOne(d => d.IdxaPhuongNavigation)
                    .WithMany(p => p.NhaThuocs)
                    .HasForeignKey(d => d.IdxaPhuong)
                    .HasConstraintName("FK__NhaThuoc__IDXaPh__00200768");
            });

            modelBuilder.Entity<NhanVienNhaThuoc>(entity =>
            {
                entity.HasKey(e => e.IdnhanVienNhaThuoc)
                    .HasName("PK__NhanVien__9FD1BD30524F4D49");

                entity.ToTable("NhanVienNhaThuoc");

                entity.HasIndex(e => e.EmailNhanvien, "UQ__NhanVien__4DEE8FFC608B62AC")
                    .IsUnique();

                entity.HasIndex(e => e.SdtnhanVien, "UQ__NhanVien__7542FE02F664CAFF")
                    .IsUnique();

                entity.HasIndex(e => e.TaiKhoan, "UQ__NhanVien__D5B8C7F0E0CEC9A1")
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
                    .HasConstraintName("FK__NhanVienN__ChucV__0A9D95DB");

                entity.HasOne(d => d.IdNhaThuocNavigation)
                    .WithMany(p => p.NhanVienNhaThuocs)
                    .HasForeignKey(d => d.IdNhaThuoc)
                    .HasConstraintName("FK__NhanVienN__idNha__09A971A2");
            });

            modelBuilder.Entity<PhanLoaiBacSiChuyenKhoa>(entity =>
            {
                entity.HasKey(e => e.IdphanLoaiBacSiChuyenKhoa)
                    .HasName("PK__PhanLoai__ABD2E476FDCA17EF");

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
                    .HasConstraintName("FK__PhanLoaiB__IDBac__6C190EBB");

                entity.HasOne(d => d.IdchuyenKhoaNavigation)
                    .WithMany(p => p.PhanLoaiBacSiChuyenKhoas)
                    .HasForeignKey(d => d.IdchuyenKhoa)
                    .HasConstraintName("FK__PhanLoaiB__IDChu__6D0D32F4");
            });

            modelBuilder.Entity<PhongKham>(entity =>
            {
                entity.HasKey(e => e.IdphongKham)
                    .HasName("PK__PhongKha__32AA370A1471B12D");

                entity.ToTable("PhongKham");

                entity.HasIndex(e => e.TenPhongKham, "UQ__PhongKha__013DD7AA269F05F2")
                    .IsUnique();

                entity.Property(e => e.IdphongKham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDPhongKham");

                entity.Property(e => e.BaoHiem).HasDefaultValueSql("((0))");

                entity.Property(e => e.DiaChi).HasColumnType("ntext");

                entity.Property(e => e.HinhAnh).HasColumnType("text");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdxaPhuong).HasColumnName("IDXaPhuong");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.NgayMoPhongKham)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenPhongKham).HasMaxLength(100);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.PhongKhams)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK__PhongKham__IDNgu__4D94879B");

                entity.HasOne(d => d.IdxaPhuongNavigation)
                    .WithMany(p => p.PhongKhams)
                    .HasForeignKey(d => d.IdxaPhuong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhongKham__IDXaP__4E88ABD4");
            });

            modelBuilder.Entity<QuanHuyen>(entity =>
            {
                entity.HasKey(e => e.IdquanHuyen)
                    .HasName("PK__QuanHuye__29AC36EEAE1B75CD");

                entity.ToTable("QuanHuyen");

                entity.HasIndex(e => e.TenQuanHuyen, "UQ__QuanHuye__7A15A2B851761CCE")
                    .IsUnique();

                entity.Property(e => e.IdquanHuyen).HasColumnName("IDQuanHuyen");

                entity.Property(e => e.TenQuanHuyen).HasMaxLength(50);
            });

            modelBuilder.Entity<QuanTriVien>(entity =>
            {
                entity.HasKey(e => e.IdquanTriVien)
                    .HasName("PK__QuanTriV__C7DEE233BC93D095");

                entity.ToTable("QuanTriVien");

                entity.HasIndex(e => e.SoDienThoai, "UQ__QuanTriV__0389B7BDB25B7A14")
                    .IsUnique();

                entity.HasIndex(e => e.TaiKhoanQtv, "UQ__QuanTriV__630AB4F30F7CF861")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__QuanTriV__A9D1053475B4C24B")
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
                    .HasName("PK__Quyen__FB764FA157C9AF37");

                entity.ToTable("Quyen");

                entity.HasIndex(e => e.TenQuyen, "UQ__Quyen__5637EE795FBFB2A0")
                    .IsUnique();

                entity.Property(e => e.Idquyen).HasColumnName("IDquyen");

                entity.Property(e => e.TenQuyen).HasMaxLength(20);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.IdsanPham)
                    .HasName("PK__SanPham__9D45E58A0E6C7A3C");

                entity.ToTable("SanPham");

                entity.HasIndex(e => e.TenSanPham, "UQ__SanPham__FCA80469F3A0194B")
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
                    .HasConstraintName("FK__SanPham__IDDonVi__1DB06A4F");

                entity.HasOne(d => d.IdloaiSanPhamNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdloaiSanPham)
                    .HasConstraintName("FK__SanPham__IDLoaiS__19DFD96B");

                entity.HasOne(d => d.IdnhaThuocNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdnhaThuoc)
                    .HasConstraintName("FK__SanPham__IDNhaTh__18EBB532");
            });

            modelBuilder.Entity<TaoLich>(entity =>
            {
                entity.HasKey(e => e.IdtaoLich)
                    .HasName("PK__TaoLich__4CD8CD8D422BAEC9");

                entity.ToTable("TaoLich");

                entity.Property(e => e.IdtaoLich).HasColumnName("IDTaoLich");

                entity.Property(e => e.IddatLich).HasColumnName("IDDatLich");

                entity.Property(e => e.IdnguoiDungDatLich).HasColumnName("IDNguoiDungDatLich");

                entity.Property(e => e.LyDoKham).HasMaxLength(250);

                entity.Property(e => e.NgayGioDatLich)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IddatLichNavigation)
                    .WithMany(p => p.TaoLiches)
                    .HasForeignKey(d => d.IddatLich)
                    .HasConstraintName("FK__TaoLich__IDDatLi__797309D9");

                entity.HasOne(d => d.IdnguoiDungDatLichNavigation)
                    .WithMany(p => p.TaoLiches)
                    .HasForeignKey(d => d.IdnguoiDungDatLich)
                    .HasConstraintName("FK__TaoLich__IDNguoi__787EE5A0");
            });

            modelBuilder.Entity<XaPhuong>(entity =>
            {
                entity.HasKey(e => e.IdxaPhuong)
                    .HasName("PK__XaPhuong__9B0E8729C51FF0F3");

                entity.ToTable("XaPhuong");

                entity.Property(e => e.IdxaPhuong).HasColumnName("IDXaPhuong");

                entity.Property(e => e.IdquanHuyen).HasColumnName("IDQuanHuyen");

                entity.Property(e => e.TenXaPhuong).HasMaxLength(50);

                entity.HasOne(d => d.IdquanHuyenNavigation)
                    .WithMany(p => p.XaPhuongs)
                    .HasForeignKey(d => d.IdquanHuyen)
                    .HasConstraintName("FK__XaPhuong__IDQuan__440B1D61");
            });

            modelBuilder.Entity<XacThucDangKyMoCoSoYte>(entity =>
            {
                entity.HasKey(e => e.IdxacThucDangKyMoCoSoYte)
                    .HasName("PK__XacThucD__D7D33CA5686C564D");

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
                    .HasConstraintName("FK__XacThucDa__IDNgu__30C33EC3");

                entity.HasOne(d => d.IdquanTriVienNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.IdquanTriVien)
                    .HasConstraintName("FK__XacThucDa__IDQua__31B762FC");

                entity.HasOne(d => d.LoaiHinhDangKyNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.LoaiHinhDangKy)
                    .HasConstraintName("FK__XacThucDa__LoaiH__32AB8735");

                entity.HasOne(d => d.XaPhuongNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.XaPhuong)
                    .HasConstraintName("FK__XacThucDa__XaPhu__3493CFA7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
