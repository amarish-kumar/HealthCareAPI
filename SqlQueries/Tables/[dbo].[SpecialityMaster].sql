USE [HealthCare]
GO

/****** Object:  Table [dbo].[SpecialityMaster]    Script Date: 2/11/2018 2:11:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SpecialityMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Speciality] [nvarchar](max) NOT NULL,
	[SearchKeyWords] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_SpecialityMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

