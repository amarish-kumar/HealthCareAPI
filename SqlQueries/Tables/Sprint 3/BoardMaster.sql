USE [HealthCare]
GO

/****** Object:  Table [dbo].[BoardMaster]    Script Date: 6/4/2018 10:21:50 PM ******/
DROP TABLE [dbo].[BoardMaster]
GO

/****** Object:  Table [dbo].[BoardMaster]    Script Date: 6/4/2018 10:21:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BoardMaster](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Board] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Active] [bit] NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_BoardMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


