USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_EMAIL_TEXT]    Script Date: 3/4/2018 11:08:29 AM ******/
DROP PROCEDURE [dbo].[SP_GET_EMAIL_TEXT]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_EMAIL_TEXT]    Script Date: 3/4/2018 11:08:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GET_EMAIL_TEXT]
(	
	@EMAIL_TYPE NVARCHAR(200)
)
AS

BEGIN

--EXEC [SP_GET_EMAIL_TEXT] 'GET_ACCESS_CODE'
--EXEC [SP_GET_EMAIL_TEXT] 'GET_ACCESS_CODE_SMS'

	IF CHARINDEX('SMS', @EMAIL_TYPE) > 0
	BEGIN

		SELECT *,
			(SELECT SettingValue FROM [SystemSettings] WHERE [SettingName]='SMSSenderAccountId') AS 'SenderAccountId',
			(SELECT SettingValue FROM [SystemSettings] WHERE [SettingName]='SMSSenderPhoneNumber') AS 'SenderAddress',
			(SELECT SettingValue FROM [SystemSettings] WHERE [SettingName]='SMSSenderAccountToken') AS 'SenderPassword'
		FROM [EmailMaster]
		WHERE EmailType = @EMAIL_TYPE

	END

	ELSE
	BEGIN
		SELECT *,
			'' AS 'SenderAccountId',
			(SELECT SettingValue FROM [SystemSettings] WHERE [SettingName]='SenderEmailAddress') AS 'SenderAddress',
			(SELECT SettingValue FROM [SystemSettings] WHERE [SettingName]='SenderEmailAddressPassword') AS 'SenderPassword'
		FROM [EmailMaster]
		WHERE EmailType = @EMAIL_TYPE

	END
END









GO


