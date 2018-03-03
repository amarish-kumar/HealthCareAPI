USE [HealthCare]
GO

/****** Object:  Table [dbo].[UserAddressMapping]    Script Date: 3/2/2018 6:42:50 PM ******/
DROP TABLE [dbo].[UserAddressMapping]
GO

/****** Object:  Table [dbo].[UserAddressMapping]    Script Date: 3/2/2018 6:42:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserAddressMapping](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[AddressTypeID] [bigint] NOT NULL,
	[Address1] [nvarchar](100) not NULL,
	[Address2] [nvarchar](100) NULL,
	[City] [nvarchar](100) not NULL,
	[State] [nvarchar](100) not NULL,
	[ZipCode] [nvarchar](100) not NULL,
	[CountryID] [bigint] not null,
	[Active] [bit] NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_UserAddressMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[UserAddressMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserAddressMapping_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[UserAddressMapping] CHECK CONSTRAINT [FK_UserAddressMapping_UserID]
GO

ALTER TABLE [dbo].[UserAddressMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserAddressMapping_AddressTypeID] FOREIGN KEY([AddressTypeID])
REFERENCES [dbo].[AddressTypeMaster] ([Id])
GO

ALTER TABLE [dbo].[UserAddressMapping] CHECK CONSTRAINT [FK_UserAddressMapping_AddressTypeID]
GO

ALTER TABLE [dbo].[UserAddressMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserAddressMapping_CountryID] FOREIGN KEY([CountryID])
REFERENCES [dbo].[CountryMaster] ([ID])
GO

ALTER TABLE [dbo].[UserAddressMapping] CHECK CONSTRAINT [FK_UserAddressMapping_CountryID]
GO

GO


