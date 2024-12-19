using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CareCarAPI.Models
{
    public partial class CareCarContext : DbContext
    {
        public CareCarContext()
        {
        }

        public CareCarContext(DbContextOptions<CareCarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DichVu> DichVus { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<LichHen> LichHens { get; set; } = null!;
        public virtual DbSet<LienHe> LienHes { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<TrangThai> TrangThais { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-G107ADQ;Database=CareCar;Integrated security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DichVu>(entity =>
            {
                entity.ToTable("DichVu");

                entity.Property(e => e.DichVuId).HasColumnName("DichVuID");

                entity.Property(e => e.TenDichVu).HasMaxLength(50);
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.ToTable("KhachHang");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.SoDienThoai).HasMaxLength(50);

                entity.Property(e => e.TenKhachHang).HasMaxLength(50);
            });

            modelBuilder.Entity<LichHen>(entity =>
            {
                entity.ToTable("LichHen");

                entity.Property(e => e.LichHenId).HasColumnName("LichHenID");

                entity.Property(e => e.DichVuId).HasColumnName("DichVuID");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.Ngay).HasColumnType("datetime");

                entity.Property(e => e.Ngayhen).HasColumnType("datetime");

                entity.Property(e => e.ThanhToan).HasMaxLength(50);

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.Property(e => e.Xe).HasMaxLength(50);

                entity.HasOne(d => d.DichVu)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.DichVuId)
                    .HasConstraintName("FK_LichHen_DichVu");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.KhachHangId)
                    .HasConstraintName("FK_LichHen_KhachHang");

                entity.HasOne(d => d.TrangThai)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.TrangThaiId)
                    .HasConstraintName("FK_LichHen_TrangThai");
            });

            modelBuilder.Entity<LienHe>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("LienHe");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.ToTable("NhanVien");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.MatKhau).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.Quyen).HasMaxLength(10);

                entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            });

            modelBuilder.Entity<TrangThai>(entity =>
            {
                entity.ToTable("TrangThai");

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.Property(e => e.TenTrangThai).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
