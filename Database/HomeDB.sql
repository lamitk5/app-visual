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

-- Bảng Tài khoản (Admin quản lý)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
CREATE TABLE Users (
    UserId   INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50)  NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100),
    Role     NVARCHAR(20)  CHECK (Role IN ('Admin', 'Staff')),
    IsActive BIT DEFAULT 1  -- Xóa mềm: 1=Hoạt động, 0=Đã xóa/Khóa
);

-- Bảng Cơ sở Homestay
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Homestays' AND xtype='U')
CREATE TABLE Homestays (
    HomestayId   INT IDENTITY(1,1) PRIMARY KEY,
    HomestayName NVARCHAR(200) NOT NULL,
    Address      NVARCHAR(500),
    Phone        NVARCHAR(15),
    IsActive     BIT DEFAULT 1  -- Xóa mềm
);

-- Bảng Loại phòng
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RoomTypes' AND xtype='U')
CREATE TABLE RoomTypes (
    RoomTypeId INT IDENTITY(1,1) PRIMARY KEY,
    TypeName   NVARCHAR(100) NOT NULL,
    BasePrice  DECIMAL(18, 2) DEFAULT 0,
    IsActive   BIT DEFAULT 1  -- Xóa mềm
);

-- Bảng Phòng
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Rooms' AND xtype='U')
CREATE TABLE Rooms (
    RoomId     INT IDENTITY(1,1) PRIMARY KEY,
    RoomName   NVARCHAR(50) NOT NULL,
    RoomTypeId INT FOREIGN KEY REFERENCES RoomTypes(RoomTypeId),
    HomestayId INT FOREIGN KEY REFERENCES Homestays(HomestayId),
    Status     NVARCHAR(50) DEFAULT 'Available',  -- Available, Occupied, Cleaning, Maintenance
    IsActive   BIT DEFAULT 1  -- Xóa mềm
);

-- Bảng Khách hàng
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Customers' AND xtype='U')
CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FullName   NVARCHAR(100) NOT NULL,
    IdCard     NVARCHAR(20) UNIQUE,
    Phone      NVARCHAR(15)
);

-- Bảng Đặt phòng
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Bookings' AND xtype='U')
CREATE TABLE Bookings (
    BookingId    INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId   INT FOREIGN KEY REFERENCES Customers(CustomerId),
    RoomId       INT FOREIGN KEY REFERENCES Rooms(RoomId),
    UserId       INT FOREIGN KEY REFERENCES Users(UserId),
    CheckInDate  DATETIME NOT NULL,
    CheckOutDate DATETIME NULL,       -- Ngày dự kiến trả phòng
    PaymentDate  DATETIME NULL,       -- Ngày thanh toán thực tế
    Deposit      DECIMAL(18, 2) DEFAULT 0,
    Status       NVARCHAR(50) DEFAULT 'Pending',  -- Pending, CheckedIn, CheckedOut, Cancelled
    TotalAmount  DECIMAL(18, 2) DEFAULT 0,
    IsDeleted    BIT DEFAULT 0  -- Xóa mềm cho đơn hàng
);

-- Bảng Danh mục Dịch vụ
-- Có thêm cột ServiceType và Status (OperationUI cần)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Services' AND xtype='U')
CREATE TABLE Services (
    ServiceId   INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName NVARCHAR(100) NOT NULL,
    ServiceType NVARCHAR(50) DEFAULT N'Tiêu dùng',  -- Tiêu dùng, Ăn uống, Di chuyển, ...
    Price       DECIMAL(18, 2) NOT NULL,
    Unit        NVARCHAR(50),                         -- Chai, Gói, Lượt, ...
    Status      NVARCHAR(50) DEFAULT N'Còn cung cấp', -- Còn cung cấp / Ngừng cung cấp
    IsActive    BIT DEFAULT 1                          -- Xóa mềm
);

-- Bảng Chi tiết dịch vụ theo đặt phòng (chính thức)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BookingServices' AND xtype='U')
CREATE TABLE BookingServices (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    BookingId   INT FOREIGN KEY REFERENCES Bookings(BookingId),
    ServiceId   INT FOREIGN KEY REFERENCES Services(ServiceId),
    Quantity    INT DEFAULT 1,
    PriceAtTime DECIMAL(18, 2)  -- Lưu giá lúc bán để tránh thay đổi sau này
);

