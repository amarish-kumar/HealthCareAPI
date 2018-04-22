USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_SURGERY_MANAGER]    Script Date: 4/22/2018 10:16:23 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_SURGERY_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_SURGERY_MANAGER]    Script Date: 4/22/2018 10:16:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_CONSULTATION_SURGERY_MANAGER]
(
	@CONSULTATION_SURGERY_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE SURGERY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @SurgeryId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @OtherDescription as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT
DECLARE @SurgeryDate AS DATETIME

SELECT	 @Id = ConsultationSurgeryList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationSurgeryList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @SurgeryId = ConsultationSurgeryList.Columns.value('SurgeryId[1]', 'BIGINT')   
	   , @SurgeryDate = ConsultationSurgeryList.Columns.value('SurgeryDate[1]', 'DATETIME')
	   , @OtherDescription = ConsultationSurgeryList.Columns.value('OtherDescription[1]', 'NVARCHAR(MAX)')
	   , @Active = ConsultationSurgeryList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_SURGERY_XML.nodes('ConsultationSurgeries') AS ConsultationSurgeryList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationSurgeries]
           ([ConsultationId]
           ,[SurgeryId]
		   ,[OtherDescription]
           ,[SurgeryDate]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ConsultationId
           ,@SurgeryId
		   ,@OtherDescription
           ,@SurgeryDate
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationSurgeries]
		   SET [SurgeryId] = ISNULL(@SurgeryId,[SurgeryId])
			  ,[OtherDescription] = @OtherDescription
			  ,[SurgeryDate] = ISNULL(@SurgeryDate, [SurgeryDate])
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


