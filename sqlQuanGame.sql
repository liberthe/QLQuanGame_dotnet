--create database QLQuanGame;

use QLQuanGame;

CREATE TABLE Phong (
    MaPhong INT PRIMARY KEY ,
    TenPhong NVARCHAR(100),
    SoMay INT,
    Giatheogio DECIMAL(10, 2)
);

INSERT INTO Phong (MaPhong, TenPhong, SoMay, Giatheogio)
VALUES 
    (1, N'Phòng thường', 25, 10000),
    (2, N'Phòng VIP', 5, 30000);

CREATE TABLE Ocung (
    MaOcung INT PRIMARY KEY,
    TenOcung NVARCHAR(100)
);

INSERT INTO Ocung (MaOcung, TenOcung) VALUES
(1, N'Samsung 970 EVO Plus 500GB'),
(2, N'Samsung 970 EVO Plus 1TB'),
(3, N'Kingston NV2 500GB'),
(4, N'Kingston NV2 1TB'),
(5, N'Western Digital Blue 1TB'),
(6, N'Western Digital Blue 2TB'),
(7, N'Seagate BarraCuda 1TB'),
(8, N'Seagate BarraCuda 2TB');


CREATE TABLE Dungluong (
    MaDL INT PRIMARY KEY,
    TenDL NVARCHAR(100)
);

INSERT INTO Dungluong (MaDL, TenDL) VALUES
(1, N'500GB'),
(2, N'1TB'),
(3, N'2TB');

CREATE TABLE Chip (
    MaChip INT PRIMARY KEY,
    TenChip NVARCHAR(100)
);

INSERT INTO Chip (MaChip, TenChip) VALUES
(1, N'Intel Core i5-13400F'),
(2, N'Intel Core i7-13700F'),
(3, N'AMD Ryzen 5 5600X'),
(4, N'AMD Ryzen 7 5800X');

CREATE TABLE RAM (
    MaRAM INT PRIMARY KEY,
    TenRAM NVARCHAR(100)
);

INSERT INTO RAM (MaRAM, TenRAM) VALUES
(1, N'Kingston Fury Beast 16GB DDR4'),
(2, N'Corsair Vengeance LPX 16GB DDR4'),
(3, N'G.Skill Ripjaws V 32GB DDR4');

CREATE TABLE Tocdo (
    MaTD INT PRIMARY KEY,
    TenTD NVARCHAR(50)
);

INSERT INTO Tocdo (MaTD, TenTD) VALUES
(1, N'3200MHz'),
(2, N'3600MHz'),
(3, N'4000MHz');

CREATE TABLE Odia (
    MaOdia INT PRIMARY KEY,
    TenOdia NVARCHAR(50)
);

INSERT INTO Odia (MaOdia, TenOdia) VALUES
(1, N'WD Black 1TB'),
(2, N'Seagate FireCuda 2TB'),
(3, N'Samsung 870 EVO 500GB');

CREATE TABLE Manhinh (
    MaMH INT PRIMARY KEY,
    TenMH NVARCHAR(50),
    Co NVARCHAR(20)
);

INSERT INTO Manhinh (MaMH, TenMH, Co) VALUES
(1, N'LG UltraGear 27GL83A', N'27 inch'),
(2, N'Dell S2721QS', N'27 inch'),
(3, N'ASUS TUF VG259QM', N'24.5 inch');

CREATE TABLE Chuot (
    MaChuot INT PRIMARY KEY,
    TenChuot NVARCHAR(100)
);

INSERT INTO Chuot (MaChuot, TenChuot) VALUES
(1, N'Logitech G203'),
(2, N'Razer DeathAdder V2'),
(3, N'SteelSeries Rival 3');


CREATE TABLE Banphim (
    MaBP INT PRIMARY KEY,
    TenBP NVARCHAR(100)
);

INSERT INTO Banphim (MaBP, TenBP) VALUES
(1, N'Logitech G213 Prodigy'),
(2, N'Razer BlackWidow V3'),
(3, N'HyperX Alloy Core RGB');


Create table Tainghe (
    MaTN int primary key,
    TenTN Nvarchar(100)
    );

    INSERT INTO Tainghe (MaTN, TenTN) VALUES
