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
, @EmergencyContactNo AS NVARCHAR(MAX), @EmergencyContactPerson AS NVARCHAR(MAX), @DLNumber AS NVARCHAR(MAX), @SSN AS NVARCHAR(MAX)
DECLARE @Active AS BIT

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
	   , @SSN = UserDetailList.Columns.value('SSN[1]', 'nvarchar(max)')
	   , @Active = UserDetailList.Columns.value('Active[1]', 'bit')
	   , @UserType = UserDetailList.Columns.value('UserType[1]', 'BIGINT')
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
					,[Address] = ISNULL(@Address, [Address])
					,[PhoneNumber] = ISNULL(@PhoneNumber, PhoneNumber)
					,[Gender] = ISNULL(@Gender, Gender)
					,[DOB] = ISNULL(@DOB, DOB)
					,[Password] = ISNULL(@Password, [Password])
					,[AlternateNo] = ISNULL(@AlternateNo, AlternateNo)
					,[EmergencyContactNo] = ISNULL(@EmergencyContactNo, EmergencyContactNo)
					,[EmergencyContactPerson] = ISNULL(@EmergencyContactPerson, EmergencyContactPerson)
					,[DLNumber] = ISNULL(@DLNumber, DLNumber)
					,[SSN] = ISNULL(@SSN, SSN)
					,[Active] = ISNULL(@Active, Active)
					,[ModifiedBy] = @USER_ID
					,[ModifiedDate] = GETDATE()
			WHERE ID = @Id
		END
	ELSE
		BEGIN
		    /*THIS BLOCK IS FOR INSERT USER DETAIL */
			INSERT INTO [dbo].[UserDetail]
			   ([FirstName]
			   ,[LastName]
			   ,[EmailAddress]
			   ,[Address]
			   ,[PhoneNumber]
			   ,[Gender]
			   ,[DOB]
			   ,[Password]
			   ,[AlternateNo]
			   ,[EmergencyContactNo]
			   ,[EmergencyContactPerson]
			   ,[DLNumber]
			   ,[SSN]
			   ,[Active]
			   ,[AddedBy]
			   ,[AddedDate])
			VALUES
			   (@FirstName
			   ,@LastName
			   ,@EmailAddress
			   ,@Address
			   ,@PhoneNumber
			   ,@Gender
			   ,@DOB
			   ,@Password
			   ,@AlternateNo
			   ,@EmergencyContactNo
			   ,@EmergencyContactPerson
			   ,@DLNumber
			   ,@SSN
			   ,@Active
			   ,@USER_ID
			   ,GETDATE())
			/*INSERT USER DETAIL BLOCK ENDS HERE*/

			/*THIS BLOCK IS FOR INSERT USER ROLE */
			INSERT INTO [dbo].[UserRoleMapping]
			   ([UserId]
			   ,[RoleId]
			   ,[IsDefault]
			   ,[Active]
			   ,[AddedBy]
			   ,[AddedDate])
				VALUES
			   (@@IDENTITY
			   , @UserType
			   , 1
			   , 1
			   ,@USER_ID
			   ,GETDATE())


			/*USER ROLE BLOCK ENDS HERE*/
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
			,[DeletedDate] = GETDATE()
		WHERE Id = @USER_ID
END

END

GO


