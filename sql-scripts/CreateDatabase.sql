-- Verifica se o banco de dados já existe e o cria se não existir
IF DB_ID('InvestPortfolioDb') IS NULL
BEGIN
    CREATE DATABASE InvestPortfolioDb;
END
GO

-- Usa o banco de dados
USE InvestPortfolioDb;
GO

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
    Amount DECIMAL(18, 2) NOT NULL,
    Date DATETIME NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES FinancialProducts(Id)
);

-- Insert initial data into Transactions
IF NOT EXISTS (SELECT * FROM Transactions)
BEGIN
    INSERT INTO Transactions (ProductId, Amount, Date, Type) VALUES (1, 150.00, '2024-06-01', 'Buy');
    INSERT INTO Transactions (ProductId, Amount, Date, Type) VALUES (2, 250.00, '2024-06-02', 'Sell');
    INSERT INTO Transactions (ProductId, Amount, Date, Type) VALUES (3, 350.00, '2024-06-03', 'Buy');
END
