USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_MEDICATION_MANAGER]    Script Date: 4/29/2018 11:04:34 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_MEDICATION_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_MEDICATION_MANAGER]    Script Date: 4/29/2018 11:04:34 PM ******/
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

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @DrugChemicalId AS BIGINT, @DrugBrandId AS BIGINT, @DrugFrequencyId AS BIGINT, @DrugUnitId AS BIGINT
, @DrugTypeId AS BIGINT, @DrugSubTypeId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @DrugChemicalOtherDescription as NVARCHAR(MAX), @DrugBrandOtherDescription as NVARCHAR(MAX)
DECLARE @DrugDosage AS DECIMAL(10,2)
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
	   , @DrugChemicalOtherDescription = ConsultationMedicationList.Columns.value('DrugChemicalOtherDescription[1]', 'NVARCHAR(MAX)')
	   , @DrugBrandOtherDescription = ConsultationMedicationList.Columns.value('DrugBrandOtherDescription[1]', 'NVARCHAR(MAX)')
	   , @DrugDosage = ConsultationMedicationList.Columns.value('DrugDosage[1]', 'DECIMAL(10,2)')
	   , @DrugUnitId = ConsultationMedicationList.Columns.value('DrugUnitId[1]', 'BIGINT') 
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
		   ,[DrugUnitId]
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
		   ,@DrugChemicalOtherDescription
		   ,@DrugBrandId
		   ,@DrugBrandOtherDescription
		   ,@DrugDosage
		   ,@DrugUnitId
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
			  ,[DrugChemicalOtherDescription] = @DrugChemicalOtherDescription
			  ,[DrugBrandId] = ISNULL(@DrugBrandId,[DrugBrandId])				
			  ,[DrugBrandOtherDescription] = @DrugBrandOtherDescription
			  ,[DrugDosage] = ISNULL(@DrugDosage,[DrugDosage])	
			  ,[DrugUnitId] = ISNULL(@DrugUnitId,[DrugUnitId])	
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


