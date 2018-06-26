USE [HealthCare]
GO

ALTER TABLE [dbo].[Consultation] DROP CONSTRAINT [FK_Consultation_UserDetail_PatientId]
GO

ALTER TABLE [dbo].[Consultation] DROP CONSTRAINT [FK_Consultation_UserDetail_DoctorId]
GO

ALTER TABLE [dbo].[Consultation] DROP CONSTRAINT [FK_Consultation_Profile_Id]
GO

ALTER TABLE [dbo].[Consultation] DROP CONSTRAINT [FK_Consultation_PackageMaster_Id]
GO

ALTER TABLE [dbo].[Consultation] DROP CONSTRAINT [FK_Consultation_ConsultationStatus_ConsultationStatusId]
GO

/****** Object:  Table [dbo].[Consultation]    Script Date: 6/8/2018 10:18:50 AM ******/
DROP TABLE [dbo].[Consultation]
GO

/****** Object:  Table [dbo].[Consultation]    Script Date: 6/8/2018 10:18:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Consultation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[PatientId] [bigint] NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[ConsultationStatusId] [bigint] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[EnableDocReview] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
	[ProfileId] [bigint] NOT NULL,
	[PackageId] [bigint] NOT NULL,
 CONSTRAINT [PK_Complaint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Consultation]  WITH CHECK ADD  CONSTRAINT [FK_Consultation_ConsultationStatus_ConsultationStatusId] FOREIGN KEY([ConsultationStatusId])
REFERENCES [dbo].[ConsultationStatus] ([Id])
GO

ALTER TABLE [dbo].[Consultation] CHECK CONSTRAINT [FK_Consultation_ConsultationStatus_ConsultationStatusId]
GO

ALTER TABLE [dbo].[Consultation]  WITH CHECK ADD  CONSTRAINT [FK_Consultation_PackageMaster_Id] FOREIGN KEY([PackageId])
REFERENCES [dbo].[PackageMaster] ([ID])
GO

ALTER TABLE [dbo].[Consultation] CHECK CONSTRAINT [FK_Consultation_PackageMaster_Id]
GO

ALTER TABLE [dbo].[Consultation]  WITH CHECK ADD  CONSTRAINT [FK_Consultation_Profile_Id] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([Id])
GO

ALTER TABLE [dbo].[Consultation] CHECK CONSTRAINT [FK_Consultation_Profile_Id]
GO

ALTER TABLE [dbo].[Consultation]  WITH CHECK ADD  CONSTRAINT [FK_Consultation_UserDetail_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[Consultation] CHECK CONSTRAINT [FK_Consultation_UserDetail_DoctorId]
GO

ALTER TABLE [dbo].[Consultation]  WITH CHECK ADD  CONSTRAINT [FK_Consultation_UserDetail_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[Consultation] CHECK CONSTRAINT [FK_Consultation_UserDetail_PatientId]
GO


