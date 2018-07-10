USE [HealthCare]
GO

/****** Object:  Trigger [trgDoctorAwards]    Script Date: 7/10/2018 11:03:10 AM ******/
DROP TRIGGER [dbo].[trgDoctorAwards]
GO

/****** Object:  Trigger [dbo].[trgDoctorAwards]    Script Date: 7/10/2018 11:03:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgDoctorAwards] ON [dbo].[DoctorAwards] FOR INSERT,UPDATE AS  
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


	INSERT INTO [dbo].[DoctorAwardsAudit]
           ([Action]
           ,[EntityId]
           ,[DoctorId]
           ,[YearReceived]
           ,[InstitutionName]
           ,[AwardName]
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
           ,[YearReceived]
           ,[InstitutionName]
           ,[AwardName]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END






GO


