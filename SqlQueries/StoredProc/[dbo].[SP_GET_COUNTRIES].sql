USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COUNTRIES]    Script Date: 3/2/2018 9:04:51 PM ******/
DROP PROCEDURE [dbo].[SP_GET_COUNTRIES]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COUNTRIES]    Script Date: 3/2/2018 9:04:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SP_GET_COUNTRIES]
(
@ACTIVE BIT = NULL
)
AS

BEGIN

--EXEC [SP_GET_COUNTRIES]

	SELECT ID,Country
	FROM [CountryMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE) 
END


GO


