USE master;
GO
DROP DATABASE IF EXISTS ERP_Assessment_DB;
GO

CREATE DATABASE ERP_Assessment_DB;
GO

USE ERP_Assessment_DB;
GO

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL
);

CREATE TABLE Items (
    ItemId INT IDENTITY(1,1) PRIMARY KEY,
    ItemName NVARCHAR(100) NOT NULL,
    Weight DECIMAL(10,2) NOT NULL,
    ParentItemId INT NULL,
    IsProcessed BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Items_ParentItem FOREIGN KEY (ParentItemId) REFERENCES Items(ItemId)
);

INSERT INTO Users (Email, PasswordHash) 
VALUES ('admin@admsystems.net', 'password123');