USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_ALLERGY_LIST]    Script Date: 4/13/2018 7:11:15 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_ALLERGY_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_ALLERGY_LIST]    Script Date: 4/13/2018 7:11:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_ALLERGY_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_ALLERGY_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_ALLERGY_LIST] 10018

SELECT CA.Id
	, CA.ConsultationId
	, CA.AllergyId
	, AM.[Description] as AllergyName
	, CA.AllergyStartDate
	, CA.Treatment
	, CA.AddedBy
	, CA.AddedDate
	, CA.ModifiedBy
	, CA.ModifiedDate	
	FROM [ConsultationAllergies] CA
	INNER JOIN Consultation C ON CA.ConsultationId = C.Id
	INNER JOIN AllergyMaster AM ON AM.ID = CA.AllergyId
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_ALLERGY_ID IS NULL OR CA.Id = @CONSULTATION_ALLERGY_ID)
	ORDER BY CA.AddedDate DESC
END





GO


