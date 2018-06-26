USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] DROP CONSTRAINT [FK_ConsultationStatusesLog_UserDetail_DoctorId]
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] DROP CONSTRAINT [FK_ConsultationStatusesLog_StatusChangeReasonMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] DROP CONSTRAINT [FK_ConsultationStatusesLog_NewConsultationStatus_Id]
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] DROP CONSTRAINT [FK_ConsultationStatusesLog_ConsultationStatus_Id]
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] DROP CONSTRAINT [FK_ConsultationStatusesLog_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationStatusesLog]    Script Date: 6/9/2018 10:48:17 AM ******/
DROP TABLE [dbo].[ConsultationStatusesLog]
GO

/****** Object:  Table [dbo].[ConsultationStatusesLog]    Script Date: 6/9/2018 10:48:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationStatusesLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[OldConsultationStatusId] [bigint] NULL,
	[ConsultationStatusId] [bigint] NOT NULL,
	[StatusChangeReasonId] [bigint] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[DoctorId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationStatusesLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationStatusesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationStatusesLog_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] CHECK CONSTRAINT [FK_ConsultationStatusesLog_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationStatusesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationStatusesLog_ConsultationStatus_Id] FOREIGN KEY([OldConsultationStatusId])
REFERENCES [dbo].[ConsultationStatus] ([Id])
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] CHECK CONSTRAINT [FK_ConsultationStatusesLog_ConsultationStatus_Id]
GO

ALTER TABLE [dbo].[ConsultationStatusesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationStatusesLog_NewConsultationStatus_Id] FOREIGN KEY([ConsultationStatusId])
REFERENCES [dbo].[ConsultationStatus] ([Id])
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] CHECK CONSTRAINT [FK_ConsultationStatusesLog_NewConsultationStatus_Id]
GO

ALTER TABLE [dbo].[ConsultationStatusesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationStatusesLog_StatusChangeReasonMaster_Id] FOREIGN KEY([StatusChangeReasonId])
REFERENCES [dbo].[StatusChangeReasonMaster] ([Id])
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] CHECK CONSTRAINT [FK_ConsultationStatusesLog_StatusChangeReasonMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationStatusesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationStatusesLog_UserDetail_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[ConsultationStatusesLog] CHECK CONSTRAINT [FK_ConsultationStatusesLog_UserDetail_DoctorId]
GO


