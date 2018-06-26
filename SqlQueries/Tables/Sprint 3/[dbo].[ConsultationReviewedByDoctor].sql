USE [HealthCare]
GO

/****** Object:  Table [dbo].[ConsultationReviewedByDoctor]    Script Date: 6/8/2018 1:04:45 AM ******/
DROP TABLE [dbo].[ConsultationReviewedByDoctor]
GO

/****** Object:  Table [dbo].[ConsultationReviewedByDoctor]    Script Date: 6/8/2018 1:04:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsultationReviewedByDoctor](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsultationId] [bigint] NOT NULL,
	[DoctorId] [bigint] NULL,
	[ResponsetoConvID] [bigint] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ConsultationReviewedByDoctor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsultationReviewedByDoctor]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationReviewedByDoctor_ConsultationId] FOREIGN KEY([ConsultationId])
REFERENCES [dbo].[Consultation] ([Id])
GO

ALTER TABLE [dbo].[ConsultationReviewedByDoctor] CHECK CONSTRAINT [FK_ConsultationReviewedByDoctor_ConsultationId]
GO

ALTER TABLE [dbo].[ConsultationReviewedByDoctor]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationReviewedByDoctor_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[ConsultationReviewedByDoctor] CHECK CONSTRAINT [FK_Conversation_UserDetail_DoctorId]
GO

