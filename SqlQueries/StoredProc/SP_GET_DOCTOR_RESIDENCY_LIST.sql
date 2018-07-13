USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_RESIDENCY_LIST]    Script Date: 7/10/2018 11:32:38 AM ******/
DROP PROCEDURE [dbo].[SP_GET_DOCTOR_RESIDENCY_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_RESIDENCY_LIST]    Script Date: 7/10/2018 11:32:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_DOCTOR_RESIDENCY_LIST]
(	
	@DOCTOR_ID BIGINT,
	@DOCTOR_RESIDENCY_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_DOCTOR_RESIDENCY_LIST] 3

SELECT DR.Id
	, DR.DoctorId
	, UD.FirstName + ', ' + UD.LastName AS 'DoctorName'
	, DR.BeginingYear
	, DR.EndingYear
	, DR.HospitalName
	, DR.City
	, DR.StateId
	, S.[State] AS [StateName]
	, DR.CountryId
	, C.Country AS [CountryName]
	, DR.AddedBy
	, DR.AddedDate
	, DR.ModifiedBy
	, DR.ModifiedDate	
	FROM [DoctorResidency] DR
	INNER JOIN UserDetail UD ON DR.DoctorId = UD.Id
	INNER JOIN StateMaster S ON DR.StateId = S.Id
	INNER JOIN CountryMaster C ON DR.CountryId = C.Id
	WHERE DR.DoctorId = @DOCTOR_ID
	AND (@DOCTOR_RESIDENCY_ID IS NULL OR DR.Id = @DOCTOR_RESIDENCY_ID)
	AND DR.Active = 1
	ORDER BY DR.AddedDate DESC
END











GO


