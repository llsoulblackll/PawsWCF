USE master
GO

-------------------
----DROP SCRIPT----
-------------------

DECLARE @uncheckConstraintSQL VARCHAR(MAX) = ''
DECLARE @dropSQL VARCHAR(MAX) = ''
SELECT @uncheckConstraintSQL += CONCAT('ALTER TABLE ', TABLE_NAME, ' NOCHECK CONSTRAINT ALL; ') FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%sys%'
SELECT @dropSQL += CONCAT('DROP TABLE ', TABLE_NAME, '; ') FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%sys%'
--SELECT @dropSQL
EXEC(@uncheckConstraintSQL)
EXEC(@dropSQL)
GO
DROP DATABASE Paws
GO

---------------------
----CREATE SCRIPT----
---------------------

CREATE DATABASE Paws 
ON PRIMARY (
	Name = Paws_Data,
	FileName = 'C:\database\Paws\Paws_Data.mdf',
	Size = 1MB,
	Filegrowth = 5MB
) LOG ON (
	Name = Paws_Log,
	FileName = 'C:\database\Paws\Paws_Log.ldf',
	Size = 1MB,
	Filegrowth = 5MB
)
GO

USE Paws
GO

CREATE TABLE District
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(300),
	Longitude DOUBLE PRECISION,
	Latitude DOUBLE PRECISION
)
GO

CREATE TABLE Race
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(300),
	--Picture VARCHAR(MAX)
)
GO

CREATE TABLE Owner
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Username VARCHAR(300) CONSTRAINT UN_Username_Owner UNIQUE,
	Password VARCHAR(300),
	Name VARCHAR(300),
	LastName VARCHAR(300),
	BirthDate DATE,
	DNI CHAR(8) CONSTRAINT UN_DNI_Owner UNIQUE,
	EMail VARCHAR(300),
	Address VARCHAR(400),
	PhoneNumber VARCHAR(12),
	ProfilePicture VARCHAR(MAX),
	DistrictId INT CONSTRAINT FK_Owner_District FOREIGN KEY REFERENCES District(Id)
)
GO

CREATE TABLE Pet
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(300),
	Age VARCHAR(100), --HOW TO REPRESENT MONTHS. SINCE WE CAN'T DO IT PROPERLY WITH DECIMALS USE STRINGS
	Description VARCHAR(MAX),
	Picture VARCHAR(MAX),
	RaceId INT CONSTRAINT FK_Pet_Race FOREIGN KEY REFERENCES Race(Id),
	OwnerId INT CONSTRAINT FK_Pet_Owner FOREIGN KEY REFERENCES Owner(Id)
)
GO

CREATE TABLE Adoption
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	State BIT,
	Description VARCHAR(MAX),
	Address VARCHAR(300),
	Age VARCHAR(100), --HOW TO REPRESENT MONTHS. SINCE WE CAN'T DO IT PROPERLY WITH DECIMALS USE STRINGS
	TotalQuantity INT,
	AvailableQuantity INT,
	PublishDate DATETIME,
	EndDate DATETIME,
	DistrictId INT CONSTRAINT FK_Adoption_District FOREIGN KEY REFERENCES District(Id), 
	OwnerId INT CONSTRAINT FK_Adoption_Owner FOREIGN KEY REFERENCES Owner(Id),
	RaceId INT CONSTRAINT FK_Adoption_Race FOREIGN KEY REFERENCES Race(Id),
	PetId INT CONSTRAINT FK_Adoption_Pet FOREIGN KEY REFERENCES Pet(Id) DEFAULT NULL--PROBABLY NO ONE WILL WANT TO PUT HIS/HER PET ON ADOPTION
)
GO

CREATE TABLE Adoption_Adopter--SEVERAL ADOPTERS PER ADOPTION
(
	AdoptionId INT FOREIGN KEY REFERENCES Adoption(Id),
	AdopterId INT FOREIGN KEY REFERENCES Owner(Id),
	AdoptedQuantity INT,
	AdoptedDate DATETIME,
	PRIMARY KEY (AdoptionId, AdopterId)
)

