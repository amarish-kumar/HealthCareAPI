USE [HealthCare]
GO

/****** Object:  Trigger [trgDoctorFellowship]    Script Date: 7/10/2018 11:07:42 AM ******/
DROP TRIGGER [dbo].[trgDoctorFellowship]
GO

/****** Object:  Trigger [dbo].[trgDoctorFellowship]    Script Date: 7/10/2018 11:07:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgDoctorFellowship] ON [dbo].[DoctorFellowship] FOR INSERT,UPDATE AS  
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

	INSERT INTO [dbo].[DoctorFellowshipAudit]
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


