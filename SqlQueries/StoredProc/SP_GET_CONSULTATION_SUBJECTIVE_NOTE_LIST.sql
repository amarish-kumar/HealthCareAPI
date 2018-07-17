USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_SUBJECTIVE_NOTE_LIST]    Script Date: 7/17/2018 10:47:36 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_SUBJECTIVE_NOTE_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_SUBJECTIVE_NOTE_LIST]    Script Date: 7/17/2018 10:47:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_SUBJECTIVE_NOTE_LIST]
(	
	@CONSULTATION_SUBJECTIVE_ID BIGINT,
	@CONSULTATION_SUBJECTIVE_NOTE_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_SUBJECTIVE_NOTE_LIST] 1

SELECT CSN.Id
	, CSN.ConsultationSubjectiveId
	, CSN.Notes	
	, CSN.[Timestamp]
	, CSN.DoctorId
	, UD.FirstName + ', ' + UD.LastName AS [DoctorName]
	, CSN.AddedBy
	, CSN.AddedDate
	, CSN.ModifiedBy
	, CSN.ModifiedDate	
	FROM [ConsultationSubjectiveNotes] CSN
	INNER JOIN ConsultationSubjectives CS ON CSN.ConsultationSubjectiveId = CS.Id
	LEFT OUTER JOIN UserDetail UD ON UD.Id = CSN.DoctorId
	WHERE CS.Id = @CONSULTATION_SUBJECTIVE_ID
	AND (@CONSULTATION_SUBJECTIVE_NOTE_ID IS NULL OR CSN.Id = @CONSULTATION_SUBJECTIVE_NOTE_ID)
	AND CSN.Active = 1
	ORDER BY CSN.AddedDate DESC
END


GO


