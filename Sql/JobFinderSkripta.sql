CREATE DATABASE JobFinder
GO
USE JobFinder
GO


------------------------------------------------------
-- TABLICA: JobType (1-N)
------------------------------------------------------
CREATE TABLE JobType 
(
    IDJobType INT PRIMARY KEY IDENTITY,
    JobName NVARCHAR(100) NOT NULL UNIQUE
);

------------------------------------------------------
-- TABLICA: Location (M-N)
------------------------------------------------------
CREATE TABLE Location 
(
    IDLocation INT PRIMARY KEY IDENTITY,
    LocationName NVARCHAR(100) NOT NULL UNIQUE
);

------------------------------------------------------
-- TABLICA: Firm (primarni entitet)
------------------------------------------------------
CREATE TABLE Firm
(
    IDFirm INT PRIMARY KEY IDENTITY,
    FirmName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(MAX),
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    WebsiteUrl NVARCHAR(MAX),
    JobTypeID INT NOT NULL
);

ALTER TABLE Firm
ADD CONSTRAINT FK_Firm_JobType
FOREIGN KEY (JobTypeID) REFERENCES JobType(IDJobType);


------------------------------------------------------
-- TABLICA: Users
------------------------------------------------------
CREATE TABLE Users 
(
    IDUser INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Phone NVARCHAR(20)
);

------------------------------------------------------
-- TABLICA: Request (korisnièki M-N entitet)
------------------------------------------------------
CREATE TABLE Request
(
    IDRequest INT IDENTITY PRIMARY KEY,
    UserID INT NOT NULL,
    FirmID INT NOT NULL,
    LocationID INT NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE Request
ADD CONSTRAINT FK_Request_User
FOREIGN KEY (UserID) REFERENCES Users(IDUser);

ALTER TABLE Request
ADD CONSTRAINT FK_Request_Firm
FOREIGN KEY (FirmID) REFERENCES Firm(IDFirm);

ALTER TABLE Request
ADD CONSTRAINT FK_Request_Location
FOREIGN KEY (LocationID) REFERENCES Location(IDLocation);

------------------------------------------------------
-- DEFAULT PODACI: JobType (bez duplikata)
------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Zidarija') INSERT INTO JobType(JobName) VALUES (N'Zidarija');
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Lièenje') INSERT INTO JobType(JobName) VALUES (N'Lièenje');
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Elektroinstalacije') INSERT INTO JobType(JobName) VALUES (N'Elektroinstalacije');
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Vodoinstalacije') INSERT INTO JobType(JobName) VALUES (N'Vodoinstalacije');
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Keramika') INSERT INTO JobType(JobName) VALUES (N'Keramika');
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Krovopokrivaèki radovi') INSERT INTO JobType(JobName) VALUES (N'Krovopokrivaèki radovi');
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Fasada') INSERT INTO JobType(JobName) VALUES (N'Fasada');
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Održavanje okuænice') INSERT INTO JobType(JobName) VALUES (N'Održavanje okuænice');
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Stolarija') INSERT INTO JobType(JobName) VALUES (N'Stolarija');
IF NOT EXISTS (SELECT 1 FROM JobType WHERE JobName = N'Klima i grijanje') INSERT INTO JobType(JobName) VALUES (N'Klima i grijanje');

GO


/*		
	   ALTER DATABASE [JobFinder]
      SET SINGLE_USER
      WITH ROLLBACK IMMEDIATE;
    -- now we’re safe to drop
    DROP DATABASE [JobFinder];
	GO
	*/

--INSERT INTO Firm (IDFirm,FirmName,Description,Email,PhoneNumber,WebsiteUrl,JobTypeID) VALUES ( 1,'Zidar','string',
--  'user@example.com','string','string',1)