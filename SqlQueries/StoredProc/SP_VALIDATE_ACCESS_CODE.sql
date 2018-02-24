USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_VALIDATE_ACCESS_CODE]    Script Date: 2/3/2018 10:42:14 AM ******/
DROP PROCEDURE [dbo].[SP_VALIDATE_ACCESS_CODE]
GO

/****** Object:  StoredProcedure [dbo].[SP_VALIDATE_ACCESS_CODE]    Script Date: 2/3/2018 10:42:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_VALIDATE_ACCESS_CODE]
(	
	@USER_ID BIGINT,
	@USER_LOGIN_AUDIT_ID BIGINT,
	@ACCESS_CODE NVARCHAR(200)
)
AS

BEGIN

--EXEC [SP_VALIDATE_ACCESS_CODE] 2, 8, '123456'
--EXEC [SP_VALIDATE_ACCESS_CODE] 2, 8, '243510'

DECLARE @VALID_ACCESS_CODE AS NVARCHAR(10), @MESSAGE AS NVARCHAR(1000)
DECLARE @ACCESS_CODE_EXIST AS BIT, @IS_VALID_CODE_PASSED AS BIT, @IS_ACCOUNT_LOCKED AS BIT

DECLARE @UNSUCCESSFUL_ATTEMPT_COUNT AS INT = 0, @ALLOWED_UNSUCCESSFUL_ATTEMPT_COUNT AS INT


/*CHECK IF THE USER IS ACTIVE OR NOT*/
IF EXISTS (SELECT ID FROM UserDetail 
			WHERE Id = @USER_ID
			AND [Active] = 0)
	BEGIN
		SET @MESSAGE = 'User account is locked. Please contact Administrator.'
		SET @IS_VALID_CODE_PASSED = 0
		SET @IS_ACCOUNT_LOCKED =  1
		SELECT @MESSAGE AS [Message]
			  , @IS_VALID_CODE_PASSED as 'IsValidCodePassed'
			  , @UNSUCCESSFUL_ATTEMPT_COUNT AS 'UnsuccessfulAttemptCount'
			  , ISNULL(@IS_ACCOUNT_LOCKED,0) AS 'IsAccountLocked'
		RETURN;
	END

/*GET THE SYSTEM SETTING VALUE FOR UnsuccessfulAttemptCount IN A VARIABLE*/
SELECT @ALLOWED_UNSUCCESSFUL_ATTEMPT_COUNT = CONVERT(INT, SettingValue)
	FROM SystemSettings
	WHERE SettingName= 'UnsuccessfulAttemptCount'
	
/*POPULATE THE VALID ACCESS CODE FROM DB TO THE VARIABLE*/
SELECT @VALID_ACCESS_CODE = AccessCode
	FROM UserLoginAudit
	WHERE Id = @USER_LOGIN_AUDIT_ID
	AND UserId = @USER_ID
END

/*CHECK IF THE RECORD EXIST IN THE USER LOGIN AUDIT TABLE*/
SET @ACCESS_CODE_EXIST = CASE @VALID_ACCESS_CODE WHEN NULL THEN 0 ELSE 1 END

IF @ACCESS_CODE_EXIST = 1
	BEGIN
		/*CHECK IF ACCESS CODE PASSED IS VALID OR NOT*/
		SET @IS_VALID_CODE_PASSED = CASE @ACCESS_CODE WHEN @VALID_ACCESS_CODE THEN 1 ELSE 0 END

		/*SET THE ATTEMPT COUNT ACCORDINGLY*/
		IF @IS_VALID_CODE_PASSED = 1
		BEGIN
			UPDATE UserDetail
				SET UnsuccessfulAttemptCount = 0
				WHERE Id = @USER_ID

			UPDATE UserLoginAudit
				SET IsTwoWayAuthPassed = 1
					, TwoFactorAuthTimestamp = GETUTCDATE()
					, ModifiedBy = @USER_ID
					, ModifiedDate = GETUTCDATE()
				WHERE Id = @USER_LOGIN_AUDIT_ID

			UPDATE UDD
			SET TwoFactorAuthDone = 1
				, TwoFactorAuthTimestamp = GETUTCDATE()
				, ModifiedBy = @USER_ID
				, ModifiedDate = GETUTCDATE()
			 FROM UserDeviceDetail UDD
			INNER JOIN UserLoginAudit ULA ON ULA.UserDeviceId = UDD.Id
			WHERE ULA.Id = @USER_LOGIN_AUDIT_ID

			SET @MESSAGE = 'Two way authantication successful. Please re-login'
			SET @IS_VALID_CODE_PASSED = 1
		END

		ELSE
		BEGIN
			UPDATE UserDetail
				SET UnsuccessfulAttemptCount = ISNULL(UnsuccessfulAttemptCount,0) + 1
				WHERE Id = @USER_ID
			
			SELECT @UNSUCCESSFUL_ATTEMPT_COUNT = UnsuccessfulAttemptCount
				FROM UserDetail
				WHERE Id = @USER_ID

			SET @MESSAGE = 'Two way authantication failed. ' + CONVERT(nvarchar(10), @UNSUCCESSFUL_ATTEMPT_COUNT) + '/'
			             + CONVERT(nvarchar(10), @ALLOWED_UNSUCCESSFUL_ATTEMPT_COUNT) + ' unsuccessful attempts done.'
			/*IF NO OF MAX ATTEMPT COUNT REACHED, INACTIVATE THE USER*/
			IF  @UNSUCCESSFUL_ATTEMPT_COUNT >= @ALLOWED_UNSUCCESSFUL_ATTEMPT_COUNT
			BEGIN
				EXEC [SP_USER_DETAIL_MANAGER] NULL, 'DISABLE_USER', @USER_ID
				SET @MESSAGE = @MESSAGE + 'Account is locked.'
			END
			
			SET @IS_VALID_CODE_PASSED = 0
		END
	END

ELSE
	BEGIN
		SET @MESSAGE = 'No records found. Try to re-login.'
		SET @IS_VALID_CODE_PASSED = 0
	END


SELECT @MESSAGE AS [Message]
	  , @IS_VALID_CODE_PASSED as 'IsValidCodePassed'
	  , @UNSUCCESSFUL_ATTEMPT_COUNT AS 'UnsuccessfulAttemptCount'
	  , ISNULL(@IS_ACCOUNT_LOCKED,0) AS 'IsAccountLocked'



GO


