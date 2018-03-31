USE [HealthCare]
GO

/****** Object:  Table [dbo].[Profile]    Script Date: 3/12/2018 11:59:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Profile](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RelationshipId] [bigint] NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[GenderId] [bigint] NOT NULL,
	[DOB] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_GenderMaster_Id] FOREIGN KEY([GenderId])
REFERENCES [dbo].[GenderMaster] ([Id])
GO

ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_GenderMaster_Id]
GO

ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_RelationshipMaster_Id] FOREIGN KEY([RelationshipId])
REFERENCES [dbo].[RelationshipMaster] ([ID])
GO

ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_RelationshipMaster_Id]
GO

ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_UserDetail_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_UserDetail_Id]
GO


