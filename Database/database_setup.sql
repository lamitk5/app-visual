-- =============================================
-- 1. TẠO CƠ SỞ DỮ LIỆU
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HomestayDB')
BEGIN
    CREATE DATABASE HomestayDB;
END
GO

USE HomestayDB;
GO
-- =============================================
-- 2. TẠO CẤU TRÚC BẢNG (3-LAYER COMPATIBLE)
-- =============================================

-- Bảng Tài khoản (Người 1 quản lý)
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100),
    Role NVARCHAR(20) CHECK (Role IN ('Admin', 'Staff')),
    IsActive BIT DEFAULT 1 -- Xóa mềm: 1=Hoạt động, 0=Đã xóa/Khóa
);

-- Bảng Cơ sở Homestay (Người 1 quản lý)
CREATE TABLE Homestays (
    HomestayId INT IDENTITY(1,1) PRIMARY KEY,
    HomestayName NVARCHAR(200) NOT NULL,
    Address NVARCHAR(500),
    Phone NVARCHAR(15),
    IsActive BIT DEFAULT 1 -- Xóa mềm
);

-- Bảng Loại phòng (Người 2 quản lý)
CREATE TABLE RoomTypes (
    RoomTypeId INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(100) NOT NULL, 
    BasePrice DECIMAL(18, 2) DEFAULT 0,
    IsActive BIT DEFAULT 1 -- Xóa mềm
);

-- Bảng Phòng (Người 2 quản lý)
CREATE TABLE Rooms (
    RoomId INT IDENTITY(1,1) PRIMARY KEY,
    RoomName NVARCHAR(50) NOT NULL,
    RoomTypeId INT FOREIGN KEY REFERENCES RoomTypes(RoomTypeId),
    HomestayId INT FOREIGN KEY REFERENCES Homestays(HomestayId),
    Status NVARCHAR(50) DEFAULT 'Available', -- Available, Occupied, Cleaning, Maintenance
    IsActive BIT DEFAULT 1 -- Xóa mềm
);

-- Bảng Khách hàng (Người 3 quản lý)
CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    IdCard NVARCHAR(20) UNIQUE,
    Phone NVARCHAR(15)
);

-- Bảng Đặt phòng (Người 3 & 5 quản lý)
CREATE TABLE Bookings (
    BookingId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT FOREIGN KEY REFERENCES Customers(CustomerId),
    RoomId INT FOREIGN KEY REFERENCES Rooms(RoomId),
    UserId INT FOREIGN KEY REFERENCES Users(UserId), -- Lưu vết nhân viên nào tạo đơn
    CheckInDate DATETIME NOT NULL,
    CheckOutDate DATETIME NULL, -- Ngày dự kiến trả phòng
    PaymentDate DATETIME NULL, -- Ngày thanh toán thực tế
    Deposit DECIMAL(18, 2) DEFAULT 0,
    Status NVARCHAR(50) DEFAULT 'Pending', -- Pending, CheckedIn, CheckedOut, Cancelled
    TotalAmount DECIMAL(18, 2) DEFAULT 0,
    IsDeleted BIT DEFAULT 0 -- Xóa mềm cho đơn hàng
);

-- Bảng Danh mục Dịch vụ (Người 4 quản lý)
CREATE TABLE Services (
    ServiceId INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Unit NVARCHAR(50), -- Chai, Gói, Lượt
    IsActive BIT DEFAULT 1 -- Xóa mềm
);

-- Bảng Chi tiết dịch vụ khách sử dụng (Người 4 quản lý)
CREATE TABLE BookingServices (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT FOREIGN KEY REFERENCES Bookings(BookingId),
    ServiceId INT FOREIGN KEY REFERENCES Services(ServiceId),
    Quantity INT DEFAULT 1,
    PriceAtTime DECIMAL(18, 2) -- Lưu giá lúc bán để tránh thay đổi sau này
);
GO

-- =============================================
-- 3. DỮ LIỆU MẪU ĐỂ TEST HỆ THỐNG
-- =============================================

-- Thêm User (Admin & Staff)
INSERT INTO Users (Username, Password, FullName, Role) 
VALUES ('admin', '123', N'Lâm Quản Lý', 'Admin'), 
       ('letan1', '123', N'Nguyễn Văn A', 'Staff');

-- Thêm Homestay
INSERT INTO Homestays (HomestayName, Address, Phone) 
VALUES (N'Homestay Phú Diễn', N'Bắc Từ Liêm, Hà Nội', '0912345678'),
       (N'Homestay Sơn Tây', N'Sơn Tây, Hà Nội', '0987654321');

-- Thêm Loại Phòng & Phòng
INSERT INTO RoomTypes (TypeName, BasePrice) VALUES (N'Phòng Đơn', 300000), (N'Phòng Đôi', 550000);
INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId) VALUES ('P101', 1, 1), ('P102', 2, 1), ('P201', 1, 2);

-- Thêm Dịch vụ
INSERT INTO Services (ServiceName, Price, Unit) 
VALUES (N'Nước suối', 10000, N'Chai'), (N'Mì ly', 15000, N'Hộp'), (N'Thuê xe máy', 150000, N'Ngày');

-- Thêm Khách hàng & 1 đơn đặt phòng mẫu
INSERT INTO Customers (FullName, IdCard, Phone) VALUES (N'Trần Thị B', '00123456789', '0900112233');
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, Deposit, Status) 
VALUES (1, 1, 2, GETDATE(), 100000, 'CheckedIn');
GO
    