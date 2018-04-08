USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CANCER_STAGE_MASTER]    Script Date: 4/8/2018 11:15:57 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CANCER_STAGE_MASTER]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CANCER_STAGE_MASTER]    Script Date: 4/8/2018 11:15:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_CANCER_STAGE_MASTER]
(
	@ACTIVE BIT = NULL,
	@DESCRIPTION NVARCHAR(100) =  NULL
)
AS

BEGIN

--EXEC [SP_GET_CANCER_STAGE_MASTER]
--EXEC [SP_GET_CANCER_STAGE_MASTER] 1, 'First'

	SELECT Id, [Description]
	FROM [CancerStageMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE)
	AND (@DESCRIPTION IS NULL OR [Description] = @DESCRIPTION)  
	ORDER BY [SortOrder]
END





GO


