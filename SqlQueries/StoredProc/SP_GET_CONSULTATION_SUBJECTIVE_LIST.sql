USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_SUBJECTIVE_LIST]    Script Date: 7/17/2018 10:31:10 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_SUBJECTIVE_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_SUBJECTIVE_LIST]    Script Date: 7/17/2018 10:31:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_SUBJECTIVE_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_SUBJECTIVE_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_SUBJECTIVE_LIST] 10018

SELECT CS.Id
	, CS.ConsultationId
	, CS.Onset	
	, CS.Duration
	, CS.Location
	, CS.[Character]
	, CS.AlleviatingFactors
	, CS.AggravatingFactors
	, CS.Radiation
	, CS.TemporalPattern
	, CS.Severity
	, CS.Chronology
	, CS.Severity
	, CS.Allergies
	, CS.AddedBy
	, CS.AddedDate
	, CS.ModifiedBy
	, CS.ModifiedDate	
	FROM [ConsultationSubjectives] CS
	INNER JOIN Consultation C ON CS.ConsultationId = C.Id
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_SUBJECTIVE_ID IS NULL OR CS.Id = @CONSULTATION_SUBJECTIVE_ID)
	AND CS.Active = 1
	ORDER BY CS.AddedDate DESC
END


GO


