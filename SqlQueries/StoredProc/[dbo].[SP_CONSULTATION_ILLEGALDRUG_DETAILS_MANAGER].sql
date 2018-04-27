USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_ILLEGALDRUG_DETAILS_MANAGER]    Script Date: 4/15/2018 2:58:31 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_ILLEGALDRUG_DETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_ILLEGALDRUG_DETAILS_MANAGER]    Script Date: 4/15/2018 2:58:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_CONSULTATION_ILLEGALDRUG_DETAILS_MANAGER]
(
	@CONSULTATION_ILLEGALDRUG_DETAILS_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE SURGERY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @ConsumeDrugs AS BIT
DECLARE @IllegalDrugsID as BIGINT, @OtherDescription as NVARCHAR(MAX), @PerFrequency as BIGINT,@Frequency as NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = ConsultationILLEGALDRUGDETAILSList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsumeDrugs = ConsultationILLEGALDRUGDETAILSList.Columns.value('ConsumeDrugs[1]', 'BIT')
	   , @IllegalDrugsID = ConsultationILLEGALDRUGDETAILSList.Columns.value('IllegalDrugsID[1]', 'BIGINT')
	   , @OtherDescription = ConsultationILLEGALDRUGDETAILSList.Columns.value('OtherDescription[1]', 'NVARCHAR(MAX)')
	   , @ConsultationId = ConsultationILLEGALDRUGDETAILSList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @Frequency  = ConsultationILLEGALDRUGDETAILSList.Columns.value('Frequency[1]', 'NVARCHAR(MAX)')
	   , @PerFrequency = ConsultationILLEGALDRUGDETAILSList.Columns.value('PerFrequency[1]', 'BIGINT')
	   , @Active = ConsultationILLEGALDRUGDETAILSList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_ILLEGALDRUG_DETAILS_XML.nodes('ConsultationIllegalDrugDetails') AS ConsultationILLEGALDRUGDETAILSList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationIllegaldrugs]
           ([ConsultationId]
           ,[Consumedrugs]
           ,[IllegalDrugsID]
		   ,[OtherDescription]
           ,[Frequency]
           ,[PerFrequency]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ConsultationId
           ,@ConsumeDrugs
           ,@IllegalDrugsID
		   ,@OtherDescription
		   ,@Frequency
		   ,@PerFrequency
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationIllegaldrugs]
		   SET [Consumedrugs] = ISNULL(@ConsumeDrugs,[Consumedrugs])
			  ,[IllegalDrugsID] = ISNULL(@IllegalDrugsID, [IllegalDrugsID])
			  ,[OtherDescription] = @OtherDescription
			  ,[Frequency] = @Frequency
			  ,[PerFrequency] = ISNULL(@PerFrequency, [PerFrequency])
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


