USE [HealthCare]
GO

ALTER TABLE [dbo].[DoctorImages] DROP CONSTRAINT [FK_DoctorImages_UserDetail_Id]
GO

/****** Object:  Table [dbo].[DoctorImages]    Script Date: 6/5/2018 11:48:49 PM ******/
DROP TABLE [dbo].[DoctorImages]
GO

/****** Object:  Table [dbo].[DoctorImages]    Script Date: 6/5/2018 11:48:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DoctorImages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[FileName] [nvarchar](300) NULL,
	[FileData] [varbinary](max) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DoctorImages]  WITH CHECK ADD  CONSTRAINT [FK_DoctorImages_UserDetail_Id] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[DoctorImages] CHECK CONSTRAINT [FK_DoctorImages_UserDetail_Id]
GO


