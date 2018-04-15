USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_ILLEGALDRUG_DETAILS_LIST]    Script Date: 4/15/2018 3:08:35 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_ILLEGALDRUG_DETAILS_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_ILLEGALDRUG_DETAILS_LIST]    Script Date: 4/15/2018 3:08:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_CONSULTATION_ILLEGALDRUG_DETAILS_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_ILLEGALDRUGDETAILS_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_CONSULTATION_ILLEGALDRUG_DETAILS_LIST] 10018

SELECT CID.Id
	, CID.ConsultationId
	, CID.Consumedrugs
	, CID.IllegalDrugsID
	, IDM.[Description] AS DrugName
	, CID.Frequency
	, CID.PerFrequency
	, CID.AddedBy
	, CID.AddedDate
	, CID.ModifiedBy
	, CID.ModifiedDate	
	FROM [ConsultationIllegaldrugs] CID
	INNER JOIN Consultation C ON CID.ConsultationId = C.Id
	INNER JOIN IllegalDrugsMaster IDM ON IDM.ID = CID.IllegalDrugsID
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_ILLEGALDRUGDETAILS_ID IS NULL OR CID.Id = @CONSULTATION_ILLEGALDRUGDETAILS_ID)
	ORDER BY CID.AddedDate DESC
END





GO


