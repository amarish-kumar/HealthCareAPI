USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationOccupation] DROP CONSTRAINT [FK_ConsultationOccupation_OccupationMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationOccupation] DROP CONSTRAINT [FK_ConsultationOccupation_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationOccupation]    Script Date: 4/22/2018 10:08:56 AM ******/
DROP TABLE [dbo].[ConsultationOccupation]
GO

/****** Object:  Table [dbo].[ConsultationOccupation]    Script Date: 4/22/2018 10:08:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationOccupation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[OccupationId] [bigint] NOT NULL,
	[OtherDescription] [nvarchar](255) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationOccupation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationOccupation]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationOccupation_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationOccupation] CHECK CONSTRAINT [FK_ConsultationOccupation_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationOccupation]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationOccupation_OccupationMaster_Id] FOREIGN KEY([OccupationId])
REFERENCES [dbo].[OccupationMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationOccupation] CHECK CONSTRAINT [FK_ConsultationOccupation_OccupationMaster_Id]
GO


