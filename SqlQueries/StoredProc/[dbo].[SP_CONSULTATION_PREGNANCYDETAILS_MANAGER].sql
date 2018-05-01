USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_PREGNANCYDETAILS_MANAGER]    Script Date: 4/29/2018 6:16:07 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_PREGNANCYDETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_PREGNANCYDETAILS_MANAGER]    Script Date: 4/29/2018 6:16:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_CONSULTATION_PREGNANCYDETAILS_MANAGER]
(
	@CONSULTATION_PREGNANCYDETAILS_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE SURGERY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @CurrentlyPregnant AS BIT
DECLARE @CurrentPregnancyMonths as BIGINT, @CurrentPregnancyEDD as DATETIME, @PregnantBefore as BIT, @MenstrualCycles as BIT, @NoMCReason as NVARCHAR(MAX),
@LastMCCycle as DATETIME,@MCRegInterval as BIT, @LenMCCycle as BIGINT,@MCStartAge as BIGINT, @MCFlow as NVARCHAR(MAX),
@MCProductType as NVARCHAR(MAX), @MCProductPerDay as BIGINT, @MCPain as BIT, @MCPainSeverity as BIGINT,
@MCSymptomID as NVARCHAR(MAX), @MCSymptomDesc as NVARCHAR(MAX), @MCSymptomIDArray as NVARCHAR(MAX), @Active as BIT,
@ReturnMessage as NVARCHAR(MAX), @Result as BIT

SELECT	 @Id = ConsultationPregnancyDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationPregnancyDetailsList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @CurrentlyPregnant = ConsultationPregnancyDetailsList.Columns.value('CurrentlyPregnant[1]', 'BIT')
	   , @CurrentPregnancyMonths = ConsultationPregnancyDetailsList.Columns.value('CurrentPregnancyMonths[1]', 'BIGINT')
	   , @CurrentPregnancyEDD = ConsultationPregnancyDetailsList.Columns.value('CurrentPregnancyEDD[1]', 'DATETIME')
	   , @PregnantBefore = ConsultationPregnancyDetailsList.Columns.value('PregnantBefore[1]','BIT')
	   , @MenstrualCycles = ConsultationPregnancyDetailsList.Columns.value('MenstrualCycles[1]','BIT')
	   , @NoMCReason  = ConsultationPregnancyDetailsList.Columns.value('NoMCReason[1]', 'NVARCHAR(MAX)')
	   , @LastMCCycle = ConsultationPregnancyDetailsList.Columns.value('LastMCCycle[1]', 'DATETIME')
	   , @MCRegInterval = ConsultationPregnancyDetailsList.Columns.value('MCRegInterval[1]','bit')
	   , @LenMCCycle = ConsultationPregnancyDetailsList.Columns.value('LenMCCycle[1]', 'BIGINT')
	   , @MCStartAge = ConsultationPregnancyDetailsList.Columns.value('MCStartAge[1]', 'BIGINT')
	   , @MCFlow = ConsultationPregnancyDetailsList.Columns.value('MCFlow[1]', 'NVARCHAR(MAX)')
	   , @MCProductType = ConsultationPregnancyDetailsList.Columns.value('MCProductType[1]', 'NVARCHAR(MAX)')
	   , @MCProductPerDay = ConsultationPregnancyDetailsList.Columns.value('MCProductPerDay[1]', 'BIGINT')
	   , @MCPain = ConsultationPregnancyDetailsList.Columns.value('MCPain[1]', 'BIT')
	   , @MCPainSeverity = ConsultationPregnancyDetailsList.Columns.value('MCPainSeverity[1]', 'BIGINT')
	   , @MCSymptomID = ConsultationPregnancyDetailsList.Columns.value('MCSymptomID[1]', 'NVARCHAR(MAX)')
	   , @MCSymptomDesc = ConsultationPregnancyDetailsList.Columns.value('MCSymptomDesc[1]', 'NVARCHAR(MAX)')
	   , @MCSymptomIDArray = ConsultationPregnancyDetailsList.Columns.value('MCSymptomIDArray[1]', 'NVARCHAR(MAX)')
	   , @Active = ConsultationPregnancyDetailsList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_PREGNANCYDETAILS_XML.nodes('ConsultationPregnancyDetails') AS ConsultationPregnancyDetailsList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationPregnancyDetails]
    ([ConsultationId] ,
	[CurrentlyPregnant],
	[CurrentPregnancyMonths],
	[CurrentPregnancyEDD],
	[PregnantBefore],
	[MenstrualCycles],
	[NoMCReason],
	[LastMCCycle],
	[MCRegInterval],
	[LenMCCycle],
	[MCStartAge],
	[MCFlow],
	[MCProductType],
	[MCProductPerDay],
	[MCPain],
	[MCPainSeverity],
	[MCSymptomID],
	[Active],
	[AddedBy],
	[AddedDate])
     VALUES
    (@ConsultationId
	   , @CurrentlyPregnant
	   , @CurrentPregnancyMonths
	   , @CurrentPregnancyEDD
	   , @PregnantBefore
	   , @MenstrualCycles
	   , @NoMCReason
	   , @LastMCCycle
	   , @MCRegInterval
	   , @LenMCCycle
	   , @MCStartAge
	   , @MCFlow
	   , @MCProductType
	   , @MCProductPerDay
	   , @MCPain
	   , @MCPainSeverity
	   , @MCSymptomID
	   ,@Active
	   ,@USER_ID
	   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationPregnancyDetails]
		SET 
		[CurrentlyPregnant] = ISNULL(@CurrentlyPregnant,[CurrentlyPregnant]),
		[CurrentPregnancyMonths] = ISNULL(@CurrentPregnancyMonths,[CurrentPregnancyMonths]),
		[CurrentPregnancyEDD] = ISNULL(@CurrentPregnancyEDD,[CurrentPregnancyEDD]) ,
		[PregnantBefore] = ISNULL(@PregnantBefore,[PregnantBefore]),
		[MenstrualCycles] = ISNULL(@MenstrualCycles,[MenstrualCycles]),
		[NoMCReason] = ISNULL(@NoMCReason,[NoMCReason]),
		[LastMCCycle] = ISNULL(@LastMCCycle,[LastMCCycle]),
		[MCRegInterval] = ISNULL(@MCRegInterval,[MCRegInterval]),
		[LenMCCycle] = ISNULL(@LenMCCycle,[LenMCCycle]),
		[MCStartAge] = ISNULL(@MCStartAge,[MCStartAge]),
		[MCFlow] = ISNULL(@MCFlow,[MCFlow]),
		[MCProductType] = ISNULL(@MCProductType,[MCProductType]),
		[MCProductPerDay] = ISNULL(@MCProductPerDay,[MCProductPerDay]),
		[MCPain]= ISNULL(@MCPain,[MCPain]),
		[MCPainSeverity]= ISNULL(@MCPainSeverity,[MCPainSeverity]),
		[MCSymptomID]= ISNULL(@MCSymptomID,[MCSymptomID]),
		[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id

		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END
END

SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END











GO


