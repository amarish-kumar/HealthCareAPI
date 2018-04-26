USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationMedications] DROP CONSTRAINT [FK_ConsultationMedications_DrugChemicalMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationMedications] DROP CONSTRAINT [FK_ConsultationMedications_DrugBrandMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationMedications] DROP CONSTRAINT [FK_ConsultationMedications_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationMedications]    Script Date: 4/25/2018 12:19:19 PM ******/
DROP TABLE [dbo].[ConsultationMedications]
GO

/****** Object:  Table [dbo].[ConsultationMedications]    Script Date: 4/25/2018 12:19:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationMedications](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[DrugChemicalId] [bigint] NOT NULL,
	[DrugChemicalOtherDescription] [nvarchar](255) NULL,
	[DrugBrandId] [bigint] NOT NULL,
	[DrugBrandOtherDescription] [nvarchar](255) NULL,
	[DrugDosage] [decimal](10, 2) NULL,
	[DrugFrequencyId] [bigint] NOT NULL,
	[DrugTypeId] [bigint] NOT NULL,
	[DrugSubTypeId] [bigint] NOT NULL,
	[DrugStartDate] [datetime] NULL,
	[DrugEndDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationMedications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationMedications]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationMedications_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationMedications] CHECK CONSTRAINT [FK_ConsultationMedications_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationMedications]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationMedications_DrugBrandMaster_Id] FOREIGN KEY([DrugBrandId])
REFERENCES [dbo].[DrugBrandMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationMedications] CHECK CONSTRAINT [FK_ConsultationMedications_DrugBrandMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationMedications]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationMedications_DrugChemicalMaster_Id] FOREIGN KEY([DrugChemicalId])
REFERENCES [dbo].[DrugChemicalMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationMedications] CHECK CONSTRAINT [FK_ConsultationMedications_DrugChemicalMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationMedications]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationMedications_DrugFrequencyMaster_Id] FOREIGN KEY([DrugFrequencyId])
REFERENCES [dbo].[DrugFrequencyMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationMedications] CHECK CONSTRAINT [FK_ConsultationMedications_DrugFrequencyMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationMedications]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationMedications_DrugTypeMaster_Id] FOREIGN KEY([DrugTypeId])
REFERENCES [dbo].[DrugTypeMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationMedications] CHECK CONSTRAINT [FK_ConsultationMedications_DrugTypeMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationMedications]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationMedications_DrugSubTypeMaster_Id] FOREIGN KEY([DrugSubTypeId])
REFERENCES [dbo].[DrugSubTypeMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationMedications] CHECK CONSTRAINT [FK_ConsultationMedications_DrugSubTypeMaster_Id]
GO
