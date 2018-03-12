USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationCancerTreatments]    Script Date: 3/12/2018 6:20:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationCancerTreatments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[CancerType] [nvarchar](255) NULL,
	[CancerStageId] [bigint] NULL,
	[DignosisDate] [datetime] NULL,
	[TreatmentType] [nvarchar](255) NULL,
	[IsTreatmentOn] [bit] NULL,
	[TreatmentCompletionDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationCancerTreatments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationCancerTreatments]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationCancerTreatments_CancerStageMaster_Id] FOREIGN KEY([CancerStageId])
REFERENCES [dbo].[CancerStageMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationCancerTreatments] CHECK CONSTRAINT [FK_ConsultationCancerTreatments_CancerStageMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationCancerTreatments]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationCancerTreatments_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationCancerTreatments] CHECK CONSTRAINT [FK_ConsultationCancerTreatments_Consultation_Id]
GO


