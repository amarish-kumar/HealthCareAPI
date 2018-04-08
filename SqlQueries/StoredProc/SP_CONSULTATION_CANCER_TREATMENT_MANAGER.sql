USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_CANCER_TREATMENT_MANAGER]    Script Date: 4/8/2018 11:51:13 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_CANCER_TREATMENT_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_CANCER_TREATMENT_MANAGER]    Script Date: 4/8/2018 11:51:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CONSULTATION_CANCER_TREATMENT_MANAGER]
(
	@CONSULTATION_CANCER_TREATMENT_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE SURGERY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @CancerStageId AS BIGINT
DECLARE @CancerType as NVARCHAR(MAX), @TreatmentType as NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT, @IsTreatmentOn AS BIT
DECLARE @DignosisDate AS DATETIME, @TreatmentCompletionDate AS DATETIME

SELECT	 @Id = ConsultationCancerTreatmentList.Columns.value('Id[1]', 'BIGINT')
	   , @CancerType = ConsultationCancerTreatmentList.Columns.value('CancerType[1]', 'NVARCHAR(MAX)')
	   , @TreatmentType = ConsultationCancerTreatmentList.Columns.value('TreatmentType[1]', 'NVARCHAR(MAX)')
	   , @ConsultationId = ConsultationCancerTreatmentList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @CancerStageId = ConsultationCancerTreatmentList.Columns.value('CancerStageId[1]', 'BIGINT')   
	   , @DignosisDate = ConsultationCancerTreatmentList.Columns.value('DignosisDate[1]', 'DATETIME')
	   , @TreatmentCompletionDate = ConsultationCancerTreatmentList.Columns.value('TreatmentCompletionDate[1]', 'DATETIME')
	   , @IsTreatmentOn = ConsultationCancerTreatmentList.Columns.value('IsTreatmentOn[1]', 'bit')
	   , @Active = ConsultationCancerTreatmentList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_CANCER_TREATMENT_XML.nodes('ConsultationCancerTreatments') AS ConsultationCancerTreatmentList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationCancerTreatments]
           ([ConsultationId]
           ,[CancerType]
           ,[CancerStageId]
           ,[DignosisDate]
           ,[TreatmentType]
           ,[IsTreatmentOn]
           ,[TreatmentCompletionDate]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ConsultationId
           ,@CancerType
           ,@CancerStageId
		   ,@DignosisDate
		   ,@TreatmentType
		   ,@IsTreatmentOn
		   ,@TreatmentCompletionDate
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationCancerTreatments]
		   SET [CancerType] = ISNULL(@CancerType,[CancerType])
			  ,[CancerStageId] = ISNULL(@CancerStageId, [CancerStageId])
			  ,[DignosisDate] = @DignosisDate
			  ,[TreatmentType] = ISNULL(@TreatmentType, [TreatmentType])
			  ,[IsTreatmentOn] = ISNULL(@IsTreatmentOn, [IsTreatmentOn])
			  ,[TreatmentCompletionDate] = @TreatmentCompletionDate
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


