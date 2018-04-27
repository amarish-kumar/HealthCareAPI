USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationIllegalDrugs]    Script Date: 4/15/2018 7:33:10 PM ******/
DROP TRIGGER [dbo].[trgConsultationIllegalDrugs]
GO

/****** Object:  Trigger [dbo].[trgConsultationIllegalDrugs]    Script Date: 4/15/2018 7:33:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Trigger [dbo].[trgConsultationIllegalDrugs] ON [dbo].[ConsultationIllegalDrugs] FOR INSERT,UPDATE AS  
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

INSERT INTO [dbo].[ConsultationIllegaldrugsAudit]
           ([Action]
           ,[EntityId]
           ,[ConsultationId]
           ,[Consumedrugs]
           ,[IllegalDrugsID]
		   ,[OtherDescription]
           ,[Frequency]
           ,[PerFrequency]
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
           ,[Consumedrugs]
           ,[IllegalDrugsID]
		   ,[OtherDescription]
           ,[Frequency]
           ,[PerFrequency]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END





GO


