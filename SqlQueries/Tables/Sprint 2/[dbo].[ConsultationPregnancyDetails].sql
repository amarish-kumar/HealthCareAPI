USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationPregnancyDetails]    Script Date: 3/23/2018 11:59:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationPregnancyDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[CurrentlyPregnant] [bit] NULL,
	[CurrentPregnancyMonths] [bigint] NULL,
	[CurrentPregnancyEDD] [bigint] NULL,
	[PregnantBefore] [bit] NULL,
	[MenstrualCycles] [bit] NULL,
	[NoMCReason] [nvarchar](500) NULL,
	[LastMCCycle] [datetime] NULL,
	[LenMCCycle] [bigint] NULL,
	[MCStartAge] [bigint] NULL,
	[MCFlow] [nvarchar](20) NULL,
	[MCProductType] [nvarchar](20) NULL,
	[MCProductPerDay] [bigint] NULL,
	[MCPain] [bit] NULL,
	[MCPainSeverity] [bigint] NULL,
	[MCSymptomID] [nvarchar](20) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationPregnancyDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[ConsultationPregnancyDetails]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationPregnancyDetails_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationPregnancyDetails] CHECK CONSTRAINT [FK_ConsultationPregnancyDetails_Consultation_Id]
GO
