USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationIllegaldrugsAudit]    Script Date: 4/16/2018 2:12:14 AM ******/
DROP TABLE [dbo].[ConsultationIllegaldrugsAudit]
GO

/****** Object:  Table [dbo].[ConsultationIllegaldrugsAudit]    Script Date: 4/16/2018 2:12:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationIllegaldrugsAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[Consumedrugs] [bit] NULL,
	[IllegalDrugsID] [bigint] NULL,
	[Frequency] [nvarchar](255) NULL,
	[PerFrequency] [bigint] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationIllegaldrugsAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


