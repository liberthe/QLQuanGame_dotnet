--Prod Bảo trì 
--1
CREATE PROCEDURE sp_GetMaintainListForMachine
    @MaMay INT
AS
BEGIN
    SELECT m.TenMay, 
           t.TenTT AS BoPhan, 
           n.TenNN AS NguyenNhan, 
           g.TenGP AS GiaiPhap, 
           b.GhiChu, 
           b.MaBT, 
           g.Chiphi, 
           b.Trangthai AS TrangthaiBaotri
    FROM dbo.Maytinh m
    JOIN dbo.ChitietBT c ON m.MaMay = c.MaMay
    JOIN dbo.Baotri b ON c.MaBT = b.MaBT
    JOIN dbo.Tinhtrang t ON c.MaTT = t.MaTT
    JOIN dbo.Nguyennhan n ON c.MaNN = n.MaNN
    JOIN dbo.Giaiphap g ON c.MaGP = g.MaGP
    WHERE m.MaMay = @MaMay AND m.Trangthai = N'Bao loi';
END
GO

--2
CREATE OR ALTER PROCEDURE sp_CreateBaotri
    @MaNV INT,
    @MaMay INT,
    @MaTT INT,
    @MaNN INT,
    @MaGP INT,
    @GhiChu NVARCHAR(MAX),
    @ChiPhi DECIMAL(18,2),
    @MaNBT INT, -- Thêm tham số mới
    @MaBT INT OUTPUT,
    @MaHD INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Tạo hóa đơn
        INSERT INTO dbo.HoaDon (LoaiHD, NgayTao, MaNV, TongTien)
        VALUES ('Chi', GETDATE(), @MaNV, 0);
        SET @MaHD = SCOPE_IDENTITY();

        -- Cập nhật trạng thái máy
        UPDATE dbo.Maytinh
        SET Trangthai = N'Bao loi'
        WHERE MaMay = @MaMay;

        -- Tạo bản ghi bảo trì
        INSERT INTO dbo.Baotri (NgayBT, MaNBT, MaHD, GhiChu, Trangthai)
        VALUES (CAST(GETDATE() AS DATE), @MaNBT, @MaHD, @GhiChu, N'Chưa thanh toán');
        SET @MaBT = SCOPE_IDENTITY();

        -- Tạo chi tiết bảo trì
        INSERT INTO dbo.ChitietBT (MaBT, MaMay, MaTT, MaNN, MaGP)
        VALUES (@MaBT, @MaMay, @MaTT, @MaNN, @MaGP);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END
GO

--3
CREATE OR ALTER PROCEDURE sp_ThanhToanBaotri
    @MaNV INT,
    @MaBTList NVARCHAR(MAX), -- Danh sách MaBT cách nhau bởi dấu phẩy
    @MaHD INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Tạo bảng tạm để lưu danh sách MaBT
        DECLARE @MaBTTable TABLE (MaBT INT);
        INSERT INTO @MaBTTable (MaBT)
        SELECT TRY_CAST(value AS INT) 
        FROM STRING_SPLIT(@MaBTList, ',')
        WHERE TRY_CAST(value AS INT) IS NOT NULL;

        -- Lấy MaHD từ bản ghi Baotri (đảm bảo tất cả MaBT trong danh sách có cùng MaHD)
        SELECT TOP 1 @MaHD = b.MaHD
        FROM dbo.Baotri b
        JOIN @MaBTTable mt ON b.MaBT = mt.MaBT;

        IF @MaHD IS NULL
        BEGIN
            -- Nếu không tìm thấy MaHD, đây là lỗi bất thường vì MaHD không được NULL
            RAISERROR (N'Không tìm thấy mã hóa đơn cho các bản ghi bảo trì.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Tính tổng chi phí
        DECLARE @TotalCost DECIMAL(18,2);
        SELECT @TotalCost = SUM(g.ChiPhi)
        FROM @MaBTTable mt
        JOIN dbo.Baotri b ON mt.MaBT = b.MaBT
        JOIN dbo.ChitietBT c ON b.MaBT = c.MaBT
        JOIN dbo.Giaiphap g ON c.MaGP = g.MaGP
        WHERE b.Trangthai != N'Đã hoàn tất';

        IF @TotalCost IS NULL
            SET @TotalCost = 0;

        -- Cập nhật tổng tiền hóa đơn
        UPDATE dbo.HoaDon
        SET TongTien = @TotalCost
        WHERE MaHD = @MaHD;

        -- Cập nhật trạng thái bảo trì
        UPDATE b
        SET b.Trangthai = N'Đã hoàn tất'
        FROM dbo.Baotri b
        JOIN @MaBTTable mt ON b.MaBT = mt.MaBT
        WHERE b.Trangthai != N'Đã hoàn tất';

        -- Cập nhật trạng thái máy
        UPDATE m
        SET m.Trangthai = CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM dbo.ChitietBT c 
                JOIN dbo.Baotri b ON c.MaBT = b.MaBT 
                WHERE c.MaMay = m.MaMay AND b.Trangthai != N'Đã hoàn tất'
            ) THEN N'Bao loi'
            ELSE N'Trong'
        END
        FROM dbo.Maytinh m
        WHERE m.MaMay IN (
            SELECT DISTINCT c.MaMay 
            FROM @MaBTTable mt
            JOIN dbo.ChitietBT c ON mt.MaBT = c.MaBT
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
go
--4
CREATE FUNCTION fn_LaySoLoiChuaHoanTat (@MaMay INT)
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(*)
        FROM dbo.Baotri b
        JOIN dbo.ChitietBT c ON b.MaBT = c.MaBT
        WHERE c.MaMay = @MaMay AND b.Trangthai != N'Đã hoàn tất'
    );
