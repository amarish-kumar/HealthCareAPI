USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationPlans] DROP CONSTRAINT [FK_ConsultationPlans_UserDetail_DoctorId]
GO

ALTER TABLE [dbo].[ConsultationPlans] DROP CONSTRAINT [FK_ConsultationPlans_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationPlans]    Script Date: 6/9/2018 10:19:09 AM ******/
DROP TABLE [dbo].[ConsultationPlans]
GO

/****** Object:  Table [dbo].[ConsultationPlans]    Script Date: 6/9/2018 10:19:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationPlans](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[Timestamp] [datetime] NULL,
	[DoctorId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationPlans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationPlans]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationPlans_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationPlans] CHECK CONSTRAINT [FK_ConsultationPlans_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationPlans]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationPlans_UserDetail_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[ConsultationPlans] CHECK CONSTRAINT [FK_ConsultationPlans_UserDetail_DoctorId]
GO


