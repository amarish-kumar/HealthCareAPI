USE [HealthCare]
GO

ALTER TABLE [dbo].[ConsultationSubjectives] DROP CONSTRAINT [FK_ConsultationSubjectives_Consultation_Id]
GO

/****** Object:  Table [dbo].[ConsultationSubjectives]    Script Date: 6/9/2018 9:36:53 AM ******/
DROP TABLE [dbo].[ConsultationSubjectives]
GO

/****** Object:  Table [dbo].[ConsultationSubjectives]    Script Date: 6/9/2018 9:36:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationSubjectives](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[Onset] [nvarchar](max) NULL,
	[Duration] [nvarchar](500) NULL,
	[Location] [nvarchar](500) NULL,
	[Character] [nvarchar](500) NULL,
	[AlleviatingFactors] [nvarchar](max) NULL,
	[AggravatingFactors] [nvarchar](max) NULL,
	[Radiation] [nvarchar](max) NULL,
	[TemporalPattern] [nvarchar](max) NULL,
	[Severity] [nvarchar](500) NULL,
	[Chronology] [nvarchar](max) NULL,
	[AdditionalSymptoms] [nvarchar](max) NULL,
	[allergies] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationSubjectives] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationSubjectives]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationSubjectives_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationSubjectives] CHECK CONSTRAINT [FK_ConsultationSubjectives_Consultation_Id]
GO


