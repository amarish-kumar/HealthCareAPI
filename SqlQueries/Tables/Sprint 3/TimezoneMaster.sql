USE [HealthCare]
GO

/****** Object:  Table [dbo].[TimezoneMaster]    Script Date: 6/5/2018 11:50:41 PM ******/
DROP TABLE [dbo].[TimezoneMaster]
GO

/****** Object:  Table [dbo].[TimezoneMaster]    Script Date: 6/5/2018 11:50:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TimezoneMaster](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Timezone] [nvarchar](50) NOT NULL,
	[Active] [bit] NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_TimezoneMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


