USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationAllergies]    Script Date: 3/12/2018 6:56:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationAllergies](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[AllergyId] [bigint] NOT NULL,
	[AllergyStartDate] [datetime] NULL,
	[Treatment] nvarchar(1000) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationAllergies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationAllergies]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationAllergies_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationAllergies] CHECK CONSTRAINT [FK_ConsultationAllergies_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationAllergies]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationAllergies_AllergyMaster_Id] FOREIGN KEY([AllergyId])
REFERENCES [dbo].[AllergyMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationAllergies] CHECK CONSTRAINT [FK_ConsultationAllergies_AllergyMaster_Id]
GO


