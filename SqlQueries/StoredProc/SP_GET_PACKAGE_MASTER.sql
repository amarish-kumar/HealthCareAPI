USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_PACKAGE_MASTER]    Script Date: 4/22/2018 11:39:42 AM ******/
DROP PROCEDURE [dbo].[SP_GET_PACKAGE_MASTER]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_PACKAGE_MASTER]    Script Date: 4/22/2018 11:39:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_PACKAGE_MASTER]
(
	@ACTIVE BIT = NULL,
	@PACKAGE_ID BIGINT =  NULL
)
AS

BEGIN

--EXEC [SP_GET_PACKAGE_MASTER]
--EXEC [SP_GET_PACKAGE_MASTER] 1, 1

	SELECT Id, [Description],[Price], [Description] + ' ($ ' + CONVERT(NVARCHAR(25), [Price]) + ')' PackageName
	FROM [PackageMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE)
	AND (@PACKAGE_ID IS NULL OR [Id] = @PACKAGE_ID) 
	ORDER BY [Price]
END








GO


