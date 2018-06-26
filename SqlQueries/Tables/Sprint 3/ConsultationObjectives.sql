USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationObjectives] DROP CONSTRAINT [FK_ConsultationObjectives_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationObjectives]    Script Date: 6/9/2018 10:07:12 AM ******/
DROP TABLE [dbo].[ConsultationObjectives]
GO

/****** Object:  Table [dbo].[ConsultationObjectives]    Script Date: 6/9/2018 10:07:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationObjectives](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[Vitals] [nvarchar](max) NULL,
	[LabResults] [nvarchar](max) NULL,
	[RadioGraphicResults] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationObjectives] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationObjectives]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationObjectives_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationObjectives] CHECK CONSTRAINT [FK_ConsultationObjectives_Consultation_Id]
GO


