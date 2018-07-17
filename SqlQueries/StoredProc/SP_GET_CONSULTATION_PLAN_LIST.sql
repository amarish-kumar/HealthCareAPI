USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_PLAN_LIST]    Script Date: 7/17/2018 11:09:38 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_PLAN_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_PLAN_LIST]    Script Date: 7/17/2018 11:09:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_PLAN_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_PLAN_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_PLAN_LIST] 10018

SELECT CP.Id
	, CP.ConsultationId
	, CP.Notes	
	, CP.[Timestamp]
	, CP.DoctorId
	, UD.FirstName + ', ' + UD.LastName AS [DoctorName]
	, CP.AddedBy
	, CP.AddedDate
	, CP.ModifiedBy
	, CP.ModifiedDate	
	FROM [ConsultationPlans] CP
	INNER JOIN Consultation C ON CP.ConsultationId = C.Id
	LEFT OUTER JOIN UserDetail UD ON UD.Id = CP.DoctorId
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_PLAN_ID IS NULL OR CP.Id = @CONSULTATION_PLAN_ID)
	AND CP.Active = 1
	ORDER BY CP.AddedDate DESC
END


GO


