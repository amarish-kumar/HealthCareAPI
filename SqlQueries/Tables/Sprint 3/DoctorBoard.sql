USE [HealthCare]
GO

ALTER TABLE [dbo].[DoctorBoard] DROP CONSTRAINT [FK_DoctorBoard_UserDetail_Id]
GO

ALTER TABLE [dbo].[DoctorBoard] DROP CONSTRAINT [FK_DoctorBoard_BoardMaster_Id]
GO

/****** Object:  Table [dbo].[DoctorBoard]    Script Date: 6/6/2018 9:38:04 AM ******/
DROP TABLE [dbo].[DoctorBoard]
GO

/****** Object:  Table [dbo].[DoctorBoard]    Script Date: 6/6/2018 9:38:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DoctorBoard](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[BoardId] [bigint] NOT NULL,
	[OtherDescription] [varchar](500) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorBoard] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DoctorBoard]  WITH CHECK ADD  CONSTRAINT [FK_DoctorBoard_BoardMaster_Id] FOREIGN KEY([BoardId])
REFERENCES [dbo].[BoardMaster] ([ID])
GO

ALTER TABLE [dbo].[DoctorBoard] CHECK CONSTRAINT [FK_DoctorBoard_BoardMaster_Id]
GO

ALTER TABLE [dbo].[DoctorBoard]  WITH CHECK ADD  CONSTRAINT [FK_DoctorBoard_UserDetail_Id] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[DoctorBoard] CHECK CONSTRAINT [FK_DoctorBoard_UserDetail_Id]
GO


