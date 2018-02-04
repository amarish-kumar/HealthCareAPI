USE [HealthCare]
GO

/****** Object:  Table [dbo].[EmailMaster]    Script Date: 2/1/2018 9:48:05 PM ******/
DROP TABLE [dbo].[EmailMaster]
GO

/****** Object:  Table [dbo].[EmailMaster]    Script Date: 2/1/2018 9:48:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmailMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[Subject] [nvarchar](1000) NOT NULL,
	[EmailType] [nvarchar](100) NOT NULL,
	[AddedBy] [bigint] NOT NULL,
	[AddedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_EmailMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


