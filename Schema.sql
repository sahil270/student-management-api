-- Create Classes Table
CREATE TABLE Classes (
    ClassId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(100) NULL
);

-- Create Students Table
CREATE TABLE Students (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    PhoneNumber CHAR(10) NOT NULL UNIQUE,
    EmailId NVARCHAR(100) NOT NULL UNIQUE,
    ClassId INT NOT NULL,
    CONSTRAINT CK_PhoneNumber_OnlyDigits CHECK (PhoneNumber LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    CONSTRAINT CK_Email_Validation CHECK (EmailId LIKE '%_@__%.__%')
);

-- Add Foreign Key Constraint between Students and Classes
ALTER TABLE Students
ADD CONSTRAINT FK_Students_Classes FOREIGN KEY (ClassId) REFERENCES Classes(ClassId)
ON DELETE CASCADE;