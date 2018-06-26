--USE master
--GO

-------------------
----DROP SCRIPT----
-------------------

/*DECLARE @uncheckConstraintSQL VARCHAR(MAX) = ''
DECLARE @dropSQL VARCHAR(MAX) = ''
SELECT @uncheckConstraintSQL += CONCAT('ALTER TABLE ', TABLE_NAME, ' NOCHECK CONSTRAINT ALL; ') FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%sys%'
SELECT @dropSQL += CONCAT('DROP TABLE ', TABLE_NAME, '; ') FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%sys%'
--SELECT @dropSQL
EXEC(@uncheckConstraintSQL)
EXEC(@dropSQL)
GO
DROP DATABASE Paws
GO*/

---------------------
----CREATE SCRIPT----
---------------------

/*CREATE DATABASE Paws 
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
GO*/

DROP DATABASE Paws
GO

CREATE DATABASE Paws
GO

USE Paws
GO

CREATE TABLE District
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(300),
	--Longitude DOUBLE PRECISION,
	--Latitude DOUBLE PRECISION
)
GO

CREATE TABLE Specie
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(300)
)
GO

CREATE TABLE Race
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(300),
	SpecieId INT CONSTRAINT FK_Race_Specie FOREIGN KEY REFERENCES Specie(Id)
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

CREATE TABLE Survey
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	HomeDescription VARCHAR(MAX),
	AmountOfPeople VARCHAR(300),
	OtherPets BIT,
	OtherPetsDescription VARCHAR(MAX),
	WorkType VARCHAR(300),
	Availability VARCHAR(MAX),
	OwnerId INT CONSTRAINT FK_Survey_Owner FOREIGN KEY REFERENCES Owner(Id) CONSTRAINT UN_Survey_Owner UNIQUE
)
GO

CREATE TABLE Pet
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(300),
	Age VARCHAR(100), --HOW TO REPRESENT MONTHS. SINCE WE CAN'T DO IT PROPERLY WITH DECIMALS USE STRINGS
	Description VARCHAR(MAX),
	Picture VARCHAR(MAX),
	PublishDate DATETIME,
	State BIT,
	OtherRace VARCHAR(300),
	SpecieId INT CONSTRAINT FK_Pet_Specie FOREIGN KEY REFERENCES Specie(Id),
	RaceId INT CONSTRAINT FK_Pet_Race FOREIGN KEY REFERENCES Race(Id),
	OwnerId INT CONSTRAINT FK_Pet_Owner FOREIGN KEY REFERENCES Owner(Id)
)
GO


CREATE TABLE Lost_Pet
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	State BIT,--0 LOST 1 FOUND
	Description VARCHAR(MAX),
	Age VARCHAR(100),--HOW TO REPRESENT MONTHS. SINCE WE CAN'T DO IT PROPERLY WITH DECIMALS USE STRINGS,
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

CREATE TABLE Auth
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Token VARCHAR(500) UNIQUE,
	CreatedAt DATETIME
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
@phoneNum VARCHAR(12),
@profilePic VARCHAR(MAX),
@disId INT,
@id INT = 0 OUT
AS
INSERT INTO Owner (Username, 
				   Password,
				   Name,
				   LastName,
				   BirthDate,
				   DNI,
				   EMail,
				   Address,
				   PhoneNumber,
				   ProfilePicture,
				   DistrictId) 
			VALUES (@user,
					@pass,
					@name,
					@lastName,
					@birthDate,
					@dni,
					@email,
					@address,
					@phoneNum,
					@profilePic,
					@disId)
SET @id = SCOPE_IDENTITY()
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
@phoneNum VARCHAR(12),
@profilePic VARCHAR(MAX),
@disId INT,
@rowCount INT = 0 OUT
AS
BEGIN
	SET NOCOUNT OFF
	UPDATE Owner SET Username = @user, 
					 Password = @pass,
					 Name = @name,
					 LastName = @lastName,
					 BirthDate = @birthDate,
					 DNI = @dni,
					 EMail = @email,
					 Address = @address,
					 PhoneNumber = @phoneNum,
					 ProfilePicture = @profilePic,
					 DistrictId = @disId
				WHERE Id = @id
	SET @rowCount = @@ROWCOUNT
END
GO


CREATE PROCEDURE usp_Owner_Delete
@id INT,
@rowCount INT = 0 OUT
AS
BEGIN TRY
	BEGIN TRANSACTION
		DELETE FROM Adoption_Adopter WHERE AdopterId = @id
		DELETE FROM Adoption WHERE OwnerId = @id
		DELETE FROM Found_Pet WHERE FoundById = @id OR DeliveredToId = @id
		DELETE FROM Lost_Pet WHERE OwnerId = @id OR FoundById = @id
		DELETE FROM Pet WHERE OwnerId = @id
		DELETE FROM Owner WHERE Id = @id
		SET @rowCount = @@ROWCOUNT	
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	THROW--THROW THE ERROR
END CATCH
GO

