USE [HealthCare]
GO

/****** Object:  Trigger [trgDoctorBoard]    Script Date: 7/10/2018 11:03:38 AM ******/
DROP TRIGGER [dbo].[trgDoctorBoard]
GO

/****** Object:  Trigger [dbo].[trgDoctorBoard]    Script Date: 7/10/2018 11:03:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgDoctorBoard] ON [dbo].[DoctorBoard] FOR INSERT,UPDATE AS  
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

	INSERT INTO [dbo].[DoctorBoardAudit]
           ([Action]
           ,[EntityId]
           ,[DoctorId]
           ,[BoardId]
           ,[OtherDescription]
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
           ,[BoardId]
           ,[OtherDescription]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END






GO


