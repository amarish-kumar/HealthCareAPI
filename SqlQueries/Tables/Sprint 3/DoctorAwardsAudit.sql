USE [HealthCare]
GO

/****** Object:  Table [dbo].[DoctorAwardsAudit]    Script Date: 6/6/2018 10:05:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DoctorAwardsAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[YearReceived] [int] NOT NULL,
	[InstitutionName] [nvarchar](255) NULL,
	[AwardName] [nvarchar](400) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorAwardsAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


