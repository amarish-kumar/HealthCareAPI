Alter table dbo.[Consultation]
add ProfileId bigint null

ALTER TABLE [dbo].[Consultation]  WITH CHECK ADD  CONSTRAINT [FK_Consultation_Profile_Id] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([Id])
GO

ALTER TABLE [dbo].[Consultation] CHECK CONSTRAINT [FK_Consultation_Profile_Id]
GO