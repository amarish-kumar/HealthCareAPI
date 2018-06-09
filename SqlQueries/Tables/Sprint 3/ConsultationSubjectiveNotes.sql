USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationSubjectiveNotes] DROP CONSTRAINT [FK_ConsultationSubjectiveNotes_UserDetail_DoctorId]
GO

ALTER TABLE [dbo].[ConsultationSubjectiveNotes] DROP CONSTRAINT [FK_ConsultationSubjectiveNotes_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationSubjectiveNotes]    Script Date: 6/9/2018 9:46:51 AM ******/
DROP TABLE [dbo].[ConsultationSubjectiveNotes]
GO

/****** Object:  Table [dbo].[ConsultationSubjectiveNotes]    Script Date: 6/9/2018 9:46:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationSubjectiveNotes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationSubjectiveId] [bigint] NOT NULL,
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
 CONSTRAINT [PK_ConsultationSubjectiveNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationSubjectiveNotes]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationSubjectiveNotes_Consultation_Id] FOREIGN KEY([ConsultationSubjectiveId])
REFERENCES [dbo].[ConsultationSubjectives] ([Id])
GO

ALTER TABLE [dbo].[ConsultationSubjectiveNotes] CHECK CONSTRAINT [FK_ConsultationSubjectiveNotes_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationSubjectiveNotes]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationSubjectiveNotes_UserDetail_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[ConsultationSubjectiveNotes] CHECK CONSTRAINT [FK_ConsultationSubjectiveNotes_UserDetail_DoctorId]
GO


