-- Cập nhật trạng thái máy về "Trong" cho các máy không có phiên thuê đang hoạt động
UPDATE MayTinh
SET Trangthai = N'Trong'
WHERE MaMay IN (
    SELECT m.MaMay
    FROM MayTinh m
    LEFT JOIN ThueMay t ON m.MaMay = t.MaMay AND t.GioKT IS NULL
    WHERE m.Trangthai = N'Dang thue' AND t.MaTM IS NULL
);

--
-- Tính thời gian thuê máy và cập nhật TongTien
DECLARE @CurrentTime DATETIME = '2025-05-28 11:53:00';

-- Tạo bảng tạm để tính chi phí thuê máy
CREATE TABLE #TempThueMayCost (
    MaHD INT,
    ThueMayCost DECIMAL(10,2)
);

-- Tính chi phí thuê máy cho các phiên thuê đang hoạt động
INSERT INTO #TempThueMayCost (MaHD, ThueMayCost)
SELECT 
    t.MaHD,
    SUM(DATEDIFF(MINUTE, t.GioBD, @CurrentTime) / 60.0 * p.Giatheogio) AS ThueMayCost
FROM ThueMay t
JOIN MayTinh m ON t.MaMay = m.MaMay
JOIN Phong p ON m.MaPhong = p.MaPhong
WHERE t.GioKT IS NULL
GROUP BY t.MaHD;

-- Cập nhật TongTien trong Hoadon
UPDATE Hoadon
SET TongTien = (
    SELECT 
        COALESCE((
            SELECT SUM(m.Gia * g.SoLuong) 
            FROM Goimon g 
            JOIN Mon m ON g.MaMon = m.MaMon 
            WHERE g.MaHD = Hoadon.MaHD
        ), 0) + 
        COALESCE((
            SELECT ThueMayCost 
            FROM #TempThueMayCost 
            WHERE MaHD = Hoadon.MaHD
        ), 0)
)
WHERE MaHD IN (1, 2, 4);

-- Xóa bảng tạm
DROP TABLE #TempThueMayCost;

--
-- Cập nhật trạng thái máy thành "Bao loi" cho các máy có bảo trì chưa hoàn tất
UPDATE MayTinh
SET Trangthai = N'Bao loi'
WHERE MaMay IN (
    SELECT MaMay
    FROM ChitietBT cb
    JOIN Baotri b ON cb.MaBT = b.MaBT
    WHERE b.TrangThai = N'Chưa thanh toán'
);
go

CREATE PROCEDURE sp_FixInconsistentMachineStatus
AS
BEGIN
    -- Cập nhật trạng thái máy về "Trong" cho các máy không có phiên thuê đang hoạt động
    UPDATE MayTinh
    SET Trangthai = N'Trong'
    WHERE MaMay IN (
        SELECT m.MaMay
        FROM MayTinh m
        LEFT JOIN ThueMay t ON m.MaMay = t.MaMay AND t.GioKT IS NULL
        WHERE m.Trangthai = N'Dang thue' AND t.MaTM IS NULL
    );

    -- Cập nhật trạng thái máy thành "Bao loi" cho các máy đang bảo trì chưa thanh toán
    UPDATE MayTinh
    SET Trangthai = N'Bao loi'
    WHERE MaMay IN (
        SELECT MaMay
        FROM ChitietBT cb
        JOIN Baotri b ON cb.MaBT = b.MaBT
        WHERE b.TrangThai = N'Chưa thanh toán'
    );
END;
go
--
-- Cập nhật trạng thái máy thành "Trong" cho các máy đã bảo trì hoàn tất và không có phiên thuê đang hoạt động
UPDATE MayTinh
SET Trangthai = N'Trong'
WHERE MaMay IN (1, 11)
AND MaMay NOT IN (
    SELECT MaMay FROM ThueMay WHERE GioKT IS NULL
);
go

-- Cập nhật trạng thái máy thành "Bao loi" cho các máy có bảo trì chưa hoàn tất
UPDATE MayTinh
SET Trangthai = N'Bao loi'
WHERE MaMay IN (
    SELECT MaMay
    FROM ChitietBT cb
    JOIN Baotri b ON cb.MaBT = b.MaBT
    WHERE b.Trangthai = N'Chưa thanh toán'
);

-- Cập nhật trạng thái máy thành "Trong" cho các máy có bảo trì đã hoàn tất và không có phiên thuê
UPDATE MayTinh
SET Trangthai = N'Trong'
WHERE MaMay IN (
    SELECT MaMay
    FROM ChitietBT cb
    JOIN Baotri b ON cb.MaBT = b.MaBT
    WHERE b.Trangthai = N'Đã hoàn tất'
) AND MaMay NOT IN (
    SELECT MaMay FROM ThueMay WHERE GioKT IS NULL
);

EXEC sp_FixInconsistentMachineStatus;

SELECT MaMay, Trangthai FROM MayTinh WHERE MaMay IN (1, 5, 11, 26, 27);
SELECT MaTM, MaMay, GioBD, GioKT FROM ThueMay WHERE MaMay IN (1, 5, 11, 26, 27) AND GioKT IS NULL;
SELECT b.MaBT, b.Trangthai, c.MaMay, t.TenTT FROM Baotri b
JOIN ChitietBT c ON b.MaBT = c.MaBT
JOIN Tinhtrang t ON c.MaTT = t.MaTT
WHERE c.MaMay IN (1, 5, 11, 26, 27);