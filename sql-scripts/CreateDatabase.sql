-- Verifica se o banco de dados já existe e o cria se não existir
IF DB_ID('InvestPortfolioDb') IS NULL
BEGIN
    CREATE DATABASE InvestPortfolioDb;
END
GO

-- Usa o banco de dados
USE InvestPortfolioDb;
GO

-- Create the FinancialProducts table--------------------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FinancialProducts' and xtype='U')
CREATE TABLE FinancialProducts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Value DECIMAL(18, 2) NOT NULL,
    MaturityDate DATETIME NOT NULL
);

-- Insert initial data into FinancialProducts
IF NOT EXISTS (SELECT * FROM FinancialProducts)
BEGIN
    INSERT INTO FinancialProducts (Name, Value, MaturityDate) VALUES ('Product A', 100.00, '2024-12-31');
    INSERT INTO FinancialProducts (Name, Value, MaturityDate) VALUES ('Product B', 200.00, '2025-12-31');
    INSERT INTO FinancialProducts (Name, Value, MaturityDate) VALUES ('Product C', 300.00, '2026-12-31');
END

-- Create the Users table--------------------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' and xtype='U')
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

-- Insert initial data into Users
IF NOT EXISTS (SELECT * FROM Users)
BEGIN
    INSERT INTO Users (Name, Email) VALUES ('John Doe', 'john.doe@example.com');
    INSERT INTO Users (Name, Email) VALUES ('Jane Smith', 'jane.smith@example.com');
    INSERT INTO Users (Name, Email) VALUES ('Alice Johnson', 'alice.johnson@example.com');
    INSERT INTO Users (Name, Email) VALUES ('Bob Brown', 'bob.brown@example.com');
    INSERT INTO Users (Name, Email) VALUES ('Charlie Davis', 'charlie.davis@example.com');
END

-- Create the Transactions table--------------------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Transactions' and xtype='U')
CREATE TABLE Transactions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    UserId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    TransactionDate DATETIME NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES FinancialProducts(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Insert initial data into Transactions
IF NOT EXISTS (SELECT * FROM Transactions)
BEGIN
    INSERT INTO Transactions (ProductId, UserId, Quantity, UnitPrice, TransactionDate, Type) VALUES (1, 1, 10, 15.00, '2024-06-01', 'Buy');
    INSERT INTO Transactions (ProductId, UserId, Quantity, UnitPrice, TransactionDate, Type) VALUES (2, 2, 20, 12.50, '2024-06-02', 'Sell');
    INSERT INTO Transactions (ProductId, UserId, Quantity, UnitPrice, TransactionDate, Type) VALUES (3, 3, 30, 11.00, '2024-06-03', 'Buy');
    INSERT INTO Transactions (ProductId, UserId, Quantity, UnitPrice, TransactionDate, Type) VALUES (1, 4, 40, 10.50, '2024-06-04', 'Sell');
    INSERT INTO Transactions (ProductId, UserId, Quantity, UnitPrice, TransactionDate, Type) VALUES (2, 5, 50, 10.00, '2024-06-05', 'Buy');
END