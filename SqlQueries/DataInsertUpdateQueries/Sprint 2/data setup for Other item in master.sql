USE [HealthCare]
GO

INSERT INTO [dbo].[AllergyMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Others', 1, 1, GETDATE())
INSERT INTO [dbo].[SurgeryMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Others', 1, 1, GETDATE())
INSERT INTO [dbo].[HealthConditionMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Others', 1, 1, GETDATE())
INSERT INTO [dbo].[OccupationMaster] ([Description],[Active],[AddedBy],[AddedDate]) VALUES ('Others', 1, 1, GETDATE())
INSERT INTO [dbo].[IllegalDrugsMaster] ([Description],[Active],[AddedBy] ,[AddedDate]) values ('Others',1,1,GETUTCDATE())

