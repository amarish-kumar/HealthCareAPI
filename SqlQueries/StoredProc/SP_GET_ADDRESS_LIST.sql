USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ADDRESS_LIST]    Script Date: 7/9/2018 10:54:16 PM ******/
DROP PROCEDURE [dbo].[SP_GET_ADDRESS_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ADDRESS_LIST]    Script Date: 7/9/2018 10:54:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_ADDRESS_LIST]
(
	@USER_ID BIGINT,
	@ADDRESS_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_ADDRESS_LIST] 3

SELECT UA.Id
	, UA.[UserID]
    ,UA.[AddressTypeID]
	,ATM.AddressType
	,UA.[Address1]
    ,UA.[Address2]
    ,UA.[City]
	,UA.[State] as StateId
	,S.[State] as StateName
	,UA.[ZipCode]
	,UA.[CountryID]
	,C.Country as CountryName
	,UA.AddedBy
	,UA.AddedDate
	,UA.ModifiedBy
	,UA.ModifiedDate	
	FROM [UserAddressMapping] UA
	INNER JOIN AddressTypeMaster ATM ON ATM.Id = UA.AddressTypeId
	INNER JOIN CountryMaster C ON C.Id = UA.CountryId
	INNER JOIN StateMaster S ON CONVERT(nvarchar(100), S.Id) = UA.[State]
	WHERE UA.[UserID] = @USER_ID
	AND (@ADDRESS_ID IS NULL OR UA.Id = @ADDRESS_ID)
	ORDER BY UA.AddedDate DESC
END









GO


