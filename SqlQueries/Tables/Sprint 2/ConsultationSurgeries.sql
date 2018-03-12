USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationSurgeries]    Script Date: 3/12/2018 6:20:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationSurgeries](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[SurgeryId] [bigint] NOT NULL,
	[SurgeryDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationSurgeries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationSurgeries]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationSurgeries_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationSurgeries] CHECK CONSTRAINT [FK_ConsultationSurgeries_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationSurgeries]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationSurgeries_SurgeryMaster_Id] FOREIGN KEY([SurgeryId])
REFERENCES [dbo].[SurgeryMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationSurgeries] CHECK CONSTRAINT [FK_ConsultationSurgeries_SurgeryMaster_Id]
GO

