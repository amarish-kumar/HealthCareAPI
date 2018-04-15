USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_FAMILY_HISTORY_MANAGER]    Script Date: 4/15/2018 2:53:31 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_FAMILY_HISTORY_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_FAMILY_HISTORY_MANAGER]    Script Date: 4/15/2018 2:53:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_CONSULTATION_FAMILY_HISTORY_MANAGER]
(
	@CONSULTATION_FAMILY_HISTORY_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR START/UPDATE FAMILY_HISTORY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @RelationshipId AS BIGINT, @HealthConditionId AS BIGINT
DECLARE @CurrentAge AS INT, @AgeOnConditionStart AS INT, @AgeOnDeath AS INT
DECLARE @CauseOfDeath AS NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @ConditionStartDate AS DATETIME
DECLARE @Active AS BIT, @IsAlive AS BIT, @Result as BIT

SELECT	 @Id = ConsultationFamilyHistoryList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationFamilyHistoryList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @RelationshipId = ConsultationFamilyHistoryList.Columns.value('RelationshipId[1]', 'BIGINT')
	   , @HealthConditionId = ConsultationFamilyHistoryList.Columns.value('HealthConditionId[1]', 'BIGINT')
	   , @CurrentAge = ConsultationFamilyHistoryList.Columns.value('CurrentAge[1]', 'INT')
	   , @ConditionStartDate = ConsultationFamilyHistoryList.Columns.value('ConditionStartDate[1]', 'DATETIME')
	   , @AgeOnConditionStart = ConsultationFamilyHistoryList.Columns.value('AgeOnConditionStart[1]', 'INT')
	   , @AgeOnDeath = ConsultationFamilyHistoryList.Columns.value('AgeOnDeath[1]', 'INT')
	   , @CauseOfDeath = ConsultationFamilyHistoryList.Columns.value('CauseOfDeath[1]', 'nvarchar(max)')
	   , @IsAlive = ConsultationFamilyHistoryList.Columns.value('IsAlive[1]', 'bit')
	   , @Active = ConsultationFamilyHistoryList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_FAMILY_HISTORY_XML.nodes('ConsultationFamilyHistory') AS ConsultationFamilyHistoryList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationFamilyHistory]
           ([ConsultationId]
           ,[RelationshipId]
           ,[HealthConditionId]
           ,[CurrentAge]
		   ,[ConditionStartDate]
           ,[AgeOnConditionStart]
           ,[IsAlive]
           ,[CauseOfDeath]
           ,[AgeOnDeath]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ConsultationId
           ,@RelationshipId
           ,@HealthConditionId
           ,@CurrentAge
		   ,@ConditionStartDate
           ,@AgeOnConditionStart
           ,@IsAlive
           ,@CauseOfDeath
           ,@AgeOnDeath
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

			SET @Result = 1;
			SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationFamilyHistory]
		   SET [RelationshipId] = ISNULL(@RelationshipId,[RelationshipId])
			  ,[HealthConditionId] = ISNULL(@HealthConditionId, [HealthConditionId])
			  ,[CurrentAge] = @CurrentAge
			  ,[ConditionStartDate] = @ConditionStartDate
			  ,[AgeOnConditionStart] = @AgeOnConditionStart
			  ,[IsAlive] = @IsAlive
			  ,[CauseOfDeath] = @CauseOfDeath
			  ,[AgeOnDeath] = @AgeOnDeath
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


