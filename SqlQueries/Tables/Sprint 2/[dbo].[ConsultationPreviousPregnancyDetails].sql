USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationPreviousPregnancyDetails]    Script Date: 3/23/2018 11:59:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationPreviousPregnancyDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[CurrentPregnancyID] [bigint] NOT NULL,
	[NoofPregnancy] [bigint] NULL,
	[ChildNo] [bigint] NULL,
	[DeliveryYear] [nvarchar](4) NULL,
	[DeliveryType] [nvarchar](20) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationPreviousPregnancyDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[ConsultationPreviousPregnancyDetails]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationPreviousPregnancyDetails_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationPreviousPregnancyDetails] CHECK CONSTRAINT [FK_ConsultationPreviousPregnancyDetails_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationPreviousPregnancyDetails]  WITH CHECK ADD  CONSTRAINT [FK_PregnancyDetails_CurrentPregnancyID] FOREIGN KEY([CurrentPregnancyID])
REFERENCES [dbo].[ConsultationPregnancyDetails] ([Id])
GO

ALTER TABLE [dbo].[ConsultationPreviousPregnancyDetails] CHECK CONSTRAINT [FK_PregnancyDetails_CurrentPregnancyID]
GO
