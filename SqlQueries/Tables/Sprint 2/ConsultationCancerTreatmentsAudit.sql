USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationCancerTreatmentsAudit]    Script Date: 3/12/2018 6:20:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationCancerTreatmentsAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[CancerType] [nvarchar](255) NULL,
	[CancerStageId] [bigint] NULL,
	[DignosisDate] [datetime] NULL,
	[TreatmentType] [nvarchar](255) NULL,
	[IsTreatmentOn] [bit] NULL,
	[TreatmentCompletionDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationCancerTreatmentsAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


