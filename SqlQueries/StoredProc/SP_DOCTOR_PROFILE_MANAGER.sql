USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_PROFILE_MANAGER]    Script Date: 6/21/2018 10:04:29 AM ******/
DROP PROCEDURE [dbo].[SP_DOCTOR_PROFILE_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_PROFILE_MANAGER]    Script Date: 6/21/2018 10:04:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_DOCTOR_PROFILE_MANAGER]
(
	@DOCTOR_PROFILE_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR CREATE/UPDATE, PASS EXCLUSIVE VALUES FOR USER DISABLING
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED
)
AS

BEGIN

/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @DoctorId AS BIGINT, @DefaultAddressId AS BIGINT, @TimezoneId AS BIGINT
DECLARE @EmailAddress1 AS NVARCHAR(MAX), @EmailAddress2 AS NVARCHAR(MAX), @EmailAddress3 AS NVARCHAR(MAX), 
@PhoneNumber1 AS NVARCHAR(MAX), @PhoneNumber2 AS NVARCHAR(MAX), @PhoneNumber3 AS NVARCHAR(MAX),
@WebsiteAddress AS NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)

DECLARE @Active AS BIT, @IsPublished AS BIT, @IsEmailAddress1Default AS BIT, @IsEmailAddress2Default AS BIT
, @IsEmailAddress3Default AS BIT, @IsPhoneNumber1Default AS BIT, @IsPhoneNumber2Default AS BIT,
@IsPhoneNumber3Default AS BIT, @Result as BIT

SELECT @Id = DoctorProfileList.Columns.value('Id[1]', 'BIGINT')
		,@DoctorId = DoctorProfileList.Columns.value('DoctorId[1]', 'BIGINT')
		,@DefaultAddressId = DoctorProfileList.Columns.value('DefaultAddressId[1]', 'BIGINT')
		,@TimezoneId = DoctorProfileList.Columns.value('TimezoneId[1]', 'BIGINT')
	    ,@EmailAddress1 = DoctorProfileList.Columns.value('EmailAddress1[1]', 'nvarchar(max)')
		,@EmailAddress2 = DoctorProfileList.Columns.value('EmailAddress2[1]', 'nvarchar(max)')
		,@EmailAddress3 = DoctorProfileList.Columns.value('EmailAddress3[1]', 'nvarchar(max)')
		,@PhoneNumber1 = DoctorProfileList.Columns.value('PhoneNumber1[1]', 'nvarchar(max)')
		,@PhoneNumber2 = DoctorProfileList.Columns.value('PhoneNumber2[1]', 'nvarchar(max)')
		,@PhoneNumber3 = DoctorProfileList.Columns.value('PhoneNumber3[1]', 'nvarchar(max)')
		,@WebsiteAddress = DoctorProfileList.Columns.value('WebsiteAddress[1]', 'nvarchar(max)')
		, @IsPublished = DoctorProfileList.Columns.value('IsPublished[1]', 'bit')
		, @IsEmailAddress1Default = DoctorProfileList.Columns.value('IsEmailAddress1Default[1]', 'bit')
		, @IsEmailAddress2Default = DoctorProfileList.Columns.value('IsEmailAddress2Default[1]', 'bit')
		, @IsEmailAddress3Default = DoctorProfileList.Columns.value('IsEmailAddress3Default[1]', 'bit')
		, @IsPhoneNumber1Default = DoctorProfileList.Columns.value('IsPhoneNumber1Default[1]', 'bit')
		, @IsPhoneNumber2Default = DoctorProfileList.Columns.value('IsPhoneNumber2Default[1]', 'bit')
		, @IsPhoneNumber3Default = DoctorProfileList.Columns.value('IsPhoneNumber3Default[1]', 'bit')
	   , @Active = DoctorProfileList.Columns.value('Active[1]', 'bit')
FROM   @DOCTOR_PROFILE_XML.nodes('DoctorProfile') AS DoctorProfileList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/


