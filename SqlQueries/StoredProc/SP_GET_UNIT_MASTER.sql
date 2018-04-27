USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_UNIT_MASTER]    Script Date: 4/26/2018 4:33:35 PM ******/
DROP PROCEDURE [dbo].[SP_GET_UNIT_MASTER]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_UNIT_MASTER]    Script Date: 4/26/2018 4:33:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_UNIT_MASTER]
(
	@ACTIVE BIT = NULL,
	@DESCRIPTION NVARCHAR(100) =  NULL,
	@SEARCH_TERM NVARCHAR(100) =  NULL
)
AS

BEGIN

--EXEC [SP_GET_UNIT_MASTER]
--EXEC [SP_GET_UNIT_MASTER] 1, 'mg'
--EXEC [SP_GET_UNIT_MASTER] 1, null, 'g'

	SELECT Id, [Description]
	FROM [UnitMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE)
	AND (@DESCRIPTION IS NULL OR [Description] = @DESCRIPTION) 
	AND (@SEARCH_TERM IS NULL OR [Description] like '%' + @SEARCH_TERM + '%') 
	ORDER BY [Description]
END









GO


