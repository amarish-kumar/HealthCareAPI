USE [HealthCare]
GO

ALTER TABLE [dbo].[DoctorEducation] DROP CONSTRAINT [FK_DoctorEducation_UserDetail_Id]
GO

ALTER TABLE [dbo].[DoctorEducation] DROP CONSTRAINT [FK_DoctorEducation_StateMaster_Id]
GO

ALTER TABLE [dbo].[DoctorEducation] DROP CONSTRAINT [FK_DoctorEducation_CountryMaster_Id]
GO

/****** Object:  Table [dbo].[DoctorEducation]    Script Date: 6/5/2018 10:19:56 PM ******/
DROP TABLE [dbo].[DoctorEducation]
GO

/****** Object:  Table [dbo].[DoctorEducation]    Script Date: 6/5/2018 10:19:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DoctorEducation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[BeginingYear] int NOT NULL,
	[EndingYear] int NOT NULL,
	[CollegeName] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[StateId] [bigint] NOT NULL,
	[CountryId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorEducation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DoctorEducation]  WITH CHECK ADD  CONSTRAINT [FK_DoctorEducation_CountryMaster_Id] FOREIGN KEY([CountryId])
REFERENCES [dbo].[CountryMaster] ([ID])
GO

ALTER TABLE [dbo].[DoctorEducation] CHECK CONSTRAINT [FK_DoctorEducation_CountryMaster_Id]
GO

ALTER TABLE [dbo].[DoctorEducation]  WITH CHECK ADD  CONSTRAINT [FK_DoctorEducation_StateMaster_Id] FOREIGN KEY([StateId])
REFERENCES [dbo].[StateMaster] ([ID])
GO

ALTER TABLE [dbo].[DoctorEducation] CHECK CONSTRAINT [FK_DoctorEducation_StateMaster_Id]
GO

ALTER TABLE [dbo].[DoctorEducation]  WITH CHECK ADD  CONSTRAINT [FK_DoctorEducation_UserDetail_Id] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[DoctorEducation] CHECK CONSTRAINT [FK_DoctorEducation_UserDetail_Id]
GO


