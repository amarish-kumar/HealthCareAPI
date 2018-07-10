USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_FELLOWSHIP_LIST]    Script Date: 7/10/2018 10:50:00 AM ******/
DROP PROCEDURE [dbo].[SP_GET_DOCTOR_FELLOWSHIP_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_FELLOWSHIP_LIST]    Script Date: 7/10/2018 10:50:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_DOCTOR_FELLOWSHIP_LIST]
(	
	@DOCTOR_ID BIGINT,
	@DOCTOR_FELLOWSHIP_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_DOCTOR_FELLOWSHIP_LIST] 3

SELECT DF.Id
	, DF.DoctorId
	, UD.FirstName + ', ' + UD.LastName AS 'DoctorName'
	, DF.BeginingYear
	, DF.EndingYear
	, DF.HospitalName
	, DF.City
	, DF.StateId
	, S.[State] AS [StateName]
	, DF.CountryId
	, C.Country AS [CountryName]
	, DF.BeginingYear
	, DF.AddedBy
	, DF.AddedDate
	, DF.ModifiedBy
	, DF.ModifiedDate	
	FROM [DoctorFellowship] DF
	INNER JOIN UserDetail UD ON DF.DoctorId = UD.Id
	INNER JOIN StateMaster S ON DF.StateId = S.Id
	INNER JOIN CountryMaster C ON DF.CountryId = C.Id
	WHERE DF.DoctorId = @DOCTOR_ID
	AND (@DOCTOR_FELLOWSHIP_ID IS NULL OR DF.Id = @DOCTOR_FELLOWSHIP_ID)
	AND DF.Active = 1
	ORDER BY DF.AddedDate DESC
END










GO


