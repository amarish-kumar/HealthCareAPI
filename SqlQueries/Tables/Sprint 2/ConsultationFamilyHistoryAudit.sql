USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationFamilyHistoryAudit]    Script Date: 4/15/2018 2:15:43 PM ******/
DROP TABLE [dbo].[ConsultationFamilyHistoryAudit]
GO

/****** Object:  Table [dbo].[ConsultationFamilyHistoryAudit]    Script Date: 4/15/2018 2:15:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationFamilyHistoryAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[RelationshipId] [bigint] NOT NULL,
	[HealthConditionId] [bigint] NOT NULL,
	[CurrentAge] [int] NULL,
	[ConditionStartDate] datetime NULL,
	[AgeOnConditionStart] [int] NULL,
	[IsAlive] [bit] NOT NULL,
	[CAuseOfDeath] [nvarchar](255) NULL,
	[AgeOnDeath] [int] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationFamilyHistoryAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


