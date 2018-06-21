USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_TIMEZONES]    Script Date: 6/20/2018 11:45:52 AM ******/
DROP PROCEDURE [dbo].[SP_GET_TIMEZONES]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_TIMEZONES]    Script Date: 6/20/2018 11:45:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_TIMEZONES]
(
	@ACTIVE BIT = NULL,
	@SEARCH_TERM NVARCHAR(100) =  NULL
)
AS

BEGIN

--EXEC [SP_GET_TIMEZONES]
--EXEC [SP_GET_TIMEZONES] 1,'Indian Standard Time'
	SELECT ID, ShortForm, Timezone, [Time]
	FROM [TimezoneMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE)
	AND (@SEARCH_TERM IS NULL OR [ShortForm] like '%' + @SEARCH_TERM + '%' OR [Timezone] like '%' + @SEARCH_TERM + '%') 
	ORDER BY [ShortForm] 
END



GO


