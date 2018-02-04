USE [HealthCare]
GO

/****** Object:  Table [dbo].[ErrorLog]    Script Date: 1/26/2018 10:47:17 PM ******/
DROP TABLE [dbo].[ErrorLog]
GO

/****** Object:  Table [dbo].[ErrorLog]    Script Date: 1/26/2018 10:47:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ErrorLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[StackTrace] [nvarchar](max) NOT NULL,
	[ExceptionType] [nvarchar](max) NOT NULL,
	[Source] [nvarchar](max) NOT NULL,
	[AddedBy] [bigint] NOT NULL,
	[AddedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