CREATE PROCEDURE usp_Owner_Find
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
	   Address,
	   PhoneNumber,
	   ProfilePicture,
	   DistrictId
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
       Address,
	   PhoneNumber,
	   ProfilePicture,
	   DistrictId 
FROM Owner
GO

CREATE PROCEDURE usp_Owner_Login
@user VARCHAR(300),
@pass VARCHAR(300)
AS
SELECT Id,
	   Username, 
       Password,
       Name,
       LastName,
       BirthDate,
       DNI,
       EMail,
       Address,
	   PhoneNumber,
	   ProfilePicture,
	   DistrictId 
FROM Owner
WHERE Username = @user AND Password = @pass
GO

---SURVEY

CREATE PROCEDURE usp_Survey_Insert
@homeDesc VARCHAR(MAX),
@people VARCHAR(300),
@otherPets BIT,
@otherPetsDesc VARCHAR(MAX),
@workType VARCHAR(300),
@avail VARCHAR(MAX),
@ownerId INT,
@id INT = 0 OUT
AS
BEGIN
	INSERT INTO Survey(HomeDescription,
					   AmountOfPeople,
					   OtherPets,
					   OtherPetsDescription,
					   WorkType,
					   Availability,
					   OwnerId)
				VALUES(@homeDesc,
					   @people,
					   @otherPets,
					   @otherPetsDesc,
					   @workType,
					   @avail,
					   @ownerId)
	SET @id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE usp_Survey_Update
@id INT,
@homeDesc VARCHAR(MAX),
@people VARCHAR(300),
@otherPets BIT,
@otherPetsDesc VARCHAR(MAX),
@workType VARCHAR(300),
@avail VARCHAR(MAX),
@ownerId INT,
@rowCount INT = 0 OUT
AS
BEGIN
	UPDATE Survey SET HomeDescription = @homeDesc,
					  AmountOfPeople = @people,
					  OtherPets = @otherPets,
					  OtherPetsDescription = @otherPetsDesc,
					  WorkType = @workType,
					  Availability = @avail
	WHERE Id = @id
	SET @rowCount = @@ROWCOUNT
END
GO

CREATE PROCEDURE usp_Survey_Find
@id INT
AS
SELECT Id,
	   HomeDescription,
	   AmountOfPeople,
	   OtherPets,
	   OtherPetsDescription,
	   WorkType,
	   Availability,
	   OwnerId
FROM Survey
WHERE Id = @id
GO

---PET

EXEC SP_HELP Pet
GO

CREATE PROCEDURE usp_Pet_Insert
@name VARCHAR(300),
@age VARCHAR(100),
@desc VARCHAR(MAX),
@picture VARCHAR(MAX),
@pubDate DATETIME,
@state BIT,
@otherRace VARCHAR(300),
@specieId INT,
@raceId INT,
@ownerId INT,
@id INT = 0 OUT
AS
BEGIN
	SET NOCOUNT OFF
	INSERT INTO Pet (Name,
					 Age,
					 Description,
					 Picture,
					 PublishDate,
					 State,
					 OtherRace,
					 SpecieId,
					 RaceId,
					 OwnerId)
			 VALUES (@name,
					 @age,
					 @desc,
					 @picture,
					 @pubDate,
					 @state,
					 @otherRace,
					 @specieId,
					 @raceId,
					 @ownerId)
	SET @id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE usp_Pet_Update
@id INT,
@name VARCHAR(300),
@age VARCHAR(100),
@desc VARCHAR(MAX),
@picture VARCHAR(MAX),
@pubDate DATETIME,
@state BIT,
@otherRace VARCHAR(300),
@specieId INT,
@raceId INT,
@ownerId INT,
@rowCount INT = 0 OUT
AS
BEGIN
	SET NOCOUNT OFF
	UPDATE Pet SET Name = @name,
				   Age = @age,
				   Description = @desc,
				   Picture = @picture,
				   PublishDate = @pubDate,
				   State = @state,
				   OtherRace = @otherRace,
				   SpecieId = @specieId,
				   RaceId = @raceId,
				   OwnerId = @ownerId
			   WHERE Id = @id
	SET @rowCount = @@ROWCOUNT
END
GO

CREATE PROCEDURE usp_Pet_Delete
@id INT,
@rowCount INT = 0 OUT
AS
BEGIN TRY
	BEGIN TRANSACTION
		--DELETE FROM Adoption WHERE PetId = @id
		--DELETE FROM Adoption_Pet WHERE PetId = @id
		DELETE FROM Lost_Pet WHERE PetId = @id
		DELETE FROM Pet WHERE Id = @id
		SET @rowCount = @@ROWCOUNT
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	THROW--THROW THE ERROR
END CATCH
GO

