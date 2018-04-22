USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_SURGERY_LIST]    Script Date: 4/22/2018 10:14:58 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_SURGERY_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_SURGERY_LIST]    Script Date: 4/22/2018 10:14:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_SURGERY_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_SURGERY_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_SURGERY_LIST] 10018

SELECT CS.Id
	, CS.ConsultationId
	, CS.SurgeryId
	, CS.OtherDescription
	, SM.[Description] as SurgeryName
	, CS.SurgeryDate
	, CS.AddedBy
	, CS.AddedDate
	, CS.ModifiedBy
	, CS.ModifiedDate	
	FROM [ConsultationSurgeries] CS
	INNER JOIN Consultation C ON CS.ConsultationId = C.Id
	INNER JOIN SurgeryMaster SM ON SM.ID = CS.SurgeryId
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_SURGERY_ID IS NULL OR CS.Id = @CONSULTATION_SURGERY_ID)
	ORDER BY CS.AddedDate DESC
END





GO


