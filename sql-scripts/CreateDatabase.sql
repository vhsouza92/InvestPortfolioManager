-- Verifica se o banco de dados já existe e o cria se não existir
IF DB_ID('InvestPortfolioDb') IS NULL
BEGIN
    CREATE DATABASE InvestPortfolioDb;
END
GO

-- Usa o banco de dados
USE InvestPortfolioDb;
GO

-- Cria a tabela de produtos financeiros se não existir
IF OBJECT_ID('dbo.FinancialProducts', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.FinancialProducts
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        MaturityDate DATE NOT NULL,
        Value DECIMAL(18, 2) NOT NULL
    );
END
GO

-- Cria a tabela de transações se não existir
IF OBJECT_ID('dbo.Transactions', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Transactions
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Type NVARCHAR(50) NOT NULL,
        ProductId INT NOT NULL,
        Date DATETIME NOT NULL,
        Amount DECIMAL(18, 2) NOT NULL,
        FOREIGN KEY (ProductId) REFERENCES dbo.FinancialProducts(Id)
    );
END
GO

-- Insere dados iniciais na tabela de produtos financeiros se estiver vazia
IF NOT EXISTS (SELECT 1 FROM dbo.FinancialProducts)
BEGIN
    INSERT INTO dbo.FinancialProducts (Name, MaturityDate, Value) VALUES
    ('Product A', '2025-01-01', 1000.00),
    ('Product B', '2026-01-01', 1500.00),
    ('Product C', '2027-01-01', 2000.00);
END
GO

-- Insere dados iniciais na tabela de transações se estiver vazia
IF NOT EXISTS (SELECT 1 FROM dbo.Transactions)
BEGIN
    INSERT INTO dbo.Transactions (Type, ProductId, Date, Amount) VALUES
    ('Buy', 1, '2024-01-01', 1000.00),
    ('Sell', 2, '2024-06-01', 1500.00),
    ('Buy', 3, '2024-12-01', 2000.00);
END
GO
