CREATE DATABASE CRUD

GO

USE CRUD

GO

CREATE TABLE student(
    [id] INT PRIMARY KEY IDENTITY NOT NULL,
    [name] VARCHAR (50) NULL,
    [gender] VARCHAR (50) NULL,
    [dob] DATETIME NULL,
    [contact] VARCHAR (50) NULL,
    [address] VARCHAR (MAX) NULL,
    [city] VARCHAR (50) NULL
)

GO

INSERT INTO student VALUES 
('Abdul Rahman','Male','2003-12-20','03052221517','ABC 123', 'Multan')

GO

