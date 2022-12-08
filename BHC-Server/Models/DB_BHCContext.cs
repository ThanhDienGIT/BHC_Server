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
        public virtual DbSet<ChucDanh> ChucDanhs { get; set; } = null!;
        public virtual DbSet<ChucDanhBacSi> ChucDanhBacSis { get; set; } = null!;
        public virtual DbSet<ChuyenKhoa> ChuyenKhoas { get; set; } = null!;
        public virtual DbSet<ChuyenKhoaPhongKham> ChuyenKhoaPhongKhams { get; set; } = null!;
        public virtual DbSet<ChuyenMon> ChuyenMons { get; set; } = null!;
        public virtual DbSet<ChuyenMoncoSo> ChuyenMoncoSos { get; set; } = null!;
        public virtual DbSet<CoSoDichVuKhac> CoSoDichVuKhacs { get; set; } = null!;
        public virtual DbSet<DanhGiaCoso> DanhGiaCosos { get; set; } = null!;
        public virtual DbSet<DatLich> DatLiches { get; set; } = null!;
        public virtual DbSet<DatLichNhanVienCoSo> DatLichNhanVienCoSos { get; set; } = null!;
        public virtual DbSet<KeHoachKham> KeHoachKhams { get; set; } = null!;
        public virtual DbSet<KeHoachNhanVienCoSo> KeHoachNhanVienCoSos { get; set; } = null!;
        public virtual DbSet<LichHen> LichHens { get; set; } = null!;
        public virtual DbSet<LoaiHinhDichVu> LoaiHinhDichVus { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<NhanVienCoSo> NhanVienCoSos { get; set; } = null!;
        public virtual DbSet<PhanLoaiBacSiChuyenKhoa> PhanLoaiBacSiChuyenKhoas { get; set; } = null!;
        public virtual DbSet<PhanLoaiChuyenKhoaNhanVien> PhanLoaiChuyenKhoaNhanViens { get; set; } = null!;
        public virtual DbSet<PhongKham> PhongKhams { get; set; } = null!;
        public virtual DbSet<QuanHuyen> QuanHuyens { get; set; } = null!;
        public virtual DbSet<QuanTriVien> QuanTriViens { get; set; } = null!;
        public virtual DbSet<Quyen> Quyens { get; set; } = null!;
        public virtual DbSet<TaoLich> TaoLiches { get; set; } = null!;
        public virtual DbSet<TaoLichNhanVienCoSo> TaoLichNhanVienCoSos { get; set; } = null!;
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
                    .HasName("PK__BacSi__32D03F8468F0202D");

                entity.ToTable("BacSi");

                entity.HasIndex(e => e.SoDienThoaiBacSi, "UQ__BacSi__2C67339D0B06EEBC")
                    .IsUnique();

                entity.HasIndex(e => e.Cccd, "UQ__BacSi__A955A0AA3D491959")
                    .IsUnique();

                entity.HasIndex(e => e.EmailBacSi, "UQ__BacSi__A9B055F14A95EE9E")
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

                entity.Property(e => e.Danhgia)
                    .HasColumnName("danhgia")
                    .HasDefaultValueSql("((0))");

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

                entity.Property(e => e.Solandatlich)
                    .HasColumnName("solandatlich")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdnguoidungNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.Idnguoidung)
                    .HasConstraintName("FK__BacSi__IDNguoidu__66603565");

                entity.HasOne(d => d.IdphongKhamNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.IdphongKham)
                    .HasConstraintName("FK__BacSi__IDPhongKh__6383C8BA");

                entity.HasOne(d => d.IdquyenNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.Idquyen)
                    .HasConstraintName("FK__BacSi__IDQuyen__6477ECF3");
            });

            modelBuilder.Entity<CacChuyenKhoaChuyenMonDangKy>(entity =>
            {
                entity.HasKey(e => e.IdCacChuyenKhoaChuyenMonDangKy)
                    .HasName("PK__CacChuye__A93C4F99471984A0");

                entity.ToTable("CacChuyenKhoaChuyenMonDangKy");

                entity.Property(e => e.IdCacChuyenKhoaChuyenMonDangKy).HasColumnName("idCacChuyenKhoaChuyenMonDangKy");

                entity.Property(e => e.Idchuyenmon).HasColumnName("idchuyenmon");

                entity.Property(e => e.Idxacthucdatlich).HasColumnName("idxacthucdatlich");

                entity.HasOne(d => d.IdxacthucdatlichNavigation)
                    .WithMany(p => p.CacChuyenKhoaChuyenMonDangKies)
                    .HasForeignKey(d => d.Idxacthucdatlich)
                    .HasConstraintName("FK__CacChuyen__idxac__3D2915A8");
            });

            modelBuilder.Entity<ChucDanh>(entity =>
            {
                entity.HasKey(e => e.IdchucDanh)
                    .HasName("PK__ChucDanh__BF5D15ECEAE532FB");

                entity.ToTable("ChucDanh");

                entity.HasIndex(e => e.TenChucDanh, "UQ__ChucDanh__BAAFE715DBB69A5E")
                    .IsUnique();

                entity.Property(e => e.IdchucDanh).HasColumnName("IDChucDanh");

                entity.Property(e => e.TenChucDanh).HasMaxLength(50);
            });

            modelBuilder.Entity<ChucDanhBacSi>(entity =>
            {
                entity.HasKey(e => e.IdChucDanhBacSi)
                    .HasName("PK__ChucDanh__17DA8846E67AF287");

                entity.ToTable("ChucDanhBacSi");

                entity.Property(e => e.IdbacSi)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDBacSi");

                entity.Property(e => e.IdchucDanh).HasColumnName("IDChucDanh");

                entity.HasOne(d => d.IdbacSiNavigation)
                    .WithMany(p => p.ChucDanhBacSis)
                    .HasForeignKey(d => d.IdbacSi)
                    .HasConstraintName("FK__ChucDanhB__IDBac__6D0D32F4");

                entity.HasOne(d => d.IdchucDanhNavigation)
                    .WithMany(p => p.ChucDanhBacSis)
                    .HasForeignKey(d => d.IdchucDanh)
                    .HasConstraintName("FK__ChucDanhB__IDChu__6E01572D");
            });

            modelBuilder.Entity<ChuyenKhoa>(entity =>
            {
                entity.HasKey(e => e.IdchuyenKhoa)
                    .HasName("PK__ChuyenKh__8929FBE59995B237");

                entity.ToTable("ChuyenKhoa");

                entity.HasIndex(e => e.TenChuyenKhoa, "UQ__ChuyenKh__E7F9B928092FDB0B")
                    .IsUnique();

                entity.Property(e => e.IdchuyenKhoa).HasColumnName("IDChuyenKhoa");

                entity.Property(e => e.Anh).HasColumnType("text");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.TenChuyenKhoa).HasMaxLength(50);
            });

            modelBuilder.Entity<ChuyenKhoaPhongKham>(entity =>
            {
                entity.HasKey(e => e.IdchuyenKhoaPhongKham)
                    .HasName("PK__ChuyenKh__9F1349A6DA7BF990");

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
                    .HasConstraintName("FK__ChuyenKho__IDChu__571DF1D5");

                entity.HasOne(d => d.IdphongKhamNavigation)
                    .WithMany(p => p.ChuyenKhoaPhongKhams)
                    .HasForeignKey(d => d.IdphongKham)
                    .HasConstraintName("FK__ChuyenKho__IDPho__5812160E");
            });

            modelBuilder.Entity<ChuyenMon>(entity =>
            {
                entity.HasKey(e => e.IdChuyenMon)
                    .HasName("PK__ChuyenMo__314169CBFDD31D16");

                entity.ToTable("ChuyenMon");

                entity.Property(e => e.AnhChuyeMon).HasColumnType("text");

                entity.Property(e => e.MoTaChuyenMon).HasColumnType("ntext");

                entity.Property(e => e.TenChuyenMon).HasMaxLength(50);
            });

            modelBuilder.Entity<ChuyenMoncoSo>(entity =>
            {
                entity.HasKey(e => e.IdchuyenMonCoSo)
                    .HasName("PK__ChuyenMo__75E12AF987FB87FF");

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
                    .HasConstraintName("FK__ChuyenMon__IDChu__0C85DE4D");

                entity.HasOne(d => d.IdcoSoDichVuKhacNavigation)
                    .WithMany(p => p.ChuyenMoncoSos)
                    .HasForeignKey(d => d.IdcoSoDichVuKhac)
                    .HasConstraintName("FK__ChuyenMon__IDCoS__0D7A0286");
            });

            modelBuilder.Entity<CoSoDichVuKhac>(entity =>
            {
                entity.HasKey(e => e.IdcoSoDichVuKhac)
                    .HasName("PK__CoSoDich__12A97EE50C071681");

                entity.ToTable("CoSoDichVuKhac");

                entity.Property(e => e.IdcoSoDichVuKhac)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDCoSoDichVuKhac");

                entity.Property(e => e.AnhDaiDienCoSo).HasColumnType("text");

                entity.Property(e => e.ChuyenMon).HasColumnType("ntext");

                entity.Property(e => e.Danhgia)
                    .HasColumnName("danhgia")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DiaChi).HasColumnType("ntext");

                entity.Property(e => e.HinhAnhCoSo).HasColumnType("text");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdxaPhuong).HasColumnName("IDXaPhuong");

                entity.Property(e => e.LoiGioiThieu).HasColumnType("ntext");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.NgayMoCoSo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Solandatlich)
                    .HasColumnName("solandatlich")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TenCoSo).HasMaxLength(50);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.Property(e => e.TrangThietBi).HasColumnType("ntext");

                entity.Property(e => e.ViTri).HasColumnType("text");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.CoSoDichVuKhacs)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK__CoSoDichV__IDNgu__02FC7413");

                entity.HasOne(d => d.IdxaPhuongNavigation)
                    .WithMany(p => p.CoSoDichVuKhacs)
                    .HasForeignKey(d => d.IdxaPhuong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSoDichV__IDXaP__03F0984C");
            });

            modelBuilder.Entity<DanhGiaCoso>(entity =>
            {
                entity.HasKey(e => e.IdDanhGiaCoso)
                    .HasName("PK__DanhGiaC__C824DCCEBCFE91D1");

                entity.ToTable("DanhGiaCoso");

                entity.Property(e => e.IdDanhGiaCoso).HasColumnName("idDanhGiaCoso");

                entity.Property(e => e.IdbacSi)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDBacSi");

                entity.Property(e => e.IdcoSoDichVuKhac)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDCoSoDichVuKhac");

                entity.Property(e => e.IdnhanVienCoSo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDNhanVienCoSo");

                entity.Property(e => e.IdphongKham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDPhongKham");

                entity.Property(e => e.NhanXet).HasColumnType("ntext");

                entity.HasOne(d => d.IdbacSiNavigation)
                    .WithMany(p => p.DanhGiaCosos)
                    .HasForeignKey(d => d.IdbacSi)
                    .HasConstraintName("FK__DanhGiaCo__IDBac__44CA3770");

                entity.HasOne(d => d.IdcoSoDichVuKhacNavigation)
                    .WithMany(p => p.DanhGiaCosos)
                    .HasForeignKey(d => d.IdcoSoDichVuKhac)
                    .HasConstraintName("FK__DanhGiaCo__IDCoS__46B27FE2");

                entity.HasOne(d => d.IdnguoidanhgiaNavigation)
                    .WithMany(p => p.DanhGiaCosos)
                    .HasForeignKey(d => d.Idnguoidanhgia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DanhGiaCo__Idngu__43D61337");

                entity.HasOne(d => d.IdnhanVienCoSoNavigation)
                    .WithMany(p => p.DanhGiaCosos)
                    .HasForeignKey(d => d.IdnhanVienCoSo)
                    .HasConstraintName("FK__DanhGiaCo__IDNha__45BE5BA9");

                entity.HasOne(d => d.IdphongKhamNavigation)
                    .WithMany(p => p.DanhGiaCosos)
                    .HasForeignKey(d => d.IdphongKham)
                    .HasConstraintName("FK__DanhGiaCo__IDPho__47A6A41B");
            });

            modelBuilder.Entity<DatLich>(entity =>
            {
                entity.HasKey(e => e.IddatLich)
                    .HasName("PK__DatLich__CE66721CA12D4B14");

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
                    .HasConstraintName("FK__DatLich__IDKeHoa__787EE5A0");
            });

            modelBuilder.Entity<DatLichNhanVienCoSo>(entity =>
            {
                entity.HasKey(e => e.IddatLichNhanVienCoSo)
                    .HasName("PK__DatLichN__BE648159BD4CDCF8");

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
                    .HasConstraintName("FK__DatLichNh__IDKeH__245D67DE");
            });

            modelBuilder.Entity<KeHoachKham>(entity =>
            {
                entity.HasKey(e => e.IdkeHoachKham)
                    .HasName("PK__KeHoachK__1312058979D29928");

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
                    .HasConstraintName("FK__KeHoachKh__IDBac__74AE54BC");
            });

            modelBuilder.Entity<KeHoachNhanVienCoSo>(entity =>
            {
                entity.HasKey(e => e.IdkeHoachNhanVienCoSo)
                    .HasName("PK__KeHoachN__54395B91C1184D41");

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
                    .HasConstraintName("FK__KeHoachNh__IDNha__208CD6FA");
            });

            modelBuilder.Entity<LichHen>(entity =>
            {
                entity.HasKey(e => e.IdLichHen)
                    .HasName("PK__LichHen__C82CFE9BFD037E4F");

                entity.ToTable("LichHen");

                entity.Property(e => e.IdLichHen).HasColumnName("idLichHen");

                entity.Property(e => e.GioHen)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdNguoiDungHenLich).HasColumnName("idNguoiDungHenLich");

                entity.Property(e => e.LinkHen).HasColumnType("text");

                entity.Property(e => e.NgayHen).HasColumnType("date");

                entity.Property(e => e.TrangThaiLichHen).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdNguoiDungHenLichNavigation)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.IdNguoiDungHenLich)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LichHen__idNguoi__40058253");
            });

            modelBuilder.Entity<LoaiHinhDichVu>(entity =>
            {
                entity.HasKey(e => e.IdLoaiHinhDichVu)
                    .HasName("PK__LoaiHinh__73E1A76A8AB42041");

                entity.ToTable("LoaiHinhDichVu");

                entity.HasIndex(e => e.TenLoaiHinhDichVu, "UQ__LoaiHinh__83E3BF1D195CC3BA")
                    .IsUnique();

                entity.Property(e => e.IdLoaiHinhDichVu).HasColumnName("idLoaiHinhDichVu");

                entity.Property(e => e.TenLoaiHinhDichVu).HasMaxLength(50);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.IdNguoiDung)
                    .HasName("PK__NguoiDun__BE010FC9D1944982");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.SoDienThoai, "UQ__NguoiDun__0389B7BDC0CF329E")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534E2E7163E")
                    .IsUnique();

                entity.HasIndex(e => e.TaiKhoan, "UQ__NguoiDun__D5B8C7F036532D8C")
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

            modelBuilder.Entity<NhanVienCoSo>(entity =>
            {
                entity.HasKey(e => e.IdnhanVienCoSo)
                    .HasName("PK__NhanVien__45007A4F30BD5715");

                entity.ToTable("NhanVienCoSo");

                entity.HasIndex(e => e.SoDienThoaiNhanVienCoSo, "UQ__NhanVien__A8A79DF41D48F579")
                    .IsUnique();

                entity.HasIndex(e => e.Cccd, "UQ__NhanVien__A955A0AABA349DA7")
                    .IsUnique();

                entity.HasIndex(e => e.EmailNhanVienCoSo, "UQ__NhanVien__D16177E438EF238E")
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

                entity.Property(e => e.Danhgia)
                    .HasColumnName("danhgia")
                    .HasDefaultValueSql("((0))");

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

                entity.Property(e => e.Solandatlich)
                    .HasColumnName("solandatlich")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdcoSoDichVuKhacNavigation)
                    .WithMany(p => p.NhanVienCoSos)
                    .HasForeignKey(d => d.IdcoSoDichVuKhac)
                    .HasConstraintName("FK__NhanVienC__IDCoS__1332DBDC");

                entity.HasOne(d => d.IdnguoidungNavigation)
                    .WithMany(p => p.NhanVienCoSos)
                    .HasForeignKey(d => d.Idnguoidung)
                    .HasConstraintName("FK__NhanVienC__IDNgu__160F4887");

                entity.HasOne(d => d.IdquyenNavigation)
                    .WithMany(p => p.NhanVienCoSos)
                    .HasForeignKey(d => d.Idquyen)
                    .HasConstraintName("FK__NhanVienC__IDQuy__14270015");
            });

            modelBuilder.Entity<PhanLoaiBacSiChuyenKhoa>(entity =>
            {
                entity.HasKey(e => e.IdphanLoaiBacSiChuyenKhoa)
                    .HasName("PK__PhanLoai__ABD2E476A05F5603");

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
                    .HasConstraintName("FK__PhanLoaiB__IDBac__70DDC3D8");

                entity.HasOne(d => d.IdchuyenKhoaNavigation)
                    .WithMany(p => p.PhanLoaiBacSiChuyenKhoas)
                    .HasForeignKey(d => d.IdchuyenKhoa)
                    .HasConstraintName("FK__PhanLoaiB__IDChu__71D1E811");
            });

            modelBuilder.Entity<PhanLoaiChuyenKhoaNhanVien>(entity =>
            {
                entity.HasKey(e => e.IdphanLoaiBacSiChuyenKhoa)
                    .HasName("PK__PhanLoai__ABD2E4765671B744");

                entity.ToTable("PhanLoaiChuyenKhoaNhanVien");

                entity.Property(e => e.IdphanLoaiBacSiChuyenKhoa).HasColumnName("IDPhanLoaiBacSiChuyenKHoa");

                entity.Property(e => e.IdnhanVienCoSo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("IDNhanVienCoSo");

                entity.HasOne(d => d.ChuyenMoncoSoNavigation)
                    .WithMany(p => p.PhanLoaiChuyenKhoaNhanViens)
                    .HasForeignKey(d => d.ChuyenMoncoSo)
                    .HasConstraintName("FK__PhanLoaiC__Chuye__1DB06A4F");

                entity.HasOne(d => d.IdnhanVienCoSoNavigation)
                    .WithMany(p => p.PhanLoaiChuyenKhoaNhanViens)
                    .HasForeignKey(d => d.IdnhanVienCoSo)
                    .HasConstraintName("FK__PhanLoaiC__IDNha__1CBC4616");
            });

            modelBuilder.Entity<PhongKham>(entity =>
            {
                entity.HasKey(e => e.IdphongKham)
                    .HasName("PK__PhongKha__32AA370AD2EAEFED");

                entity.ToTable("PhongKham");

                entity.HasIndex(e => e.TenPhongKham, "UQ__PhongKha__013DD7AA601416EF")
                    .IsUnique();

                entity.Property(e => e.IdphongKham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDPhongKham");

                entity.Property(e => e.AnhDaiDienPhongKham).HasColumnType("text");

                entity.Property(e => e.BaoHiem).HasDefaultValueSql("((0))");

                entity.Property(e => e.ChuyenMon).HasColumnType("ntext");

                entity.Property(e => e.Danhgia)
                    .HasColumnName("danhgia")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DiaChi).HasColumnType("ntext");

                entity.Property(e => e.HinhAnh).HasColumnType("text");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdxaPhuong).HasColumnName("IDXaPhuong");

                entity.Property(e => e.LoiGioiThieu).HasColumnType("ntext");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.NgayMoPhongKham)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Solandatlich)
                    .HasColumnName("solandatlich")
                    .HasDefaultValueSql("((0))");

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
                    .HasName("PK__QuanHuye__29AC36EEE0C4B434");

                entity.ToTable("QuanHuyen");

                entity.HasIndex(e => e.TenQuanHuyen, "UQ__QuanHuye__7A15A2B8C739FAAF")
                    .IsUnique();

                entity.Property(e => e.IdquanHuyen).HasColumnName("IDQuanHuyen");

                entity.Property(e => e.TenQuanHuyen).HasMaxLength(50);
            });

            modelBuilder.Entity<QuanTriVien>(entity =>
            {
                entity.HasKey(e => e.IdquanTriVien)
                    .HasName("PK__QuanTriV__C7DEE2339E0BF4F6");

                entity.ToTable("QuanTriVien");

                entity.HasIndex(e => e.SoDienThoai, "UQ__QuanTriV__0389B7BDA9B33FC7")
                    .IsUnique();

                entity.HasIndex(e => e.TaiKhoanQtv, "UQ__QuanTriV__630AB4F37DB0D151")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__QuanTriV__A9D1053499E1FF78")
                    .IsUnique();

                entity.Property(e => e.IdquanTriVien).HasColumnName("IDQuanTriVien");

                entity.Property(e => e.AnhQtv)
                    .HasColumnType("text")
                    .HasColumnName("AnhQTV");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CCCD");

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
                    .HasName("PK__Quyen__FB764FA17F6B8187");

                entity.ToTable("Quyen");

                entity.HasIndex(e => e.TenQuyen, "UQ__Quyen__5637EE790B37DA09")
                    .IsUnique();

                entity.Property(e => e.Idquyen).HasColumnName("IDquyen");

                entity.Property(e => e.TenQuyen).HasMaxLength(20);
            });

            modelBuilder.Entity<TaoLich>(entity =>
            {
                entity.HasKey(e => e.IdtaoLich)
                    .HasName("PK__TaoLich__4CD8CD8DD32FD4C4");

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
                    .HasConstraintName("FK__TaoLich__IDDatLi__7E37BEF6");

                entity.HasOne(d => d.IdnguoiDungDatLichNavigation)
                    .WithMany(p => p.TaoLiches)
                    .HasForeignKey(d => d.IdnguoiDungDatLich)
                    .HasConstraintName("FK__TaoLich__IDNguoi__7D439ABD");
            });

            modelBuilder.Entity<TaoLichNhanVienCoSo>(entity =>
            {
                entity.HasKey(e => e.IdtaoLichNhanVienCoSo)
                    .HasName("PK__TaoLichN__1B3A9FD9B1D07688");

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
                    .HasConstraintName("FK__TaoLichNh__IDDat__2A164134");

                entity.HasOne(d => d.IdnguoiDungDatLichNavigation)
                    .WithMany(p => p.TaoLichNhanVienCoSos)
                    .HasForeignKey(d => d.IdnguoiDungDatLich)
                    .HasConstraintName("FK__TaoLichNh__IDNgu__29221CFB");
            });

            modelBuilder.Entity<XaPhuong>(entity =>
            {
                entity.HasKey(e => e.IdxaPhuong)
                    .HasName("PK__XaPhuong__9B0E8729DB184773");

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
                    .HasName("PK__XacThucD__D7D33CA5DAC0F68A");

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
                    .HasConstraintName("FK__XacThucDa__IDNgu__3587F3E0");

                entity.HasOne(d => d.IdquanTriVienNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.IdquanTriVien)
                    .HasConstraintName("FK__XacThucDa__IDQua__367C1819");

                entity.HasOne(d => d.LoaiHinhDangKyNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.LoaiHinhDangKy)
                    .HasConstraintName("FK__XacThucDa__LoaiH__37703C52");

                entity.HasOne(d => d.XaPhuongNavigation)
                    .WithMany(p => p.XacThucDangKyMoCoSoYtes)
                    .HasForeignKey(d => d.XaPhuong)
                    .HasConstraintName("FK__XacThucDa__XaPhu__395884C4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
