USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_EDUCATION_LIST]    Script Date: 7/10/2018 10:49:44 AM ******/
DROP PROCEDURE [dbo].[SP_GET_DOCTOR_EDUCATION_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_EDUCATION_LIST]    Script Date: 7/10/2018 10:49:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_DOCTOR_EDUCATION_LIST]
(	
	@DOCTOR_ID BIGINT,
	@DOCTOR_EDUCATION_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_DOCTOR_EDUCATION_LIST] 3

SELECT DE.Id
	, DE.DoctorId
	, UD.FirstName + ', ' + UD.LastName AS 'DoctorName'
	, DE.BeginingYear
	, DE.EndingYear
	, DE.CollegeName
	, DE.City
	, DE.StateId
	, S.[State] AS [StateName]
	, DE.CountryId
	, C.Country AS [CountryName]
	, DE.BeginingYear
	, DE.AddedBy
	, DE.AddedDate
	, DE.ModifiedBy
	, DE.ModifiedDate	
	FROM [DoctorEducation] DE
	INNER JOIN UserDetail UD ON DE.DoctorId = UD.Id
	INNER JOIN StateMaster S ON DE.StateId = S.Id
	INNER JOIN CountryMaster C ON DE.CountryId = C.Id
	WHERE DE.DoctorId = @DOCTOR_ID
	AND (@DOCTOR_EDUCATION_ID IS NULL OR DE.Id = @DOCTOR_EDUCATION_ID)
	AND DE.Active = 1
	ORDER BY DE.AddedDate DESC
END










GO


