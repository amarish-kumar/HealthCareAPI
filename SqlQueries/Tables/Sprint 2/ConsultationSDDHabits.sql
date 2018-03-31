

CREATE TABLE [dbo].[ConsultationSDDHabits](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[ConsumeAlcohol] [bit] NOT NULL,
	[AlcoholConsumptionFreq] [nvarchar](15) NULL,
	[DrinksPerDay] [int] NULL,
	[DrinksPerWeek] [int] NULL,
	[DoSmoke] [bit] NOT NULL,
	[EverSmoked] [bit] NOT NULL,
	[YearOfQuittingSmoking] [int] NULL,
	[SmokingFreq] [int] NULL,
	[ConsumeDrugs] [bit] NOT NULL,
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
 CONSTRAINT [PK_ConsultationSDDHabits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationSDDHabits]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationSDDHabits_Consultation_Id] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationSDDHabits] CHECK CONSTRAINT [FK_ConsultationSDDHabits_Consultation_Id]
GO


