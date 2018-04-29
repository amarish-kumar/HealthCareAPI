USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_MEDICATION_LIST]    Script Date: 4/29/2018 11:07:31 PM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_MEDICATION_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_MEDICATION_LIST]    Script Date: 4/29/2018 11:07:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_MEDICATION_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_MEDICATION_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_MEDICATION_LIST] 10018

SELECT CM.Id
	, CM.ConsultationId
	, CM.DrugChemicalId
	, DC.[Description] as DrugChemicalName
	, CM.DrugChemicalOtherDescription
	, CM.DrugBrandId
	, DB.[Description] as DrugBrandName
	, CM.DrugBrandOtherDescription
	, CM.DrugFrequencyId
	, DF.[Description] as DrugFrequencyName
	, CM.DrugTypeId
	, DT.[Description] as DrugTypeName
	, CM.DrugSubTypeId
	, DST.[Description] as DrugSubTypeName
	, CM.DrugDosage
	, CM.DrugUnitId
	, UM.[Description] as DrugUnitName
	, CM.DrugStartDate
	, CM.DrugEndDate
	, CM.AddedBy
	, CM.AddedDate
	, CM.ModifiedBy
	, CM.ModifiedDate	
	FROM [ConsultationMedications] CM
	INNER JOIN Consultation C ON CM.ConsultationId = C.Id
	INNER JOIN DrugChemicalMaster DC ON DC.ID = CM.DrugChemicalId
	INNER JOIN DrugBrandMaster DB ON DB.ID = CM.DrugBrandId
	INNER JOIN DrugFrequencyMaster DF ON DF.ID = CM.DrugFrequencyId
	INNER JOIN DrugTypeMaster DT ON DT.ID = CM.DrugTypeId
	INNER JOIN DrugSubTypeMaster DST ON DST.ID = CM.DrugSubTypeId
	INNER JOIN UnitMaster UM ON UM.ID = CM.DrugUnitId
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_MEDICATION_ID IS NULL OR CM.Id = @CONSULTATION_MEDICATION_ID)
	ORDER BY CM.AddedDate DESC
END








GO


