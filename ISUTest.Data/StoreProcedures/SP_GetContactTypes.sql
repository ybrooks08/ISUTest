IF EXISTS (SELECT name FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetContactTypes]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[SP_GetContactTypes]
GO
-- ===================================================================================== ----
-- Description		: Store procedure to list all reservations wiht their related contacts and contact types
-- Author		: Yosbel Brooks - for ISU
-- Created date		: 02/02/2021
-- ===================================================================================== ---- 
CREATE PROCEDURE [dbo].[SP_GetContactTypes]
AS
BEGIN
	SELECT id, description 
	FROM ContactType 
END
GO


