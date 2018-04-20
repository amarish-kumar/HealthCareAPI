USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationBloodPressureReading] DROP CONSTRAINT [FK_ConsultationBloodPressureReading_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationBloodPressureReading]    Script Date: 4/17/2018 10:46:04 AM ******/
DROP TABLE [dbo].[ConsultationBloodPressureReading]
GO

/****** Object:  Table [dbo].[ConsultationBloodPressureReading]    Script Date: 4/17/2018 10:46:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationBloodPressureReading](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[Systolic] [int] NOT NULL,
	[Diastolic] [int] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationBloodPressureReading] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationBloodPressureReading]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationBloodPressureReading_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationBloodPressureReading] CHECK CONSTRAINT [FK_ConsultationBloodPressureReading_Consultation_Id]
GO


