USE HomestayDB;
GO

-- =============================================
-- 1. Them nhieu Homestay
-- =============================================
IF NOT EXISTS (SELECT 1 FROM Homestays WHERE HomestayName = N'Homestay Ha Long')
BEGIN
    INSERT INTO Homestays (HomestayName, Address, Phone) VALUES
    (N'Homestay Ha Long',    N'Ha Long, Quang Ninh',    '0911000111'),
    (N'Homestay Da Nang',    N'Ngu Hanh Son, Da Nang',  '0922000222'),
    (N'Homestay Da Lat',     N'TP Da Lat, Lam Dong',    '0933000333'),
    (N'Homestay Phu Quoc',   N'Phu Quoc, Kien Giang',   '0944000444'),
    (N'Homestay Sapa',       N'Sa Pa, Lao Cai',         '0955000555'),
    (N'Homestay Hoi An',     N'Hoi An, Quang Nam',      '0966000666'),
    (N'Homestay Nha Trang',  N'Nha Trang, Khanh Hoa',   '0977000777');
END
GO

-- =============================================
-- 2. Them nhieu RoomTypes
-- =============================================
IF NOT EXISTS (SELECT 1 FROM RoomTypes WHERE TypeName = N'Phong VIP')
BEGIN
    INSERT INTO RoomTypes (TypeName, BasePrice) VALUES
    (N'Phong VIP',       1200000),
    (N'Phong Gia Dinh',  800000),
    (N'Phong Dorm',      150000),
    (N'Bungalow',        1500000);
END
GO

-- =============================================
-- 3. Them nhieu Rooms cho tung Homestay
-- =============================================

-- Homestay 1 (Phu Dien) - da co P101, P102
IF NOT EXISTS (SELECT 1 FROM Rooms WHERE RoomName = 'P103')
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status) VALUES
    ('P103', 3, 1, 'Available'),  -- Phong VIP
    ('P104', 4, 1, 'Available'),  -- Phong Gia Dinh
    ('P105', 1, 1, 'Available');  -- Phong Don
END

-- Homestay 2 (Son Tay) - da co P201
IF NOT EXISTS (SELECT 1 FROM Rooms WHERE RoomName = 'P202')
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status) VALUES
    ('P202', 2, 2, 'Available'),  -- Phong Doi
    ('P203', 3, 2, 'Available'),  -- Phong VIP
    ('P204', 1, 2, 'Available');  -- Phong Don
END

-- Homestay 3 (Ha Long)
IF NOT EXISTS (SELECT 1 FROM Rooms WHERE RoomName = 'HL101')
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status) VALUES
    ('HL101', 1, 3, 'Available'),
    ('HL102', 2, 3, 'Available'),
    ('HL103', 3, 3, 'Available'),
    ('HL104', 4, 3, 'Available'),
    ('HL105', 5, 3, 'Available');
END

-- Homestay 4 (Da Nang)
IF NOT EXISTS (SELECT 1 FROM Rooms WHERE RoomName = 'DN101')
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status) VALUES
    ('DN101', 1, 4, 'Available'),
    ('DN102', 2, 4, 'Available'),
    ('DN103', 3, 4, 'Available'),
    ('DN104', 4, 4, 'Available'),
    ('DN105', 5, 4, 'Available'),
    ('DN106', 1, 4, 'Available');
END

-- Homestay 5 (Da Lat)
IF NOT EXISTS (SELECT 1 FROM Rooms WHERE RoomName = 'DL101')
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status) VALUES
    ('DL101', 1, 5, 'Available'),
    ('DL102', 2, 5, 'Available'),
    ('DL103', 3, 5, 'Available'),
    ('DL104', 5, 5, 'Available'),
    ('DL105', 5, 5, 'Available');
END

-- Homestay 6 (Phu Quoc)
IF NOT EXISTS (SELECT 1 FROM Rooms WHERE RoomName = 'PQ101')
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status) VALUES
    ('PQ101', 1, 6, 'Available'),
    ('PQ102', 2, 6, 'Available'),
    ('PQ103', 3, 6, 'Available'),
    ('PQ104', 4, 6, 'Available'),
    ('PQ105', 5, 6, 'Available');
END

-- Homestay 7 (Sapa)
IF NOT EXISTS (SELECT 1 FROM Rooms WHERE RoomName = 'SP101')
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status) VALUES
    ('SP101', 1, 7, 'Available'),
    ('SP102', 2, 7, 'Available'),
    ('SP103', 5, 7, 'Available'),
    ('SP104', 5, 7, 'Available');
