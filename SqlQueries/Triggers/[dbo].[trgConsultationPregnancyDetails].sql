USE [HealthCare]
GO

/****** Object:  Trigger [trgConsultationPregnancyDetails]    Script Date: 5/1/2018 7:33:10 PM ******/
DROP TRIGGER [dbo].[trgConsultationPregnancyDetails]
GO

/****** Object:  Trigger [dbo].[trgConsultationPregnancyDetails]    Script Date: 5/1/2018 7:33:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Trigger [dbo].[trgConsultationPregnancyDetails] ON [dbo].[ConsultationPregnancyDetails] FOR INSERT,UPDATE AS  
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

INSERT INTO [dbo].[ConsultationPregnancyDetailsAudit]
           (
			[Action],
			[EntityId],
			[ConsultationId],
			[CurrentlyPregnant],
			[CurrentPregnancyMonths],
			[CurrentPregnancyEDD],
			[PregnantBefore],
			[MenstrualCycles],
			[NoMCReason],
			[LastMCCycle],
			[MCRegInterval],
			[LenMCCycle],
			[MCStartAge],
			[MCFlow],
			[MCProductType],
			[MCProductPerDay],
			[MCPain],
			[MCPainSeverity],
			[MCSymptomID],
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
			[CurrentlyPregnant],
			[CurrentPregnancyMonths],
			[CurrentPregnancyEDD],
			[PregnantBefore],
			[MenstrualCycles],
			[NoMCReason],
			[LastMCCycle],
			[MCRegInterval],
			[LenMCCycle],
			[MCStartAge],
			[MCFlow],
			[MCProductType],
			[MCProductPerDay],
			[MCPain],
			[MCPainSeverity],
			[MCSymptomID],
			[Active],
			[AddedBy],
			[AddedDate],
			[ModifiedBy],
			[ModifiedDate],
			[DeletedBy],
			[DeletedDate] FROM Inserted

END





GO


