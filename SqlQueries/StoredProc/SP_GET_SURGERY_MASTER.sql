USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SURGERY_MASTER]    Script Date: 4/8/2018 10:57:51 AM ******/
DROP PROCEDURE [dbo].[SP_GET_SURGERY_MASTER]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SURGERY_MASTER]    Script Date: 4/8/2018 10:57:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_SURGERY_MASTER]
(
	@ACTIVE BIT = NULL,
	@DESCRIPTION NVARCHAR(100) =  NULL,
	@SEARCH_TERM NVARCHAR(100) =  NULL
)
AS

BEGIN

--EXEC [SP_GET_SURGERY_MASTER]
--EXEC [SP_GET_SURGERY_MASTER] 1, 'Abdominal liposuction'
--EXEC [SP_GET_SURGERY_MASTER] 1, null, 'Abdo'

	SELECT Id, [Description]
	FROM [SurgeryMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE)
	AND (@DESCRIPTION IS NULL OR [Description] = @DESCRIPTION) 
	AND (@SEARCH_TERM IS NULL OR [Description] like '%' + @SEARCH_TERM + '%') 
	ORDER BY [Description]
END




GO


