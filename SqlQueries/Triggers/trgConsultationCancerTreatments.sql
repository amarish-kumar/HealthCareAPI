USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationCancerTreatments]    Script Date: 4/8/2018 11:25:59 AM ******/
DROP TRIGGER [dbo].[trgConsultationCancerTreatments]
GO

/****** Object:  Trigger [dbo].[trgConsultationCancerTreatments]    Script Date: 4/8/2018 11:25:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgConsultationCancerTreatments] ON [dbo].[ConsultationCancerTreatments] FOR INSERT,UPDATE AS  
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

INSERT INTO [dbo].[ConsultationCancerTreatmentsAudit]
           ([Action]
           ,[EntityId]
           ,[ConsultationId]
           ,[CancerType]
           ,[CancerStageId]
           ,[DignosisDate]
           ,[TreatmentType]
           ,[IsTreatmentOn]
           ,[TreatmentCompletionDate]
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
           ,[CancerType]
           ,[CancerStageId]
           ,[DignosisDate]
           ,[TreatmentType]
           ,[IsTreatmentOn]
           ,[TreatmentCompletionDate]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END




GO


