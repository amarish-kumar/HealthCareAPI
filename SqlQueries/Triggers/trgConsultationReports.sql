USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationReports]    Script Date: 4/8/2018 9:57:59 AM ******/
DROP TRIGGER [dbo].[trgConsultationReports]
GO

/****** Object:  Trigger [dbo].[trgConsultationReports]    Script Date: 4/8/2018 9:57:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Trigger [dbo].[trgConsultationReports] ON [dbo].[ConsultationReports] FOR INSERT,UPDATE AS  
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

INSERT INTO [dbo].[ConsultationReportsAudit]
           ([Action]
           ,[EntityId]
           ,[ConsultationId]
           ,[FileName]
           ,[FileData]
           ,[Description]
           ,[DoctorName]
           ,[DoctorPhoneNumber]
           ,[CountryId]
           ,[ReportDate]
           ,[LabName]
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
           ,[FileName]
           ,[FileData]
           ,[Description]
           ,[DoctorName]
           ,[DoctorPhoneNumber]
           ,[CountryId]
           ,[ReportDate]
           ,[LabName]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END



GO


