USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_OCCUPATION_MASTER]    Script Date: 4/17/2018 10:48:12 PM ******/
DROP PROCEDURE [dbo].[SP_GET_OCCUPATION_MASTER]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_OCCUPATION_MASTER]    Script Date: 4/17/2018 10:48:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_OCCUPATION_MASTER]
(
	@ACTIVE BIT = NULL,
	@DESCRIPTION NVARCHAR(100) =  NULL,
	@SEARCH_TERM NVARCHAR(100) =  NULL
)
AS

BEGIN

--EXEC [SP_GET_OCCUPATION_MASTER]
--EXEC [SP_GET_OCCUPATION_MASTER] 1, 'Working'
--EXEC [SP_GET_OCCUPATION_MASTER] 1, null, 'o'

	SELECT Id, [Description]
	FROM [OccupationMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE)
	AND (@DESCRIPTION IS NULL OR [Description] = @DESCRIPTION) 
	AND (@SEARCH_TERM IS NULL OR [Description] like '%' + @SEARCH_TERM + '%') 
	ORDER BY [Description]
END

GO


