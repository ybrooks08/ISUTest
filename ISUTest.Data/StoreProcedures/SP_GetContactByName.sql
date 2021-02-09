IF EXISTS (SELECT name FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetContactByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[SP_GetContactByName]
GO
-- ===================================================================================== ----
-- Description		: Store procedure to list all reservations wiht their related contacts and contact types
-- Author		: Yosbel Brooks - for ISU
-- Created date		: 02/02/2021
-- ===================================================================================== ---- 
CREATE PROCEDURE [dbo].[SP_GetContactByName]
(
	@contactName varchar(15) 
)
AS
BEGIN

	IF @contactName IS NOT NULL
	SELECT c.id AS contacId, c.name, c.phoneNumber, c.birth, c.contactTypeId, ct.description AS contactTyppe 
	FROM Contact c 	
	INNER JOIN ContactType ct ON c.contactTypeId = ct.id
	WHERE c.name = @contactName
END
GO


