USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_AWARD_LIST]    Script Date: 7/10/2018 10:49:07 AM ******/
DROP PROCEDURE [dbo].[SP_GET_DOCTOR_AWARD_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_AWARD_LIST]    Script Date: 7/10/2018 10:49:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_DOCTOR_AWARD_LIST]
(	
	@DOCTOR_ID BIGINT,
	@DOCTOR_AWARD_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_DOCTOR_AWARD_LIST] 3

SELECT DA.Id
	, DA.DoctorId
	, UD.FirstName + ', ' + UD.LastName AS 'DoctorName'
	, DA.YearReceived
	, DA.InstitutionName
	, DA.AwardName
	, DA.AddedBy
	, DA.AddedDate
	, DA.ModifiedBy
	, DA.ModifiedDate	
	FROM [DoctorAwards] DA
	INNER JOIN UserDetail UD ON DA.DoctorId = UD.Id
	WHERE DA.DoctorId = @DOCTOR_ID
	AND (@DOCTOR_AWARD_ID IS NULL OR DA.Id = @DOCTOR_AWARD_ID)
	AND DA.Active = 1
	ORDER BY DA.AddedDate DESC
END










GO


