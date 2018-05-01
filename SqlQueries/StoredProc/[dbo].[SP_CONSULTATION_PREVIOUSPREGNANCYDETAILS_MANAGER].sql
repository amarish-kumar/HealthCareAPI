USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_PREVIOUSPREGNANCYDETAILS_MANAGER]    Script Date: 5/1/2018 10:18:53 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_PREVIOUSPREGNANCYDETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_PREVIOUSPREGNANCYDETAILS_MANAGER]    Script Date: 5/1/2018 10:18:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_CONSULTATION_PREVIOUSPREGNANCYDETAILS_MANAGER]
(
	@CONSULTATION_PREVIOUSPREGNANCYDETAILS_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE SURGERY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT
DECLARE @CurrentPregnancyID as BIGINT, @NoofPregnancy as BIGINT, @ChildNo as BIGINT, @DeliveryYear as NVARCHAR(MAX),
@DeliveryType as NVARCHAR(MAX), @Active as BIT,@ReturnMessage as NVARCHAR(MAX), @Result as BIT

SELECT	 @Id = ConsultationPreviousPregnancyDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationPreviousPregnancyDetailsList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @CurrentPregnancyID = ConsultationPreviousPregnancyDetailsList.Columns.value('CurrentPregnancyID[1]', 'BIGINT')
	   , @NoofPregnancy = ConsultationPreviousPregnancyDetailsList.Columns.value('NoofPregnancy[1]', 'BIGINT')
	   , @ChildNo = ConsultationPreviousPregnancyDetailsList.Columns.value('ChildNo[1]', 'BIGINT')
	   , @DeliveryYear = ConsultationPreviousPregnancyDetailsList.Columns.value('DeliveryYear[1]','NVARCHAR(MAX)')
	   , @DeliveryType = ConsultationPreviousPregnancyDetailsList.Columns.value('DeliveryType[1]','NVARCHAR(MAX)')
	   , @Active = ConsultationPreviousPregnancyDetailsList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_PREVIOUSPREGNANCYDETAILS_XML.nodes('ConsultationPreviousPregnancyDetails') AS ConsultationPreviousPregnancyDetailsList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationPreviousPregnancyDetails]
    ([ConsultationId] ,
	[CurrentPregnancyID],
	[NoofPregnancy],
	[ChildNo],
	[DeliveryYear],
	[DeliveryType],
	[Active],
	[AddedBy],
	[AddedDate])
     VALUES
    (@ConsultationId
	   , @CurrentPregnancyID
	   , @NoofPregnancy
	   , @ChildNo
	   , @DeliveryYear
	   , @DeliveryType
	   ,@Active
	   ,@USER_ID
	   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationPreviousPregnancyDetails]
		SET 
		[CurrentPregnancyID] = ISNULL(@CurrentPregnancyID,[CurrentPregnancyID]),
		[NoofPregnancy] = ISNULL(@NoofPregnancy,[NoofPregnancy]),
		[ChildNo] = ISNULL(@ChildNo,[ChildNo]) ,
		[DeliveryYear] = ISNULL(@DeliveryYear,[DeliveryYear]),
		[DeliveryType] = ISNULL(@DeliveryType,[DeliveryType]),
		[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id

		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END
END

SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END












GO


