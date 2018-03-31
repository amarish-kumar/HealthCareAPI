USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_USER_DETAIL_MANAGER]    Script Date: 1/30/2018 12:35:23 PM ******/
DROP PROCEDURE [dbo].[SP_USER_DETAIL_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_USER_DETAIL_MANAGER]    Script Date: 1/30/2018 12:35:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_USER_DETAIL_MANAGER]
(
	@USER_DETAIL_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR CREATE/UPDATE, PASS EXCLUSIVE VALUES FOR USER DISABLING, PASSWORD RESET ETC
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED
)
AS

BEGIN

/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @Gender AS BIGINT, @UserType AS BIGINT
DECLARE @FirstName AS NVARCHAR(MAX), @LastName AS NVARCHAR(MAX), @EmailAddress AS NVARCHAR(MAX), @Address AS NVARCHAR(MAX)
, @PhoneNumber AS NVARCHAR(MAX), @DOB AS NVARCHAR(MAX), @Password AS NVARCHAR(MAX), @AlternateNo AS NVARCHAR(MAX)
, @EmergencyContactNo AS NVARCHAR(MAX), @EmergencyContactPerson AS NVARCHAR(MAX), @DLNumber AS NVARCHAR(MAX), @DLCopy AS NVARCHAR(MAX), @SSN AS NVARCHAR(MAX)
, @IsEmailVerified AS BIGINT, @IsPhoneVerified AS BIGINT, @TnCID AS BIGINT
,@AddressTypeID as BIGINT,@Address1 AS NVARCHAR(MAX),@Address2 AS NVARCHAR(MAX),@City AS NVARCHAR(MAX)
,@State AS NVARCHAR(MAX),@ZipCode AS NVARCHAR(MAX),@CountryID as BIGINT  

DECLARE @Active AS BIT
DECLARE @IdentityVal AS BIGINT

SELECT @Id = UserDetailList.Columns.value('Id[1]', 'BIGINT')
	   , @FirstName = UserDetailList.Columns.value('FirstName[1]', 'nvarchar(max)')
	   , @LastName = UserDetailList.Columns.value('LastName[1]', 'nvarchar(max)')
	   , @EmailAddress = UserDetailList.Columns.value('EmailAddress[1]', 'nvarchar(max)')
	   , @Address = UserDetailList.Columns.value('Address[1]', 'nvarchar(max)')
	   , @PhoneNumber = UserDetailList.Columns.value('PhoneNumber[1]', 'nvarchar(max)')
	   , @Gender = UserDetailList.Columns.value('Gender[1]', 'INT')
	   , @DOB = UserDetailList.Columns.value('DOB[1]', 'nvarchar(max)')
	   , @Password = UserDetailList.Columns.value('Password[1]', 'nvarchar(max)')
	   , @AlternateNo = UserDetailList.Columns.value('AlternateNo[1]', 'nvarchar(max)')
	   , @EmergencyContactNo = UserDetailList.Columns.value('EmergencyContactNo[1]', 'nvarchar(max)')
	   , @EmergencyContactPerson = UserDetailList.Columns.value('EmergencyContactPerson[1]', 'nvarchar(max)')
	   , @DLNumber = UserDetailList.Columns.value('DLNumber[1]', 'nvarchar(max)')
	   , @DLCopy= UserDetailList.Columns.value('DLCopy[1]', 'nvarchar(max)')
	   , @SSN = UserDetailList.Columns.value('SSN[1]', 'nvarchar(max)')
	   , @Active = UserDetailList.Columns.value('Active[1]', 'bit')
	   , @UserType = UserDetailList.Columns.value('UserType[1]', 'BIGINT')
	   , @IsEmailVerified = UserDetailList.Columns.value('isEmailVerified[1]', 'BIGINT')
	   , @IsPhoneVerified = UserDetailList.Columns.value('isPhoneVerified[1]', 'BIGINT')
	   , @TnCID= UserDetailList.Columns.value('TnCID[1]', 'BIGINT')
	   , @AddressTypeID = UserDetailList.Columns.value('AddressTypeID[1]', 'BIGINT')
	   , @Address1 = UserDetailList.Columns.value('Address1[1]', 'nvarchar(max)')
	   , @Address2 = UserDetailList.Columns.value('Address2[1]', 'nvarchar(max)')
	   , @City = UserDetailList.Columns.value('City[1]', 'nvarchar(max)')
	   , @State = UserDetailList.Columns.value('State[1]', 'nvarchar(max)')
	   , @ZipCode = UserDetailList.Columns.value('ZipCode[1]', 'nvarchar(max)')
	   , @CountryID = UserDetailList.Columns.value('CountryID[1]', 'BIGINT')
FROM   @USER_DETAIL_XML.nodes('UserSignUp') AS UserDetailList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/


IF @OPERATION IS NULL

