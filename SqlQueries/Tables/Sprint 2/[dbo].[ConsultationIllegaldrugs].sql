USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationIllegaldrugs]    Script Date: 3/23/2018 11:59:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationIllegaldrugs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[Consumedrugs] [bit] NULL,
	[IllegalDrugsID] [bigint] NULL,
	[Frequency] [nvarchar](255) NULL,
	[PerDay] [bigint] NULL,
	[PerWeek] [bigint] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationIllegaldrugs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationIllegaldrugs]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationIllegaldrugs_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationIllegaldrugs] CHECK CONSTRAINT [FK_ConsultationIllegaldrugs_Consultation_Id]
GO


ALTER TABLE [dbo].[ConsultationIllegaldrugs]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationIllegaldrugs_IllegalDrugsID] FOREIGN KEY([IllegalDrugsID])
REFERENCES [dbo].[IllegalDrugsMaster] ([Id])
GO

ALTER TABLE [dbo].[ConsultationIllegaldrugs] CHECK CONSTRAINT [FK_ConsultationIllegaldrugs_IllegalDrugsID]
GO