-- =============================================
-- Bảng Hóa đơn tạm (OperationUI dùng để theo dõi dịch vụ trong phiên khách)
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TempInvoices' AND xtype='U')
CREATE TABLE TempInvoices (
    InvoiceID   INT IDENTITY(1,1) PRIMARY KEY,
    RoomID      INT FOREIGN KEY REFERENCES Rooms(RoomId),
    CreatedDate DATETIME DEFAULT GETDATE(),
    Status      NVARCHAR(50) DEFAULT N'Chưa thanh toán',  -- Chưa thanh toán / Đã thanh toán
    TotalAmount DECIMAL(18, 2) DEFAULT 0,
    PaidDate    DATETIME NULL
);

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TempInvoiceDetails' AND xtype='U')
CREATE TABLE TempInvoiceDetails (
    DetailID    INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID   INT FOREIGN KEY REFERENCES TempInvoices(InvoiceID),
    ServiceID   INT FOREIGN KEY REFERENCES Services(ServiceId),
    Quantity    INT DEFAULT 1,
    UnitPrice   DECIMAL(18, 2) NOT NULL,
    TotalPrice  DECIMAL(18, 2) NOT NULL,
    AddedDate   DATETIME DEFAULT GETDATE(),
    IsCancelled BIT DEFAULT 0  -- Soft-delete: 0=Còn, 1=Đã hủy
);
GO

-- =============================================
-- 3. DỮ LIỆU MẪU ĐỂ TEST HỆ THỐNG
-- =============================================

-- Thêm User (Admin & Staff) - chỉ thêm nếu chưa có
IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'admin')
    INSERT INTO Users (Username, Password, FullName, Role)
    VALUES ('admin', '123', N'Lâm Quản Lý', 'Admin');

IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'letan1')
    INSERT INTO Users (Username, Password, FullName, Role)
    VALUES ('letan1', '123', N'Nguyễn Văn A', 'Staff');

-- Thêm Homestay
IF NOT EXISTS (SELECT 1 FROM Homestays WHERE HomestayName = N'Homestay Phú Diễn')
    INSERT INTO Homestays (HomestayName, Address, Phone)
    VALUES (N'Homestay Phú Diễn', N'Bắc Từ Liêm, Hà Nội', '0912345678'),
           (N'Homestay Sơn Tây',  N'Sơn Tây, Hà Nội',      '0987654321');

-- Thêm Loại Phòng & Phòng
IF NOT EXISTS (SELECT 1 FROM RoomTypes WHERE TypeName = N'Phòng Đơn')
BEGIN
    INSERT INTO RoomTypes (TypeName, BasePrice) VALUES (N'Phòng Đơn', 300000), (N'Phòng Đôi', 550000);
    INSERT INTO Rooms     (RoomName, RoomTypeId, HomestayId)
    VALUES ('P101', 1, 1), ('P102', 2, 1), ('P201', 1, 2);
END

-- Thêm Dịch vụ (có ServiceType & Status)
IF NOT EXISTS (SELECT 1 FROM Services WHERE ServiceName = N'Nước suối')
    INSERT INTO Services (ServiceName, ServiceType, Price, Unit, Status)
    VALUES (N'Nước suối',   N'Ăn uống',    10000,  N'Chai', N'Còn cung cấp'),
           (N'Mì ly',       N'Ăn uống',    15000,  N'Hộp',  N'Còn cung cấp'),
           (N'Thuê xe máy', N'Di chuyển', 150000, N'Ngày', N'Còn cung cấp');

-- Thêm Khách hàng & 1 đơn đặt phòng mẫu
IF NOT EXISTS (SELECT 1 FROM Customers WHERE IdCard = '00123456789')
BEGIN
    INSERT INTO Customers (FullName, IdCard, Phone)
    VALUES (N'Trần Thị B', '00123456789', '0900112233');

    INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, Deposit, Status)
    VALUES (1, 1, 2, GETDATE(), 100000, 'CheckedIn');
END
GO
