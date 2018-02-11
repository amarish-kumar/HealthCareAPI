USE [HealthCare]
GO

/****** Object:  Table [dbo].[Conversation]    Script Date: 2/17/2018 10:02:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Conversation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[PatientId] [bigint] NULL,
	[DoctorId] [bigint] NULL,
	[IsLocked] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Conversation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Conversation]  WITH CHECK ADD  CONSTRAINT [FK_Conversation_Consultation_ConsultationId] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[Conversation] CHECK CONSTRAINT [FK_Conversation_Consultation_ConsultationId]
GO

ALTER TABLE [dbo].[Conversation]  WITH CHECK ADD  CONSTRAINT [FK_Conversation_UserDetail_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[Conversation] CHECK CONSTRAINT [FK_Conversation_UserDetail_DoctorId]
GO

ALTER TABLE [dbo].[Conversation]  WITH CHECK ADD  CONSTRAINT [FK_Conversation_UserDetail_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[Conversation] CHECK CONSTRAINT [FK_Conversation_UserDetail_PatientId]
GO


