USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_OBJECTIVE_LIST]    Script Date: 7/17/2018 10:33:34 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_OBJECTIVE_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_OBJECTIVE_LIST]    Script Date: 7/17/2018 10:33:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_OBJECTIVE_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_OBJECTIVE_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_OBJECTIVE_LIST] 10018

SELECT CO.Id
	, CO.ConsultationId
	, CO.Vitals	
	, CO.LabResults
	, CO.RadioGraphicResults
	, CO.AddedBy
	, CO.AddedDate
	, CO.ModifiedBy
	, CO.ModifiedDate	
	FROM [ConsultationObjectives] CO
	INNER JOIN Consultation C ON CO.ConsultationId = C.Id
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_OBJECTIVE_ID IS NULL OR CO.Id = @CONSULTATION_OBJECTIVE_ID)
	AND CO.Active = 1
	ORDER BY CO.AddedDate DESC
END


GO


