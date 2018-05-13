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

CREATE TABLE Pet
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(300),
	Age VARCHAR(100), --HOW TO REPRESENT MONTHS. SINCE WE CAN'T DO IT PROPERLY WITH DECIMALS USE STRINGS
	Description VARCHAR(MAX),
	Picture VARCHAR(MAX),
	SpecieId INT CONSTRAINT FK_Pet_Specie FOREIGN KEY REFERENCES Specie(Id),
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
	--EndDate DATETIME,
	DistrictId INT CONSTRAINT FK_Adoption_District FOREIGN KEY REFERENCES District(Id), 
	OwnerId INT CONSTRAINT FK_Adoption_Owner FOREIGN KEY REFERENCES Owner(Id),
	SpecieId INT CONSTRAINT FK_Adoption_Specie FOREIGN KEY REFERENCES Specie(Id),
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

CREATE TABLE Found_Pet--WHEN YOU FIND A PET, YOU CAN'T KNOW IF IT'S REGISTERED SO MORE COLUMNS ARE NEEDED ON THIS TABLE
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	State BIT,--0 FOUND 1 DELIVERED
	Description VARCHAR(MAX),
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

---PET

EXEC SP_HELP Pet
GO

CREATE PROCEDURE usp_Pet_Insert
@name VARCHAR(300),
@age VARCHAR(100),
@desc VARCHAR(MAX),
@picture VARCHAR(MAX),
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
					 SpecieId,
					 RaceId,
					 OwnerId)
			 VALUES (@name,
					 @age,
					 @desc,
					 @picture,
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
		DELETE FROM Adoption WHERE PetId = @id
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
		   SpecieId,
		   RaceId,
		   OwnerId
	FROM Pet
GO

---ADOPTION

EXEC SP_HELP Adoption
GO

CREATE PROCEDURE usp_Adoption_Insert
@state BIT,
@desc VARCHAR(MAX),
@address VARCHAR(300),
@age VARCHAR(100),
@totalQuantity INT,
@availableQuantity INT,
@publishDate DATETIME,
--@endDate DATETIME,
@disId INT,
@ownerId INT,
@specieId INT,
@raceId INT,
@petId INT,
@id INT = 0 OUT
AS
BEGIN
	INSERT INTO Adoption (State,
						  Description,
						  Address,
						  Age,
						  TotalQuantity,
						  AvailableQuantity,
						  PublishDate,
						  --EndDate,
						  DistrictId,
						  OwnerId,
						  SpecieId,
						  RaceId,
						  PetId)
				VALUES (@state,
						@desc,
						@address,
						@age,
						@totalQuantity,
						@availableQuantity,
						@publishDate,
						--@endDate,
						@disId,
						@ownerId,
						@specieId,
						@raceId,
						@petId)
	SET @id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE usp_Adoption_Update
@id INT,
@state BIT,
@availableQuantity INT,
@rowCount INT = 0 OUT
--@endDate DATETIME
AS
BEGIN
	UPDATE Adoption SET State = @state,
						AvailableQuantity = @availableQuantity
						--EndDate = @endDate
			        WHERE Id = @id
	SET @rowCount = @@ROWCOUNT
END
GO

CREATE PROCEDURE usp_Adoption_Delete
@id INT,
@rowCount INT = 0 OUT
AS
BEGIN TRY
	BEGIN TRAN
		DELETE FROM Adoption_Adopter WHERE AdoptionId = @id
		DELETE FROM Adoption WHERE Id = @id
		SET @rowCount = @@ROWCOUNT
	COMMIT TRAN
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 1
		ROLLBACK TRAN
		THROW
END CATCH
GO

CREATE PROCEDURE usp_Adoption_Find
@id INT
AS
SELECT Id,
	   State,
	   Description,
	   Address,
	   Age,
	   TotalQuantity,
	   AvailableQuantity,
	   PublishDate,
	   --EndDate,
	   DistrictId,
	   OwnerId,
	   SpecieId,
	   RaceId,
	   PetId
FROM Adoption
WHERE Id = @id
GO

CREATE PROCEDURE usp_Adoption_FindAll
AS
SELECT Id,
	   State,
	   Description,
	   Address,
	   Age,
	   TotalQuantity,
	   AvailableQuantity,
	   PublishDate,
	   --EndDate,
	   DistrictId,
	   OwnerId,
	   SpecieId,
	   RaceId,
	   PetId
FROM Adoption
GO

---ADOPTION_ADOPTER

EXEC SP_HELP Adoption_Adopter
GO

CREATE PROCEDURE usp_Adoption_Adopter_Insert
@adoptionId INT,
@adopterId INT,
@adoptedQuantity INT,
@adoptedDate DATETIME,
@rowCount INT = 0 OUT
AS
BEGIN
	INSERT INTO Adoption_Adopter (AdoptionId,
								  AdopterId,
								  AdoptedQuantity,
								  AdoptedDate)
						  VALUES (@adoptionId,
								  @adopterId,
								  @adoptedQuantity,
								  @adoptedDate)
	SET @rowCount = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE usp_Adoption_Adopter_Delete
@adoptionId INT,
@adopterId INT,
@rowCount INT = 0 OUT
AS
BEGIN
	DELETE FROM Adoption_Adopter 
	WHERE AdoptionId = @adoptionId AND AdopterId = @adopterId
	SET @rowCount = @@ROWCOUNT
END
GO

CREATE PROCEDURE usp_Adoption_Adopter_Find
@adoptionId INT,
@adopterId INT
AS
SELECT AdoptionId,
	   AdopterId,
	   AdoptedQuantity,
	   AdoptedDate
FROM Adoption_Adopter
WHERE AdoptionId = @adoptionId AND AdopterId = @adopterId
GO

CREATE PROCEDURE usp_Adoption_Adopter_FindAll
@adoptionId INT
AS
SELECT AdoptionId,
	   AdopterId,
	   AdoptedQuantity,
	   AdoptedDate
FROM Adoption_Adopter
WHERE AdoptionId = @adoptionId
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

---FOUND_PET

EXEC SP_HELP Found_Pet
GO

ALTER PROCEDURE usp_Found_Pet_Insert
@state BIT,
@desc VARCHAR(MAX),
@lon DOUBLE PRECISION,
@lat DOUBLE PRECISION,
@foundDate DATETIME,
@deliveredDate DATETIME,
@address VARCHAR(300),
@disId INT,
@raceId INT,
@foundById INT,
@deliveredToId INT,
@id INT = 0 OUT
AS
BEGIN
	INSERT INTO Found_Pet (State,
						   Description,
						   Longitude,
						   Latitude,
						   FoundDate,
						   DeliveredDate,
						   Address,
						   DistrictId,
						   RaceId,
						   FoundById,
						   DeliveredToId)
					VALUES (@state,
							@desc,
							@lon,
							@lat,
							@foundDate,
							@deliveredDate,
							@address,
							@disId,
							@raceId,
							@foundById,
							@deliveredToId)
	SET @id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE usp_Found_Pet_Update
@state BIT,
@deliveredDate DATETIME,
@deliveredToId INT,
@rowCount INT = 0 OUT
AS
BEGIN
	UPDATE Found_Pet SET State = @state,
					     DeliveredDate = @deliveredDate,
						 DeliveredToId = @deliveredToId
	SET @rowCount = @@ROWCOUNT
END
GO

CREATE PROCEDURE usp_Found_Pet_Delete
@id INT,
@rowCount INT = 0 OUT
AS
BEGIN
	DELETE FROM Found_Pet
	WHERE Id = @id
	SET @rowCount = @@ROWCOUNT
END
GO

CREATE PROCEDURE usp_Found_Pet_Find
@id INT
AS
SELECT Id,
	   State,
	   Description,
	   Longitude,
	   Latitude,
	   FoundDate,
	   DeliveredDate,
	   Address,
	   DistrictId,
	   RaceId,
	   FoundById,
	   DeliveredToId
FROM Found_Pet
WHERE Id = @id
GO

CREATE PROCEDURE usp_Found_Pet_FindAll
AS
SELECT Id,
	   State,
	   Description,
	   Longitude,
	   Latitude,
	   FoundDate,
	   DeliveredDate,
	   Address,
	   DistrictId,
	   RaceId,
	   FoundById,
	   DeliveredToId
FROM Found_Pet
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
CREATE PROCEDURE usp_District_FindAll
AS
SELECT Id,
	   Name
FROM District
GO