END
GO

--prod đặt máy
-- sp_GetBillList

-- sp_GetMayTinhByPhong
CREATE PROCEDURE sp_GetMayTinhByPhong
    @MaPhong INT
AS
BEGIN
    SELECT 
        m.MaMay, m.TenMay, m.Trangthai, m.MaPhong,
        o.TenOcung, dl.TenDL, c.TenChip, r.TenRAM, 
        t.TenTD, od.TenOdia, mh.TenMH, ch.TenChuot, 
        bp.TenBP, tn.TenTN
    FROM Maytinh m
    LEFT JOIN Ocung o ON m.MaOcung = o.MaOcung
    LEFT JOIN Dungluong dl ON m.MaDL = dl.MaDL
    LEFT JOIN Chip c ON m.MaChip = c.MaChip
    LEFT JOIN RAM r ON m.MaRAM = r.MaRAM
    LEFT JOIN Tocdo t ON m.MaTD = t.MaTD
    LEFT JOIN Odia od ON m.MaOdia = od.MaOdia
    LEFT JOIN Manhinh mh ON m.MaMH = mh.MaMH
    LEFT JOIN Chuot ch ON m.MaChuot = ch.MaChuot
    LEFT JOIN Banphim bp ON m.MaBP = bp.MaBP
    LEFT JOIN Tainghe tn ON m.MaTN = tn.MaTN
    WHERE m.MaPhong = @MaPhong
END
go
-- sp_StartThueMay
CREATE PROCEDURE sp_StartThueMay
    @MaMay INT,
    @MaHD INT,
    @MaTM INT OUTPUT
AS
BEGIN
    INSERT INTO Thuemay (MaMay, GioBD, GioKT, MaHD)
    VALUES (@MaMay, GETDATE(), NULL, @MaHD)
    SET @MaTM = SCOPE_IDENTITY()
END
go

-- sp_EndThueMay

CREATE OR ALTER PROCEDURE sp_EndThueMay @MaTM INT 
AS
BEGIN 
    SET NOCOUNT ON;
    SET XACT_ABORT ON; -- Tự rollback nếu gặp lỗi nghiêm trọng

    DECLARE @MaMay INT;

    -- Kiểm tra phiên thuê tồn tại và chưa kết thúc
    IF NOT EXISTS (SELECT 1 FROM ThueMay WHERE MaTM = @MaTM AND GioKT IS NULL)
    BEGIN
        RAISERROR(N'Phiên thuê không tồn tại hoặc đã kết thúc.', 16, 1);
        RETURN;
    END

    -- Lưu MaMay vào biến
    SELECT @MaMay = MaMay FROM ThueMay WHERE MaTM = @MaTM;

    -- Giao dịch
    BEGIN TRANSACTION;

    -- Cập nhật thời gian kết thúc
    UPDATE ThueMay
    SET GioKT = GETDATE()
    WHERE MaTM = @MaTM;

    -- Cập nhật trạng thái máy
    UPDATE MayTinh
    SET Trangthai = N'Trong'
    WHERE MaMay = @MaMay;

    COMMIT TRANSACTION;
END
GO


-- sp_GetCurrentThueMay
CREATE PROCEDURE sp_GetCurrentThueMay
    @MaMay INT
AS
BEGIN
    SELECT MaTM, MaMay, GioBD, GioKT, MaHD
    FROM Thuemay
    WHERE MaMay = @MaMay AND GioKT IS NULL
END
go

CREATE PROCEDURE sp_CreateHoaDon
    @MaNV INT,
    @LoaiHD NVARCHAR(10),
    @MaHD INT OUTPUT
AS
BEGIN
    INSERT INTO Hoadon (NgayTao, MaNV, LoaiHD, TongTien)
    VALUES (GETDATE(), @MaNV, @LoaiHD, 0)
    SET @MaHD = SCOPE_IDENTITY()
END
go
SELECT * FROM ThueMay WHERE mamay=1 