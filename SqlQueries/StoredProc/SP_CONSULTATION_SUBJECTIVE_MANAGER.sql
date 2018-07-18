USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_SUBJECTIVE_MANAGER]    Script Date: 7/18/2018 10:42:36 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_SUBJECTIVE_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_SUBJECTIVE_MANAGER]    Script Date: 7/18/2018 10:42:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_CONSULTATION_SUBJECTIVE_MANAGER]
(
	@CONSULTATION_SUBJECTIVE_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Onset as NVARCHAR(MAX), @Duration as NVARCHAR(MAX)
, @Location as NVARCHAR(MAX), @Character as NVARCHAR(MAX), @AlleviatingFactors as NVARCHAR(MAX)
, @AggravatingFactors as NVARCHAR(MAX), @Radiation as NVARCHAR(MAX), @TemporalPattern as NVARCHAR(MAX)
, @Severity as NVARCHAR(MAX), @Chronology as NVARCHAR(MAX), @AdditionalSymptoms as NVARCHAR(MAX)
, @Allergies as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = ConsultationSubjectiveList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationSubjectiveList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @Onset = ConsultationSubjectiveList.Columns.value('Onset[1]', 'NVARCHAR(MAX)')
	   , @Duration = ConsultationSubjectiveList.Columns.value('Duration[1]', 'NVARCHAR(MAX)')
	   , @Location = ConsultationSubjectiveList.Columns.value('Location[1]', 'NVARCHAR(MAX)')
	   , @Character = ConsultationSubjectiveList.Columns.value('Character[1]', 'NVARCHAR(MAX)')
	   , @AlleviatingFactors = ConsultationSubjectiveList.Columns.value('AlleviatingFactors[1]', 'NVARCHAR(MAX)')
	   , @AggravatingFactors = ConsultationSubjectiveList.Columns.value('AggravatingFactors[1]', 'NVARCHAR(MAX)')
	   , @Radiation = ConsultationSubjectiveList.Columns.value('Radiation[1]', 'NVARCHAR(MAX)')
	   , @TemporalPattern = ConsultationSubjectiveList.Columns.value('TemporalPattern[1]', 'NVARCHAR(MAX)')
	   , @Severity = ConsultationSubjectiveList.Columns.value('Severity[1]', 'NVARCHAR(MAX)')
	   , @Chronology = ConsultationSubjectiveList.Columns.value('Chronology[1]', 'NVARCHAR(MAX)')
	   , @AdditionalSymptoms = ConsultationSubjectiveList.Columns.value('AdditionalSymptoms[1]', 'NVARCHAR(MAX)')
	   , @Allergies = ConsultationSubjectiveList.Columns.value('Allergies[1]', 'NVARCHAR(MAX)')
	   , @Active = ConsultationSubjectiveList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_SUBJECTIVE_XML.nodes('ConsultationSubjectives') AS ConsultationSubjectiveList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationSubjectives]
           ([ConsultationId]
           ,[Onset]
           ,[Duration]
           ,[Location]
           ,[Character]
           ,[AlleviatingFactors]
           ,[AggravatingFactors]
           ,[Radiation]
           ,[TemporalPattern]
           ,[Severity]
           ,[Chronology]
           ,[AdditionalSymptoms]
           ,[allergies]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ConsultationId
           ,@Onset
		   ,@Duration
           ,@Location
		   ,@Character
		   ,@AlleviatingFactors
		   ,@AggravatingFactors
		   ,@Radiation
		   ,@TemporalPattern
		   ,@Severity
		   ,@Chronology
		   ,@AdditionalSymptoms
		   ,@Allergies
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationSubjectives]
		   SET [Onset] = @Onset
			  ,[Duration] = @Duration
			  ,[Location] = @Location
			  ,[Character] = @Character
			  ,[AlleviatingFactors] = @AlleviatingFactors
			  ,[AggravatingFactors] = @AggravatingFactors
			  ,[Radiation] = @Radiation
			  ,[TemporalPattern] = @TemporalPattern
			  ,[Severity] = @Severity
			  ,[Chronology] = @Chronology
			  ,[AdditionalSymptoms] = @AdditionalSymptoms
			  ,[allergies] = @Allergies
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


