use healthCare
Alter table dbo.[Consultation]
add PackageId bigint null

ALTER TABLE [dbo].[Consultation]  WITH CHECK ADD  CONSTRAINT [FK_Consultation_PackageMaster_Id] FOREIGN KEY([PackageId])
REFERENCES [dbo].[PackageMaster] ([Id])
GO

ALTER TABLE [dbo].[Consultation] CHECK CONSTRAINT [FK_Consultation_PackageMaster_Id]
GO

UPDATE [dbo].[Consultation]
SET PackageId = 1

ALTER TABLE [dbo].[Consultation] ALTER COLUMN PackageId bigint NOT NULL
ALTER TABLE [dbo].[Consultation] ALTER COLUMN ProfileId bigint NOT NULL