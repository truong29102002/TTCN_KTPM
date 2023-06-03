using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestPJ.Models
{
    public partial class QLCHXeContext : DbContext
    {
        public QLCHXeContext()
        {
        }

        public QLCHXeContext(DbContextOptions<QLCHXeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<GiaThue> GiaThues { get; set; } = null!;
        public virtual DbSet<HangXe> HangXes { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; } = null!;
        public virtual DbSet<HopDongThue> HopDongThues { get; set; } = null!;
        public virtual DbSet<HopDongThueChiTiet> HopDongThueChiTiets { get; set; } = null!;
        public virtual DbSet<Mau> Maus { get; set; } = null!;
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<Oto> Otos { get; set; } = null!;
        public virtual DbSet<PhieuNhapChiTiet> PhieuNhapChiTiets { get; set; } = null!;
        public virtual DbSet<Phieunhap> Phieunhaps { get; set; } = null!;
        public virtual DbSet<PhuongTien> PhuongTiens { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<ThongTinCongTy> ThongTinCongTies { get; set; } = null!;
        public virtual DbSet<ThongTinKhachHang> ThongTinKhachHangs { get; set; } = null!;
        public virtual DbSet<TrangThaiThue> TrangThaiThues { get; set; } = null!;
        public virtual DbSet<XeMay> XeMays { get; set; } = null!;
        public virtual DbSet<XeTai> XeTais { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLCHXe;Persist Security Info=True;User ID=sa;Password=12345678; Encrypt=True; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.TaiKhoan);

                entity.ToTable("Account");

                entity.Property(e => e.TaiKhoan).HasMaxLength(50);

                entity.Property(e => e.Matkhau).HasMaxLength(50);
            });

            modelBuilder.Entity<GiaThue>(entity =>
            {
                entity.ToTable("GiaThue");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.GiaThue1).HasColumnName("GiaThue");

                entity.Property(e => e.LoaiXe).HasMaxLength(10);
            });

            modelBuilder.Entity<HangXe>(entity =>
            {
                entity.HasKey(e => e.IdHangXe);

                entity.ToTable("HangXe");

                entity.Property(e => e.IdHangXe).HasMaxLength(10);

                entity.Property(e => e.Loaixe).HasMaxLength(50);

                entity.Property(e => e.Tenhanngxe).HasMaxLength(50);
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(10)
                    .HasColumnName("MaHD");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .HasColumnName("MaNV");

                entity.Property(e => e.Ngaylap).HasColumnType("date");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_HoaDon_ThongTinKhachHang");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_HoaDon_NhanVien");
            });

            modelBuilder.Entity<HoaDonChiTiet>(entity =>
            {
                entity.HasKey(e => new { e.MaHd, e.IdPt });

                entity.ToTable("HoaDonChiTiet");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(10)
                    .HasColumnName("MaHD");

                entity.Property(e => e.IdPt)
                    .HasMaxLength(50)
                    .HasColumnName("IdPT");

                entity.HasOne(d => d.IdPtNavigation)
                    .WithMany(p => p.HoaDonChiTiets)
                    .HasForeignKey(d => d.IdPt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HoaDonChiTiet_PhuongTien");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.HoaDonChiTiets)
                    .HasForeignKey(d => d.MaHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HoaDonChiTiet_HoaDon");
            });

            modelBuilder.Entity<HopDongThue>(entity =>
            {
                entity.HasKey(e => e.MaHdthue);

                entity.ToTable("HopDongThue");

                entity.Property(e => e.MaHdthue)
                    .HasMaxLength(10)
                    .HasColumnName("MaHDThue");

                entity.Property(e => e.IdPt)
                    .HasMaxLength(50)
                    .HasColumnName("IdPT");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .HasColumnName("MaNV");

                entity.Property(e => e.Makh)
                    .HasMaxLength(10)
                    .HasColumnName("makh");

                entity.Property(e => e.Mathue).HasMaxLength(10);

                entity.HasOne(d => d.IdPtNavigation)
                    .WithMany(p => p.HopDongThues)
                    .HasForeignKey(d => d.IdPt)
                    .HasConstraintName("FK_HopDongThue_PhuongTien");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HopDongThues)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_HopDongThue_NhanVien");

                entity.HasOne(d => d.MakhNavigation)
                    .WithMany(p => p.HopDongThues)
                    .HasForeignKey(d => d.Makh)
                    .HasConstraintName("FK_HopDongThue_ThongTinKhachHang");

                entity.HasOne(d => d.MathueNavigation)
                    .WithMany(p => p.HopDongThues)
                    .HasForeignKey(d => d.Mathue)
                    .HasConstraintName("FK_HopDongThue_TrangThaiThue");
            });

            modelBuilder.Entity<HopDongThueChiTiet>(entity =>
            {
                entity.HasKey(e => new { e.MaHdthue, e.IdPt });

                entity.ToTable("HopDongThueChiTiet");

                entity.Property(e => e.MaHdthue)
                    .HasMaxLength(10)
                    .HasColumnName("MaHDThue");

                entity.Property(e => e.IdPt)
                    .HasMaxLength(50)
                    .HasColumnName("IdPT");

                entity.HasOne(d => d.IdPtNavigation)
                    .WithMany(p => p.HopDongThueChiTiets)
                    .HasForeignKey(d => d.IdPt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HopDongThueChiTiet_PhuongTien");

                entity.HasOne(d => d.MaHdthueNavigation)
                    .WithMany(p => p.HopDongThueChiTiets)
                    .HasForeignKey(d => d.MaHdthue)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HopDongThueChiTiet_HopDongThue");
            });

            modelBuilder.Entity<Mau>(entity =>
            {
                entity.HasKey(e => e.Mamau);

                entity.ToTable("Mau");

                entity.Property(e => e.Mamau).HasMaxLength(10);

                entity.Property(e => e.Tenmau).HasMaxLength(20);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNcc);

                entity.ToTable("NhaCungCap");

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.DiaChiNcc)
                    .HasMaxLength(200)
                    .HasColumnName("DiaChiNCC");

                entity.Property(e => e.MaStncc)
                    .HasMaxLength(20)
                    .HasColumnName("MaSTNCC");

                entity.Property(e => e.SoDtncc)
                    .HasMaxLength(10)
                    .HasColumnName("SoDTNCC");

                entity.Property(e => e.TenNcc)
                    .HasMaxLength(50)
                    .HasColumnName("TenNCC");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv);

                entity.ToTable("NhanVien");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .HasColumnName("MaNV");

                entity.Property(e => e.DiaChiNv)
                    .HasMaxLength(200)
                    .HasColumnName("DiaChiNV");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .HasColumnName("gender");

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sodienthoai).HasMaxLength(10);

                entity.Property(e => e.Taikhoan).HasMaxLength(50);

                entity.Property(e => e.TenNv)
                    .HasMaxLength(50)
                    .HasColumnName("TenNV");

                entity.HasOne(d => d.TaikhoanNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.Taikhoan)
                    .HasConstraintName("FK_NhanVien_Account");
            });

            modelBuilder.Entity<Oto>(entity =>
            {
                entity.HasKey(e => e.IdOto);

                entity.ToTable("Oto");

                entity.Property(e => e.IdOto).HasMaxLength(10);

                entity.Property(e => e.IdPt)
                    .HasMaxLength(50)
                    .HasColumnName("IdPT");

                entity.Property(e => e.Kieudongco).HasMaxLength(20);

                entity.HasOne(d => d.IdPtNavigation)
                    .WithMany(p => p.Otos)
                    .HasForeignKey(d => d.IdPt)
                    .HasConstraintName("FK_Oto_PhuongTien");
            });

            modelBuilder.Entity<PhieuNhapChiTiet>(entity =>
            {
                entity.HasKey(e => new { e.Maphieunhap, e.IdPt });

                entity.ToTable("PhieuNhapChiTiet");

                entity.Property(e => e.Maphieunhap).HasMaxLength(10);

                entity.Property(e => e.IdPt)
                    .HasMaxLength(50)
                    .HasColumnName("IdPT");

                entity.Property(e => e.GhiChu).HasMaxLength(200);

                entity.HasOne(d => d.IdPtNavigation)
                    .WithMany(p => p.PhieuNhapChiTiets)
                    .HasForeignKey(d => d.IdPt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhapChiTiet_PhuongTien");

                entity.HasOne(d => d.MaphieunhapNavigation)
                    .WithMany(p => p.PhieuNhapChiTiets)
                    .HasForeignKey(d => d.Maphieunhap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhieuNhapChiTiet_Phieunhap");
            });

            modelBuilder.Entity<Phieunhap>(entity =>
            {
                entity.HasKey(e => e.Maphieunhap);

                entity.ToTable("Phieunhap");

                entity.Property(e => e.Maphieunhap).HasMaxLength(10);

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .HasColumnName("MaNV");

                entity.Property(e => e.Ngaynhap).HasColumnType("date");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.Phieunhaps)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK_Phieunhap_NhaCungCap");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.Phieunhaps)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_Phieunhap_NhanVien");
            });

            modelBuilder.Entity<PhuongTien>(entity =>
            {
                entity.HasKey(e => e.IdPt)
                    .HasName("PK_PhuongTien_1");

                entity.ToTable("PhuongTien");

                entity.Property(e => e.IdPt)
                    .HasMaxLength(50)
                    .HasColumnName("IdPT");

                entity.Property(e => e.Donvi).HasMaxLength(10);

                entity.Property(e => e.IdHangXe).HasMaxLength(10);

                entity.Property(e => e.Mamau).HasMaxLength(10);

                entity.Property(e => e.Mota).HasMaxLength(200);

                entity.Property(e => e.NamSx).HasColumnName("NamSX");

                entity.Property(e => e.Ngaynhap).HasColumnType("date");

                entity.Property(e => e.TenXe).HasMaxLength(100);

                entity.HasOne(d => d.IdHangXeNavigation)
                    .WithMany(p => p.PhuongTiens)
                    .HasForeignKey(d => d.IdHangXe)
                    .HasConstraintName("FK_PhuongTien_HangXe");

                entity.HasOne(d => d.MamauNavigation)
                    .WithMany(p => p.PhuongTiens)
                    .HasForeignKey(d => d.Mamau)
                    .HasConstraintName("FK_PhuongTien_Mau");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("test");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(10)
                    .HasColumnName("MaHD");

                entity.Property(e => e.Ngaylap).HasColumnType("date");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");

                entity.Property(e => e.TenNv)
                    .HasMaxLength(50)
                    .HasColumnName("TenNV");

                entity.Property(e => e.TenXe).HasMaxLength(100);
            });

            modelBuilder.Entity<ThongTinCongTy>(entity =>
            {
                entity.HasKey(e => e.TenCongTy);

                entity.ToTable("ThongTinCongTy");

                entity.Property(e => e.TenCongTy).HasMaxLength(10);

                entity.Property(e => e.Diachicongty).HasMaxLength(200);

                entity.Property(e => e.Masothue).HasMaxLength(20);

                entity.Property(e => e.Sodtcongty).HasMaxLength(10);
            });

            modelBuilder.Entity<ThongTinKhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("ThongTinKhachHang");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.IdKh)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id_KH");

                entity.Property(e => e.Sodienthoai).HasMaxLength(10);

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<TrangThaiThue>(entity =>
            {
                entity.HasKey(e => e.Mathue);

                entity.ToTable("TrangThaiThue");

                entity.Property(e => e.Mathue).HasMaxLength(10);

                entity.Property(e => e.Ngaybatdau).HasColumnType("date");

                entity.Property(e => e.Ngayketthuc).HasColumnType("date");
            });

            modelBuilder.Entity<XeMay>(entity =>
            {
                entity.HasKey(e => e.IdXeMay);

                entity.ToTable("XeMay");

                entity.Property(e => e.IdXeMay).HasMaxLength(10);

                entity.Property(e => e.IdPt)
                    .HasMaxLength(50)
                    .HasColumnName("IdPT");

                entity.HasOne(d => d.IdPtNavigation)
                    .WithMany(p => p.XeMays)
                    .HasForeignKey(d => d.IdPt)
                    .HasConstraintName("FK_XeMay_PhuongTien");
            });

            modelBuilder.Entity<XeTai>(entity =>
            {
                entity.HasKey(e => e.XeTai1);

                entity.ToTable("XeTai");

                entity.Property(e => e.XeTai1)
                    .HasMaxLength(10)
                    .HasColumnName("XeTai");

                entity.Property(e => e.IdPt)
                    .HasMaxLength(50)
                    .HasColumnName("IdPT");

                entity.HasOne(d => d.IdPtNavigation)
                    .WithMany(p => p.XeTais)
                    .HasForeignKey(d => d.IdPt)
                    .HasConstraintName("FK_XeTai_PhuongTien");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
