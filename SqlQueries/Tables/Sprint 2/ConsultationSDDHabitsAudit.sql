USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationSDDHabitsAudit]    Script Date: 3/12/2018 6:47:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationSDDHabitsAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[ConsumeAlcohol] [bit] NULL,
	[AlcoholConsumptionFreq] [nvarchar](15) NULL,
	[DrinksPerDay] [int] NULL,
	[DrinksPerWeek] [int] NULL,
	[DoSmoke] [bit] NOT NULL,
	[EverSmoked] [bit] NOT NULL,
	[YearOfQuittingSmoking] [int] NULL,
	[SmokingFreq] [int] NULL,
	[ConsumeDrugs] [bit] NULL,
	[IllegalDrugsId] [bigint] NULL,
	[DrugsConsumptionFreq] [nvarchar](15) NULL,
	[DrugsPerDay] [int] NULL,
	[DrugsPerWeek] [int] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationSDDHabitsAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



