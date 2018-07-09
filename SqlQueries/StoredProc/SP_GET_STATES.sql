USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_STATES]    Script Date: 6/29/2018 9:42:34 AM ******/
DROP PROCEDURE [dbo].[SP_GET_STATES]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_STATES]    Script Date: 6/29/2018 9:42:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_STATES]
(
	@COUNTRY_ID BIGINT = NULL,
	@STATE_ID BIGINT = NULL,
	@ACTIVE BIT = NULL
)
AS

BEGIN

--EXEC [SP_GET_STATES]
--EXEC [SP_GET_STATES] 1 
--EXEC [SP_GET_STATES] NULL, 1

	SELECT S.Id, S.CountryId, C.Country as CountryName, S.[State]
	FROM [StateMaster] S
	INNER JOIN [CountryMaster] C on C.Id=S.CountryId
	WHERE (@ACTIVE IS NULL OR S.[Active] = @ACTIVE) 
	AND (@COUNTRY_ID IS NULL OR S.CountryId = @COUNTRY_ID) 
	AND (@STATE_ID IS NULL OR S.Id = @STATE_ID) 
END



GO


