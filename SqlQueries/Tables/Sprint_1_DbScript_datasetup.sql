use [HealthCare]

SET IDENTITY_INSERT [dbo].[UserDetail] ON
INSERT [dbo].[UserDetail] ([Id], [FirstName], [LastName], [EmailAddress], [PhoneNumber], 
[Gender], [DOB], [Password], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], 
[DeletedBy], [DeletedDate],[IsEmailVerified],[IsPhoneVerified]) 
VALUES (1, N'Admin', N'Admin', N'Admin', N'1234567890', 1, N'1/14/2000', N'admin@123', 1, 
1, CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), 1, 
CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), NULL, NULL,1,1)
SET IDENTITY_INSERT [dbo].[UserDetail] OFF



SET IDENTITY_INSERT [dbo].[SystemSettings] ON
INSERT [dbo].[SystemSettings] ([Id], [SettingName], [SettingValue], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (1, N'TwoFactorAuthTimeout', N'15', 1, 1, CAST(0x07C0C92EC9B4C53D0B0000 AS DateTimeOffset), NULL, NULL, NULL, NULL)
INSERT [dbo].[SystemSettings] ([Id], [SettingName], [SettingValue], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (2, N'UnsuccessfulAttemptCount', N'5', 1, 1, CAST(0x07C0C92EC9B4C53D0B0000 AS DateTimeOffset), NULL, NULL, NULL, NULL)
INSERT [dbo].[SystemSettings] ([Id], [SettingName], [SettingValue], [Active], [AddedBy], [AddedDate]) VALUES (3, N'SenderEmailAddress', N'healthcareappmailer@gmail.com', 1, 1, CAST(0x07C0C92EC9B4C53D0B0000 AS DateTimeOffset))
INSERT [dbo].[SystemSettings] ([Id], [SettingName], [SettingValue], [Active], [AddedBy], [AddedDate]) VALUES (4, N'SenderEmailAddressPassword', N'healthcareappmailer@777', 1, 1, CAST(0x07C0C92EC9B4C53D0B0000 AS DateTimeOffset))
INSERT [dbo].[SystemSettings] ([Id], [SettingName], [SettingValue], [Active], [AddedBy], [AddedDate]) VALUES (5, N'SMSSenderAccountId', N'AC50b93c726f0e0e8a034a687c33ba5243', 1, 1, CAST(0x07C0C92EC9B4C53D0B0000 AS DateTimeOffset))
INSERT [dbo].[SystemSettings] ([Id], [SettingName], [SettingValue], [Active], [AddedBy], [AddedDate]) VALUES (6, N'SMSSenderAccountToken', N'020c1d6b8296445779167f5dce1b11b1', 1, 1, CAST(0x07C0C92EC9B4C53D0B0000 AS DateTimeOffset))
INSERT [dbo].[SystemSettings] ([Id], [SettingName], [SettingValue], [Active], [AddedBy], [AddedDate]) VALUES (7, N'SMSSenderPhoneNumber', N'+15612204349', 1, 1, CAST(0x07C0C92EC9B4C53D0B0000 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[SystemSettings] OFF


SET IDENTITY_INSERT [dbo].[RoleMaster] ON
INSERT [dbo].[RoleMaster] ([Id], [RoleName], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (1, N'Owner', 1, 1, CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleMaster] ([Id], [RoleName], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (2, N'CSR', 1, 1, CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleMaster] ([Id], [RoleName], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (3, N'CSRAdmin', 1, 1, CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleMaster] ([Id], [RoleName], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (4, N'Patient', 1, 1, CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleMaster] ([Id], [RoleName], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (5, N'Doctor', 1, 1, CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[RoleMaster] OFF

SET IDENTITY_INSERT [dbo].[GenderMaster] ON
INSERT [dbo].[GenderMaster] ([Id], [GenderName], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (1, N'Male', 1, 1, CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), NULL, NULL, NULL, NULL)
INSERT [dbo].[GenderMaster] ([Id], [GenderName], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (3, N'Female', 1, 1, CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), NULL, NULL, NULL, NULL)
INSERT [dbo].[GenderMaster] ([Id], [GenderName], [Active], [AddedBy], [AddedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (4, N'Others', 1, 1, CAST(0x070084B1109BC23D0B4A01 AS DateTimeOffset), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[GenderMaster] OFF


SET IDENTITY_INSERT [dbo].[TAndCMaster] ON
INSERT INTO [dbo].[TAndCMaster]
           ([Id],[DocumentPath] ,[VersionNumber],[Active] ,[AddedBy],[AddedDate]) VALUES (1,'c:/docs/tandc_v1.pdf',1, 0, 1, GETDATE())
INSERT INTO [dbo].[TAndCMaster]
           ([Id],[DocumentPath] ,[VersionNumber],[Active] ,[AddedBy],[AddedDate]) VALUES (2, 'c:/docs/tandc_v2.pdf',1, 1, 1, GETDATE())
SET IDENTITY_INSERT [dbo].[TAndCMaster] OFF

INSERT INTO [dbo].[EmailMaster]
           ([Body],[Subject],[EmailType],[AddedBy],[AddedDate]) VALUES ('Your access code is {0} .','Access Code', 'GET_ACCESS_CODE', 1, GETDATE())
INSERT INTO [dbo].[EmailMaster]
           ([Body],[Subject],[EmailType],[AddedBy],[AddedDate]) VALUES ('Your access code is {0} .','Access Code', 'GET_ACCESS_CODE_SMS', 1, GETDATE())