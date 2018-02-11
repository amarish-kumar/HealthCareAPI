USE [HealthCare]
GO

INSERT INTO [dbo].[Conversation]
			([Description] ,[ConsultationId],[PatientId],[DoctorId],[IsLocked],[Active],[AddedBy],[AddedDate])
     VALUES
           ('Doctor response 1',8,NULL,3,0,1,3,getdate())
GO

INSERT INTO [dbo].[Conversation]
			([Description] ,[ConsultationId],[PatientId],[DoctorId],[IsLocked],[Active],[AddedBy],[AddedDate])
     VALUES
           ('patient response 1',8,4,NULL,0,1,4,getdate())
GO

INSERT INTO [dbo].[Conversation]
			([Description] ,[ConsultationId],[PatientId],[DoctorId],[IsLocked],[Active],[AddedBy],[AddedDate])
     VALUES
           ('Doctor response 2',8,NULL,3,0,1,3,getdate())
GO

INSERT INTO [dbo].[Conversation]
			([Description] ,[ConsultationId],[PatientId],[DoctorId],[IsLocked],[Active],[AddedBy],[AddedDate])
     VALUES
           ('patient response 2',8,4,NULL,0,1,4,getdate())
GO

INSERT INTO [dbo].[Conversation]
			([Description] ,[ConsultationId],[PatientId],[DoctorId],[IsLocked],[Active],[AddedBy],[AddedDate])
     VALUES
           ('patient response 3',8,4,NULL,0,1,4,getdate())
GO
