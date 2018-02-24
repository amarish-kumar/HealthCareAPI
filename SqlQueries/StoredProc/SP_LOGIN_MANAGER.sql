USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOGIN_MANAGER]    Script Date: 2/4/2018 1:48:36 PM ******/
DROP PROCEDURE [dbo].[SP_LOGIN_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOGIN_MANAGER]    Script Date: 2/4/2018 1:48:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_LOGIN_MANAGER]
(
	@USERNAME NVARCHAR(100),
	@PASSWORD NVARCHAR(100),
	@IP_ADDRESS NVARCHAR(200)
)
AS

BEGIN

--EXEC [dbo].[SP_LOGIN_MANAGER] '+919619645344','10dulkaree', '101.120.222.558'
--EXEC [dbo].[SP_LOGIN_MANAGER] 'kunalsmehtajobs@gmail.com','10dulkar', '101.120.222.558'
--EXEC [dbo].[SP_LOGIN_MANAGER] 'kunalsmehtajobs@gmail.com','10dulkar', '101.120.222.558'
DECLARE @USER_ID AS BIGINT, @USER_ROLE_ID AS BIGINT, @USER_DEVICE_ID AS BIGINT
,@TWO_WAY_AUTH_TIMEOUT_DAYS AS BIGINT, @LOGIN_AUDIT_ID AS BIGINT
DECLARE @TWO_FACTOR_AUTH_TS AS DATETIME
DECLARE @SESSION_ID AS NVARCHAR(100) = NULL
DECLARE @TWO_FACTOR_AUTH_DONE AS BIT, @IS_PASSWORD_VERIFIED AS BIT, @IS_USER_ACTIVE AS BIT
SET @IS_PASSWORD_VERIFIED = 0
SET @IS_USER_ACTIVE = 0

	SELECT @USER_ID = UD.Id, @IS_USER_ACTIVE = UD.[Active]
	FROM UserDetail UD
		WHERE (EmailAddress = @USERNAME OR Phonenumber = @USERNAME)
		AND [Password] = @PASSWORD
		


/*IF USER IS FOUND WITH SAME USER ID AND PWD, GO AHEAD*/
IF @USER_ID IS NOT NULL
	
	BEGIN
		/*SET PASSWORD VERIFIED FLAG TO TRUE */
		SET @IS_PASSWORD_VERIFIED = 1

		/*IF THE USER ACCOUNT IS LOCKED WE NEED NOT DO THE FURTHER PROCESS*/
		IF  @IS_USER_ACTIVE = 1
		BEGIN
		/*CHECK IF DEVICE IS THERE FOR THE USER OR NOT
		IF NOT MAKE AN ENTRY IN USER DEVICE DETAIL TABLE
		AND SET THE @TWO_FACTOR_AUTH_DONE AND @USER_DEVICE_ID*/

		IF NOT EXISTS (
			SELECT Id FROM DBO.UserDeviceDetail
				WHERE UserId = @USER_ID
				AND IpAddress = @IP_ADDRESS				
		)

		BEGIN

			INSERT INTO [dbo].[UserDeviceDetail]
			   ([UserId]
			   ,[IpAddress]
			   ,[TwoFactorAuthDone]
			   ,[Active]
			   ,[AddedBy]
			   ,[AddedDate])
			VALUES
			   ( @USER_ID
			   , @IP_ADDRESS
			   , 0
			   , 1
			   , @USER_ID
			   , GETUTCDATE())

			SET @USER_DEVICE_ID = @@IDENTITY
			SET @TWO_FACTOR_AUTH_DONE = 0
		END

		ELSE
			BEGIN
				SELECT @USER_DEVICE_ID = Id 
					, @TWO_FACTOR_AUTH_DONE = TwoFactorAuthDone
					, @TWO_FACTOR_AUTH_TS = TwoFactorAuthTimestamp
				FROM DBO.UserDeviceDetail
					WHERE UserId = @USER_ID
					AND IpAddress = @IP_ADDRESS		
			END
						
		/*
		MARK ALL PREVIOUS LOGIN AUDIT RECORDS AS IN ACTIVE
		INSERT A RECORD IN THE USER LOGIN AUDIT TABLE*/

		UPDATE [dbo].[UserLoginAudit]
		SET [Active] = 0,
			[ModifiedBy] = @USER_ID,
			[ModifiedDate] = GETUTCDATE()
		WHERE UserId = @USER_ID
		AND [UserDeviceId] = @USER_DEVICE_ID
		AND [Active] = 1

		INSERT INTO [dbo].[UserLoginAudit]
				   ([UserId]
				   ,[UserDeviceId]
				   ,[IsTwoWayAuthNeeded]
				   ,[AccessCode]
				   ,[IsTwoWayAuthPassed]
				   ,[TwoFactorAuthTimestamp]
				   ,[SessionId]				   
				   ,[Active]
				   ,[AddedBy]
				   ,[AddedDate])
			 VALUES
				   (@USER_ID
				   , @USER_DEVICE_ID
				   , CASE @TWO_FACTOR_AUTH_DONE WHEN 0 THEN 1 ELSE 0 END
				   , CASE @TWO_FACTOR_AUTH_DONE WHEN 0 THEN cast((900000 * Rand() + 100000) as int ) ELSE NULL END
				   , @TWO_FACTOR_AUTH_DONE
				   , @TWO_FACTOR_AUTH_TS
				   , NEWID()
				   , 1
				   , @USER_ID
				   , GETUTCDATE())

		SET @LOGIN_AUDIT_ID = @@IDENTITY

		/*SET THE SESSION_ID IF THE TWO WAY AUTH IS DONE*/

		IF @TWO_FACTOR_AUTH_DONE = 1
		BEGIN
			SELECT @SESSION_ID = SessionId
			FROM [dbo].[UserLoginAudit]
			WHERE Id = @LOGIN_AUDIT_ID
		END
	
		END
	END

SELECT @IS_PASSWORD_VERIFIED AS IS_PASSWORD_VERIFIED,
	   @USER_ID AS [USER_ID],  
	   ISNULL(@TWO_FACTOR_AUTH_DONE,0) AS TWO_FACTOR_AUTH_DONE,
	   @USER_DEVICE_ID AS USER_DEVICE_ID, 
	   @TWO_FACTOR_AUTH_TS AS TWO_FACTOR_AUTH_TS,
	   @SESSION_ID AS SESSION_ID,
	   @IS_USER_ACTIVE AS IS_USER_ACTIVE 

END








GO