CREATE PROCEDURE usp_Pet_Find
@id INT
AS
SELECT Id,
	   Name,
	   Age,
	   Description,
	   Picture,
	   PublishDate,
	   State,
	   OtherRace,
	   SpecieId,
	   RaceId,
	   OwnerId
FROM Pet
WHERE Id = @id
GO

--OVERLOADED SP
CREATE PROCEDURE usp_Pet_FindAll
@id INT = 0
AS
IF @id < 0
	SELECT Id,
		   Name,
		   Age,
		   Description,
		   Picture,
		   PublishDate,
		   State,
		   OtherRace,
		   SpecieId,
		   RaceId,
		   OwnerId
	FROM Pet
	WHERE OwnerId = @id
ELSE
	SELECT Id,
		   Name,
		   Age,
		   Description,
		   Picture,
		   PublishDate,
	       State,
	       OtherRace,
		   SpecieId,
		   RaceId,
		   OwnerId
	FROM Pet
GO


---LOST_PET

EXEC SP_HELP Lost_Pet
GO

CREATE PROCEDURE usp_Lost_Pet_Insert
@state BIT,
@desc VARCHAR(MAX),
@age VARCHAR(100),
@lon DOUBLE PRECISION,
@lat DOUBLE PRECISION,
@lostDate DATETIME,
@foundDate DATETIME,
@address VARCHAR(300),
@disId INT,
@ownerId INT,
@foundById INT,
@petId INT,
@id INT = 0 OUT
AS
BEGIN
	INSERT INTO Lost_Pet (State,
						  Description,
						  Age,
						  Longitude,
						  Latitude,
						  LostDate,
						  FoundDate,
						  Address,
						  DistrictId,
						  OwnerId,
						  FoundById,
						  PetId)
				   VALUES (@state,
						   @desc,
						   @age,
						   @lon,
						   @lat,
						   @lostDate,
						   @foundDate,
						   @address,
						   @disId,
						   @ownerId,
						   @foundById,
						   @petId)
	SET @id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE usp_Lost_Pet_Update
@id INT,
@state BIT,
@foundDate DATETIME,
@foundById INT,
@rowCount INT = 0 OUT
AS
BEGIN
	UPDATE Lost_Pet SET State = @state,
						FoundDate = @foundDate,
						FoundById = @foundById
					WHERE Id = @id
	SET @rowCount = @@ROWCOUNT
END
GO

CREATE PROCEDURE usp_Lost_Pet_Delete
@id INT,
@rowCount INT = 0 OUT
AS
BEGIN
	DELETE FROM Lost_Pet
	WHERE Id = @id
	SET @rowCount = @@ROWCOUNT
END
GO

CREATE PROCEDURE usp_Lost_Pet_Find
@id INT
AS
SELECT Id,
	   State,
	   Description,
	   Age,
	   Longitude,
	   Latitude,
	   LostDate,
	   FoundDate,
	   Address,
	   DistrictId,
	   OwnerId,
	   FoundById,
	   PetId
FROM Lost_Pet
WHERE Id = @id
GO

CREATE PROCEDURE usp_Lost_Pet_FindAll
AS
SELECT Id,
	   State,
	   Description,
	   Age,
	   Longitude,
	   Latitude,
	   LostDate,
	   FoundDate,
	   Address,
	   DistrictId,
	   OwnerId,
	   FoundById,
	   PetId
FROM Lost_Pet
GO


---SPECIE 
CREATE PROCEDURE usp_Specie_FindAll
AS
SELECT Id,
	   Name
FROM Specie
GO

---RACE
CREATE PROCEDURE usp_Race_FindAll
@specieId INT
AS
SELECT Id,
	   Name,
	   SpecieId
FROM Race
WHERE SpecieId = @specieId
GO



---DISTRICT
CREATE PROCEDURE usp_District_Find
@id INT
AS
SELECT Id,
	   Name
FROM District
WHERE Id = @id
GO


CREATE PROCEDURE usp_District_FindAll
AS
SELECT Id,
	   Name
FROM District
GO

---AUTH

CREATE PROCEDURE usp_Auth_Insert
@token VARCHAR(MAX),
@createdAt DATETIME
AS
INSERT INTO Auth (Token, CreatedAt)
		VALUES (@token, @createdAt)
GO

CREATE PROCEDURE usp_Auth_Delete
@id INT
AS
DELETE FROM Auth
WHERE Id = @id
GO

CREATE PROCEDURE usp_Auth_Find
@token VARCHAR(MAX)
AS
--IF (SELECT COUNT(*) FROM Auth WHERE Token = @token) > 0 --AND DATEDIFF(DAY, GETDATE(), createdAt) > 30)
	SELECT Id, Token, CreatedAt FROM Auth WHERE Token = @token
GO
