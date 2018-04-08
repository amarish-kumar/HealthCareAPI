USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationSurgeries]    Script Date: 4/8/2018 11:26:25 AM ******/
DROP TRIGGER [dbo].[trgConsultationSurgeries]
GO

/****** Object:  Trigger [dbo].[trgConsultationSurgeries]    Script Date: 4/8/2018 11:26:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgConsultationSurgeries] ON [dbo].[ConsultationSurgeries] FOR INSERT,UPDATE AS  
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

INSERT INTO [dbo].[ConsultationSurgeriesAudit]
           ([Action]
           ,[EntityId]
           ,[ConsultationId]
           ,[SurgeryId]
           ,[SurgeryDate]
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
           ,[SurgeryId]
           ,[SurgeryDate]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END




GO


