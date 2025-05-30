CREATE PROCEDURE sp_CalculateTotalByMaHD
    @MaHD INT,
    @TongTien DECIMAL(18,2) OUTPUT
AS
BEGIN
    SELECT @TongTien = ISNULL(SUM(g.SoLuong * m.Gia), 0)
    FROM Goimon g
    JOIN Mon m ON g.MaMon = m.MaMon
    WHERE g.MaHD = @MaHD
END
go

--
CREATE PROCEDURE sp_GetGoiMonByMaHD
    @MaHD INT
AS
BEGIN
    SELECT g.MaMon, m.TenMon, g.SoLuong, m.Gia, (g.SoLuong * m.Gia) AS ThanhTien
    FROM Goimon g
    JOIN Mon m ON g.MaMon = m.MaMon
    WHERE g.MaHD = @MaHD
END
go

--
CREATE PROCEDURE sp_AddOrUpdateGoiMon
    @MaHD INT,
    @MaMon INT,
    @SoLuong INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    IF EXISTS (SELECT 1 FROM Goimon WHERE MaHD = @MaHD AND MaMon = @MaMon)
        UPDATE Goimon SET SoLuong = SoLuong + @SoLuong WHERE MaHD = @MaHD AND MaMon = @MaMon
    ELSE
        INSERT INTO Goimon (MaHD, MaMon, SoLuong) VALUES (@MaHD, @MaMon, @SoLuong)
    COMMIT;
END
go

--
CREATE PROCEDURE sp_TimKiemMon
    @Keyword NVARCHAR(100)
AS
BEGIN
    SELECT MaMon, TenMon, Gia, Anh, MaLoai
    FROM Mon
    WHERE TenMon LIKE N'%' + @Keyword + N'%'
END
go

--
CREATE PROCEDURE sp_LayTatCaMon
    @MaLoai INT = NULL
AS
BEGIN
    SELECT MaMon, TenMon, Gia, Anh, MaLoai
    FROM Mon
    WHERE @MaLoai IS NULL OR MaLoai = @MaLoai
END
go

--
CREATE PROCEDURE sp_CapNhatMon
    @MaMon INT,
    @TenMon NVARCHAR(100),
    @Gia DECIMAL(18,2),
    @GiaNhap DECIMAL(18,2),
    @TonKho INT,
    @MaLoai INT,
    @Anh NVARCHAR(255)
AS
BEGIN
    UPDATE Mon
    SET TenMon = @TenMon, Gia = @Gia, GiaNhap = @GiaNhap, TonKho = @TonKho, MaLoai = @MaLoai, Anh = @Anh
    WHERE MaMon = @MaMon
END
go

--
CREATE PROCEDURE sp_XoaMon
    @MaMon INT,
    @Result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Goimon WHERE MaMon = @MaMon)
        SET @Result = -1 -- Món đang được sử dụng
    ELSE
    BEGIN
        DELETE FROM Mon WHERE MaMon = @MaMon
        SET @Result = @@ROWCOUNT
    END
END
go

--
CREATE PROCEDURE sp_ThemMon
    @TenMon NVARCHAR(100),
    @Gia DECIMAL(18,2),
    @GiaNhap DECIMAL(18,2),
    @TonKho INT,
    @MaLoai INT,
    @Anh NVARCHAR(255),
    @MaMon INT OUTPUT
AS
BEGIN
    INSERT INTO Mon (TenMon, Gia, GiaNhap, TonKho, MaLoai, Anh)
    VALUES (@TenMon, @Gia, @GiaNhap, @TonKho, @MaLoai, @Anh)
    SET @MaMon = SCOPE_IDENTITY()
END
go

--
CREATE PROCEDURE sp_GetMaHDFromThuemay
    @MaMay INT,
    @MaPhong INT,
    @MaHD INT OUTPUT
AS
BEGIN
    SELECT TOP 1 @MaHD = t.MaHD
    FROM Thuemay t
    JOIN Maytinh m ON t.MaMay = m.MaMay
    WHERE t.MaMay = @MaMay
    AND m.MaPhong = @MaPhong
    AND m.Trangthai = 'Dang thue'
    AND t.GioKT IS NULL
    ORDER BY t.GioBD DESC
END
go

--
CREATE PROCEDURE sp_CapNhatTongTienHoaDon
    @MaHD INT,
    @TongTien DECIMAL(18,2)
AS
BEGIN
    UPDATE Hoadon
    SET TongTien = ISNULL(TongTien, 0) + @TongTien
    WHERE MaHD = @MaHD
END
go

--
CREATE PROCEDURE sp_XacNhanGoiMon
    @MaHD INT,
    @MaMon INT,
    @SoLuong INT,
    @Result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM Goimon WHERE MaHD = @MaHD AND MaMon = @MaMon)
            UPDATE Goimon SET SoLuong = SoLuong + @SoLuong WHERE MaHD = @MaHD AND MaMon = @MaMon
        ELSE
            INSERT INTO Goimon (MaHD, MaMon, SoLuong) VALUES (@MaHD, @MaMon, @SoLuong)

        DECLARE @TongTien DECIMAL(18,2)
        SELECT @TongTien = ISNULL(SUM(g.SoLuong * m.Gia), 0)
        FROM Goimon g
        JOIN Mon m ON g.MaMon = m.MaMon
        WHERE g.MaHD = @MaHD

        UPDATE Hoadon SET TongTien = ISNULL(TongTien, 0) + @TongTien WHERE MaHD = @MaHD

        SET @Result = 1
        COMMIT
    END TRY
    BEGIN CATCH
        ROLLBACK
        SET @Result = -1
    END CATCH
END
go

--