CREATE TABLE Lost_Pet
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	State BIT,--0 LOST 1 FOUND
	Description VARCHAR(MAX),
	Longitude DOUBLE PRECISION,
	Latitude DOUBLE PRECISION,
	LostDate DATETIME DEFAULT GETDATE(),
	FoundDate DATETIME DEFAULT NULL,
	Address VARCHAR(300) DEFAULT NULL, --MAYBE ADDRESS WILL NOT BE NEEDED, BECAUSE OF THE MAP
	DistrictId INT FOREIGN KEY REFERENCES District(Id),
	OwnerId INT FOREIGN KEY REFERENCES Owner(Id),
	FoundById INT FOREIGN KEY REFERENCES Owner(Id) DEFAULT NULL, 
	PetId INT FOREIGN KEY REFERENCES Pet(Id)
)
GO

CREATE TABLE Found_Pet--WHEN YOU FIND A PET, YOU CAN'T KNOW IF IT'S REGISTERED SO MORE COLUMNS ARE NEEDED ON THIS TABLE
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	State BIT,--0 FOUND 1 DELIVERED
	Description VARCHAR(MAX),
	Age VARCHAR(100),--HOW TO REPRESENT MONTHS. SINCE WE CAN'T DO IT PROPERLY WITH DECIMALS USE STRINGS,
	Longitude DOUBLE PRECISION,
	Latitude DOUBLE PRECISION,
	FoundDate DATETIME DEFAULT GETDATE(),
	DeliveredDate DATETIME DEFAULT NULL,
	Address VARCHAR(300) DEFAULT NULL, --MAYBE ADDRESS WILL NOT BE NEEDED, BECAUSE OF THE MAP
	DistrictId INT FOREIGN KEY REFERENCES District(Id),
	RaceId INT FOREIGN KEY REFERENCES Race(Id), --ADD RAZA COLUMN SINCE FOUND PETS ARE USUALLY NOT REGISTERED
	FoundById INT FOREIGN KEY REFERENCES Owner(Id), --FOUND BY
	DeliveredToId INT FOREIGN KEY REFERENCES Owner(Id) DEFAULT NULL
)
GO

EXEC SP_TABLES '%', 'dbo'

------------------------
-------PROCEDURES-------
------------------------

---OWNER
EXEC SP_HELP Owner
GO

CREATE PROCEDURE usp_Owner_Insert
@user VARCHAR(300),
@pass VARCHAR(300),
@name VARCHAR(300),
@lastName VARCHAR(300),
@birthDate DATE,
@dni CHAR(8),
@email VARCHAR(300),
@address VARCHAR(400),
@phoneNum VARCHAR(12)
AS
INSERT INTO Owner (Username, 
				   Password,
				   Name,
				   LastName,
				   BirthDate,
				   DNI,
				   EMail,
				   Address) 
			VALUES (@user,
					@pass,
					@name,
					@lastName,
					@birthDate,
					@dni,
					@email,
					@address)
GO

CREATE PROCEDURE usp_Owner_Update
@id INT,
@user VARCHAR(300),
@pass VARCHAR(300),
@name VARCHAR(300),
@lastName VARCHAR(300),
@birthDate DATE,
@dni CHAR(8),
@email VARCHAR(300),
@address VARCHAR(400),
@phoneNum VARCHAR(12)
AS
UPDATE Owner SET Username = @user, 
				   Password = @pass,
				   Name = @name,
				   LastName = @lastName,
				   BirthDate = @birthDate,
				   DNI = @dni,
				   EMail = @email,
				   Address = @address
			 WHERE Id = @id
GO


CREATE PROCEDURE usp_Owner_Delete
@id INT
AS
BEGIN TRY
	BEGIN TRANSACTION
		DELETE FROM Adoption_Adopter WHERE AdopterId = @id
		DELETE FROM Adoption WHERE OwnerId = @id
		DELETE FROM Found_Pet WHERE FoundById = @id OR DeliveredToId = @id
		DELETE FROM Lost_Pet WHERE OwnerId = @id OR FoundById = @id
		DELETE FROM Pet WHERE OwnerId = @id
		DELETE FROM Owner WHERE Id = @id	
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	THROW--THROW THE ERROR
END CATCH
GO

CREATE PROCEDURE ups_Owner_Find
@id INT
AS
SELECT Id,
	   Username, 
	   Password,
	   Name,
	   LastName,
	   BirthDate,
	   DNI,
	   EMail,
	   Address
FROM Owner
WHERE Id = @id
GO 

CREATE PROCEDURE usp_Owner_FindAll
AS
SELECT Id,
	   Username, 
       Password,
       Name,
       LastName,
       BirthDate,
       DNI,
       EMail,
       Address 
FROM Owner
GO

---PET

SP_COLUMNS Found_Pet



