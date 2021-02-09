IF EXISTS (SELECT name FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetContacts]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[SP_GetContacts]
GO
-- ===================================================================================== ----
-- Description		: Store procedure to list all reservations wiht their related contacts and contact types
-- Author		: Yosbel Brooks - for ISU
-- Created date		: 02/02/2021
-- ===================================================================================== ---- 
CREATE PROCEDURE [dbo].[SP_GetContacts]
AS
BEGIN
	SELECT c.id, c.name, c.phoneNumber, c.birth, c.contactTypeId, ct.description AS contactTyppe 
	FROM Contact c 
	INNER JOIN ContactType ct ON c.contactTypeId = ct.id
END
GO


