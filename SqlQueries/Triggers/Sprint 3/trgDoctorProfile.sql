USE [HealthCare]
GO

/****** Object:  Trigger [trgDoctorProfile]    Script Date: 6/20/2018 11:40:19 AM ******/
DROP TRIGGER [dbo].[trgDoctorProfile]
GO

/****** Object:  Trigger [dbo].[trgDoctorProfile]    Script Date: 6/20/2018 11:40:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE Trigger [dbo].[trgDoctorProfile] ON [dbo].[DoctorProfile] FOR INSERT,UPDATE AS  
BEGIN  
DECLARE @ACTION AS NVARCHAR(10)

 IF NOT EXISTS(SELECT * FROM INSERTED)
       SET @ACTION = 'Delete';
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
            SET @ACTION = 'Insert';
        ELSE
            SET @ACTION = 'Update';
    END	

INSERT INTO [dbo].[DoctorProfileAudit]
           ([Action]
           ,[EntityId]
           ,[DoctorId]
           ,[IsPublished]
           ,[EmailAddress1]
           ,[IsEmailAddress1Default]
           ,[EmailAddress2]
           ,[IsEmailAddress2Default]
           ,[EmailAddress3]
           ,[IsEmailAddress3Default]
           ,[PhoneNumber1]
           ,[IsPhoneNumber1Default]
           ,[PhoneNumber2]
           ,[IsPhoneNumber2Default]
           ,[PhoneNumber3]
           ,[IsPhoneNumber3Default]
           ,[DefaultAddressId]
           ,[WebsiteAddress]
           ,[TimezoneId]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate])

    SELECT @ACTION
           ,[Id]
           ,[DoctorId]
           ,[IsPublished]
           ,[EmailAddress1]
           ,[IsEmailAddress1Default]
           ,[EmailAddress2]
           ,[IsEmailAddress2Default]
           ,[EmailAddress3]
           ,[IsEmailAddress3Default]
           ,[PhoneNumber1]
           ,[IsPhoneNumber1Default]
           ,[PhoneNumber2]
           ,[IsPhoneNumber2Default]
           ,[PhoneNumber3]
           ,[IsPhoneNumber3Default]
           ,[DefaultAddressId]
           ,[WebsiteAddress]
           ,[TimezoneId]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[DeletedBy]
           ,[DeletedDate] FROM Inserted

END






GO


