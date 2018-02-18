USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONVERSATION_MANAGER]    Script Date: 2/17/2018 8:00:28 PM ******/
DROP PROCEDURE [dbo].[SP_CONVERSATION_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONVERSATION_MANAGER]    Script Date: 2/17/2018 8:00:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_CONVERSATION_MANAGER]
(
	@CONVERSATION_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --CONVERSATION FOR START/UPDATE CONVERSATION RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @PatientId AS BIGINT, @DoctorId AS BIGINT, @ConsultationId AS BIGINT
DECLARE @Description AS NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT, @IsLocked as BIT

SELECT	 @Id = ConversationList.Columns.value('Id[1]', 'BIGINT')
	   , @Description = ConversationList.Columns.value('Description[1]', 'nvarchar(max)')
	   , @PatientId = ConversationList.Columns.value('PatientId[1]', 'BIGINT')
	   , @DoctorId = ConversationList.Columns.value('DoctorId[1]', 'BIGINT')
	   , @ConsultationId = ConversationList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @Active = ConversationList.Columns.value('Active[1]', 'bit')
FROM   @CONVERSATION_XML.nodes('Conversation') AS ConversationList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION = 'CONVERSATION'
BEGIN

	IF @Id IS NULL OR @Id =0 
	BEGIN

	INSERT INTO [dbo].[Conversation]
			([Description]
			,[ConsultationId]
			,[PatientId]
			,[DoctorId]
			,[IsLocked]
			,[Active]
			,[AddedBy]
			,[AddedDate])
		VALUES
			(@Description
			,@ConsultationId
			,@PatientId
			,@DoctorId			
			,0
			,@Active
			,@USER_ID
			,GETDATE())

			SET @Result = 1;
			SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN
		SELECT @IsLocked = IsLocked
		FROM [dbo].[Conversation]
		WHERE Id = @Id 
		IF ISNULL(@IsLocked, 1) <> 1
		BEGIN
			UPDATE [dbo].[Conversation]
				SET
				[Description] = @Description,
				[IsLocked] = @IsLocked,
				[ModifiedBy] = @USER_ID,
				[ModifiedDate] = GETDATE()
			WHERE Id = @Id
			SET @Result = 1;
			SET @ReturnMessage = 'Record updated successfully.'
		END

		ELSE
		BEGIN
			SET @Result = 0;
			SET @ReturnMessage = 'Record does not exist or locked.'
		END

	END
END

SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END



GO