IF @OPERATION IS NULL

BEGIN
	IF @Id IS NOT NULL AND @Id > 0
		BEGIN
			/*THIS BLOCK IS FOR UPDATE */
			UPDATE [dbo].[DoctorProfile]
				SET [EmailAddress1] = ISNULL(@EmailAddress1, [EmailAddress1])
				   ,[IsEmailAddress1Default] = ISNULL(@IsEmailAddress1Default, [IsEmailAddress1Default])
				   ,[EmailAddress2] = ISNULL(@EmailAddress2, [EmailAddress2])
				   ,[IsEmailAddress2Default] = ISNULL(@IsEmailAddress2Default, [IsEmailAddress2Default])
				   ,[EmailAddress3] = ISNULL(@EmailAddress3, [EmailAddress3])
				   ,[IsEmailAddress3Default] = ISNULL(@IsEmailAddress3Default, [IsEmailAddress3Default])
				   ,[PhoneNumber1] = ISNULL(@PhoneNumber1, [PhoneNumber1])
				   ,[IsPhoneNumber1Default] = ISNULL(@IsPhoneNumber1Default, [IsPhoneNumber1Default])
				   ,[PhoneNumber2] = ISNULL(@PhoneNumber2, [PhoneNumber2])
				   ,[IsPhoneNumber2Default] = ISNULL(@IsPhoneNumber2Default, [IsPhoneNumber2Default])
				   ,[PhoneNumber3] = ISNULL(@PhoneNumber3, [PhoneNumber3])
				   ,[IsPhoneNumber3Default] = ISNULL(@IsPhoneNumber3Default, [IsPhoneNumber3Default])
				   ,[DefaultAddressId] = ISNULL(@DefaultAddressId, [DefaultAddressId])
				   ,[WebsiteAddress] = ISNULL(@WebsiteAddress, [WebsiteAddress])
				   ,[TimezoneId] = ISNULL(@TimezoneId, [TimezoneId])
				   ,[Active] = ISNULL(@Active, Active)
				   ,[ModifiedBy] = @USER_ID
				   ,[ModifiedDate] = GETUTCDATE()
			WHERE ID = @Id			
			SET @Result = 1;
			SET @ReturnMessage = 'Record updated successfully.'
		END
	ELSE
		BEGIN
		    /*THIS BLOCK IS FOR INSERT Doctor Profile */
			INSERT INTO [dbo].[DoctorProfile]
				   ([DoctorId]
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
				   ,[AddedDate])
			 VALUES
				   (@DoctorId
				   ,@IsPublished
				   ,@EmailAddress1
				   ,@IsEmailAddress1Default
				   ,@EmailAddress2
				   ,@IsEmailAddress2Default
				   ,@EmailAddress3
				   ,@IsEmailAddress3Default
				   ,@PhoneNumber1
				   ,@IsPhoneNumber1Default
				   ,@PhoneNumber2
				   ,@IsPhoneNumber2Default
				   ,@PhoneNumber3
				   ,@IsPhoneNumber3Default
				   ,@DefaultAddressId
				   ,@WebsiteAddress
				   ,@TimezoneId
				   ,@Active
				   ,@USER_ID
				   ,GETUTCDATE())

			SET @Result = 1;
			SET @ReturnMessage = 'Record created successfully.'
			/*INSERT Doctor Profile BLOCK ENDS HERE*/

		END

END
	
IF @OPERATION = 'SOFT_DELETE'
BEGIN
	
	UPDATE [dbo].[DoctorProfile]
		SET [ACTIVE] = 0
			,[DeletedBy] = @USER_ID
			,[DeletedDate] = GETUTCDATE()
		WHERE Id = @Id
	SET @Result = 1;
	SET @ReturnMessage = 'Record deleted successfully.'
END

SELECT @Result AS Result, @ReturnMessage AS ReturnMessage

END



GO


