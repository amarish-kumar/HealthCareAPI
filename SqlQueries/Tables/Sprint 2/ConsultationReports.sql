USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationReports]    Script Date: 3/12/2018 5:49:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConsultationReports](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[FileName] [nvarchar](300) NULL,
	[FileData] [varbinary](max) NULL,
	[Description] [nvarchar](max) NULL,
	[DoctorName] [nvarchar](300) NULL,
	[DoctorPhoneNumber] [nvarchar](20) NULL,
	[CountryId] [bigint] NOT NULL,
	[ReportDate] [datetime] NOT NULL,
	[LabName] [nvarchar](300) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationReports] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ConsultationReports]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationReports_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationReports] CHECK CONSTRAINT [FK_ConsultationReports_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationReports]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationReports_Country_Id] FOREIGN KEY([CountryId])
REFERENCES [dbo].[CountryMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationReports] CHECK CONSTRAINT [FK_ConsultationReports_Country_Id]
GO


