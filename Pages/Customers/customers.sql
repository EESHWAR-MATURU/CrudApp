CREATE TABLE customers (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Created_at DATETIME NOT NULL DEFAULT GETDATE()
);

INSERT INTO customers (Name, PhoneNumber, Email)
VALUES ('John Doe', '1234567890', 'john.doe@example.com');
