ALTER TABLE [dbo].[UserRoleMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleMapping_TAndCMaster_tandcid] FOREIGN KEY([tandcid])
REFERENCES [dbo].[TAndCMaster] ([Id])
GO

ALTER TABLE [dbo].[UserRoleMapping] CHECK CONSTRAINT [FK_UserRoleMapping_TAndCMaster_tandcid]
GO