(1, N'HyperX Cloud II'),
(2, N'Logitech G Pro X'),
(3, N'Razer Kraken X');

    CREATE TABLE Maytinh (
    MaMay INT PRIMARY KEY IDENTITY(1,1),
    TenMay NVARCHAR(100),
    MaPhong INT,
    MaOcung INT,
    MaDL INT,
    MaChip INT,
    MaRAM INT,
    MaTD INT,
    MaOdia INT,
    MaMH INT,
    MaChuot INT,
    MaBP INT,
    MaTN INT,
    Trangthai nvarchar (50),
    FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong),
    FOREIGN KEY (MaOcung) REFERENCES Ocung(MaOcung),
    FOREIGN KEY (MaDL) REFERENCES Dungluong(MaDL),
    FOREIGN KEY (MaChip) REFERENCES Chip(MaChip),
    FOREIGN KEY (MaRAM) REFERENCES RAM(MaRAM),
    FOREIGN KEY (MaTD) REFERENCES Tocdo(MaTD),
    FOREIGN KEY (MaOdia) REFERENCES Odia(MaOdia),
    FOREIGN KEY (MaMH) REFERENCES Manhinh(MaMH),
    FOREIGN KEY (MaChuot) REFERENCES Chuot(MaChuot),
    FOREIGN KEY (MaBP) REFERENCES Banphim(MaBP),
    FOREIGN KEY (MaTN) REFERENCES Tainghe(MaTN)
);

SET IDENTITY_INSERT Maytinh ON;

