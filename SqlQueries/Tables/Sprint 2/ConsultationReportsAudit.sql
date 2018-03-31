USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationReports]    Script Date: 3/12/2018 5:51:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConsultationReportsAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] nvarchar(10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
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
 CONSTRAINT [PK_ConsultationReportsAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
