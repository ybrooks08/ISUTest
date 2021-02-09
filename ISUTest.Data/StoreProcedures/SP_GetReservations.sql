IF EXISTS (SELECT name FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetReservations]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[SP_GetReservations]
GO
-- ===================================================================================== ----
-- Description		: Store procedure to list all reservations wiht their related contacts and contact types
-- Author		: Yosbel Brooks - for ISU
-- Created date		: 02/02/2021
-- ===================================================================================== ---- 
CREATE PROCEDURE [dbo].[SP_GetReservations]
AS
BEGIN
	SELECT r.id AS reservationId, r.description AS resevDesc, r.stars, r.favorite, r.createdDate, r.modifiedDate, c.id AS contacId, c.name, c.phoneNumber, c.birth, c.contactTypeId, ct.description AS contactTyppe 
	FROM Reservation r 
	INNER JOIN Contact c ON r.contactName = c.name
	INNER JOIN ContactType ct ON c.contactTypeId = ct.id
	ORDER BY r.modifiedDate
END
GO


