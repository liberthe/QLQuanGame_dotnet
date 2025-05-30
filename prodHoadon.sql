CREATE OR ALTER PROCEDURE sp_GetBillListFiltered
    @NgayBD DATETIME,
    @NgayKT DATETIME,
    @TenNV NVARCHAR(100) = NULL,
    @LoaiHD NVARCHAR(50) = NULL
AS
BEGIN
    SELECT 
        h.MaHD, 
        h.NgayTao, 
        nv.TenNV, 
        h.LoaiHD,
        ISNULL(bt.ChiPhi, 0) AS ChiPhi,
        ISNULL(gm.TongGiaMon, 0) AS GiaMon,
        ISNULL(tm.TongThueMay, 0) AS ThueMay,
        h.TongTien
    FROM Hoadon h
    LEFT JOIN Nhanvien nv ON h.MaNV = nv.MaNV
    LEFT JOIN (
        SELECT bt.MaHD, SUM(gp.ChiPhi) AS ChiPhi
        FROM Baotri bt
        JOIN ChitietBT cbt ON bt.MaBT = cbt.MaBT
        JOIN Giaiphap gp ON cbt.MaGP = gp.MaGP
        GROUP BY bt.MaHD
    ) bt ON h.MaHD = bt.MaHD
    LEFT JOIN (
        SELECT g.MaHD, SUM(g.SoLuong * m.Gia) AS TongGiaMon
        FROM Goimon g
        JOIN Mon m ON g.MaMon = m.MaMon
        GROUP BY g.MaHD
    ) gm ON h.MaHD = gm.MaHD
    LEFT JOIN (
        SELECT tm.MaHD, 
               SUM(DATEDIFF(MINUTE, tm.GioBD, ISNULL(tm.GioKT, GETDATE())) / 60.0 * p.Giatheogio) AS TongThueMay
        FROM Thuemay tm
        JOIN Maytinh mt ON tm.MaMay = mt.MaMay
        JOIN Phong p ON mt.MaPhong = p.MaPhong
        GROUP BY tm.MaHD
    ) tm ON h.MaHD = tm.MaHD
    WHERE h.NgayTao BETWEEN @NgayBD AND @NgayKT
        AND (@TenNV IS NULL OR nv.TenNV LIKE '%' + @TenNV + '%')
        AND (@LoaiHD IS NULL OR h.LoaiHD = @LoaiHD)
END
go
--

CREATE PROCEDURE sp_GetAllPhong
AS
BEGIN
    SELECT MaPhong, TenPhong, SoMay, Giatheogio
    FROM Phong
END
go

--
CREATE PROCEDURE sp_UpdateTongTien
    @MaHD INT,
    @Amount DECIMAL(18,2)
AS
BEGIN
    UPDATE Hoadon
    SET TongTien = TongTien + @Amount
    WHERE MaHD = @MaHD
END
go

--
CREATE OR ALTER PROCEDURE sp_GetBillList
    @NgayBD DATETIME,
    @NgayKT DATETIME
AS
BEGIN
    SELECT 
        h.MaHD, 
        h.NgayTao, 
        nv.TenNV, 
        h.LoaiHD,
        ISNULL(bt.ChiPhi, 0) AS ChiPhi,
        ISNULL(gm.TongGiaMon, 0) AS GiaMon,
        ISNULL(tm.TongThueMay, 0) AS ThueMay,
        h.TongTien
    FROM Hoadon h
    LEFT JOIN Nhanvien nv ON h.MaNV = nv.MaNV
    LEFT JOIN (
        SELECT bt.MaHD, SUM(gp.ChiPhi) AS ChiPhi
        FROM Baotri bt
        JOIN ChitietBT cbt ON bt.MaBT = cbt.MaBT
        JOIN Giaiphap gp ON cbt.MaGP = gp.MaGP
        GROUP BY bt.MaHD
    ) bt ON h.MaHD = bt.MaHD
    LEFT JOIN (
        SELECT g.MaHD, SUM(g.SoLuong * m.Gia) AS TongGiaMon
        FROM Goimon g
        JOIN Mon m ON g.MaMon = m.MaMon
        GROUP BY g.MaHD
    ) gm ON h.MaHD = gm.MaHD
    LEFT JOIN (
        SELECT tm.MaHD, 
               SUM(DATEDIFF(HOUR, tm.GioBD, ISNULL(tm.GioKT, GETDATE())) * p.Giatheogio) AS TongThueMay
        FROM Thuemay tm
        JOIN Maytinh mt ON tm.MaMay = mt.MaMay
        JOIN Phong p ON mt.MaPhong = p.MaPhong
        GROUP BY tm.MaHD
    ) tm ON h.MaHD = tm.MaHD
    WHERE h.NgayTao BETWEEN @NgayBD AND @NgayKT
END
go

--
