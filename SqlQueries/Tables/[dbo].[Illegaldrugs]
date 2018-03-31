USE [HealthCare]
GO

/****** Object:  Table [dbo].[Illegaldrugs]    Script Date: 3/23/2018 11:59:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Illegaldrugs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProfileId] [bigint] NOT NULL,
	[Consumedrugs] [bit] NULL,
	[Nameofdrug] [nvarchar](255) NULL,
	[Frequency] [nvarchar](255) NOT NULL,
	[PerDay] [bigint] NULL,
	[PerWeek] [bigint] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Illegaldrugs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Illegaldrugs]  WITH CHECK ADD  CONSTRAINT [FK_Illegaldrugs_ProfileId] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([Id])
GO

ALTER TABLE [dbo].[Illegaldrugs] CHECK CONSTRAINT [FK_Illegaldrugs_ProfileId]
GO


