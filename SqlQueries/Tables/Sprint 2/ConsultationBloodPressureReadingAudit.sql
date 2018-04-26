USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationBloodPressureReadingAudit]    Script Date: 4/17/2018 10:46:29 AM ******/
DROP TABLE [dbo].[ConsultationBloodPressureReadingAudit]
GO

/****** Object:  Table [dbo].[ConsultationBloodPressureReadingAudit]    Script Date: 4/17/2018 10:46:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationBloodPressureReadingAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
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
 CONSTRAINT [PK_ConsultationBloodPressureReadingAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

