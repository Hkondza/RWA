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
-- TABLICA: JobOffer
------------------------------------------------------

CREATE TABLE JobOffer
(
    IDJobOffer INT IDENTITY PRIMARY KEY,
    FirmID INT NOT NULL,
    JobTypeID INT NOT NULL,
    LocationID INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Salary NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE JobOffer
ADD CONSTRAINT FK_JobOffer_Firm
FOREIGN KEY (FirmID) REFERENCES Firm(IDFirm);

ALTER TABLE JobOffer
ADD CONSTRAINT FK_JobOffer_JobType
FOREIGN KEY (JobTypeID) REFERENCES JobType(IDJobType);

ALTER TABLE JobOffer
ADD CONSTRAINT FK_JobOffer_Location
FOREIGN KEY (LocationID) REFERENCES Location(IDLocation);



------------------------------------------------------
-- TABLICA: JobApplication
------------------------------------------------------

CREATE TABLE JobApplication
(
    IDJobApplication INT IDENTITY PRIMARY KEY,
    JobOfferID INT NOT NULL,
    UserID INT NOT NULL,
    Message NVARCHAR(MAX),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Applied',
    AppliedAt DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE JobApplication
ADD CONSTRAINT FK_JobApplication_JobOffer
FOREIGN KEY (JobOfferID) REFERENCES JobOffer(IDJobOffer);

ALTER TABLE JobApplication
ADD CONSTRAINT FK_JobApplication_User
FOREIGN KEY (UserID) REFERENCES Users(IDUser);


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

------------------------------------------------------
-- DEFAULT PODACI: Location
------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Zagreb')
    INSERT INTO Location (LocationName) VALUES (N'Zagreb');

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Split')
    INSERT INTO Location (LocationName) VALUES (N'Split');

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Rijeka')
    INSERT INTO Location (LocationName) VALUES (N'Rijeka');

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Osijek')
    INSERT INTO Location (LocationName) VALUES (N'Osijek');

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Zadar')
    INSERT INTO Location (LocationName) VALUES (N'Zadar');

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Pula')
    INSERT INTO Location (LocationName) VALUES (N'Pula');

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Varaždin')
    INSERT INTO Location (LocationName) VALUES (N'Varaždin');

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Slavonski Brod')
    INSERT INTO Location (LocationName) VALUES (N'Slavonski Brod');

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Dubrovnik')
    INSERT INTO Location (LocationName) VALUES (N'Dubrovnik');

IF NOT EXISTS (SELECT 1 FROM Location WHERE LocationName = N'Karlovac')
    INSERT INTO Location (LocationName) VALUES (N'Karlovac');

------------------------------------------------------
-- DEFAULT PODACI: Users
------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = N'admin')
INSERT INTO Users (Username, Email, PasswordHash, Role, FirstName, LastName, Phone)
VALUES
(N'admin', N'admin@jobfinder.hr', N'admin123HASH', N'Admin', N'Admin', N'User', N'0990000000');

IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = N'user1')
INSERT INTO Users (Username, Email, PasswordHash, Role, FirstName, LastName, Phone)
VALUES
(N'user1', N'user1@jobfinder.hr', N'user123HASH', N'User', N'Ivan', N'Iviæ', N'0911111111');

IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = N'user2')
INSERT INTO Users (Username, Email, PasswordHash, Role, FirstName, LastName, Phone)
VALUES
(N'user2', N'user2@jobfinder.hr', N'user123HASH', N'User', N'Petra', N'Periæ', N'0922222222');

------------------------------------------------------
-- DEFAULT PODACI: Firm
------------------------------------------------------

INSERT INTO Firm (FirmName, Description, Email, PhoneNumber, WebsiteUrl, JobTypeID)
VALUES
(N'ElektroPlus',
 N'Elektroinstalacije, popravci i održavanje',
 N'info@elektroplus.hr',
 N'0911111111',
 N'https://www.elektroplus.hr',
 1),

(N'VodoinstalaterPro',
 N'Vodoinstalaterske usluge i hitne intervencije',
 N'kontakt@vodoinstalaterpro.hr',
 N'0922222222',
 N'https://www.vodoinstalaterpro.hr',
 2),

(N'KrovServis',
 N'Popravak, sanacija i zamjena krovova',
 N'info@krovservis.hr',
 N'0933333333',
 N'https://www.krovservis.hr',
 3),

(N'GradnjaPlus',
 N'Grubi graðevinski radovi i adaptacije',
 N'gradnja@gradnjaplus.hr',
 N'0944444444',
 N'https://www.gradnjaplus.hr',
 4),

(N'FasadaExpert',
 N'Izrada i obnova fasada',
 N'kontakt@fasadaexpert.hr',
 N'0955555555',
 N'https://www.fasadaexpert.hr',
 5),

(N'KeramikaLux',
 N'Postavljanje keramièkih ploèica',
 N'info@keramikalux.hr',
 N'0966666666',
 N'https://www.keramikalux.hr',
 6),

(N'StolarijaDrvo',
 N'Izrada namještaja i stolarije po mjeri',
 N'info@stolarijadrvo.hr',
 N'0977777777',
 N'https://www.stolarijadrvo.hr',
 7),

(N'KlimaTerm',
 N'Ugradnja i servis klima i grijanja',
 N'kontakt@klimaterm.hr',
 N'0988888888',
 N'https://www.klimaterm.hr',
 8),

(N'OdržavanjePlus',
 N'Održavanje objekata i sitni popravci',
 N'info@odrzavanjeplus.hr',
 N'0999999999',
 N'https://www.odrzavanjeplus.hr',
 9),

(N'AdaptacijeMax',
 N'Adaptacije stanova i poslovnih prostora',
 N'adaptacije@adaptacijemax.hr',
 N'0900000000',
 N'https://www.adaptacijemax.hr',
 10);

 SELECT * FROM Firm
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