USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ADDRESSTYPE]    Script Date: 3/2/2018 8:54:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_ADDRESSTYPE]
(
@ACTIVE BIT = NULL
)
AS

BEGIN

--EXEC [SP_GET_ADDRESSTYPE]

	SELECT ID,AddressType
	FROM [AddressTypeMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE) 
END

GO

