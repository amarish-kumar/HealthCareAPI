USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationSDDHABITS]    Script Date: 4/16/2018 12:24:00 AM ******/
DROP TRIGGER [dbo].[trgConsultationSDDHABITS]
GO

/****** Object:  Trigger [dbo].[trgConsultationSDDHABITS]    Script Date: 4/16/2018 12:24:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE Trigger [dbo].[trgConsultationSDDHABITS] ON [dbo].[ConsultationSDDHABITS] FOR INSERT,UPDATE AS  
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

INSERT INTO [dbo].[ConsultationSDDHABITSAudit]
           ([Action]
           ,[EntityId]
		   ,[ConsultationId]
           ,[DoSmoke]
           ,[EverSmoked]
           ,[YearOfQuittingSmoking]
           ,[SmokingFreq]
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
           ,[DoSmoke]
           ,[EverSmoked]
           ,[YearOfQuittingSmoking]
           ,[SmokingFreq]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END






GO


