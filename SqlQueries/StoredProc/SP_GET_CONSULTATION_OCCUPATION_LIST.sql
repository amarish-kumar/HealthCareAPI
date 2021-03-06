USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_OCCUPATION_LIST]    Script Date: 4/22/2018 10:14:23 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_OCCUPATION_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_OCCUPATION_LIST]    Script Date: 4/22/2018 10:14:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_OCCUPATION_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_OCCUPATION_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_OCCUPATION_LIST] 10018

SELECT CO.Id
	, CO.ConsultationId
	, CO.OccupationId
	, CO.OtherDescription
	, OM.[Description] OccupationName
	, CO.AddedBy
	, CO.AddedDate
	, CO.ModifiedBy
	, CO.ModifiedDate	
	FROM [ConsultationOccupation] CO
	INNER JOIN Consultation C ON CO.ConsultationId = C.Id
	INNER JOIN OccupationMaster OM ON CO.OccupationId = OM.Id
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_OCCUPATION_ID IS NULL OR CO.Id = @CONSULTATION_OCCUPATION_ID)
	ORDER BY CO.AddedDate DESC
END








GO


