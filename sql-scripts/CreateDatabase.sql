-- Verifica se o banco de dados já existe e o cria se não existir
IF DB_ID('InvestPortfolioDb') IS NULL
BEGIN
    CREATE DATABASE InvestPortfolioDb;
END
GO

-- Usa o banco de dados
USE InvestPortfolioDb;
GO

-- Create the Users table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' and xtype='U')
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    UpdatedDate DATETIME NULL
);

-- Insert initial data into Users
IF NOT EXISTS (SELECT * FROM Users)
BEGIN
    INSERT INTO Users (Name, Email, CreatedDate) VALUES ('User 1', 'user1@example.com', GETDATE());
    INSERT INTO Users (Name, Email, CreatedDate) VALUES ('User 2', 'user2@example.com', GETDATE());
    INSERT INTO Users (Name, Email, CreatedDate) VALUES ('User 3', 'user3@example.com', GETDATE());
END

-- Create the FinancialProducts table
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

-- Create the Transactions table
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
    INSERT INTO Transactions (ProductId, UserId, Quantity, UnitPrice, TransactionDate, Type) VALUES (2, 2, 20, 25.00, '2024-06-02', 'Sell');
    INSERT INTO Transactions (ProductId, UserId, Quantity, UnitPrice, TransactionDate, Type) VALUES (3, 3, 30, 35.00, '2024-06-03', 'Buy');
END

-- Create the Portfolios table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Portfolios' and xtype='U')
CREATE TABLE Portfolios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    CreatedDate DATETIME NOT NULL,
    UpdatedDate DATETIME NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Create the PortfolioItems table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PortfolioItems' and xtype='U')
CREATE TABLE PortfolioItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PortfolioId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    TotalValue DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (PortfolioId) REFERENCES Portfolios(Id),
    FOREIGN KEY (ProductId) REFERENCES FinancialProducts(Id)
);

-- Insert initial data into Portfolios and PortfolioItems
IF NOT EXISTS (SELECT * FROM Portfolios)
BEGIN
    INSERT INTO Portfolios (UserId, CreatedDate) VALUES (1, GETDATE());
    INSERT INTO Portfolios (UserId, CreatedDate) VALUES (2, GETDATE());
    INSERT INTO Portfolios (UserId, CreatedDate) VALUES (3, GETDATE());
END

IF NOT EXISTS (SELECT * FROM PortfolioItems)
BEGIN
    INSERT INTO PortfolioItems (PortfolioId, ProductId, Quantity, TotalValue) VALUES (1, 1, 10, 150.00);
    INSERT INTO PortfolioItems (PortfolioId, ProductId, Quantity, TotalValue) VALUES (2, 2, 20, 500.00);
    INSERT INTO PortfolioItems (PortfolioId, ProductId, Quantity, TotalValue) VALUES (3, 3, 30, 1050.00);
END
