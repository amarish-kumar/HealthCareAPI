USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationMedications]    Script Date: 4/25/2018 12:24:30 PM ******/
DROP TRIGGER [dbo].[trgConsultationMedications]
GO

/****** Object:  Trigger [dbo].[trgConsultationMedications]    Script Date: 4/25/2018 12:24:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE Trigger [dbo].[trgConsultationMedications] ON [dbo].[ConsultationMedications] FOR INSERT,UPDATE AS  
BEGIN  
DECLARE @ACTION AS NVARCHAR(10)

 IF NOT EXISTS(SELECT * FROM INSERTED)
       SET @ACTION = 'Delete';
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
            SET @ACTION = 'Insert';
        ELSE
            SET @ACTION = 'Update';
    END


	INSERT INTO [dbo].[ConsultationMedicationsAudit]
           ([Action]
           ,[EntityId]
           ,[ConsultationId]
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
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate])     

    SELECT @ACTION
           ,[Id]
           ,[ConsultationId]
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
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END







GO


