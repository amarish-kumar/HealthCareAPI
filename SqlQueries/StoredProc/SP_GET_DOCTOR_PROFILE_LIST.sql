USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_PROFILE_LIST]    Script Date: 6/22/2018 10:45:20 AM ******/
DROP PROCEDURE [dbo].[SP_GET_DOCTOR_PROFILE_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_PROFILE_LIST]    Script Date: 6/22/2018 10:45:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_DOCTOR_PROFILE_LIST]
(	
	@USER_ID BIGINT,
	@DOCTOR_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_DOCTOR_PROFILE_LIST] 3 
--EXEC [SP_GET_DOCTOR_PROFILE_LIST] 4, 'Patient' 
	DECLARE @IS_CSR AS BIT, @IS_DOCTOR AS BIT;
	SET @IS_CSR = 0
	SET @IS_DOCTOR = 0
		IF EXISTS(
		SELECT URM.RoleId, R.RoleName 
		FROM UserRoleMapping URM
		INNER JOIN RoleMaster R ON R.Id = URM.Roleid
		WHERE URM.USERID=@USER_ID AND R.RoleName = 'CSRAdmin')

	BEGIN
		SET @IS_CSR = 1
	END

	ELSE
	BEGIN
		IF EXISTS(
			SELECT URM.RoleId, R.RoleName 
			FROM UserRoleMapping URM
			INNER JOIN RoleMaster R ON R.Id = URM.Roleid
			WHERE URM.USERID=@USER_ID AND R.RoleName = 'Doctor')

	BEGIN
		SET @IS_DOCTOR = 1
	END

	END
SELECT DP.[Id] ,DP.[DoctorId] ,DP.[IsPublished]
      ,DP.[EmailAddress1] ,DP.[IsEmailAddress1Default],DP.[EmailAddress2],DP.[IsEmailAddress2Default]
      ,DP.[EmailAddress3],DP.[IsEmailAddress3Default],DP.[PhoneNumber1],DP.[IsPhoneNumber1Default]
      ,DP.[PhoneNumber2],DP.[IsPhoneNumber2Default],DP.[PhoneNumber3],DP.[IsPhoneNumber3Default]
      ,DP.[DefaultAddressId],DP.[WebsiteAddress],DP.[TimezoneId],DP.[Active]
	  ,UD.LastName + ', ' + UD.FirstName AS DoctorName, T.ShortForm + ' ' + T.[Time] AS TimezoneDescription
  FROM [dbo].[DoctorProfile] DP
	INNER JOIN TimezoneMaster T ON T.Id = DP.[TimezoneId]
	INNER JOIN UserDetail UD ON UD.Id = DP.DoctorId
	WHERE DP.[Active] = 1 AND (@DOCTOR_ID IS NULL OR DP.DoctorId = @DOCTOR_ID)
	AND (@IS_CSR = 1 OR @IS_DOCTOR = 1)
	ORDER BY DP.AddedDate DESC
END








GO


