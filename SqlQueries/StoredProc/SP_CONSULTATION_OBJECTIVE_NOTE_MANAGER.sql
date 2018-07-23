USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_OBJECTIVE_NOTE_MANAGER]    Script Date: 7/23/2018 10:19:15 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_OBJECTIVE_NOTE_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_OBJECTIVE_NOTE_MANAGER]    Script Date: 7/23/2018 10:19:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_CONSULTATION_OBJECTIVE_NOTE_MANAGER]
(
	@CONSULTATION_OBJECTIVE_NOTE_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationObjectiveId AS BIGINT, @DoctorId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Notes as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT
DECLARE @Timestamp AS DATETIME

SELECT	 @Id = ConsultationPlanList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationObjectiveId = ConsultationPlanList.Columns.value('ConsultationObjectiveId[1]', 'BIGINT')
	   , @DoctorId = ConsultationPlanList.Columns.value('DoctorId[1]', 'BIGINT')
	   , @Notes = ConsultationPlanList.Columns.value('Notes[1]', 'NVARCHAR(MAX)')
	   , @Timestamp = ConsultationPlanList.Columns.value('Timestamp[1]', 'DATETIME')   
	   , @Active = ConsultationPlanList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_OBJECTIVE_NOTE_XML.nodes('ConsultationObjectiveNotes') AS ConsultationPlanList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN
	
	INSERT INTO [dbo].[ConsultationObjectiveNotes]
           ([ConsultationObjectiveId]
           ,[Notes]
           ,[Timestamp]
           ,[DoctorId]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])

     VALUES
           (@ConsultationObjectiveId
           ,@Notes
		   ,@Timestamp
           ,@DoctorId
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationObjectiveNotes]
		   SET [Notes] = @Notes
			  ,[Timestamp] = @Timestamp
			  ,[DoctorId] = @DoctorId
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


