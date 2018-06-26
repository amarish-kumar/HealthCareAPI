USE [HealthCare]
GO

ALTER TABLE [dbo].[DoctorAwards] DROP CONSTRAINT [FK_DoctorAwards_UserDetail_Id]
GO

/****** Object:  Table [dbo].[DoctorAwards]    Script Date: 6/5/2018 10:26:56 PM ******/
DROP TABLE [dbo].[DoctorAwards]
GO

/****** Object:  Table [dbo].[DoctorAwards]    Script Date: 6/5/2018 10:26:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DoctorAwards](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[YearReceived] [int] NOT NULL,
	[InstitutionName] [nvarchar](255) NULL,
	[AwardName] [nvarchar](400) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorAwards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DoctorAwards]  WITH CHECK ADD  CONSTRAINT [FK_DoctorAwards_UserDetail_Id] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[DoctorAwards] CHECK CONSTRAINT [FK_DoctorAwards_UserDetail_Id]
GO


