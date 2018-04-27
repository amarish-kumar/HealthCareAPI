USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationMedicationsAudit]    Script Date: 4/25/2018 12:22:08 PM ******/
DROP TABLE [dbo].[ConsultationMedicationsAudit]
GO

/****** Object:  Table [dbo].[ConsultationMedicationsAudit]    Script Date: 4/25/2018 12:22:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationMedicationsAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[DrugChemicalId] [bigint] NOT NULL,
	[DrugChemicalOtherDescription] [nvarchar](255) NULL,
	[DrugBrandId] [bigint] NOT NULL,
	[DrugBrandOtherDescription] [nvarchar](255) NULL,
	[DrugDosage] [decimal](10, 2) NULL,
	[DrugFrequencyId] [bigint] NOT NULL,
	[DrugTypeId] [bigint] NOT NULL,
	[DrugSubTypeId] [bigint] NOT NULL,
	[DrugStartDate] [datetime] NULL,
	[DrugEndDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationMedicationsAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


