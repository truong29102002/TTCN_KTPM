-- insert table
create proc pr_insertNVAC(@manv nvarchar(10), @hoTen nvarchar(50), @ngaysinh date, @diachi nvarchar(200),
@sodt nvarchar(10), @taiKhoan nvarchar(50), @gender nvarchar(50), @matkhau nvarchar(50), @quyen int)
as
Begin
	insert into Account values(@taiKhoan, @matkhau, @quyen)
	insert into NhanVien values (@manv, @hoTen, @ngaysinh, @diachi, @sodt, @taiKhoan, @gender)
 end

 Drop PROCEDURE pr_insertNVAC
  Execute pr_insertNVAC '3', 'xuan truong', '2002-10-29', N'Nam dinh', '0326054827', 'xtruong', 'nam', '1234567', 0

 -- update table
 create proc pr_UpdatetNVAC(@manv nvarchar(10), @hoTen nvarchar(50), @ngaysinh date, @diachi nvarchar(200),
@sodt nvarchar(10), @taiKhoan nvarchar(50), @gender nvarchar(50), @matkhau nvarchar(50), @quyen int)
as
Begin
	Update Account set TaiKhoan = @taiKhoan, Matkhau = @matkhau, Quyen=@quyen Where @taiKhoan = TaiKhoan
	Update NhanVien set MaNV = @manv, TenNV = @hoTen, NgaySinh = @ngaysinh, DiaChiNV = @diachi, Sodienthoai = @sodt, Taikhoan = @taiKhoan, gender=@gender where @manv = MaNV
 end

 
 Execute pr_UpdatetNVAC '3', 'nguyen xuan truong', '2002-10-29', N'Nam dinh', '0326054827', 'xtruong', 'nam', '12345678', 0


  -- delete table
 create proc pr_DeleteNVAC(@manv nvarchar(10))
as
Begin
	Declare @tk nvarchar(50)
	set @tk = (select TaiKhoan from NhanVien where @manv = MaNV)
	Delete NhanVien Where @manv = MaNV
	Delete Account Where Taikhoan = @tk
	
 end

 Drop procedure pr_DeleteNVAC

 Execute pr_DeleteNVAC 'a394bf5c-4'