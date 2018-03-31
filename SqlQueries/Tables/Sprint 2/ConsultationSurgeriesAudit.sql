USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationSurgeriesAudit]    Script Date: 3/12/2018 6:21:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationSurgeriesAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[SurgeryId] [bigint] NOT NULL,
	[SurgeryDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationSurgeriesAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


