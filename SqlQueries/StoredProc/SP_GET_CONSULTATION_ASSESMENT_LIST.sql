USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_ASSESMENT_LIST]    Script Date: 7/17/2018 11:52:05 PM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_ASSESMENT_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_ASSESMENT_LIST]    Script Date: 7/17/2018 11:52:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_ASSESMENT_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_ASSESMENT_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_ASSESMENT_LIST] 10018

SELECT CA.Id
	, CA.ConsultationId
	, CA.Notes	
	, CA.DiagnosisNotes
	, CA.DiagnosisDoctorId
	, UD.FirstName + ', ' + UD.LastName AS [DiagnosisDoctorName]
	, CA.DiagnosisTimestamp	
	, CA.DiffDiagnosisTimestamp
	, CA.DiffDiagnosisNotes
	, CA.DiffDiagnosisDoctorId
	, UDD.FirstName + ', ' + UDD.LastName AS [DiffDiagnosisDoctorName]
	, CA.AddedBy
	, CA.AddedDate
	, CA.ModifiedBy
	, CA.ModifiedDate	
	FROM [ConsultationAssesments] CA
	INNER JOIN Consultation C ON CA.ConsultationId = C.Id
	LEFT OUTER JOIN UserDetail UD ON UD.Id = CA.DiagnosisDoctorId
	LEFT OUTER JOIN UserDetail UDD ON UDD.Id = CA.DiffDiagnosisDoctorId
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_ASSESMENT_ID IS NULL OR CA.Id = @CONSULTATION_ASSESMENT_ID)
	AND CA.Active = 1
	ORDER BY CA.AddedDate DESC
END



GO