END

-- Homestay 8 (Hoi An)
IF NOT EXISTS (SELECT 1 FROM Rooms WHERE RoomName = 'HA101')
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status) VALUES
    ('HA101', 1, 8, 'Available'),
    ('HA102', 2, 8, 'Available'),
    ('HA103', 3, 8, 'Available'),
    ('HA104', 4, 8, 'Available');
END

-- Homestay 9 (Nha Trang)
IF NOT EXISTS (SELECT 1 FROM Rooms WHERE RoomName = 'NT101')
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeId, HomestayId, Status) VALUES
    ('NT101', 1, 9, 'Available'),
    ('NT102', 2, 9, 'Available'),
    ('NT103', 3, 9, 'Available'),
    ('NT104', 4, 9, 'Available'),
    ('NT105', 5, 9, 'Available');
END
GO

-- =============================================
-- 4. Them nhieu Customers
-- =============================================
IF NOT EXISTS (SELECT 1 FROM Customers WHERE IdCard = '00123456790')
BEGIN
    INSERT INTO Customers (FullName, IdCard, Phone) VALUES
    (N'Nguyen Van An',      '00123456790', '0911222333'),
    (N'Le Thi Binh',        '00123456791', '0922333444'),
    (N'Pham Van Cuong',     '00123456792', '0933444555'),
    (N'Hoang Thi Dung',     '00123456793', '0944555666'),
    (N'Do Minh Duc',        '00123456794', '0955666777'),
    (N'Vu Thi Giang',       '00123456795', '0966777888'),
    (N'Bui Van Hung',       '00123456796', '0977888999'),
    (N'Dang Thi Lan',       '00123456797', '0988999000'),
    (N'Trinh Van Long',     '00123456798', '0999000111'),
    (N'Ngo Thi Mai',        '00123456799', '0900111222'),
    (N'Tran Van Khoa',      '00123456800', '0911333444'),
    (N'Nguyen Thi Hoa',     '00123456801', '0922444555'),
    (N'Le Van Tuan',        '00123456802', '0933555666'),
    (N'Pham Thi Nga',       '00123456803', '0944666777'),
    (N'Hoang Van Phuc',     '00123456804', '0955777888');
END
GO

-- =============================================
-- 5. Them nhieu Services
-- =============================================
IF NOT EXISTS (SELECT 1 FROM Services WHERE ServiceName = N'Bia Tiger')
BEGIN
    INSERT INTO Services (ServiceName, ServiceType, Price, Unit, Status) VALUES
    (N'Bia Tiger',          N'An uong',    20000,  N'Lon',   N'Con cung cap'),
    (N'Bia Heineken',       N'An uong',    30000,  N'Lon',   N'Con cung cap'),
    (N'Nuoc cam',           N'An uong',    25000,  N'Ly',    N'Con cung cap'),
    (N'Ca phe den',         N'An uong',    20000,  N'Ly',    N'Con cung cap'),
    (N'Ca phe sua',         N'An uong',    25000,  N'Ly',    N'Con cung cap'),
    (N'Com trua',           N'An uong',    50000,  N'Phan',  N'Con cung cap'),
    (N'Com toi',            N'An uong',    60000,  N'Phan',  N'Con cung cap'),
    (N'Giat do',            N'Tieu dung',  30000,  N'Kg',    N'Con cung cap'),
    (N'Thu xe dap',         N'Di chuyen',  50000,  N'Ngay',  N'Con cung cap'),
    (N'Thu xe oto 4 cho',   N'Di chuyen', 800000,  N'Ngay',  N'Con cung cap'),
    (N'Tour tham quan',     N'Di chuyen', 200000,  N'Nguoi', N'Con cung cap'),
    (N'Spa massage',        N'Tieu dung', 300000,  N'Luot',  N'Con cung cap');
END
GO

-- =============================================
-- 6. Lay RoomId cho tung Homestay de tao Bookings
-- =============================================
DECLARE @Room_HL1 INT, @Room_HL2 INT, @Room_HL3 INT;
DECLARE @Room_DN1 INT, @Room_DN2 INT, @Room_DN3 INT;
DECLARE @Room_DL1 INT, @Room_DL2 INT, @Room_DL3 INT;
DECLARE @Room_PQ1 INT, @Room_PQ2 INT, @Room_PQ3 INT;
DECLARE @Room_SP1 INT, @Room_SP2 INT;
DECLARE @Room_HA1 INT, @Room_HA2 INT;
DECLARE @Room_NT1 INT, @Room_NT2 INT, @Room_NT3 INT;

