USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_ASSESMENT_MANAGER]    Script Date: 7/17/2018 10:17:12 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_ASSESMENT_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_ASSESMENT_MANAGER]    Script Date: 7/17/2018 10:17:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CONSULTATION_ASSESMENT_MANAGER]
(
	@CONSULTATION_ASSESMENT_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @DiagnosisDoctorId AS BIGINT, @DiffDiagnosisDoctorId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Notes as NVARCHAR(MAX), @DiagnosisNotes as NVARCHAR(MAX)
, @DiffDiagnosisNotes as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT
DECLARE @DiagnosisTimestamp AS DATETIME, @DiffDiagnosisTimestamp AS DATETIME

SELECT	 @Id = ConsultationObjectiveList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationObjectiveList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @DiagnosisDoctorId = ConsultationObjectiveList.Columns.value('DiagnosisDoctorId[1]', 'BIGINT')
	   , @DiffDiagnosisDoctorId = ConsultationObjectiveList.Columns.value('DiffDiagnosisDoctorId[1]', 'BIGINT')
	   , @Notes = ConsultationObjectiveList.Columns.value('Notes[1]', 'NVARCHAR(MAX)')
	   , @DiagnosisNotes = ConsultationObjectiveList.Columns.value('DiagnosisNotes[1]', 'NVARCHAR(MAX)')
	   , @DiffDiagnosisNotes = ConsultationObjectiveList.Columns.value('DiffDiagnosisNotes[1]', 'NVARCHAR(MAX)')	
	   , @DiagnosisTimestamp = ConsultationObjectiveList.Columns.value('DiagnosisTimestamp[1]', 'DATETIME')
	   , @DiffDiagnosisTimestamp = ConsultationObjectiveList.Columns.value('DiffDiagnosisTimestamp[1]', 'DATETIME')   
	   , @Active = ConsultationObjectiveList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_ASSESMENT_XML.nodes('ConsultationObjectives') AS ConsultationObjectiveList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN
	
	INSERT INTO [dbo].[ConsultationAssesments]
           ([ConsultationId]
           ,[Notes]
           ,[DiagnosisTimestamp]
           ,[DiagnosisNotes]
           ,[DiagnosisDoctorId]
           ,[DiffDiagnosisTimestamp]
           ,[DiffDiagnosisNotes]
           ,[DiffDiagnosisDoctorId]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])

     VALUES
           (@ConsultationId
           ,@Notes
		   ,@DiagnosisTimestamp
           ,@DiagnosisNotes
		   ,@DiagnosisDoctorId
		   ,@DiffDiagnosisTimestamp
		   ,@DiffDiagnosisNotes
		   ,@DiffDiagnosisDoctorId
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationAssesments]
		   SET [Notes] = @Notes
			  ,[DiagnosisTimestamp] = @DiagnosisTimestamp
			  ,[DiagnosisNotes] = @DiagnosisNotes
			  ,[DiagnosisDoctorId] = @DiagnosisDoctorId
			  ,[DiffDiagnosisTimestamp] = @DiffDiagnosisTimestamp
			  ,[DiffDiagnosisNotes] = @DiffDiagnosisNotes
			  ,[DiffDiagnosisDoctorId] = @DiffDiagnosisDoctorId
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


