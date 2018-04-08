USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_REPORT_LIST]    Script Date: 4/8/2018 10:32:46 PM ******/
DROP PROCEDURE [dbo].[SP_GET_REPORT_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_REPORT_LIST]    Script Date: 4/8/2018 10:32:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_REPORT_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_REPORT_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_REPORT_LIST] 10018

	SELECT CR.Id
	, CR.ConsultationId
	, CR.[FileName]
	, CR.FileData
	, CR.[Description]
	, CR.DoctorName
	, CR.DoctorPhoneNumber
	, CR.ReportDate
	, CR.LabName
	, CR.CountryId
	, CM.Country
	, CR.AddedBy
	, CR.AddedDate
	, CR.ModifiedBy
	, CR.ModifiedDate	
	FROM [ConsultationReports] CR
	INNER JOIN Consultation C ON CR.ConsultationId = C.Id
	INNER JOIN CountryMaster CM ON CM.ID = CR.CountryId
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_REPORT_ID IS NULL OR CR.Id = @CONSULTATION_REPORT_ID)
	ORDER BY CR.AddedDate DESC
END



GO