SELECT @Room_HL1 = RoomId FROM Rooms WHERE RoomName = 'HL101';
SELECT @Room_HL2 = RoomId FROM Rooms WHERE RoomName = 'HL102';
SELECT @Room_HL3 = RoomId FROM Rooms WHERE RoomName = 'HL103';
SELECT @Room_DN1 = RoomId FROM Rooms WHERE RoomName = 'DN101';
SELECT @Room_DN2 = RoomId FROM Rooms WHERE RoomName = 'DN102';
SELECT @Room_DN3 = RoomId FROM Rooms WHERE RoomName = 'DN103';
SELECT @Room_DL1 = RoomId FROM Rooms WHERE RoomName = 'DL101';
SELECT @Room_DL2 = RoomId FROM Rooms WHERE RoomName = 'DL102';
SELECT @Room_DL3 = RoomId FROM Rooms WHERE RoomName = 'DL103';
SELECT @Room_PQ1 = RoomId FROM Rooms WHERE RoomName = 'PQ101';
SELECT @Room_PQ2 = RoomId FROM Rooms WHERE RoomName = 'PQ102';
SELECT @Room_PQ3 = RoomId FROM Rooms WHERE RoomName = 'PQ103';
SELECT @Room_SP1 = RoomId FROM Rooms WHERE RoomName = 'SP101';
SELECT @Room_SP2 = RoomId FROM Rooms WHERE RoomName = 'SP102';
SELECT @Room_HA1 = RoomId FROM Rooms WHERE RoomName = 'HA101';
SELECT @Room_HA2 = RoomId FROM Rooms WHERE RoomName = 'HA102';
SELECT @Room_NT1 = RoomId FROM Rooms WHERE RoomName = 'NT101';
SELECT @Room_NT2 = RoomId FROM Rooms WHERE RoomName = 'NT102';
SELECT @Room_NT3 = RoomId FROM Rooms WHERE RoomName = 'NT103';

-- =============================================
-- 7. Them Bookings trải dài 30 ngay cho nhieu Homestay
-- =============================================

