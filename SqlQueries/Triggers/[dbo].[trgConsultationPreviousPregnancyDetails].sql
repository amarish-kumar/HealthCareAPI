USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationPreviousPregnancyDetails]    Script Date: 5/1/2018 7:33:10 PM ******/
DROP TRIGGER [dbo].[trgConsultationPreviousPregnancyDetails]
GO

/****** Object:  Trigger [dbo].[trgConsultationPreviousPregnancyDetails]    Script Date: 5/1/2018 7:33:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Trigger [dbo].[trgConsultationPreviousPregnancyDetails] ON [dbo].[ConsultationPreviousPregnancyDetails] FOR INSERT,UPDATE AS  
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

INSERT INTO [dbo].[ConsultationPreviousPregnancyDetailsAudit]
           (
			[Action],
			[EntityId],
			[ConsultationId],
			[CurrentPregnancyID],
			[NoofPregnancy],
			[ChildNo],
			[DeliveryYear],
			[DeliveryType],
			[Active],
			[AddedBy],
			[AddedDate],
			[ModifiedBy],
			[ModifiedDate],
			[DeletedBy],
			[DeletedDate]
		   )

    SELECT @ACTION
           ,[Id]
           ,[ConsultationId],
			[CurrentPregnancyID],
			[NoofPregnancy],
			[ChildNo],
			[DeliveryYear],
			[DeliveryType],
			[Active],
			[AddedBy],
			[AddedDate],
			[ModifiedBy],
			[ModifiedDate],
			[DeletedBy],
			[DeletedDate] FROM Inserted

END





GO


