USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationOccupation]    Script Date: 4/17/2018 10:03:31 PM ******/
DROP TRIGGER [dbo].[trgConsultationOccupation]
GO

/****** Object:  Trigger [dbo].[trgConsultationOccupation]    Script Date: 4/17/2018 10:03:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Trigger [dbo].[trgConsultationOccupation] ON [dbo].[ConsultationOccupation] FOR INSERT,UPDATE AS  
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

	INSERT INTO [dbo].[ConsultationOccupationAudit]
           ([Action]
           ,[EntityId]
           ,[ConsultationId]
           ,[OccupationId]
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
           ,[OccupationId]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END


GO


