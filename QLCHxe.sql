USE [master]
GO
/****** Object:  Database [QLCHXe]    Script Date: 6/12/2023 8:39:14 PM ******/
CREATE DATABASE [QLCHXe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLCHXe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QLCHXe.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLCHXe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QLCHXe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QLCHXe] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLCHXe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLCHXe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLCHXe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLCHXe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLCHXe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLCHXe] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLCHXe] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLCHXe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLCHXe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLCHXe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLCHXe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLCHXe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLCHXe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLCHXe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLCHXe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLCHXe] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLCHXe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLCHXe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLCHXe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLCHXe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLCHXe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLCHXe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLCHXe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLCHXe] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLCHXe] SET  MULTI_USER 
GO
ALTER DATABASE [QLCHXe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLCHXe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLCHXe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLCHXe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLCHXe] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLCHXe] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QLCHXe] SET QUERY_STORE = ON
GO
ALTER DATABASE [QLCHXe] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QLCHXe]
GO
/****** Object:  UserDefinedFunction [dbo].[tinhtien]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[tinhtien](@mahd nvarchar(10))
returns money 
as
begin
	declare @tongtien money
	set @tongtien =( select SUM(ThanhTien) from test where MaHD =@mahd )	
	return @tongtien
end
GO
/****** Object:  UserDefinedFunction [dbo].[tk_chitietxe]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[tk_chitietxe](@mahd nvarchar(10),@loaixe nvarchar(50))
returns @ds2 table 
(
	MAHD nvarchar(10),
	NGAYLAP date,
	TENXE nvarchar(100),
	TENNV nvarchar(50),
	TENKH nvarchar(50),
	SOLUONG money,
	GIA money,
	THANHTIEN money
)
as 
begin
	insert into @ds2 
	select HoaDon.MaHD,Ngaylap,TenXe,TenNV,TenKH,HoaDonChiTiet.Soluong,PhuongTien.Gia,HoaDonChiTiet.Soluong*PhuongTien.Gia as 'Thanh Tien' 
	from HoaDon inner join HoaDonChiTiet
	on HoaDon.MaHD = HoaDonChiTiet.MaHD inner join NhanVien 
	on NhanVien.MaNV = HoaDon.MaNV inner join ThongTinKhachHang 
	on ThongTinKhachHang.MaKH = HoaDon.MaKH inner join PhuongTien 
	on PhuongTien.IdPT = HoaDonChiTiet.IdPT inner join HangXe 
	on HangXe.IdHangXe = PhuongTien.IdHangXe
	where HoaDon.MaHD = @mahd and Loaixe = @loaixe
	return
end
GO
/****** Object:  UserDefinedFunction [dbo].[tk_xe]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[tk_xe](@tungay date,@denngay date,@loaixe nvarchar(50))
returns @ds table
(
	MAHD nvarchar(10),
	NGAYLAP date,
	TENNV nvarchar(50),
	TENKH nvarchar(50),
	SOLUONG int
)
as
begin 
	insert into @ds 
	select distinct HoaDon.MaHD,Ngaylap,TenNV,TenKH,SUM(HoaDonChiTiet.Soluong) as'So Luong' from HoaDon inner join HoaDonChiTiet
	on HoaDon.MaHD = HoaDonChiTiet.MaHD inner join NhanVien 
	on NhanVien.MaNV = HoaDon.MaNV inner join ThongTinKhachHang 
	on ThongTinKhachHang.MaKH = HoaDon.MaKH inner join PhuongTien 
	on PhuongTien.IdPT = HoaDonChiTiet.IdPT inner join HangXe 
	on HangXe.IdHangXe = PhuongTien.IdHangXe
	where (Ngaylap between @tungay and @denngay) and Loaixe = @loaixe
	group by HoaDon.MaHD,Ngaylap,TenNV,TenKH
	return
end
GO
/****** Object:  Table [dbo].[HangXe]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangXe](
	[IdHangXe] [nvarchar](10) NOT NULL,
	[Tenhanngxe] [nvarchar](50) NULL,
	[Loaixe] [nvarchar](50) NULL,
 CONSTRAINT [PK_HangXe] PRIMARY KEY CLUSTERED 
(
	[IdHangXe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhuongTien]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhuongTien](
	[IdPT] [nvarchar](50) NOT NULL,
	[NamSX] [int] NULL,
	[Gia] [float] NULL,
	[Mamau] [nvarchar](10) NULL,
	[TenXe] [nvarchar](100) NULL,
	[Ngaynhap] [date] NULL,
	[Soluong] [int] NULL,
	[Mota] [nvarchar](200) NULL,
	[Donvi] [nvarchar](10) NULL,
	[IdHangXe] [nvarchar](10) NULL,
 CONSTRAINT [PK_PhuongTien_1] PRIMARY KEY CLUSTERED 
(
	[IdPT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongTinKhachHang]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinKhachHang](
	[MaKH] [nvarchar](10) NOT NULL,
	[TenKH] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](200) NULL,
	[Sodienthoai] [nvarchar](10) NULL,
 CONSTRAINT [PK_ThongTinKhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [nvarchar](10) NOT NULL,
	[TenNV] [nvarchar](50) NULL,
	[NgaySinh] [date] NULL,
	[DiaChiNV] [nvarchar](200) NULL,
	[Sodienthoai] [nvarchar](10) NULL,
	[Taikhoan] [nvarchar](50) NULL,
	[gender] [nvarchar](50) NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[test]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[test] 
as 

	select HoaDon.MaHD,Ngaylap,TenXe,TenNV,TenKH,HoaDonChiTiet.Soluong,PhuongTien.Gia,HoaDonChiTiet.Soluong*PhuongTien.Gia as 'ThanhTien' 
from HoaDon inner join HoaDonChiTiet
	on HoaDon.MaHD = HoaDonChiTiet.MaHD inner join NhanVien 
	on NhanVien.MaNV = HoaDon.MaNV inner join ThongTinKhachHang 
	on ThongTinKhachHang.MaKH = HoaDon.MaKH inner join PhuongTien 
	on PhuongTien.IdPT = HoaDonChiTiet.IdPT inner join HangXe 
	on HangXe.IdHangXe = PhuongTien.IdHangXe
GO
/****** Object:  Table [dbo].[Account]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[TaiKhoan] [nvarchar](50) NOT NULL,
	[Matkhau] [nvarchar](100) NULL,
	[Quyen] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[TaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kho]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho](
	[id] [nchar](10) NOT NULL,
	[NhanVienThem] [nvarchar](250) NULL,
	[TenKho] [nvarchar](250) NULL,
	[DiaChiKho] [nvarchar](250) NULL,
	[DienTich] [float] NULL,
 CONSTRAINT [PK_Kho] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kho_PhuongTien]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho_PhuongTien](
	[idPT] [nvarchar](50) NOT NULL,
	[idKho] [nchar](10) NULL,
	[idMau] [nvarchar](10) NULL,
	[NamSx] [int] NULL,
	[Gia] [float] NULL,
	[TenXe] [nvarchar](100) NULL,
	[SoLuongKho] [int] NULL,
	[DonVi] [nvarchar](10) NULL,
	[Description] [nvarchar](200) NULL,
	[idHangXe] [nvarchar](10) NULL,
	[MaNCC] [nvarchar](10) NULL,
	[createAt] [date] NULL,
	[updateAt] [date] NULL,
	[nvUpdate] [nvarchar](250) NULL,
	[soLuongNhap] [int] NULL,
 CONSTRAINT [PK_Kho_PhuongTien] PRIMARY KEY CLUSTERED 
(
	[idPT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mau]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mau](
	[Mamau] [nvarchar](10) NOT NULL,
	[Tenmau] [nvarchar](20) NULL,
 CONSTRAINT [PK_Mau] PRIMARY KEY CLUSTERED 
(
	[Mamau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[MaNCC] [nvarchar](10) NOT NULL,
	[TenNCC] [nvarchar](50) NULL,
	[DiaChiNCC] [nvarchar](200) NULL,
	[SoDTNCC] [nvarchar](10) NULL,
 CONSTRAINT [PK_NhaCungCap] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order](
	[id] [nchar](10) NOT NULL,
	[soLuong] [nchar](10) NULL,
	[thanhtien] [nchar](10) NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orderDetails]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orderDetails](
	[tenXeMua] [nvarchar](250) NULL,
	[Gia] [float] NULL,
	[soLuongMua] [int] NULL,
	[id] [nchar](10) NOT NULL,
	[idKH] [nvarchar](10) NULL,
	[idOrder] [nchar](10) NULL,
 CONSTRAINT [PK_orderDetails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oto]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oto](
	[IdOto] [nvarchar](10) NOT NULL,
	[Sochongoi] [int] NULL,
	[Kieudongco] [nvarchar](20) NULL,
	[IdPT] [nvarchar](50) NULL,
 CONSTRAINT [PK_Oto] PRIMARY KEY CLUSTERED 
(
	[IdOto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oto_kho]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oto_kho](
	[idOtoKho] [nvarchar](10) NOT NULL,
	[Sochongoi] [int] NULL,
	[KieuDongCo] [nvarchar](20) NULL,
	[idPTKho] [nvarchar](50) NULL,
 CONSTRAINT [PK_Oto_kho] PRIMARY KEY CLUSTERED 
(
	[idOtoKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongTinCongTy]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinCongTy](
	[TenCongTy] [nvarchar](10) NOT NULL,
	[Diachicongty] [nvarchar](200) NULL,
	[Sodtcongty] [nvarchar](10) NULL,
	[Masothue] [nvarchar](20) NULL,
 CONSTRAINT [PK_ThongTinCongTy] PRIMARY KEY CLUSTERED 
(
	[TenCongTy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[XeMay]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XeMay](
	[IdXeMay] [nvarchar](10) NOT NULL,
	[CongSuat] [int] NULL,
	[IdPT] [nvarchar](50) NULL,
 CONSTRAINT [PK_XeMay] PRIMARY KEY CLUSTERED 
(
	[IdXeMay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[XeMay_Kho]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XeMay_Kho](
	[idXMKho] [nvarchar](10) NOT NULL,
	[CongSuat] [int] NULL,
	[idPT] [nvarchar](50) NULL,
 CONSTRAINT [PK_XeMay_Kho] PRIMARY KEY CLUSTERED 
(
	[idXMKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[XeTai]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XeTai](
	[XeTai] [nvarchar](10) NOT NULL,
	[Trongtai] [float] NULL,
	[IdPT] [nvarchar](50) NULL,
 CONSTRAINT [PK_XeTai] PRIMARY KEY CLUSTERED 
(
	[XeTai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[XeTai_Kho]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XeTai_Kho](
	[idXeTaiKho] [nvarchar](10) NOT NULL,
	[TrongTai] [float] NULL,
	[idPT] [nvarchar](50) NULL,
 CONSTRAINT [PK_XeTai_Kho] PRIMARY KEY CLUSTERED 
(
	[idXeTaiKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([TaiKhoan], [Matkhau], [Quyen]) VALUES (N'admin', N'$2a$11$9Tn0VKGiiPUOjnCD/qqio.9HMExONpJSl9Ez5x57SwdyKN6nID0X6', 1)
INSERT [dbo].[Account] ([TaiKhoan], [Matkhau], [Quyen]) VALUES (N'nv', N'$2a$11$aTJWB/jp.FjQw/UOvKSzqeEyoeVqgNj8/VZWgbxUydgYyY/yWL2Yq', 0)
INSERT [dbo].[Account] ([TaiKhoan], [Matkhau], [Quyen]) VALUES (N'nv1', N'$2a$11$y.qzpWDdsrIfClbVGMhZw.sa0nDjJg/k3Kv3tRIXHhJTFPnwA2mLO', 0)
INSERT [dbo].[Account] ([TaiKhoan], [Matkhau], [Quyen]) VALUES (N'truongadmin', N'$2a$11$8iafCFb2HZLra1MAA1U0jOf8iL5xVSUYnfCbjVWpegvAsk32uOv/6', 1)
INSERT [dbo].[Account] ([TaiKhoan], [Matkhau], [Quyen]) VALUES (N'truongnv', N'$2a$11$d1lzi1pR8WHnyntIEh23XOdyM7SBybLI3ud/ItV436XDnSr1pbOEu', 0)
GO
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'1', N'Honda', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'10', N'Aprilia', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'11', N'KTM', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'12', N'Benelli', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'14', N'Cửu Long TMT', N'Xe tải')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'15', N'Hyundai', N'Xe tải')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'16', N'Isuzu', N'Xe tải')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'17', N'Chiến Thắng', N'Xe tải')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'18', N'Daewoo', N'Xe tải')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'19', N'Fuso', N'Xe tải')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'2', N'Yamaha', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'20', N'Veam', N'Xe tải')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'21', N'Jac', N'Xe tải')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'22', N'THACO KIA', N'Xe tải')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'3', N'Piaggio', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'30', N'Vinfast', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'31', N'Mazda', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'32', N'Toyota', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'33', N'Mercedes', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'34', N'Lexus', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'35', N'Ford', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'36', N'Hyundai', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'37', N'BMW', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'38', N'Audi', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'39', N'Ferrari', N'Ô tô')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'4', N'SYM', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'5', N'Suzuki', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'6', N'Triumph', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'7', N'Harley Davidson', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'8', N'Ducati', N'Xe Máy')
INSERT [dbo].[HangXe] ([IdHangXe], [Tenhanngxe], [Loaixe]) VALUES (N'9', N'Kawasaki', N'Xe Máy')
GO
INSERT [dbo].[Kho] ([id], [NhanVienThem], [TenKho], [DiaChiKho], [DienTich]) VALUES (N'KH135B    ', N'NV9942', N'kho 3', N'Nam Định', 1200)
INSERT [dbo].[Kho] ([id], [NhanVienThem], [TenKho], [DiaChiKho], [DienTich]) VALUES (N'KH164E    ', N'NV2843', N'kho 4', N'Nam Định', 1200)
INSERT [dbo].[Kho] ([id], [NhanVienThem], [TenKho], [DiaChiKho], [DienTich]) VALUES (N'KH2338    ', N'NV2552', N'kho 2', N'Nam Định', 1200)
INSERT [dbo].[Kho] ([id], [NhanVienThem], [TenKho], [DiaChiKho], [DienTich]) VALUES (N'KHE68     ', N'NV3308', N'Kho 5', N'Ha Noi', 123)
INSERT [dbo].[Kho] ([id], [NhanVienThem], [TenKho], [DiaChiKho], [DienTich]) VALUES (N'KHFAA     ', N'NV9942', N'kho 1', N'Nam Định', 1200)
GO
INSERT [dbo].[Kho_PhuongTien] ([idPT], [idKho], [idMau], [NamSx], [Gia], [TenXe], [SoLuongKho], [DonVi], [Description], [idHangXe], [MaNCC], [createAt], [updateAt], [nvUpdate], [soLuongNhap]) VALUES (N'PT105A', N'KHE68     ', N'37', 2016, 200000, N'VF8', 0, N'c', N'xe vf', N'10', N'32', CAST(N'2023-06-08' AS Date), CAST(N'2023-06-10' AS Date), N'NV9942', 3)
INSERT [dbo].[Kho_PhuongTien] ([idPT], [idKho], [idMau], [NamSx], [Gia], [TenXe], [SoLuongKho], [DonVi], [Description], [idHangXe], [MaNCC], [createAt], [updateAt], [nvUpdate], [soLuongNhap]) VALUES (N'PT156E', N'KHE68     ', N'1', 2007, 1110, N'Yamaha', 0, N'c', N'Xe phân khối lớn', N'8', N'5', CAST(N'2023-06-07' AS Date), CAST(N'2023-06-11' AS Date), N'NV9942', 9)
INSERT [dbo].[Kho_PhuongTien] ([idPT], [idKho], [idMau], [NamSx], [Gia], [TenXe], [SoLuongKho], [DonVi], [Description], [idHangXe], [MaNCC], [createAt], [updateAt], [nvUpdate], [soLuongNhap]) VALUES (N'PT1937', N'KH164E    ', N'11', 2015, 100000, N'Air Blade', 0, N'c', N'xe cu', N'10', N'5', CAST(N'2023-06-08' AS Date), CAST(N'2023-06-11' AS Date), N'NV9942', 9)
INSERT [dbo].[Kho_PhuongTien] ([idPT], [idKho], [idMau], [NamSx], [Gia], [TenXe], [SoLuongKho], [DonVi], [Description], [idHangXe], [MaNCC], [createAt], [updateAt], [nvUpdate], [soLuongNhap]) VALUES (N'PT1D38', N'KHE68     ', N'51', 2017, 3000000, N'VF', 0, N'c', N'xe cỡ nhỏ', N'10', N'7', CAST(N'2023-06-09' AS Date), CAST(N'2023-06-10' AS Date), N'NV9942', 6)
INSERT [dbo].[Kho_PhuongTien] ([idPT], [idKho], [idMau], [NamSx], [Gia], [TenXe], [SoLuongKho], [DonVi], [Description], [idHangXe], [MaNCC], [createAt], [updateAt], [nvUpdate], [soLuongNhap]) VALUES (N'PT433', N'KHE68     ', N'51', 2017, 3000000, N'VF', 0, N'c', N'xe cỡ nhỏ', N'10', N'7', CAST(N'2023-06-10' AS Date), CAST(N'2023-06-10' AS Date), N'NV9942', 8)
INSERT [dbo].[Kho_PhuongTien] ([idPT], [idKho], [idMau], [NamSx], [Gia], [TenXe], [SoLuongKho], [DonVi], [Description], [idHangXe], [MaNCC], [createAt], [updateAt], [nvUpdate], [soLuongNhap]) VALUES (N'PT76C', N'KHE68     ', N'7', 2021, 200000, N'VF8', 0, N'c', N'xe vf', N'10', N'32', CAST(N'2023-06-08' AS Date), CAST(N'2023-06-10' AS Date), N'NV9942', 6)
INSERT [dbo].[Kho_PhuongTien] ([idPT], [idKho], [idMau], [NamSx], [Gia], [TenXe], [SoLuongKho], [DonVi], [Description], [idHangXe], [MaNCC], [createAt], [updateAt], [nvUpdate], [soLuongNhap]) VALUES (N'PTCE1', N'KH164E    ', N'2', 2023, 111, N'test', 0, N'c', N'1', N'10', N'7', CAST(N'2023-06-07' AS Date), CAST(N'2023-06-11' AS Date), N'NV9942', 9)
INSERT [dbo].[Kho_PhuongTien] ([idPT], [idKho], [idMau], [NamSx], [Gia], [TenXe], [SoLuongKho], [DonVi], [Description], [idHangXe], [MaNCC], [createAt], [updateAt], [nvUpdate], [soLuongNhap]) VALUES (N'PTD93', N'KH164E    ', N'30', 2015, 150000, N'Xe điện', 0, N'c', N'xe diện vf', N'10', N'32', CAST(N'2023-06-08' AS Date), CAST(N'2023-06-11' AS Date), N'NV9942', 9)
GO
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'1', N'Hổ phách')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'10', N'Da bò')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'11', N'Cam cháy')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'12', N'Hồng y')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'13', N'Đỏ yên chi')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'14', N'Men ngọc')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'15', N'Anh đào')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'16', N'Xanh hoàng hôn')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'17', N'Chàm')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'18', N'Xanh nõn chuối')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'19', N'Xanh cô ban')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'2', N'Ametit')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'20', N'Đồng')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'21', N'San hô')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'22', N'Kem')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'23', N'Đỏ thắm')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'24', N'Xanh lơ (cánh chả)')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'25', N'Lục bảo')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'26', N'Vàng kim loại')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'27', N'Xám')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'28', N'Xanh lá cây')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'29', N'Vòi voi')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'3', N'Xanh berin')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'30', N'Chàm')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'31', N'Ngọc thạch')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'32', N'Kaki')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'33', N'Oải hương')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'34', N'Vàng chanh')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'35', N'Hồng sẫm')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'36', N'Hạt dẻ')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'37', N'Cẩm quỳ')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'38', N'Hoa cà')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'39', N'Lam sẫm')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'4', N'Xanh da trời')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'40', N'Thổ hoàng')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'41', N'Ô liu')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'42', N'Da cam')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'43', N'Lan tím')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'44', N'Lòng đào')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'45', N'Dừa cạn')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'46', N'Hồng')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'47', N'Mận')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'48', N'Xanh thủy tinh')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'49', N'Hồng đất')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'5', N'Be')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'50', N'Tía')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'51', N'Đỏ')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'52', N'Cá hồi')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'53', N'Đỏ tươi')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'54', N'Nâu đen')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'55', N'Bạc')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'56', N'Nâu tanin')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'57', N'Mòng két')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'58', N'Xanh Thổ')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'59', N'Đỏ son')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'6', N'Nâu sẫm')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'60', N'Tím')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'61', N'Xanh crôm')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'62', N'Trắng')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'63', N'Vàng')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'7', N'Đen')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'8', N'Xanh dương')
INSERT [dbo].[Mau] ([Mamau], [Tenmau]) VALUES (N'9', N'Nâu')
GO
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'1', N'Công Ty CPTM & XNK Việt Hồng Chinh', N'Km 5 Đ. 9D KCN Nam, TP. Đông Hà, Tỉnh Quảng Trị', N'0942944357')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'10', N'Công Ty TNHH Việt Nhật', N'143 Thanh Am Thượng Thanh, Long Biên,Hà Nội', N'987980068')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'11', N'Công Ty Phát Triển Thắng Lợi', N' Khu 12, P. Kim Đức Tp. Việt Trì,Phú Thọ', N'913007911')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'12', N'Công Ty TNHH  Việt Nhật', N' 143 Thanh Am, Thượng Thanh Long Biên,Hà Nội', N'906219539')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'13', N'Công Ty Cổ Phần  Vĩnh Hưng', N' Đội 7, Yên Xá, X. Tân Triều H. Thanh Trì,Hà Nội', N'979951368')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'14', N'Công Ty TNHH Minh Khuê', N' Số Nhà 16, Ngách 159, Ngõ 192 Lê Trọng Tấn, P. Định Công, Q. Hoàng Mai,Hà Nội', N'2435810567')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'2', N'Công ty Mô tô Mã Lực', N'651 Âu Cơ, Phường Hòa Thạnh, Quận Tân Phú, TP Hồ Chí Minh (TPHCM)', N'0906358398')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'3', N'CÔNG TY TNHH HƯỚNG TUYẾT', N'Tiểu Khu Phú Mỹ, Thị Trấn Phú Xuyên, Huyện Phú Xuyên, Tp. Hà Nội', N'0943243636')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'30', N'Công ty Toyota Việt Nam', N'6PJ7+CRF, Phường Hùng Vương, Phúc Yên, Vĩnh Phúc', N'0211386810')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'31', N'Công ty THACO Trường Hải', N'541 Nguyễn Văn Cừ, Phường Gia Thuỵ, Quận Long Biên, Hà Nội.', N'0243875891')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'32', N'Thương hiệu ô tô Vinfast', N'Số 7, đường Bằng Lăng 1, Phường Việt Hưng, Quận Long Biên, Thành phố Hà Nội', N'1900232389')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'33', N'Công ty phân phối Ô tô Hòa Phát', N'2 Khu Phố, Bình Đức, Thuận An, Bình Dương', N'18002017')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'34', N'Công ty Ford Vietnam limited', N'23 P. Phan Chu Trinh, Phan Chu Trinh, Hoàn Kiếm, Hà Nội', N'0246263460')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'35', N'Công ty cổ phần Ô tô TMT', N'Số 4 Tôn Thất Tùng, Phường Trung Tự ,Quận Đống Đa, Hà Nội ', N'1900545462')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'4', N'CÔNG TY TNHH SẢN XUẤT LẮP RÁP TUẤN NGHĨA', N'Số 18 Trần Quang Diệu, Ô Chợ Dừa, Quận Đống Đa, TP Hà Nội (TPHN)', N'0243644864')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'5', N'CÔNG TY TNHH HOÀ BÌNH MINH', N'Lô 8A, Đường Đồng Khởi, P.Tân Hiệp, Tp.Biên Hoà, Đồng Nai', N'0613675522')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'6', N'CÔNG TY TNHH THƯƠNG MẠI NGỌC HOA', N'Số 98, Đường Cầu Gỗ, Q. Hoàn Kiếm, TP Hà Nội (TPHN)', N'2439320317')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'7', N' Công Ty  Ô Tô Bảo Ngọc', N'Phòng 708 Toà Nhà Sunrise Building 2, KĐT Sài Đồng, Q. Long Biên', N'962935586')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'8', N'Công Ty TNHH Hưng Việt Long', N'68 Quan Nhân, Trung Hòa, Cầu Giấy Hà Nội Điện thoại', N'978875559')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChiNCC], [SoDTNCC]) VALUES (N'9', N'Công Ty TNHH Ô Tô Việt Nhân', N'368A QL 51, Xã An Hòa TP. Biên Hòa,Đồng Nai', N'906864353')
GO
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [NgaySinh], [DiaChiNV], [Sodienthoai], [Taikhoan], [gender]) VALUES (N'NV2552', N'nguyễn xuân trường', CAST(N'2023-06-21' AS Date), N'Nam Định', N'0326054827', N'truongadmin', N'Nam')
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [NgaySinh], [DiaChiNV], [Sodienthoai], [Taikhoan], [gender]) VALUES (N'NV2843', N'nguyễn xuân trường', CAST(N'2023-06-21' AS Date), N'Nam Định', N'0326054827', N'truongnv', N'Nam')
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [NgaySinh], [DiaChiNV], [Sodienthoai], [Taikhoan], [gender]) VALUES (N'NV3308', N'nguyễn xuân trường', CAST(N'2023-06-21' AS Date), N'Nam Định', N'0326054827', N'nv', N'Nam')
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [NgaySinh], [DiaChiNV], [Sodienthoai], [Taikhoan], [gender]) VALUES (N'NV6152', N'nguyễn xuân trường', CAST(N'2023-06-21' AS Date), N'Nam Định', N'0326054827', N'nv1', N'Nam')
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [NgaySinh], [DiaChiNV], [Sodienthoai], [Taikhoan], [gender]) VALUES (N'NV9942', N'admin', CAST(N'2002-11-29' AS Date), N'Nam Định', N'0326054828', N'admin', N'Nam')
GO
INSERT [dbo].[order] ([id], [soLuong], [thanhtien]) VALUES (N'OR21A7    ', N'1         ', N'150000    ')
GO
INSERT [dbo].[orderDetails] ([tenXeMua], [Gia], [soLuongMua], [id], [idKH], [idOrder]) VALUES (N'Xe điện', 150000, 1, N'OD816     ', N'KH208B', N'OR21A7    ')
GO
INSERT [dbo].[Oto] ([IdOto], [Sochongoi], [Kieudongco], [IdPT]) VALUES (N'OT13BD', 8, N'81', N'PT105A')
INSERT [dbo].[Oto] ([IdOto], [Sochongoi], [Kieudongco], [IdPT]) VALUES (N'OT14EA', 4, N'81', N'PT76C')
GO
INSERT [dbo].[Oto_kho] ([idOtoKho], [Sochongoi], [KieuDongCo], [idPTKho]) VALUES (N'OT13BD', 8, N'81', N'PT105A')
INSERT [dbo].[Oto_kho] ([idOtoKho], [Sochongoi], [KieuDongCo], [idPTKho]) VALUES (N'OT14EA', 4, N'81', N'PT76C')
GO
INSERT [dbo].[PhuongTien] ([IdPT], [NamSX], [Gia], [Mamau], [TenXe], [Ngaynhap], [Soluong], [Mota], [Donvi], [IdHangXe]) VALUES (N'PT105A', 2016, 200000, N'37', N'VF8', CAST(N'2023-06-08' AS Date), 9, N'xe vf', N'c', N'10')
INSERT [dbo].[PhuongTien] ([IdPT], [NamSX], [Gia], [Mamau], [TenXe], [Ngaynhap], [Soluong], [Mota], [Donvi], [IdHangXe]) VALUES (N'PT156E', 2007, 1110, N'1', N'Yamaha', CAST(N'2023-06-07' AS Date), 8, N'Xe phân khối lớn', N'c', N'8')
INSERT [dbo].[PhuongTien] ([IdPT], [NamSX], [Gia], [Mamau], [TenXe], [Ngaynhap], [Soluong], [Mota], [Donvi], [IdHangXe]) VALUES (N'PT1937', 2015, 100000, N'11', N'Air Blade', CAST(N'2023-06-08' AS Date), 7, N'xe cu', N'c', N'10')
INSERT [dbo].[PhuongTien] ([IdPT], [NamSX], [Gia], [Mamau], [TenXe], [Ngaynhap], [Soluong], [Mota], [Donvi], [IdHangXe]) VALUES (N'PT1D38', 2017, 3000000, N'51', N'VF', CAST(N'2023-06-09' AS Date), 5, N'xe cỡ nhỏ', N'c', N'10')
INSERT [dbo].[PhuongTien] ([IdPT], [NamSX], [Gia], [Mamau], [TenXe], [Ngaynhap], [Soluong], [Mota], [Donvi], [IdHangXe]) VALUES (N'PT433', 2019, 3000000, N'51', N'VF', CAST(N'2023-06-10' AS Date), 8, N'xe cỡ nhỏ', N'c', N'10')
INSERT [dbo].[PhuongTien] ([IdPT], [NamSX], [Gia], [Mamau], [TenXe], [Ngaynhap], [Soluong], [Mota], [Donvi], [IdHangXe]) VALUES (N'PT76C', 2021, 200000, N'7', N'VF8', CAST(N'2023-06-08' AS Date), 1, N'xe vf', N'c', N'10')
INSERT [dbo].[PhuongTien] ([IdPT], [NamSX], [Gia], [Mamau], [TenXe], [Ngaynhap], [Soluong], [Mota], [Donvi], [IdHangXe]) VALUES (N'PTCE1', 2023, 111, N'2', N'test', CAST(N'2023-06-07' AS Date), 7, N'1', N'c', N'10')
INSERT [dbo].[PhuongTien] ([IdPT], [NamSX], [Gia], [Mamau], [TenXe], [Ngaynhap], [Soluong], [Mota], [Donvi], [IdHangXe]) VALUES (N'PTD93', 2015, 150000, N'30', N'Xe điện', CAST(N'2023-06-08' AS Date), 7, N'xe diện vf', N'c', N'10')
GO
INSERT [dbo].[ThongTinCongTy] ([TenCongTy], [Diachicongty], [Sodtcongty], [Masothue]) VALUES (N'VINFAST', N' Số 7, đường Bằng Lăng 1, Khu đô thị sinh thái Vinhomes Riverside, Phường Việt Hưng, Quận Long Biên, Hà Nội', N'1900232389', N' 0108926276')
GO
INSERT [dbo].[ThongTinKhachHang] ([MaKH], [TenKH], [DiaChi], [Sodienthoai]) VALUES (N'KH208B', N'', N'', N'')
GO
INSERT [dbo].[XeMay] ([IdXeMay], [CongSuat], [IdPT]) VALUES (N'XM2103', 120, N'PT1937')
INSERT [dbo].[XeMay] ([IdXeMay], [CongSuat], [IdPT]) VALUES (N'XM26CD', 50, N'PTD93')
INSERT [dbo].[XeMay] ([IdXeMay], [CongSuat], [IdPT]) VALUES (N'XM9F8', 150, N'PT156E')
INSERT [dbo].[XeMay] ([IdXeMay], [CongSuat], [IdPT]) VALUES (N'XMDCB', 12, N'PTCE1')
GO
INSERT [dbo].[XeMay_Kho] ([idXMKho], [CongSuat], [idPT]) VALUES (N'XM2103', 120, N'PT1937')
INSERT [dbo].[XeMay_Kho] ([idXMKho], [CongSuat], [idPT]) VALUES (N'XM26CD', 50, N'PTD93')
INSERT [dbo].[XeMay_Kho] ([idXMKho], [CongSuat], [idPT]) VALUES (N'XM9F8', 150, N'PT156E')
INSERT [dbo].[XeMay_Kho] ([idXMKho], [CongSuat], [idPT]) VALUES (N'XMDCB', 12, N'PTCE1')
GO
INSERT [dbo].[XeTai] ([XeTai], [Trongtai], [IdPT]) VALUES (N'XT1722', 20, N'PT1D38')
INSERT [dbo].[XeTai] ([XeTai], [Trongtai], [IdPT]) VALUES (N'XT212F', 20, N'PT433')
GO
INSERT [dbo].[XeTai_Kho] ([idXeTaiKho], [TrongTai], [idPT]) VALUES (N'XT1722', 20, N'PT1D38')
INSERT [dbo].[XeTai_Kho] ([idXeTaiKho], [TrongTai], [idPT]) VALUES (N'XT212F', 20, N'PT433')
GO
ALTER TABLE [dbo].[Kho_PhuongTien]  WITH CHECK ADD  CONSTRAINT [FK_Kho_PhuongTien_HangXe] FOREIGN KEY([idHangXe])
REFERENCES [dbo].[HangXe] ([IdHangXe])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kho_PhuongTien] CHECK CONSTRAINT [FK_Kho_PhuongTien_HangXe]
GO
ALTER TABLE [dbo].[Kho_PhuongTien]  WITH CHECK ADD  CONSTRAINT [FK_Kho_PhuongTien_Kho] FOREIGN KEY([idKho])
REFERENCES [dbo].[Kho] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kho_PhuongTien] CHECK CONSTRAINT [FK_Kho_PhuongTien_Kho]
GO
ALTER TABLE [dbo].[Kho_PhuongTien]  WITH CHECK ADD  CONSTRAINT [FK_Kho_PhuongTien_Mau] FOREIGN KEY([idMau])
REFERENCES [dbo].[Mau] ([Mamau])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kho_PhuongTien] CHECK CONSTRAINT [FK_Kho_PhuongTien_Mau]
GO
ALTER TABLE [dbo].[Kho_PhuongTien]  WITH CHECK ADD  CONSTRAINT [FK_Kho_PhuongTien_NhaCungCap] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NhaCungCap] ([MaNCC])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kho_PhuongTien] CHECK CONSTRAINT [FK_Kho_PhuongTien_NhaCungCap]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_Account] FOREIGN KEY([Taikhoan])
REFERENCES [dbo].[Account] ([TaiKhoan])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_Account]
GO
ALTER TABLE [dbo].[orderDetails]  WITH CHECK ADD  CONSTRAINT [FK_orderDetails_order] FOREIGN KEY([idOrder])
REFERENCES [dbo].[order] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[orderDetails] CHECK CONSTRAINT [FK_orderDetails_order]
GO
ALTER TABLE [dbo].[orderDetails]  WITH CHECK ADD  CONSTRAINT [FK_orderDetails_ThongTinKhachHang] FOREIGN KEY([idKH])
REFERENCES [dbo].[ThongTinKhachHang] ([MaKH])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[orderDetails] CHECK CONSTRAINT [FK_orderDetails_ThongTinKhachHang]
GO
ALTER TABLE [dbo].[Oto]  WITH CHECK ADD  CONSTRAINT [FK_Oto_PhuongTien] FOREIGN KEY([IdPT])
REFERENCES [dbo].[PhuongTien] ([IdPT])
GO
ALTER TABLE [dbo].[Oto] CHECK CONSTRAINT [FK_Oto_PhuongTien]
GO
ALTER TABLE [dbo].[Oto_kho]  WITH CHECK ADD  CONSTRAINT [FK_Oto_kho_Kho_PhuongTien] FOREIGN KEY([idPTKho])
REFERENCES [dbo].[Kho_PhuongTien] ([idPT])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Oto_kho] CHECK CONSTRAINT [FK_Oto_kho_Kho_PhuongTien]
GO
ALTER TABLE [dbo].[PhuongTien]  WITH CHECK ADD  CONSTRAINT [FK_PhuongTien_HangXe] FOREIGN KEY([IdHangXe])
REFERENCES [dbo].[HangXe] ([IdHangXe])
GO
ALTER TABLE [dbo].[PhuongTien] CHECK CONSTRAINT [FK_PhuongTien_HangXe]
GO
ALTER TABLE [dbo].[PhuongTien]  WITH CHECK ADD  CONSTRAINT [FK_PhuongTien_Mau] FOREIGN KEY([Mamau])
REFERENCES [dbo].[Mau] ([Mamau])
GO
ALTER TABLE [dbo].[PhuongTien] CHECK CONSTRAINT [FK_PhuongTien_Mau]
GO
ALTER TABLE [dbo].[XeMay]  WITH CHECK ADD  CONSTRAINT [FK_XeMay_PhuongTien] FOREIGN KEY([IdPT])
REFERENCES [dbo].[PhuongTien] ([IdPT])
GO
ALTER TABLE [dbo].[XeMay] CHECK CONSTRAINT [FK_XeMay_PhuongTien]
GO
ALTER TABLE [dbo].[XeMay_Kho]  WITH CHECK ADD  CONSTRAINT [FK_XeMay_Kho_Kho_PhuongTien] FOREIGN KEY([idPT])
REFERENCES [dbo].[Kho_PhuongTien] ([idPT])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[XeMay_Kho] CHECK CONSTRAINT [FK_XeMay_Kho_Kho_PhuongTien]
GO
ALTER TABLE [dbo].[XeTai]  WITH CHECK ADD  CONSTRAINT [FK_XeTai_PhuongTien] FOREIGN KEY([IdPT])
REFERENCES [dbo].[PhuongTien] ([IdPT])
GO
ALTER TABLE [dbo].[XeTai] CHECK CONSTRAINT [FK_XeTai_PhuongTien]
GO
ALTER TABLE [dbo].[XeTai_Kho]  WITH CHECK ADD  CONSTRAINT [FK_XeTai_Kho_Kho_PhuongTien] FOREIGN KEY([idPT])
REFERENCES [dbo].[Kho_PhuongTien] ([idPT])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[XeTai_Kho] CHECK CONSTRAINT [FK_XeTai_Kho_Kho_PhuongTien]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeleteNVAC]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create proc [dbo].[pr_DeleteNVAC](@manv nvarchar(10))
as
Begin
	Declare @tk nvarchar(50)
	set @tk = (select TaiKhoan from NhanVien where @manv = MaNV)
	Delete NhanVien Where @manv = MaNV
	Delete Account Where Taikhoan = @tk
	
 end
GO
/****** Object:  StoredProcedure [dbo].[pr_insertNVAC]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[pr_insertNVAC](@manv nvarchar(10), @hoTen nvarchar(50), @ngaysinh date, @diachi nvarchar(200),
@sodt nvarchar(10), @taiKhoan nvarchar(50), @gender nvarchar(50), @matkhau nvarchar(100), @quyen int)
as
Begin
	insert into Account values(@taiKhoan, @matkhau, @quyen)
	insert into NhanVien values (@manv, @hoTen, @ngaysinh, @diachi, @sodt, @taiKhoan, @gender)
 end
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdatetNVAC]    Script Date: 6/12/2023 8:39:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create proc [dbo].[pr_UpdatetNVAC](@manv nvarchar(10), @hoTen nvarchar(50), @ngaysinh date, @diachi nvarchar(200),
@sodt nvarchar(10), @taiKhoan nvarchar(50), @gender nvarchar(50), @matkhau nvarchar(100), @quyen int)
as
Begin
	Update Account set TaiKhoan = @taiKhoan, Matkhau = @matkhau, Quyen=@quyen Where @taiKhoan = TaiKhoan
	Update NhanVien set MaNV = @manv, TenNV = @hoTen, NgaySinh = @ngaysinh, DiaChiNV = @diachi, Sodienthoai = @sodt, Taikhoan = @taiKhoan, gender=@gender where @manv = MaNV
 end
GO
USE [master]
GO
ALTER DATABASE [QLCHXe] SET  READ_WRITE 
GO
