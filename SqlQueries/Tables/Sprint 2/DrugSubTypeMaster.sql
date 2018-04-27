USE [HealthCare]
GO

ALTER TABLE [dbo].[DrugSubTypeMaster] DROP CONSTRAINT [FK_DrugSubTypeMaster_DrugTypeMaster_Id]
GO

/****** Object:  Table [dbo].[DrugSubTypeMaster]    Script Date: 4/23/2018 9:39:09 AM ******/
DROP TABLE [dbo].[DrugSubTypeMaster]
GO

/****** Object:  Table [dbo].[DrugSubTypeMaster]    Script Date: 4/23/2018 9:39:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DrugSubTypeMaster](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugTypeId] [bigint] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Active] [bit] NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_DrugSubTypeMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DrugSubTypeMaster]  WITH CHECK ADD  CONSTRAINT [FK_DrugSubTypeMaster_DrugTypeMaster_Id] FOREIGN KEY([DrugTypeId])
REFERENCES [dbo].[DrugTypeMaster] ([Id])
GO

ALTER TABLE [dbo].[DrugSubTypeMaster] CHECK CONSTRAINT [FK_DrugSubTypeMaster_DrugTypeMaster_Id]
GO


