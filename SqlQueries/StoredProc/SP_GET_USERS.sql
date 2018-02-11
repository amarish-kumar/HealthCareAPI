USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_USERS]    Script Date: 2/11/2018 11:44:36 AM ******/
DROP PROCEDURE [dbo].[SP_GET_USERS]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_USERS]    Script Date: 2/11/2018 11:44:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SP_GET_USERS]
(	
	@USER_ROLE NVARCHAR(100) = NULL,
	@ACTIVE BIT = NULL
)
AS

BEGIN

--EXEC [SP_GET_USERS]
--EXEC [SP_GET_USERS] 'Patient'

	SELECT DISTINCT UD.*
	FROM [UserDetail] UD
	INNER JOIN [UserRoleMapping] URM ON URM.UserId = UD.Id
	INNER JOIN [RoleMaster] R ON R.Id = URM.Id  AND (@USER_ROLE IS NULL OR R.RoleName = @USER_ROLE)
	WHERE (@ACTIVE IS NULL OR UD.[Active] = @ACTIVE) 
END












GO


