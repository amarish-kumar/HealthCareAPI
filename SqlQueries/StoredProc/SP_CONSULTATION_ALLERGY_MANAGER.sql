USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_ALLERGY_MANAGER]    Script Date: 4/22/2018 10:11:15 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_ALLERGY_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_ALLERGY_MANAGER]    Script Date: 4/22/2018 10:11:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_CONSULTATION_ALLERGY_MANAGER]
(
	@CONSULTATION_ALLERGY_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @AllergyId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Treatment as NVARCHAR(MAX), @OtherDescription as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT
DECLARE @AllergyStartDate AS DATETIME

SELECT	 @Id = ConsultationAllergyList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationAllergyList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @AllergyId = ConsultationAllergyList.Columns.value('AllergyId[1]', 'BIGINT')   
	   , @AllergyStartDate = ConsultationAllergyList.Columns.value('AllergyStartDate[1]', 'DATETIME')
	   , @Treatment = ConsultationAllergyList.Columns.value('Treatment[1]', 'NVARCHAR(MAX)')
	   , @OtherDescription = ConsultationAllergyList.Columns.value('OtherDescription[1]', 'NVARCHAR(MAX)')
	   , @Active = ConsultationAllergyList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_ALLERGY_XML.nodes('ConsultationAllergies') AS ConsultationAllergyList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationAllergies]
           ([ConsultationId]
           ,[AllergyId]
		   ,[OtherDescription]
           ,[AllergyStartDate]
           ,[Treatment]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ConsultationId
           ,@AllergyId
		   ,@OtherDescription
           ,@AllergyStartDate
		   ,@Treatment
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationAllergies]
		   SET [AllergyId] = ISNULL(@AllergyId,[AllergyId])
			  ,[OtherDescription] = @OtherDescription
			  ,[AllergyStartDate] = ISNULL(@AllergyStartDate, [AllergyStartDate])
			  ,[Treatment] = ISNULL(@Treatment, [Treatment])
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


