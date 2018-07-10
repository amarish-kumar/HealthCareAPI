USE [HealthCare]
GO

/****** Object:  Trigger [trgDoctorResidency]    Script Date: 7/10/2018 11:08:18 AM ******/
DROP TRIGGER [dbo].[trgDoctorResidency]
GO

/****** Object:  Trigger [dbo].[trgDoctorResidency]    Script Date: 7/10/2018 11:08:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgDoctorResidency] ON [dbo].[DoctorResidency] FOR INSERT,UPDATE AS  
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
	
	INSERT INTO [dbo].[DoctorResidencyAudit]
           ([Action]
           ,[EntityId]
           ,[DoctorId]
           ,[BeginingYear]
           ,[EndingYear]
           ,[HospitalName]
           ,[City]
           ,[StateId]
           ,[CountryId]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate])
               
    SELECT @ACTION
           ,[Id]
           ,[DoctorId]
           ,[BeginingYear]
           ,[EndingYear]
           ,[HospitalName]
           ,[City]
           ,[StateId]
           ,[CountryId]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END






GO


