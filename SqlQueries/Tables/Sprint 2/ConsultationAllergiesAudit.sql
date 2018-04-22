USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationAllergiesAudit]    Script Date: 4/22/2018 10:04:53 AM ******/
DROP TABLE [dbo].[ConsultationAllergiesAudit]
GO

/****** Object:  Table [dbo].[ConsultationAllergiesAudit]    Script Date: 4/22/2018 10:04:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationAllergiesAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[AllergyId] [bigint] NOT NULL,
	[OtherDescription] [nvarchar](255) NULL,
	[AllergyStartDate] [datetime] NULL,
	[Treatment] [nvarchar](1000) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationAllergiesAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


