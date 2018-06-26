USE [HealthCare]
GO

ALTER TABLE [dbo].[DoctorProfile] DROP CONSTRAINT [FK_DoctorProfile_UserDetail_Id]
GO

ALTER TABLE [dbo].[DoctorProfile] DROP CONSTRAINT [FK_DoctorProfile_UserAddressMapping_Id]
GO

ALTER TABLE [dbo].[DoctorProfile] DROP CONSTRAINT [FK_DoctorProfile_TimezoneMaster_Id]
GO

/****** Object:  Table [dbo].[DoctorProfile]    Script Date: 6/6/2018 9:47:46 AM ******/
DROP TABLE [dbo].[DoctorProfile]
GO

/****** Object:  Table [dbo].[DoctorProfile]    Script Date: 6/6/2018 9:47:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DoctorProfile](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[EmailAddress1] [nvarchar](300) NOT NULL,
	[IsEmailAddress1Default] [bit] NOT NULL,
	[EmailAddress2] [nvarchar](300) NULL,
	[IsEmailAddress2Default] [bit] NOT NULL,
	[EmailAddress3] [nvarchar](300) NULL,
	[IsEmailAddress3Default] [bit] NOT NULL,
	[PhoneNumber1] [nvarchar](20) NOT NULL,
	[IsPhoneNumber1Default] [bit] NOT NULL,
	[PhoneNumber2] [nvarchar](20) NULL,
	[IsPhoneNumber2Default] [bit] NOT NULL,
	[PhoneNumber3] [nvarchar](20) NULL,
	[IsPhoneNumber3Default] [bit] NOT NULL,
	[DefaultAddressId] [bigint] NULL,
	[WebsiteAddress] [nvarchar](300) NULL,
	[TimezoneId] [bigint] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DoctorProfile]  WITH CHECK ADD  CONSTRAINT [FK_DoctorProfile_TimezoneMaster_Id] FOREIGN KEY([TimezoneId])
REFERENCES [dbo].[TimezoneMaster] ([ID])
GO

ALTER TABLE [dbo].[DoctorProfile] CHECK CONSTRAINT [FK_DoctorProfile_TimezoneMaster_Id]
GO

ALTER TABLE [dbo].[DoctorProfile]  WITH CHECK ADD  CONSTRAINT [FK_DoctorProfile_UserAddressMapping_Id] FOREIGN KEY([DefaultAddressId])
REFERENCES [dbo].[UserAddressMapping] ([ID])
GO

ALTER TABLE [dbo].[DoctorProfile] CHECK CONSTRAINT [FK_DoctorProfile_UserAddressMapping_Id]
GO

ALTER TABLE [dbo].[DoctorProfile]  WITH CHECK ADD  CONSTRAINT [FK_DoctorProfile_UserDetail_Id] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[DoctorProfile] CHECK CONSTRAINT [FK_DoctorProfile_UserDetail_Id]
GO


