use [HealthCare]

/****** Object:  Table [dbo].[UserDetail]    Script Date: 01/16/2018 22:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[EmailAddress] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Gender] [int]  NULL,
	[DOB] [nvarchar](100) NULL,
	[Password] [nvarchar](500) NULL,
	[AlternateNo] [nvarchar](50) NULL,
	[EmergencyContactNo] [nvarchar](50) NULL,
	[EmergencyContactPerson] [nvarchar](255) NULL,
	[DLNumber] [nvarchar](500) NULL,
	[DLCopy] [nvarchar](500) NULL,
	[SSN] [nvarchar](255) NULL,
	[UnsuccessfulAttemptCount] int NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[IsPhoneVerified] [bit] NOT NULL,
 CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UC_UserDetail_EmailAddress] UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
),
 CONSTRAINT [UC_UserDetail_PhoneNumber] UNIQUE NONCLUSTERED 
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SystemSettings]    Script Date: 01/16/2018 22:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemSettings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SettingName] [nvarchar](50) NOT NULL,
	[SettingValue] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_SystemSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UC_SettingName] UNIQUE NONCLUSTERED 
(
	[SettingName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMaster]    Script Date: 01/16/2018 22:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_RoleMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UC_RoleName] UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenderMaster]    Script Date: 01/16/2018 22:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenderMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GenderName] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_GenderMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UC_GenderName] UNIQUE NONCLUSTERED 
(
	[GenderName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserDeviceDetail]    Script Date: 01/16/2018 22:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDeviceDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[IpAddress] [nvarchar](50) NOT NULL,
	[DeviceType] [nvarchar](50) NULL,
	[TwoFactorAuthDone] [bit] NOT NULL,
	[TwoFactorAuthTimestamp] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_UserDeviceDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleMapping]    Script Date: 01/16/2018 22:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleMapping](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[TAndCId] [bigint] NULL,
	[IsDefault] [bit] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_UserRoleMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginAudit]    Script Date: 01/16/2018 22:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserDeviceId] [bigint] NOT NULL,
	[IsTwoWayAuthNeeded] [bit] NOT NULL,
	[AccessCode] [nvarchar](10) NULL,
	[IsTwoWayAuthPassed] [bit] NOT NULL,
	[TwoFactorAuthTimestamp] [datetime] NULL,
	[SessionId] [nvarchar](255) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_UserLoginAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  ForeignKey [FK_UserDeviceDetail_UserDetail_UserId]    Script Date: 01/16/2018 22:11:26 ******/
ALTER TABLE [dbo].[UserDeviceDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserDeviceDetail_UserDetail_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetail] ([Id])
GO
ALTER TABLE [dbo].[UserDeviceDetail] CHECK CONSTRAINT [FK_UserDeviceDetail_UserDetail_UserId]
GO

/****** Object:  ForeignKey [FK_UserLoginAudit_UserDetail_UserDeviceId]    Script Date: 01/16/2018 22:11:26 ******/
ALTER TABLE [dbo].[UserLoginAudit]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginAudit_UserDetail_UserDeviceId] FOREIGN KEY([UserDeviceId])
REFERENCES [dbo].[UserDeviceDetail] ([Id])
GO
ALTER TABLE [dbo].[UserLoginAudit] CHECK CONSTRAINT [FK_UserLoginAudit_UserDetail_UserDeviceId]
GO
/****** Object:  ForeignKey [FK_UserLoginAudit_UserDetail_UserId]    Script Date: 01/16/2018 22:11:26 ******/
ALTER TABLE [dbo].[UserLoginAudit]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginAudit_UserDetail_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetail] ([Id])
GO
ALTER TABLE [dbo].[UserLoginAudit] CHECK CONSTRAINT [FK_UserLoginAudit_UserDetail_UserId]
GO

/****** Object:  ForeignKey [FK_UserRoleMapping_RoleMaster_RoleId]    Script Date: 01/16/2018 22:11:26 ******/
ALTER TABLE [dbo].[UserRoleMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleMapping_RoleMaster_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleMaster] ([Id])
GO
ALTER TABLE [dbo].[UserRoleMapping] CHECK CONSTRAINT [FK_UserRoleMapping_RoleMaster_RoleId]
GO
/****** Object:  ForeignKey [FK_UserRoleMapping_RoleMaster_TAndCId]    Script Date: 02/12/2018 01:33:26 ******/
ALTER TABLE [dbo].[UserRoleMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleMapping_RoleMaster_TAndCId] FOREIGN KEY([TAndCId])
REFERENCES [dbo].[TAndCMaster] ([Id])
GO
ALTER TABLE [dbo].[UserRoleMapping] CHECK CONSTRAINT [FK_UserRoleMapping_RoleMaster_TAndCId]
GO
/****** Object:  ForeignKey [FK_UserRoleMapping_UserDetail_UserId]    Script Date: 01/16/2018 22:11:26 ******/
ALTER TABLE [dbo].[UserRoleMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleMapping_UserDetail_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetail] ([Id])
GO
ALTER TABLE [dbo].[UserRoleMapping] CHECK CONSTRAINT [FK_UserRoleMapping_UserDetail_UserId]
GO


/****** Object:  Table [dbo].[TAndCMaster]    Script Date: 1/20/2018 8:13:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TAndCMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentPath] [nvarchar](max) NOT NULL,
	[VersionNumber] [int] NOT NULL,
	[RoleID] [bigint] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_TAndCMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TAndCMaster]  WITH CHECK ADD  CONSTRAINT [[FK_RoleTAndCMapping_TAndCMaster_TAndCId]] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RoleMaster] ([Id])
GO


CREATE TABLE [dbo].[PatientTAndCMapping](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[TAndCId] [bigint] NOT NULL,
	[SignnedDate] [datetimeoffset](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_PatientTAndCMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PatientTAndCMapping]  WITH CHECK ADD  CONSTRAINT [FK_PatientTAndCMapping_TAndCMaster_TAndCId] FOREIGN KEY([TAndCId])
REFERENCES [dbo].[TAndCMaster] ([Id])
GO

ALTER TABLE [dbo].[PatientTAndCMapping] CHECK CONSTRAINT [FK_PatientTAndCMapping_TAndCMaster_TAndCId]
GO

ALTER TABLE [dbo].[PatientTAndCMapping]  WITH CHECK ADD  CONSTRAINT [FK_PatientTAndCMapping_UserDetail_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[PatientTAndCMapping] CHECK CONSTRAINT [FK_PatientTAndCMapping_UserDetail_UserId]
GO
