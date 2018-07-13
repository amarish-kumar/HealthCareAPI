USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_IMAGE_LIST]    Script Date: 7/10/2018 10:50:16 AM ******/
DROP PROCEDURE [dbo].[SP_GET_DOCTOR_IMAGE_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_IMAGE_LIST]    Script Date: 7/10/2018 10:50:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_DOCTOR_IMAGE_LIST]
(	
	@DOCTOR_ID BIGINT,
	@DOCTOR_IMAGE_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_DOCTOR_IMAGE_LIST] 3

SELECT DI.Id
	, DI.DoctorId
	, UD.FirstName + ', ' + UD.LastName AS 'DoctorName'
	, DI.IsPrimary
	, DI.[FileName]
	, DI.FileData
	, DI.AddedBy
	, DI.AddedDate
	, DI.ModifiedBy
	, DI.ModifiedDate	
	FROM [DoctorImages] DI
	INNER JOIN UserDetail UD ON DI.DoctorId = UD.Id
	WHERE DI.DoctorId = @DOCTOR_ID
	AND (@DOCTOR_IMAGE_ID IS NULL OR DI.Id = @DOCTOR_IMAGE_ID)
	AND DI.Active = 1
	ORDER BY DI.AddedDate DESC
END











GO


