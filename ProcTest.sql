-------------
----RACE TEST
-------------

EXEC usp_Race_FindAll
GO


---------------
--DISTRICT TEST
---------------

EXEC usp_District_FindAll
GO

-------------
---OWNER TEST
-------------

EXEC sp_helptext usp_owner_Insert

EXEC usp_Owner_Insert 'admin', 'admin', 'admin', 'paws', '0001-01-01', '00000000', 'admin@paws.com', NULL, '000000000', NULL, NULL
GO

DECLARE @curr_date DATE = GETDATE()
EXEC usp_Owner_Insert 'user1', 'user1', 'user1', 'user1', @curr_date, '45874411', 'user1@paws.com', NULL, '556845588', NULL, NULL
GO

DECLARE @rowCount INT = 0
EXEC usp_Owner_Update 3, 'user1', 'user1', 'user1', 'user1', '1990-06-05', '45874411', 'user1@paws.com', NULL, '556845588', NULL, NULL, @rowCount OUT
PRINT @rowCount
GO

EXEC usp_Owner_Delete 2
GO

EXEC usp_Owner_Find 1
GO

EXEC usp_Owner_FindAll
GO

EXEC usp_Owner_Login 'admin', 'admin'
GO


-------------
-----PET TEST
-------------

EXEC usp_Pet_Insert 'Fido', '3 años', 'Little brown dog', NULL, NULL, NULL, 2
GO

EXEC usp_Pet_Update 1, 'Fido', '4 años', 'Little brown dog', NULL, NULL, 3
GO

EXEC usp_Pet_Delete 4
GO

EXEC usp_Pet_Find 1
GO

EXEC usp_Pet_FindAll @id = 2
GO


---------------
--ADOPTION TEST
---------------

EXEC sp_help Adoption
GO

DECLARE @currDate DATE = GETDATE()
EXEC usp_Adoption_Insert 1, 'Perritos en Adopcion', NULL, '5 meses', 5, 5, @currDate, NULL, 2, NULL, NULL, 2
GO

DECLARE @currDate DATE = GETDATE()
EXEC usp_Adoption_Update 1, 0, 0, @currDate
GO

EXEC usp_Adoption_Delete 1
GO

EXEC usp_Adoption_Find 1
GO

EXEC usp_Adoption_FindAll
GO


-----------------------
--ADOPTION_ADOPTER TEST
-----------------------

DECLARE @currDate DATE = GETDATE()
EXEC usp_Adoption_Adopter_Insert 2, 2, 2, @currDate
GO

EXEC usp_Adoption_Adopter_Delete 1, 3
GO

EXEC usp_Adoption_Adopter_Find 1, 3
GO

EXEC usp_Adoption_Adopter_FindAll 2
GO


---------------
--LOST_PET TEST
---------------

DECLARE @currDate DATE = GETDATE()
EXEC usp_Lost_Pet_Insert 1, 'Big red dog', '2 años', 332.2, 442.331, @currDate, NULL, NULL, NULL, 3, NULL, 2
GO

DECLARE @currDate DATE = GETDATE()
EXEC usp_Lost_Pet_Update 2, 0, @currDate, 3
GO

EXEC usp_Lost_Pet_Delete 1
GO

EXEC usp_Lost_Pet_Find 1
GO

EXEC usp_Lost_Pet_FindAll
GO

----------------
--FOUND_PET TEST
----------------

DECLARE @currDate DATE = GETDATE()
EXEC usp_Found_Pet_Insert 1, 'Small yellow cat', 22314.552, 8842.1, @currDate, NULL, NULL, NULL, NULL, 3, NULL
GO

DECLARE @currDate DATE = GETDATE()
EXEC usp_Found_Pet_Update 0, @currDate, 3
GO

EXEC usp_Found_Pet_Delete 1
GO

EXEC usp_Found_Pet_Find 1
GO

EXEC usp_Found_Pet_FindAll
GO


SP_COLUMNS Pet
GO

INSERT INTO Race (Name) VALUES ('Chow Chow')
GO

EXEC usp_Race_FindAll

