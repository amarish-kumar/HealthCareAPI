USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationObjectiveNotes] DROP CONSTRAINT [FK_ConsultationObjectiveNotes_UserDetail_DoctorId]
GO

ALTER TABLE [dbo].[ConsultationObjectiveNotes] DROP CONSTRAINT [FK_ConsultationObjectiveNotes_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationObjectiveNotes]    Script Date: 6/9/2018 10:08:33 AM ******/
DROP TABLE [dbo].[ConsultationObjectiveNotes]
GO

/****** Object:  Table [dbo].[ConsultationObjectiveNotes]    Script Date: 6/9/2018 10:08:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationObjectiveNotes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationObjectiveId] [bigint] NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[Timestamp] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationObjectiveNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationObjectiveNotes]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationObjectiveNotes_Consultation_Id] FOREIGN KEY([ConsultationObjectiveId])
REFERENCES [dbo].[ConsultationObjectives] ([Id])
GO

ALTER TABLE [dbo].[ConsultationObjectiveNotes] CHECK CONSTRAINT [FK_ConsultationObjectiveNotes_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationObjectiveNotes]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationObjectiveNotes_UserDetail_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[ConsultationObjectiveNotes] CHECK CONSTRAINT [FK_ConsultationObjectiveNotes_UserDetail_DoctorId]
GO


