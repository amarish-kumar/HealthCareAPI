USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationAssesments] DROP CONSTRAINT [FK_ConsultationAssesments_UserDetail_DiffDiagnosisDoctorId]
GO

ALTER TABLE [dbo].[ConsultationAssesments] DROP CONSTRAINT [FK_ConsultationAssesments_UserDetail_DiagnosisDoctorId]
GO

ALTER TABLE [dbo].[ConsultationAssesments] DROP CONSTRAINT [FK_ConsultationAssesments_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationAssesments]    Script Date: 6/9/2018 10:15:49 AM ******/
DROP TABLE [dbo].[ConsultationAssesments]
GO

/****** Object:  Table [dbo].[ConsultationAssesments]    Script Date: 6/9/2018 10:15:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationAssesments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[DiagnosisTimestamp] [datetime] NULL,
	[DiagnosisNotes] [nvarchar](max) NULL,
	[DiagnosisDoctorId] [bigint] NOT NULL,
	[DiffDiagnosisTimestamp] [datetime] NULL,
	[DiffDiagnosisNotes] [nvarchar](max) NULL,
	[DiffDiagnosisDoctorId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationAssesments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationAssesments]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationAssesments_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationAssesments] CHECK CONSTRAINT [FK_ConsultationAssesments_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationAssesments]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationAssesments_UserDetail_DiagnosisDoctorId] FOREIGN KEY([DiagnosisDoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[ConsultationAssesments] CHECK CONSTRAINT [FK_ConsultationAssesments_UserDetail_DiagnosisDoctorId]
GO

ALTER TABLE [dbo].[ConsultationAssesments]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationAssesments_UserDetail_DiffDiagnosisDoctorId] FOREIGN KEY([DiffDiagnosisDoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[ConsultationAssesments] CHECK CONSTRAINT [FK_ConsultationAssesments_UserDetail_DiffDiagnosisDoctorId]
GO