-- === Homestay Ha Long (HomestayId=3) ===
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (2, @Room_HL1, 1, DATEADD(DAY, -30, GETDATE()), DATEADD(DAY, -28, GETDATE()), 200000, 'CheckedOut', 2400000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (3, @Room_HL2, 1, DATEADD(DAY, -25, GETDATE()), DATEADD(DAY, -23, GETDATE()), 300000, 'CheckedOut', 1600000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (4, @Room_HL3, 2, DATEADD(DAY, -18, GETDATE()), DATEADD(DAY, -16, GETDATE()), 500000, 'CheckedOut', 3600000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (5, @Room_HL1, 1, DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -8, GETDATE()), 200000, 'CheckedOut', 2400000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (6, @Room_HL2, 2, DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -3, GETDATE()), 300000, 'CheckedOut', 1600000);

-- === Homestay Da Nang (HomestayId=4) ===
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (7, @Room_DN1, 1, DATEADD(DAY, -28, GETDATE()), DATEADD(DAY, -25, GETDATE()), 300000, 'CheckedOut', 3600000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (8, @Room_DN2, 2, DATEADD(DAY, -22, GETDATE()), DATEADD(DAY, -19, GETDATE()), 500000, 'CheckedOut', 2400000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (9, @Room_DN3, 1, DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -12, GETDATE()), 400000, 'CheckedOut', 4800000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (10, @Room_DN1, 2, DATEADD(DAY, -8, GETDATE()), DATEADD(DAY, -5, GETDATE()), 300000, 'CheckedOut', 3600000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (11, @Room_DN2, 1, DATEADD(DAY, -3, GETDATE()), DATEADD(DAY, -1, GETDATE()), 500000, 'CheckedOut', 2400000);

-- === Homestay Da Lat (HomestayId=5) ===
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (12, @Room_DL1, 2, DATEADD(DAY, -27, GETDATE()), DATEADD(DAY, -24, GETDATE()), 200000, 'CheckedOut', 3600000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (13, @Room_DL2, 1, DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, -17, GETDATE()), 300000, 'CheckedOut', 2400000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (14, @Room_DL3, 2, DATEADD(DAY, -12, GETDATE()), DATEADD(DAY, -10, GETDATE()), 400000, 'CheckedOut', 3600000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (15, @Room_DL1, 1, DATEADD(DAY, -6, GETDATE()), DATEADD(DAY, -4, GETDATE()), 200000, 'CheckedOut', 2400000);

-- === Homestay Phu Quoc (HomestayId=6) ===
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (2, @Room_PQ1, 1, DATEADD(DAY, -26, GETDATE()), DATEADD(DAY, -22, GETDATE()), 500000, 'CheckedOut', 4800000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (3, @Room_PQ2, 2, DATEADD(DAY, -18, GETDATE()), DATEADD(DAY, -15, GETDATE()), 600000, 'CheckedOut', 3200000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (4, @Room_PQ3, 1, DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -7, GETDATE()), 800000, 'CheckedOut', 7200000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (5, @Room_PQ1, 2, DATEADD(DAY, -4, GETDATE()), DATEADD(DAY, -2, GETDATE()), 500000, 'CheckedOut', 4800000);

-- === Homestay Sapa (HomestayId=7) ===
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (6, @Room_SP1, 1, DATEADD(DAY, -24, GETDATE()), DATEADD(DAY, -21, GETDATE()), 200000, 'CheckedOut', 3600000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (7, @Room_SP2, 2, DATEADD(DAY, -16, GETDATE()), DATEADD(DAY, -13, GETDATE()), 300000, 'CheckedOut', 2400000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (8, @Room_SP1, 1, DATEADD(DAY, -7, GETDATE()), DATEADD(DAY, -5, GETDATE()), 200000, 'CheckedOut', 2400000);

-- === Homestay Hoi An (HomestayId=8) ===
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (9, @Room_HA1, 2, DATEADD(DAY, -23, GETDATE()), DATEADD(DAY, -20, GETDATE()), 300000, 'CheckedOut', 3600000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (10, @Room_HA2, 1, DATEADD(DAY, -14, GETDATE()), DATEADD(DAY, -11, GETDATE()), 400000, 'CheckedOut', 2400000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (11, @Room_HA1, 2, DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -3, GETDATE()), 300000, 'CheckedOut', 3600000);

-- === Homestay Nha Trang (HomestayId=9) ===
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (12, @Room_NT1, 1, DATEADD(DAY, -21, GETDATE()), DATEADD(DAY, -18, GETDATE()), 300000, 'CheckedOut', 3600000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (13, @Room_NT2, 2, DATEADD(DAY, -13, GETDATE()), DATEADD(DAY, -10, GETDATE()), 400000, 'CheckedOut', 2400000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (14, @Room_NT3, 1, DATEADD(DAY, -7, GETDATE()), DATEADD(DAY, -4, GETDATE()), 600000, 'CheckedOut', 5400000);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, CheckOutDate, Deposit, Status, TotalAmount)
VALUES (15, @Room_NT1, 2, DATEADD(DAY, -2, GETDATE()), DATEADD(DAY, -1, GETDATE()), 300000, 'CheckedOut', 1200000);

-- === Bookings dang CheckedIn (hien tai) ===
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, Deposit, Status, TotalAmount)
VALUES (2, @Room_HL1, 1, GETDATE(), 200000, 'CheckedIn', 0);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, Deposit, Status, TotalAmount)
VALUES (3, @Room_DN1, 2, GETDATE(), 300000, 'CheckedIn', 0);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, Deposit, Status, TotalAmount)
VALUES (4, @Room_DL1, 1, GETDATE(), 200000, 'CheckedIn', 0);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, Deposit, Status, TotalAmount)
VALUES (5, @Room_PQ1, 2, GETDATE(), 500000, 'CheckedIn', 0);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, Deposit, Status, TotalAmount)
VALUES (6, @Room_SP1, 1, GETDATE(), 200000, 'CheckedIn', 0);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, Deposit, Status, TotalAmount)
VALUES (7, @Room_HA1, 2, GETDATE(), 300000, 'CheckedIn', 0);
INSERT INTO Bookings (CustomerId, RoomId, UserId, CheckInDate, Deposit, Status, TotalAmount)
VALUES (8, @Room_NT1, 1, GETDATE(), 300000, 'CheckedIn', 0);

GO

-- =============================================
-- 8. Them BookingServices cho cac booking da checkout
-- =============================================

-- Lay BookingId bat dau tu BookingId = 2 (booking hien tai la 1)
DECLARE @bid INT;
DECLARE booking_cursor CURSOR FOR
    SELECT BookingId FROM Bookings WHERE Status = 'CheckedOut' AND BookingId > 1 ORDER BY BookingId;

OPEN booking_cursor;
FETCH NEXT FROM booking_cursor INTO @bid;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Moi booking them 2-4 dich vu ngau nhien
    INSERT INTO BookingServices (BookingId, ServiceId, Quantity, PriceAtTime)
    SELECT @bid, ServiceId, 
        CASE WHEN ServiceId <= 7 THEN ABS(CHECKSUM(NEWID())) % 10 + 1
             ELSE ABS(CHECKSUM(NEWID())) % 3 + 1 END,
        Price
    FROM Services 
    WHERE ServiceId IN (
        1 + ABS(CHECKSUM(NEWID())) % 12,
        1 + ABS(CHECKSUM(NEWID())) % 12,
        1 + ABS(CHECKSUM(NEWID())) % 12
    )
    AND IsActive = 1;

    FETCH NEXT FROM booking_cursor INTO @bid;
END

CLOSE booking_cursor;
DEALLOCATE booking_cursor;
GO

-- =============================================
-- 9. Them TempInvoices cho cac phong dang CheckedIn
-- =============================================
DECLARE @room_cursor CURSOR;
DECLARE @rId INT;

SET @room_cursor = CURSOR FOR
    SELECT DISTINCT b.RoomId FROM Bookings b WHERE b.Status = 'CheckedIn' AND b.CheckOutDate IS NULL;

OPEN @room_cursor;
FETCH NEXT FROM @room_cursor INTO @rId;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF NOT EXISTS (SELECT 1 FROM TempInvoices WHERE RoomID = @rId AND Status = N'Chua thanh toan')
    BEGIN
        INSERT INTO TempInvoices (RoomID, Status, TotalAmount) VALUES (@rId, N'Chua thanh toan', 0);
        DECLARE @newInv INT = SCOPE_IDENTITY();
        
        INSERT INTO TempInvoiceDetails (InvoiceID, ServiceID, Quantity, UnitPrice, TotalPrice)
        SELECT @newInv, ServiceId, 
            ABS(CHECKSUM(NEWID())) % 5 + 1,
            Price,
            Price * (ABS(CHECKSUM(NEWID())) % 5 + 1)
        FROM Services 
        WHERE ServiceId IN (
            1 + ABS(CHECKSUM(NEWID())) % 12,
            1 + ABS(CHECKSUM(NEWID())) % 12
        )
        AND IsActive = 1;
    END

    FETCH NEXT FROM @room_cursor INTO @rId;
END

CLOSE @room_cursor;
DEALLOCATE @room_cursor;
GO

-- =============================================
-- 10. Verify
-- =============================================
SELECT 'Homestays' AS Tbl, COUNT(*) AS Cnt FROM Homestays WHERE IsActive = 1
UNION ALL SELECT 'RoomTypes', COUNT(*) FROM RoomTypes
UNION ALL SELECT 'Rooms', COUNT(*) FROM Rooms WHERE IsActive = 1
UNION ALL SELECT 'Customers', COUNT(*) FROM Customers
UNION ALL SELECT 'Services', COUNT(*) FROM Services WHERE IsActive = 1
UNION ALL SELECT 'Bookings', COUNT(*) FROM Bookings WHERE IsDeleted = 0
UNION ALL SELECT 'BookingServices', COUNT(*) FROM BookingServices
UNION ALL SELECT 'TempInvoices', COUNT(*) FROM TempInvoices
UNION ALL SELECT 'TempInvoiceDetails', COUNT(*) FROM TempInvoiceDetails;
GO

-- Xem doanh thu theo homestay
SELECT h.HomestayName, 
    COUNT(b.BookingId) AS SoDon,
    ISNULL(SUM(b.TotalAmount), 0) AS DoanhThu
FROM Homestays h
LEFT JOIN Rooms r ON h.HomestayId = r.HomestayId
LEFT JOIN Bookings b ON r.RoomId = b.RoomId AND b.IsDeleted = 0 AND b.Status IN ('CheckedIn', 'CheckedOut')
WHERE h.IsActive = 1
GROUP BY h.HomestayId, h.HomestayName
ORDER BY DoanhThu DESC;
GO
