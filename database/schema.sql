-- Create GordonDB database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GordonDB')
BEGIN
    CREATE DATABASE GordonDB;
END
GO

USE GordonDB;
GO

-- Create Tickets table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tickets')
BEGIN
    CREATE TABLE Tickets (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Title NVARCHAR(255) NOT NULL,
        Content NVARCHAR(MAX) NOT NULL,
        Status NVARCHAR(50) DEFAULT 'Open',
        Severity NVARCHAR(50) DEFAULT 'Normal',
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME DEFAULT GETUTCDATE()
    );
END
GO

-- Create Customers table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customers')
BEGIN
    CREATE TABLE Customers (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(255) NOT NULL,
        Email NVARCHAR(255),
        RiskLevel NVARCHAR(50) DEFAULT 'Normal',
        CreatedAt DATETIME DEFAULT GETUTCDATE()
    );
END
GO

-- Create SentimentLog table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SentimentLog')
BEGIN
    CREATE TABLE SentimentLog (
        Id INT PRIMARY KEY IDENTITY(1,1),
        TicketId INT,
        Sentiment NVARCHAR(50),
        Confidence DECIMAL(3,2),
        CreatedAt DATETIME DEFAULT GETUTCDATE()
    );
END
GO
