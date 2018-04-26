USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_MEDICATION_MANAGER]    Script Date: 4/26/2018 11:36:08 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_MEDICATION_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_MEDICATION_MANAGER]    Script Date: 4/26/2018 11:36:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CONSULTATION_MEDICATION_MANAGER]
(
	@CONSULTATION_MEDICATION_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @DrugChemicalId AS BIGINT, @DrugBrandId AS BIGINT, @DrugFrequencyId AS BIGINT
, @DrugTypeId AS BIGINT, @DrugSubTypeId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @DrugChemicalDescription as NVARCHAR(MAX), @DrugBrandDescription as NVARCHAR(MAX)
DECLARE @DrugDosage AS DECIMAL
DECLARE @Active AS BIT, @Result as BIT
DECLARE @DrugStartDate AS DATETIME, @DrugEndDate AS DATETIME

SELECT	 @Id = ConsultationMedicationList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationMedicationList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @DrugChemicalId = ConsultationMedicationList.Columns.value('DrugChemicalId[1]', 'BIGINT')  
	   , @DrugBrandId = ConsultationMedicationList.Columns.value('DrugBrandId[1]', 'BIGINT') 
	   , @DrugFrequencyId = ConsultationMedicationList.Columns.value('DrugFrequencyId[1]', 'BIGINT') 
	   , @DrugTypeId = ConsultationMedicationList.Columns.value('DrugTypeId[1]', 'BIGINT') 
	   , @DrugSubTypeId = ConsultationMedicationList.Columns.value('DrugSubTypeId[1]', 'BIGINT')  
	   , @DrugStartDate = ConsultationMedicationList.Columns.value('DrugStartDate[1]', 'DATETIME')
	   , @DrugEndDate = ConsultationMedicationList.Columns.value('DrugEndDate[1]', 'DATETIME')
	   , @DrugChemicalDescription = ConsultationMedicationList.Columns.value('DrugChemicalDescription[1]', 'NVARCHAR(MAX)')
	   , @DrugBrandDescription = ConsultationMedicationList.Columns.value('DrugBrandDescription[1]', 'NVARCHAR(MAX)')
	   , @DrugDosage = ConsultationMedicationList.Columns.value('DrugDosage[1]', 'DECIMAL')
	   , @Active = ConsultationMedicationList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_MEDICATION_XML.nodes('ConsultationMedications') AS ConsultationMedicationList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN


	INSERT INTO [dbo].[ConsultationMedications]
           ([ConsultationId]
           ,[DrugChemicalId]
           ,[DrugChemicalOtherDescription]
           ,[DrugBrandId]
           ,[DrugBrandOtherDescription]
           ,[DrugDosage]
           ,[DrugFrequencyId]
           ,[DrugTypeId]
           ,[DrugSubTypeId]
           ,[DrugStartDate]
           ,[DrugEndDate]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES          
           (@ConsultationId
           ,@DrugChemicalId
		   ,@DrugChemicalDescription
		   ,@DrugBrandId
		   ,@DrugBrandDescription
		   ,@DrugDosage
           ,@DrugFrequencyId
		   ,@DrugTypeId
		   ,@DrugSubTypeId
		   ,@DrugStartDate
		   ,@DrugEndDate
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationMedications]
		   SET [DrugChemicalId] = ISNULL(@DrugChemicalId,[DrugChemicalId])				
			  ,[DrugChemicalOtherDescription] = @DrugChemicalDescription
			  ,[DrugBrandId] = ISNULL(@DrugBrandId,[DrugBrandId])				
			  ,[DrugBrandOtherDescription] = @DrugBrandDescription
			  ,[DrugDosage] = ISNULL(@DrugDosage,[DrugDosage])	
			  ,[DrugFrequencyId] = ISNULL(@DrugFrequencyId,[DrugFrequencyId])		
			  ,[DrugTypeId] = ISNULL(@DrugTypeId,[DrugTypeId])		
			  ,[DrugSubTypeId] = ISNULL(@DrugSubTypeId,[DrugSubTypeId])		
			  ,[DrugStartDate] = ISNULL(@DrugStartDate,[DrugStartDate])		
			  ,[DrugEndDate] = ISNULL(@DrugEndDate,[DrugEndDate])		
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

