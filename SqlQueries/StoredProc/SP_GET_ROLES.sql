USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ROLES]    Script Date: 2/3/2018 1:00:59 PM ******/
DROP PROCEDURE [dbo].[SP_GET_ROLES]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ROLES]    Script Date: 2/3/2018 1:00:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_ROLES]
(	
	@ROLE_NAME NVARCHAR(200) = NULL,
	@ACTIVE BIT = NULL
)
AS

BEGIN

--EXEC [SP_GET_ROLES] 
--EXEC [SP_GET_ROLES] 'Owner'
	SELECT *
	FROM [RoleMaster]
	WHERE (@ROLE_NAME IS NULL OR [RoleName] = @ROLE_NAME)
	AND (@ACTIVE IS NULL OR [Active] = @ACTIVE) 
END










GO


