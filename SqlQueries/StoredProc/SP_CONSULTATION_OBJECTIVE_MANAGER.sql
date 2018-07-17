USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_OBJECTIVE_MANAGER]    Script Date: 7/17/2018 10:07:16 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_OBJECTIVE_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_OBJECTIVE_MANAGER]    Script Date: 7/17/2018 10:07:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CONSULTATION_OBJECTIVE_MANAGER]
(
	@CONSULTATION_OBJECTIVE_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Vitals as NVARCHAR(MAX), @LabResults as NVARCHAR(MAX)
, @RadioGraphicResults as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = ConsultationObjectiveList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationObjectiveList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @Vitals = ConsultationObjectiveList.Columns.value('Vitals[1]', 'NVARCHAR(MAX)')
	   , @LabResults = ConsultationObjectiveList.Columns.value('LabResults[1]', 'NVARCHAR(MAX)')
	   , @RadioGraphicResults = ConsultationObjectiveList.Columns.value('RadioGraphicResults[1]', 'NVARCHAR(MAX)')	   
	   , @Active = ConsultationObjectiveList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_OBJECTIVE_XML.nodes('ConsultationObjectives') AS ConsultationObjectiveList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN


	INSERT INTO [dbo].[ConsultationObjectives]
           ([ConsultationId]
           ,[Vitals]
           ,[LabResults]
           ,[RadioGraphicResults]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])

     VALUES
           (@ConsultationId
           ,@Vitals
		   ,@LabResults
           ,@RadioGraphicResults
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationObjectives]
		   SET [Vitals] = @Vitals
			  ,[LabResults] = @LabResults
			  ,[RadioGraphicResults] = @RadioGraphicResults
			  ,[ModifiedBy] = @USER_ID
			  ,[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id
		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END
END

SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END













GO


