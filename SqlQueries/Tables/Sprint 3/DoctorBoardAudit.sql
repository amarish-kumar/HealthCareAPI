USE [HealthCare]
GO

/****** Object:  Table [dbo].[DoctorBoardAudit]    Script Date: 6/6/2018 10:05:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DoctorBoardAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](10) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[DoctorId] [bigint] NOT NULL,
	[BoardId] [bigint] NOT NULL,
	[OtherDescription] [varchar](500) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_DoctorBoardAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


