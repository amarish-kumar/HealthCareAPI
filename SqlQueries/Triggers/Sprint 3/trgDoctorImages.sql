USE [HealthCare]
GO

/****** Object:  Trigger [trgDoctorImages]    Script Date: 7/10/2018 11:07:59 AM ******/
DROP TRIGGER [dbo].[trgDoctorImages]
GO

/****** Object:  Trigger [dbo].[trgDoctorImages]    Script Date: 7/10/2018 11:07:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgDoctorImages] ON [dbo].[DoctorImages] FOR INSERT,UPDATE AS  
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
	
	INSERT INTO [dbo].[DoctorImagesAudit]
           ([Action]
           ,[EntityId]
           ,[DoctorId]
           ,[IsPrimary]
           ,[FileName]
           ,[FileData]
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
           ,[IsPrimary]
           ,[FileName]
           ,[FileData]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END






GO