BEGIN
	IF @Id IS NOT NULL AND @Id > 0
		BEGIN
			/*THIS BLOCK IS FOR UPDATE */
			UPDATE [dbo].[UserDetail]
				SET [FirstName] = ISNULL(@FirstName, FirstName)
					,[LastName] = ISNULL(@LastName, LastName)
					,[EmailAddress] = ISNULL(@EmailAddress, EmailAddress)
					,[PhoneNumber] = ISNULL(@PhoneNumber, PhoneNumber)
					,[Gender] = ISNULL(@Gender, Gender)
					,[DOB] = ISNULL(@DOB, DOB)
					,[Password] = ISNULL(@Password, [Password])
					,[AlternateNo] = ISNULL(@AlternateNo, AlternateNo)
					,[EmergencyContactNo] = ISNULL(@EmergencyContactNo, EmergencyContactNo)
					,[EmergencyContactPerson] = ISNULL(@EmergencyContactPerson, EmergencyContactPerson)
					,[DLNumber] = ISNULL(@DLNumber, DLNumber)
					,[DLCopy]= ISNULL(@DLCopy, DLCopy)
					,[SSN] = ISNULL(@SSN, SSN)
					,[Active] = ISNULL(@Active, Active)
					,[ModifiedBy] = @USER_ID
					,[ModifiedDate] = GETUTCDATE()
			WHERE ID = @Id
			
			UPDATE [dbo].[UserRoleMapping]
				SET [TandCId] = ISNULL(@TnCID, TandCId)
				WHERE ID = @Id

			UPDATE [DBO].[USERADDRESSMAPPING]
			SET Address1 = @Address1,
			Address2 = @Address2,
			City = @City,
			State = @State,
			ZipCode = @ZipCode,
			CountryID = @CountryID,
			Active = @Active,
			[ModifiedBy] = @USER_ID,
			[ModifiedDate] = GETUTCDATE()
			WHERE USERID = @Id AND ADDRESSTYPEID = @AddressTypeID

		END
	ELSE
		BEGIN
		    /*THIS BLOCK IS FOR INSERT USER DETAIL */
			INSERT INTO [dbo].[UserDetail]
			   ([FirstName]
			   ,[LastName]
			   ,[EmailAddress]
			   ,[PhoneNumber]
			   ,[Gender]
			   ,[DOB]
			   ,[Password]
			   ,[AlternateNo]
			   ,[EmergencyContactNo]
			   ,[EmergencyContactPerson]
			   ,[DLNumber]
			   ,[DLCopy]
			   ,[SSN]
			   ,[Active]
			   ,[AddedBy]
			   ,[AddedDate]
			   ,[IsEmailVerified]
			   ,[IsPhoneVerified])
			VALUES
			   (@FirstName
			   ,@LastName
			   ,@EmailAddress
			   ,@PhoneNumber
			   ,@Gender
			   ,@DOB
			   ,@Password
			   ,@AlternateNo
			   ,@EmergencyContactNo
			   ,@EmergencyContactPerson
			   ,@DLNumber
			   ,@DLCopy
			   ,@SSN
			   ,@Active
			   ,@USER_ID
			   ,GETUTCDATE()
			   ,@IsEmailVerified
			   ,@IsPhoneVerified)
			/*INSERT USER DETAIL BLOCK ENDS HERE*/

			set @IdentityVal = @@IDENTITY

			/*THIS BLOCK IS FOR INSERT USER ROLE */
			INSERT INTO [dbo].[UserRoleMapping]
			   ([UserId]
			   ,[RoleId]
			   ,[TandCId]
			   ,[IsDefault]
			   ,[Active]
			   ,[AddedBy]
			   ,[AddedDate])
				VALUES
			   (@IdentityVal
			   , @UserType
			   , @TnCID
			   , 1
			   , 1
			   ,@USER_ID
			   ,GETUTCDATE())
			
			/*THIS BLOCK IS TO ADD THE DEFAULT PROFILE IN CASE OF PATIENT*/			
			--IF @UserType = 4
			BEGIN
				DECLARE @PROFILE_XML AS XML
				SET @PROFILE_XML = '<Profile><Active>true</Active><UserId>' + CONVERT(NVARCHAR(25), @IdentityVal) + '</UserId><RelationshipId>1</RelationshipId><FirstName>' + CONVERT(NVARCHAR(255), @FirstName) + '</FirstName><LastName>' + CONVERT(NVARCHAR(255), @LastName) + '</LastName><GenderId>' + CONVERT(NVARCHAR(25), @Gender) + '</GenderId><DOB>' + CONVERT(NVARCHAR(25), @DOB) + '</DOB><IsDefault>true</IsDefault></Profile>'
				EXEC SP_PROFILE_MANAGER @PROFILE_XML, NULL, @IdentityVal
			END

			/*THIS BLOCK IS FOR INSERT USERADDRESS MAPPING */

			if @UserType = 2 or  @UserType = 3
				INSERT INTO [dbo].[UserAddressMapping]
				([UserID],
				[AddressTypeID],
				[Address1],
				[Address2],
				[City],
				[State],
				[ZipCode],
				[CountryID],
				[Active],
				[AddedBy],
				[AddedDate])
				VALUES
				(@IdentityVal,
				@AddressTypeID,
				@Address1,
				@Address2,
				@City,
				@State,
				@ZipCode,
				@CountryID,
				1,
				@USER_ID,
				GETUTCDATE()
				)
			/*USERADDRESS MAPPING BLOCK ENDS HERE*/
		END

END

IF @OPERATION = 'PASSWORD_RESET'
BEGIN
	declare @newpwd varchar(20)
	exec [dbo].uspRandChars @len=8, @output=@newpwd out

	UPDATE [dbo].[UserDetail]
		SET [Password] = @newpwd
		WHERE Id = @Id
END
	
IF @OPERATION = 'DISABLE_USER'
BEGIN
	
	UPDATE [dbo].[UserDetail]
		SET [ACTIVE] = 0
			,[DeletedBy] = @USER_ID
			,[DeletedDate] = GETUTCDATE()
		WHERE Id = @USER_ID
END

END

GO