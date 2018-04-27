USE [HealthCare]
GO

truncate table [dbo].[DrugTypeMaster]

INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Oral Solid Dosage',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Oral Liquid',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Topical',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Rectal',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Vaginal',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Injections',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Inhaler',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Opthalmic',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Ear Drop',1, 1, GETDATE())
INSERT INTO [dbo].[DrugTypeMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Nasal Drop',1, 1, GETDATE())