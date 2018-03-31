USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationFamilyHistory]    Script Date: 3/12/2018 6:29:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConsultationFamilyHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[RelationshipId] [bigint] NOT NULL,
	[HealthConditionId] [bigint] NOT NULL,
	[CurrentAge] int NULL,
	[AgeOnConditionStart] int NULL,
	[IsAlive] bit NOT NULL,
	[CAuseOfDeath] nvarchar(255) NULL,
	[AgeOnDeath] int NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationFamilyHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ConsultationFamilyHistory]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationFamilyHistory_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationFamilyHistory] CHECK CONSTRAINT [FK_ConsultationFamilyHistory_Consultation_Id]
GO

ALTER TABLE [dbo].[ConsultationFamilyHistory]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationFamilyHistory_RelationshipMaster_Id] FOREIGN KEY([RelationshipId])
REFERENCES [dbo].[RelationshipMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationFamilyHistory] CHECK CONSTRAINT [FK_ConsultationFamilyHistory_RelationshipMaster_Id]
GO

ALTER TABLE [dbo].[ConsultationFamilyHistory]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationFamilyHistory_HealthConditionMaster_Id] FOREIGN KEY([HealthConditionId])
REFERENCES [dbo].[HealthConditionMaster] ([ID])
GO

ALTER TABLE [dbo].[ConsultationFamilyHistory] CHECK CONSTRAINT [FK_ConsultationFamilyHistory_HealthConditionMaster_Id]
GO


