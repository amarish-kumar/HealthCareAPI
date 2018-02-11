USE [HealthCare]
GO

INSERT INTO [dbo].[ConsultationStatus] ([Description],[SortOrder],[Active],[AddedBy],[AddedDate]) VALUES  ('Initiated',1,1, 1,getdate())
INSERT INTO [dbo].[ConsultationStatus] ([Description],[SortOrder],[Active],[AddedBy],[AddedDate]) VALUES  ('In Progress',2,1,1,getdate())
INSERT INTO [dbo].[ConsultationStatus] ([Description],[SortOrder],[Active],[AddedBy],[AddedDate]) VALUES  ('Need Clarification',3,1,1,getdate())
INSERT INTO [dbo].[ConsultationStatus] ([Description],[SortOrder],[Active],[AddedBy],[AddedDate]) VALUES  ('Completed',4,1,1,getdate())

