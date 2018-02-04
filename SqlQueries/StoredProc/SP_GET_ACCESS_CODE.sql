USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ACCESS_CODE]    Script Date: 2/3/2018 10:41:05 AM ******/
DROP PROCEDURE [dbo].[SP_GET_ACCESS_CODE]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ACCESS_CODE]    Script Date: 2/3/2018 10:41:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_ACCESS_CODE]
(	
	@USER_ID BIGINT,
	@IP_ADDRESS NVARCHAR(200)
)
AS

BEGIN

--EXEC [SP_GET_ACCESS_CODE] 2, '101.120.222.558'
	SELECT ULA.AccessCode, ULA.UserId, ULA.Id AS UserLoginAuditId, UD.EmailAddress, UD.PhoneNumber
		FROM UserLoginAudit ULA
		INNER JOIN UserDeviceDetail UDD ON ULA.UserId = UDD.UserId AND ULA.UserDeviceId = UDD.Id
		INNER JOIN UserDetail UD ON UD.Id = ULA.UserId
	WHERE ULA.UserId = @USER_ID AND UDD.IpAddress = @IP_ADDRESS
	AND ULA.Active = 1
END






GO


