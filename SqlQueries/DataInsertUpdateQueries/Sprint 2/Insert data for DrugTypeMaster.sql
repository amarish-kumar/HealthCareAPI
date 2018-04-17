USE [HealthCare]
GO

INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Topical',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Oral',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Injection',1, 1, GETDATE())