USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_SDDHABITS_MANAGER]    Script Date: 4/16/2018 12:02:33 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_SDDHABITS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_SDDHABITS_MANAGER]    Script Date: 4/16/2018 12:02:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_CONSULTATION_SDDHABITS_MANAGER]
(
	@CONSULTATION_SDDHABITS_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE SURGERY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @DoSmoke AS BIT, @EverSmoked AS BIT
DECLARE @YearOfQuittingSmoking as BIGINT, @SmokingFreq as BIGINT, @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = ConsultationSDDHabitsList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationSDDHabitsList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @DoSmoke = ConsultationSDDHabitsList.Columns.value('DoSmoke[1]', 'BIT')
	   , @EverSmoked = ConsultationSDDHabitsList.Columns.value('EverSmoked[1]', 'BIT')
	   , @YearOfQuittingSmoking  = ConsultationSDDHabitsList.Columns.value('YearOfQuittingSmoking[1]', 'BIGINT')
	   , @SmokingFreq = ConsultationSDDHabitsList.Columns.value('SmokingFreq[1]', 'BIGINT')
	   , @Active = ConsultationSDDHabitsList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_SDDHABITS_XML.nodes('ConsultationSDDHabits') AS ConsultationSDDHabitsList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationSDDHabits]
           ([ConsultationId]
           ,[DoSmoke]
           ,[EverSmoked]
           ,[YearOfQuittingSmoking]
           ,[SmokingFreq]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ConsultationId
           ,@DoSmoke
           ,@EverSmoked
		   ,@YearOfQuittingSmoking
		   ,@SmokingFreq
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationSDDHabits]
		   SET [DoSmoke] = ISNULL(@DoSmoke,[DoSmoke])
			  ,[EverSmoked] = ISNULL(@EverSmoked, [EverSmoked])
			  ,[YearOfQuittingSmoking] = @YearOfQuittingSmoking
			  ,[SmokingFreq] = ISNULL(@SmokingFreq, [SmokingFreq])
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


