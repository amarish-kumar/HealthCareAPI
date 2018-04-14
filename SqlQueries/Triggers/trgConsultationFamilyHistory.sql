USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationFamilyHistory]    Script Date: 4/9/2018 9:52:19 PM ******/
DROP TRIGGER [dbo].[trgConsultationFamilyHistory]
GO

/****** Object:  Trigger [dbo].[trgConsultationFamilyHistory]    Script Date: 4/9/2018 9:52:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgConsultationFamilyHistory] ON [dbo].[ConsultationFamilyHistory] FOR INSERT,UPDATE AS  
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

	INSERT INTO [dbo].[ConsultationFamilyHistoryAudit]
           ([Action]
           ,[EntityId]
           ,[ConsultationId]
           ,[RelationshipId]
           ,[HealthConditionId]
           ,[CurrentAge]
           ,[AgeOnConditionStart]
           ,[IsAlive]
           ,[CAuseOfDeath]
           ,[AgeOnDeath]
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
           ,[RelationshipId]
           ,[HealthConditionId]
           ,[CurrentAge]
           ,[AgeOnConditionStart]
           ,[IsAlive]
           ,[CAuseOfDeath]
           ,[AgeOnDeath]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END





GO


