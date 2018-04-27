USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationBloodPressureReadingAudit]    Script Date: 4/17/2018 10:46:48 AM ******/
DROP TRIGGER [dbo].[trgConsultationBloodPressureReadingAudit]
GO

/****** Object:  Trigger [dbo].[trgConsultationBloodPressureReadingAudit]    Script Date: 4/17/2018 10:46:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgConsultationBloodPressureReadingAudit] ON [dbo].[ConsultationBloodPressureReading] FOR INSERT,UPDATE AS  
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

	INSERT INTO [dbo].[ConsultationBloodPressureReadingAudit]
           ([Action]
           ,[EntityId]
           ,[ConsultationId]
           ,[Systolic]
           ,[Diastolic]
           ,[Timestamp]
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
           ,[Systolic]
           ,[Diastolic]
           ,[Timestamp]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END







GO


