USE [HealthCare]
GO

/****** Object:  Table [dbo].[DoctorSpecialityMapping]    Script Date: 2/11/2018 2:10:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DoctorSpecialityMapping](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SpecialityID] [bigint] NOT NULL,
	[DoctorID] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_DoctorSpecialityMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DoctorSpecialityMapping]  WITH CHECK ADD  CONSTRAINT [FK_DoctorSpecialityMapping_SpecialityID] FOREIGN KEY([SpecialityID])
REFERENCES [dbo].[SpecialityMaster] ([Id])
GO

ALTER TABLE [dbo].[DoctorSpecialityMapping] CHECK CONSTRAINT [FK_DoctorSpecialityMapping_SpecialityID]
GO


