USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_FAMILY_HISTORY_LIST]    Script Date: 4/22/2018 10:15:35 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_FAMILY_HISTORY_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_FAMILY_HISTORY_LIST]    Script Date: 4/22/2018 10:15:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_FAMILY_HISTORY_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_FAMILY_HISTORY_ID BIGINT = NULL,
	@RELATIONSHIP_ID BIGINT = NULL,
	@EXCLUDE_SELF BIT = 1 
)
AS

BEGIN

/*this sp will be used for family/personal history
by default the @EXCLUDE_SELF = 1 so it will just provide family history
for getting personal history pass the relationship id and @EXCLUDE_SELF = 0*/
--EXEC [SP_GET_CONSULTATION_FAMILY_HISTORY_LIST] 10018
/*BLOCK TO GET THE RELATIONSHIP ID OF "SELF"*/
DECLARE @SELF_ID AS BIGINT 
SELECT @SELF_ID = ID FROM RelationshipMaster
WHERE [Description] = 'Self'

SELECT CFH.Id
	, CFH.ConsultationId
	, CFH.RelationshipId
	, RM.[Description] AS Relationship
	, CFH.HealthConditionId
	, HCM.[Description] AS HealthCondition
	, CFH.OtherHealthConditionDescription
	, CFH.CurrentAge
	, CFH.ConditionStartDate
	, CFH.AgeOnConditionStart
	, CFH.IsAlive
	, CFH.CauseOfDeath
	, CFH.AgeOnDeath
	, CFH.AddedBy
	, CFH.AddedDate
	, CFH.ModifiedBy
	, CFH.ModifiedDate	
	FROM [ConsultationFamilyHistory] CFH
	INNER JOIN Consultation C ON CFH.ConsultationId = C.Id
	INNER JOIN HealthConditionMaster HCM ON HCM.ID = CFH.HealthConditionId
	INNER JOIN RelationshipMaster RM ON RM.ID = CFH.RelationshipId
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_FAMILY_HISTORY_ID IS NULL OR CFH.Id = @CONSULTATION_FAMILY_HISTORY_ID)
	AND (@RELATIONSHIP_ID IS NULL OR CFH.RelationshipId = @RELATIONSHIP_ID)
	AND (@EXCLUDE_SELF IS NULL OR @EXCLUDE_SELF = 0  OR CFH.RelationshipId <> @SELF_ID)
	ORDER BY CFH.AddedDate DESC
END






GO