INSERT INTO Maytinh (MaMay, TenMay, MaPhong, MaOcung, MaDL, MaChip, MaRAM, MaTD, MaOdia, MaMH, MaChuot, MaBP, MaTN, Trangthai) VALUES
(1, N'PC-Thuong-01', 1, 3, 1, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(2, N'PC-Thuong-02', 1, 3, 1, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(3, N'PC-Thuong-03', 1, 4, 2, 1, 1, 1, 3, 2, 1, 1, 1, N'Trong'),
(4, N'PC-Thuong-04', 1, 4, 2, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(5, N'PC-Thuong-05', 1, 5, 2, 1, 2, 1, 1, 3, 3, 3, 3, N'Dang thue'),
(6, N'PC-Thuong-06', 1, 5, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Trong'),
(7, N'PC-Thuong-07', 1, 7, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Dang thue'),
(8, N'PC-Thuong-08', 1, 7, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Trong'),
(9, N'PC-Thuong-09', 1, 3, 1, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(10, N'PC-Thuong-10', 1, 3, 1, 1, 1, 1, 3, 2, 1, 1, 1, N'Trong'),
(11, N'PC-Thuong-11', 1, 4, 2, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(12, N'PC-Thuong-12', 1, 4, 2, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(13, N'PC-Thuong-13', 1, 5, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Trong'),
(14, N'PC-Thuong-14', 1, 5, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Dang thue'),
(15, N'PC-Thuong-15', 1, 7, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Dang thue'),
(16, N'PC-Thuong-16', 1, 7, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Trong'),
(17, N'PC-Thuong-17', 1, 3, 1, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(18, N'PC-Thuong-18', 1, 3, 1, 1, 1, 1, 3, 2, 1, 1, 1, N'Trong'),
(19, N'PC-Thuong-19', 1, 4, 2, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(20, N'PC-Thuong-20', 1, 4, 2, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(21, N'PC-Thuong-21', 1, 5, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Trong'),
(22, N'PC-Thuong-22', 1, 5, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Dang thue'),
(23, N'PC-Thuong-23', 1, 7, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Dang thue'),
(24, N'PC-Thuong-24', 1, 7, 2, 3, 2, 1, 1, 3, 3, 3, 3, N'Trong'),
(25, N'PC-Thuong-25', 1, 3, 1, 1, 1, 1, 3, 2, 1, 1, 1, N'Dang thue'),
(26, N'PC-VIP-01', 2, 1, 1, 2, 3, 2, 2, 1, 2, 2, 2, N'Dang thue'),
(27, N'PC-VIP-02', 2, 1, 1, 2, 3, 2, 2, 1, 2, 2, 2, N'Dang thue'),
(28, N'PC-VIP-03', 2, 2, 2, 4, 3, 3, 2, 1, 2, 2, 2, N'Dang thue'),
(29, N'PC-VIP-04', 2, 2, 2, 4, 3, 3, 2, 1, 2, 2, 2, N'Trong'),
(30, N'PC-VIP-05', 2, 1, 1, 2, 3, 2, 2, 1, 2, 2, 2, N'Dang thue');

SET IDENTITY_INSERT Maytinh OFF;

CREATE TABLE Nhanvien (
    MaNV INT PRIMARY KEY IDENTITY(1,1),
    TenNV NVARCHAR(100),
    Gioitinh Nvarchar(10),
    Namsinh DATE,
    Diachi NVARCHAR(100),
    SDT VARCHAR(20)
);
 alter table nhanvien add email varchar (100);
 --delete Nhanvien
SET IDENTITY_INSERT Nhanvien ON;

INSERT INTO Nhanvien (MaNV, TenNV, Gioitinh, Namsinh, Diachi, SDT, Email) VALUES 
(1, N'Bùi Thị Hoa', N'Nữ', '2004-01-01', N'Hoài Đức - Ha Noi', '0358777999', 'buithihoaaa10a3@gmail.com'),
(2, N'Nguyen Thi Phuong Anh', N'Nữ', '2004-01-01', N'Dong Da - Ha Noi', '0359788909', 'panh2k4@gmail.com'),
(3, N'Nguyen Thi Hong Ngoc', N'Nữ', '2004-01-01', N'Thanh Xuan - Ha Noi', '012342321', 'hngoc2101@gmail.com'),
(4, N'Nguyen Thi Hai Yen', N'Nữ', '2004-01-01', N'Ha Dong - Ha Noi', '0338928983', 'yennguyen080404@gmail.com'),
(5, N'Nguyen Thi Hai Yen', N'Nữ', '2004-01-01', N'Dong Da - Ha Noi', '0879382783', 'nguyenyen2310@gmail.com');

SET IDENTITY_INSERT Nhanvien OFF;


CREATE TABLE Taikhoan (
    MaTK INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50),
    MatKhau VARCHAR(50),
    MaNV INT,
    NgayTao DATE,
    FOREIGN KEY (MaNV) REFERENCES Nhanvien(MaNV)
);

SET IDENTITY_INSERT Taikhoan ON;

INSERT INTO Taikhoan (MaTK, TenDangNhap, MatKhau, MaNV, NgayTao) VALUES
(1, 'hoabt1', '12345678', 1, '2025-05-01'),
(2, 'phuonganh2', '12345678', 2, '2025-05-03'),
(3, 'hongngoc3', '12345678', 3, '2025-05-05'),
(4, 'haiyen4', '12345678', 4, '2025-05-07'),
(5, 'haiyen5', '12345678', 5, '2025-05-09');

SET IDENTITY_INSERT Taikhoan OFF;


CREATE TABLE LoaiMon (
    MaLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(100)
);

SET IDENTITY_INSERT LoaiMon ON;

INSERT INTO LoaiMon (MaLoai, TenLoai) VALUES
(1, N'Đồ ăn'),
(2, N'Đồ uống'),
(3, N'Snack');

SET IDENTITY_INSERT LoaiMon OFF;


CREATE TABLE Mon (
    MaMon INT IDENTITY(1,1) PRIMARY KEY,
    TenMon NVARCHAR(100),
    Gia DECIMAL(10,2),
    GiaNhap DECIMAL(10,2),
    TonKho INT,
    MaLoai INT,
    Anh NVARCHAR(255),
    FOREIGN KEY (MaLoai) REFERENCES LoaiMon(MaLoai)
);

SET IDENTITY_INSERT Mon ON;

INSERT INTO Mon (MaMon, TenMon, Gia, GiaNhap, TonKho, MaLoai, Anh) VALUES
(1, N'Mì Hảo Hảo', 15000.00, 10000.00, 50, 1, N'mi_hao_hao.jpg'),
(2, N'Xúc xích Đức', 25000.00, 17000.00, 30, 1, N'xuc_xich_duc.jpg'),
(3, N'Trà sữa trân châu', 35000.00, 24000.00, 40, 2, N'tra_sua_tran_chau.jpg'),
(4, N'Coca-Cola 330ml', 15000.00, 10000.00, 50, 2, N'coca_cola.jpg'),
(5, N'Cà phê sữa đá', 20000.00, 14000.00, 25, 2, N'ca_phe_sua_da.jpg'),
(6, N'Khoai tây chiên Lay’s', 20000.00, 13000.00, 45, 3, N'khoai_tay_chien_lays.jpg'),
(7, N'Bim bim Oishi', 15000.00, 10000.00, 50, 3, N'bim_bim_oishi.jpg'),
(8, N'Kẹo mút Chupa Chups', 10000.00, 6000.00, 60, 3, N'keo_mut_chupa_chups.jpg'),
(9, N'Snack bắp rang', 15000.00, 9000.00, 35, 3, N'snack_bap_rang.jpg'),
(10, N'Sting dâu 330ml', 15000.00, 10000.00, 50, 2, N'sting_dau.jpg');

SET IDENTITY_INSERT Mon OFF;


CREATE TABLE Hoadon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    LoaiHD NVARCHAR(10),
    NgayTao DATETIME,
    MaNV INT,
    TongTien DECIMAL(10,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (MaNV) REFERENCES Nhanvien(MaNV)
);

SET IDENTITY_INSERT Hoadon ON;

INSERT INTO Hoadon (MaHD, LoaiHD, NgayTao, MaNV, TongTien) VALUES
(1, N'Thu', '2025-05-15 10:00:00', 1, 0),
(2, N'Thu', '2025-05-16 15:30:00', 2, 0),
(3, N'Chi', '2025-05-17 09:00:00', 3, 0),
(4, N'Thu', '2025-05-18 20:00:00', 4, 0),
(5, N'Chi', '2025-05-19 14:00:00', 5, 0),
(32, N'Chi', '2025-05-25 14:00:00', 1, 0),
(36, N'Chi', '2025-05-25 14:00:00', 1, 0),
(38, N'Chi', '2025-05-25 14:00:00', 1, 0),
(42, N'Chi', '2025-05-25 14:00:00', 1, 0),
(44, N'Chi', '2025-05-25 14:00:00', 1, 0),
(48, N'Chi', '2025-05-25 14:00:00', 1, 0),
(51, N'Chi', '2025-05-25 14:00:00', 1, 0),
(55, N'Chi', '2025-05-25 14:00:00', 1, 0),
(58, N'Chi', '2025-05-25 14:00:00', 1, 0);

SET IDENTITY_INSERT Hoadon OFF;


CREATE TABLE Goimon (
    MaHD INT,
    MaMon INT,
    SoLuong INT,
    PRIMARY KEY (MaHD, MaMon),
    FOREIGN KEY (MaHD) REFERENCES Hoadon(MaHD),
    FOREIGN KEY (MaMon) REFERENCES Mon(MaMon)
);

INSERT INTO Goimon (MaHD, MaMon, SoLuong) VALUES
-- Hóa đơn 1 (Thu)
(1, 1, 2), -- Mì Hảo Hảo, 2 gói
(1, 3, 1), -- Trà sữa trân châu, 1 ly
(1, 7, 3), -- Bim bim Oishi, 3 gói
-- Hóa đơn 2 (Thu)
(2, 4, 2), -- Coca-Cola 330ml, 2 lon
(2, 6, 2), -- Khoai tây chiên Lay’s, 2 gói
(2, 2, 1), -- Xúc xích Đức, 1 cái
-- Hóa đơn 4 (Thu)
(4, 5, 1), -- Cà phê sữa đá, 1 ly
(4, 8, 3), -- Kẹo mút Chupa Chups, 3 cái
(4, 9, 2); -- Snack bắp rang, 2 gói

CREATE TABLE Thuemay (
    MaTM INT IDENTITY(1,1) PRIMARY KEY,
    MaMay INT,
    GioBD DATETIME,
    GioKT DATETIME,
    MaHD INT,
    FOREIGN KEY (MaMay) REFERENCES Maytinh(MaMay),
    FOREIGN KEY (MaHD) REFERENCES Hoadon(MaHD)
);

SET IDENTITY_INSERT Thuemay ON;

INSERT INTO Thuemay (MaTM, MaMay, GioBD, GioKT, MaHD) VALUES
(1, 1, '2025-05-15 09:30:00', '2025-05-15 11:30:00', 1),
(2, 2, '2025-05-15 10:00:00', NULL, 1),
(3, 4, '2025-05-16 15:00:00', '2025-05-16 17:00:00', 2),
(4, 5, '2025-05-16 15:30:00', NULL, 2),
(5, 11, '2025-05-18 19:30:00', '2025-05-18 21:30:00', 4),
(6, 26, '2025-05-18 20:00:00', NULL, 4);

SET IDENTITY_INSERT Thuemay OFF;

CREATE TABLE Tinhtrang (
    MaTT INT PRIMARY KEY,
    TenTT NVARCHAR(100)
);

INSERT INTO Tinhtrang (MaTT, TenTT) VALUES
(1, N'Máy không khởi động'),
(2, N'Màn hình xanh (BSOD)'),
(3, N'Chơi game bị lag'),
(4, N'Không kết nối được mạng');

CREATE TABLE Nguyennhan (
    MaNN INT PRIMARY KEY,
    TenNN NVARCHAR(100)
);

INSERT INTO Nguyennhan (MaNN, TenNN) VALUES
(1, N'Hỏng ổ cứng'),
(2, N'Lỗi driver card đồ họa'),
(3, N'RAM không đủ'),
(4, N'Mất kết nối router');

CREATE TABLE Giaiphap (
    MaGP INT PRIMARY KEY,
    TenGP NVARCHAR(100),
    Chiphi Decimal (10,2)
);

INSERT INTO Giaiphap (MaGP, TenGP, Chiphi) VALUES
(1, N'Thay ổ cứng mới', 1200000.00),
(2, N'Cài lại driver card đồ họa', 200000.00),
(3, N'Nâng cấp RAM 16GB', 1500000.00),
(4, N'Cấu hình lại router', 300000.00);

CREATE TABLE NhaBT (
    MaNBT INT PRIMARY KEY,
    TenNBT NVARCHAR(100),
    DiaChi NVARCHAR(200),
    SDT NVARCHAR(20)
);

INSERT INTO NhaBT (MaNBT, TenNBT, DiaChi, SDT) VALUES
(1, N'Công ty TNHH Sửa chữa PC Hà Nội', N'123 Cầu Giấy, Hà Nội', '0987654321'),
(2, N'Dịch vụ IT Minh Anh', N'45 Hoài Đức, Hà Nội', '0912345678'),
(3, N'Trung tâm Bảo hành Phong Vũ', N'10 Thanh Xuân, Hà Nội', '0935123456'),
(4, N'Kỹ thuật viên Quang Huy', N'78 Đống Đa, Hà Nội', '0908765432');

-- Đảm bảo bảng Baotri đã được tạo (nếu chưa tạo, chạy lệnh sau)
CREATE TABLE Baotri (
    MaBT INT IDENTITY(1,1) PRIMARY KEY,
    NgayBT DATE NOT NULL,
    MaNBT INT NOT NULL,
    MaHD INT NOT NULL,
    GhiChu NVARCHAR(255),
    TrangThai NVARCHAR(50),
    FOREIGN KEY (MaNBT) REFERENCES NhaBT(MaNBT),
    FOREIGN KEY (MaHD) REFERENCES Hoadon(MaHD),
    CONSTRAINT CHK_Baotri_TrangThai CHECK (TrangThai IN (N'Đã hoàn tất', N'Chưa thanh toán')),
    CONSTRAINT CHK_Baotri_NgayBT CHECK (NgayBT <= CAST(GETDATE() AS DATE))
);

-- Chèn dữ liệu vào bảng Baotri
SET IDENTITY_INSERT Baotri ON;

INSERT INTO Baotri (MaBT, NgayBT, MaNBT, MaHD, GhiChu, TrangThai) VALUES
(1, '2025-05-25', 1, 32, N'Giải pháp: Cài lại driver card đồ họa', N'Đã hoàn tất'),
(2, '2025-05-25', 1, 38, N'Giải pháp: Cấu hình lại router', N'Đã hoàn tất'),
(3, '2025-05-25', 1, 36, N'Giải pháp: Cài lại driver card đồ họa', N'Đã hoàn tất'),
(4, '2025-05-25', 1, 42, N'Cài lại driver card đồ họa', N'Đã hoàn tất'),
(5, '2025-05-25', 1, 44, N'Cấu hình lại router', N'Đã hoàn tất'),
(6, '2025-05-25', 1, 48, N'Nâng cấp RAM 16GB', N'Đã hoàn tất'),
(7, '2025-05-25', 1, 48, N'Cấu hình lại router', N'Đã hoàn tất'),
(8, '2025-05-25', 1, 51, N'Nâng cấp RAM 16GB', N'Đã hoàn tất'),
(9, '2025-05-25', 1, 55, N'Nâng cấp RAM 16GB', N'Đã hoàn tất'),
(10, '2025-05-25', 1, 58, N'Cấu hình lại router', N'Đã hoàn tất');

SET IDENTITY_INSERT Baotri OFF;

CREATE TABLE ChitietBT (
    MaBT INT,
    MaMay INT,
    MaTT INT,
    MaNN INT,
    MaGP INT,
    PRIMARY KEY (MaBT, MaMay),
    FOREIGN KEY (MaBT) REFERENCES Baotri(MaBT),
    FOREIGN KEY (MaMay) REFERENCES Maytinh(MaMay),
    FOREIGN KEY (MaTT) REFERENCES Tinhtrang(MaTT),
    FOREIGN KEY (MaNN) REFERENCES Nguyennhan(MaNN),
    FOREIGN KEY (MaGP) REFERENCES Giaiphap(MaGP)
);

INSERT INTO ChitietBT (MaBT, MaMay, MaTT, MaNN, MaGP) VALUES
(1, 1, 2, 2, 2), -- Máy 1: Màn hình xanh, lỗi driver, cài lại driver (khớp với ghi chú)
(1, 5, 2, 2, 2), -- Máy 5: Màn hình xanh, lỗi driver, cài lại driver
(2, 11, 4, 4, 4), -- Máy 11: Không kết nối mạng, mất kết nối router, cấu hình router (khớp với ghi chú)
(3, 27, 2, 2, 2), -- Máy 27: Màn hình xanh, lỗi driver, cài lại driver (khớp với ghi chú)
(4, 1, 2, 2, 2), -- MaBT 4: Cài lại driver card đồ họa
(5, 5, 4, 4, 4), -- MaBT 5: Cấu hình lại router
(6, 11, 3, 3, 3), -- MaBT 6: Nâng cấp RAM 16GB
(7, 26, 4, 4, 4), -- MaBT 7: Cấu hình lại router
(8, 27, 3, 3, 3), -- MaBT 8: Nâng cấp RAM 16GB
(9, 1, 3, 3, 3),  -- MaBT 9: Nâng cấp RAM 16GB
(10, 5, 4, 4, 4);
UPDATE Hoadon
SET TongTien = 
    CASE MaHD
        WHEN 1 THEN 140000.00 -- Thu: Thuê máy (30,000) + Gọi món (110,000)
        WHEN 2 THEN 125000.00 -- Thu: Thuê máy (30,000) + Gọi món (95,000)
        WHEN 3 THEN 2400000.00 -- Chi: Bảo trì (2,400,000)
        WHEN 4 THEN 120000.00 -- Thu: Thuê máy (40,000) + Gọi món (80,000)
        WHEN 5 THEN 2000000.00 -- Chi: Bảo trì (2,000,000)
        -- Giả định TongTien cho MaHD 32, 36, 38, 42, 44, 48, 51, 55, 58 (Chi: Bảo trì)
        WHEN 32 THEN 200000.00
        WHEN 36 THEN 200000.00
        WHEN 38 THEN 300000.00
        WHEN 42 THEN 200000.00
        WHEN 44 THEN 300000.00
        WHEN 48 THEN 1500000.00
        WHEN 51 THEN 1500000.00
        WHEN 55 THEN 1500000.00
        WHEN 58 THEN 300000.00
    END
WHERE MaHD IN (1, 2, 3, 4, 5, 32, 36, 38, 42, 44, 48, 51, 55, 58);