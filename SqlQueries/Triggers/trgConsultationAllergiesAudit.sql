USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationAllergiesAudit]    Script Date: 4/9/2018 10:07:24 PM ******/
DROP TRIGGER [dbo].[trgConsultationAllergiesAudit]
GO

/****** Object:  Trigger [dbo].[trgConsultationAllergiesAudit]    Script Date: 4/9/2018 10:07:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgConsultationAllergiesAudit] ON [dbo].[ConsultationAllergies] FOR INSERT,UPDATE AS  
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

	INSERT INTO [dbo].[ConsultationAllergiesAudit]
           ([Action]
           ,[EntityId]
           ,[ConsultationId]
           ,[AllergyId]
           ,[AllergyStartDate]
           ,[Treatment]
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
           ,[AllergyId]
           ,[AllergyStartDate]
           ,[Treatment]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END





GO


